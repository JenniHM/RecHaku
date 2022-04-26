using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReseptiHaku.Models;
using PagedList;
using System.Data.Entity.SqlServer;
using System.Globalization;

namespace ReseptiHakuTesti.Views
{
    public class foodsController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: foods

        public ActionResult Index(string sortOrder, string currentFilter1, string searchString1, string FuclassCategory, string currentFuclassCategory, int? page, int? pagesize)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.RuokaNimiSortParm = String.IsNullOrEmpty(sortOrder) ? "ruokanimi_desc" : "";
            ViewBag.RuoanKayttoLuokkaSortParm = sortOrder == "RuoanKayttoLuokka" ? "ruoankayttoluokka_desc" : "RuoanKayttoLuokka";
            ViewBag.RaakaAineLuokkaSortParm = sortOrder == "RaakaAineLuokka" ? "raakaaineluokka_desc" : "RaakaAineLuokka";
            ViewBag.ValmistustapaSortParm = sortOrder == "Valmistustapa" ? "valmistustapa_desc" : "Valmistustapa";
            ViewBag.RuoanKayttoLuokkaYSortParm = sortOrder == "RuoanKayttoLuokkaY" ? "ruoankayttoluokkay_desc" : "RuoanKayttoLuokkaY";
            ViewBag.RaakaAineLuokkaYSortParm = sortOrder == "RaakaAineLuokkaY" ? "raakaaineluokkay_desc" : "RaakaAineLuokkaY";
            ViewBag.ElintarviketyyppiSortParm = sortOrder == "Elintarviketyyppi" ? "elintarviketyyppi_desc" : "Elintarviketyyppi";
            ViewBag.SyotavaOsuusSortParm = sortOrder == "SyotavaOsuus" ? "syotavaosuus_desc" : "SyotavaOsuus";

            if (searchString1 != null)
            {
                page = 1;
            }
            else
            {
                searchString1 = currentFilter1;
            }

            ViewBag.currentFilter1 = searchString1;

            var raakaAineet = from f in db.food.Include(f => f.foodtype_FI).Include(f => f.fuclass_FI).Include(f => f.fuclass_FI1).Include(f => f.igclass_FI).Include(f => f.igclass_FI1).Include(f => f.process_FI)
                              select f;

            if (!String.IsNullOrEmpty(searchString1))
            {
                raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1));
            }

            //Tehdään tässä kohden haku tuoteryhmällä, jos se on asetettu käyttöliittymässä <-- seuraavat haut tarkentavat tätä tulosjoukkoa
            if (!String.IsNullOrEmpty(FuclassCategory) && (FuclassCategory != "0"))
            {
                string para = FuclassCategory;
                raakaAineet = raakaAineet.Where(f => f.fuclass_FI.THSCODE == para);
            }

            //Fuclass-categoria -hakufiltterin laitto muistiin
            if ((FuclassCategory != null) && (FuclassCategory != "0"))
            {
                page = 1;
            }
            else
            {
                FuclassCategory = currentFuclassCategory;
            }

            ViewBag.currentFuclassCategory = FuclassCategory;


            // hakufiltterin laitto muistiin
            if (!String.IsNullOrEmpty(searchString1)) // jos hakufiltteri on käytössä, niin käytetään sitä ja sen lisäksi lajitellaan tulokset
            {
                switch (sortOrder)
                {
                    case "ruokanimi_desc":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderByDescending(f => f.FOODNAME);
                        break;
                    case "RuoanKayttoLuokka":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderBy(f => f.fuclass_FI.DESCRIPT);
                        break;
                    case "ruoankayttoluokka_desc":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderByDescending(f => f.fuclass_FI.DESCRIPT);
                        break;
                    case "RaakaAineLuokka":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderBy(f => f.igclass_FI.DESCRIPT);
                        break;
                    case "raakaineluokka_desc":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderByDescending(f => f.igclass_FI.DESCRIPT);
                        break;
                    case "Valmistustapa":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderBy(f => f.process_FI.DESCRIPT);
                        break;
                    case "valmistustapa_desc":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderByDescending(f => f.process_FI.DESCRIPT);
                        break;
                    case "RuoanKayttoLuokkaY":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderBy(f => f.fuclass_FI1.DESCRIPT);
                        break;
                    case "ruoankayttoluokkay_desc":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderByDescending(f => f.fuclass_FI1.DESCRIPT);
                        break;
                    case "RaakaAineLuokkaY":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderBy(f => f.igclass_FI1.DESCRIPT);
                        break;
                    case "raakaaineluokkay_desc":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderByDescending(f => f.igclass_FI1.DESCRIPT);
                        break;
                    case "Elintarviketyyppi":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderBy(f => f.foodtype_FI.DESCRIPT);
                        break;
                    case "elintarviketyyppi_desc":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderByDescending(f => f.foodtype_FI.DESCRIPT);
                        break;
                    case "SyotavaOsuus":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderBy(f => f.EDPORT);
                        break;
                    case "syotavaosuus_desc":
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderByDescending(f => f.EDPORT);
                        break;
                    default:
                        raakaAineet = raakaAineet.Where(f => f.FOODNAME.Contains(searchString1)).OrderBy(f => f.FOODNAME);
                        break;
                }
            }
            else if (!String.IsNullOrEmpty(FuclassCategory) && (FuclassCategory != "0")) // jos käytössä on kategoriarajaus, niin käytetään sitä ja sen lisäksi lajitellaan
            {
                string para = FuclassCategory;
                switch (sortOrder)
                {
                    case "ruokanimi_desc":
                        raakaAineet = raakaAineet.Where(f => f.fuclass_FI.THSCODE == para).OrderByDescending(f => f.FOODNAME);
                        break;
                    case "RuoanKayttoLuokka":
                        raakaAineet = raakaAineet.OrderBy(f => f.fuclass_FI.DESCRIPT);
                        break;
                    case "ruoankayttoluokka_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.fuclass_FI.DESCRIPT);
                        break;
                    case "RaakaAineLuokka":
                        raakaAineet = raakaAineet.OrderBy(f => f.igclass_FI.DESCRIPT);
                        break;
                    case "raakaineluokka_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.igclass_FI.DESCRIPT);
                        break;
                    case "Valmistustapa":
                        raakaAineet = raakaAineet.OrderBy(f => f.process_FI.DESCRIPT);
                        break;
                    case "valmistustapa_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.process_FI.DESCRIPT);
                        break;
                    case "RuoanKayttoLuokkaY":
                        raakaAineet = raakaAineet.OrderBy(f => f.fuclass_FI1.DESCRIPT);
                        break;
                    case "ruoankayttoluokkay_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.fuclass_FI1.DESCRIPT);
                        break;
                    case "RaakaAineLuokkaY":
                        raakaAineet = raakaAineet.OrderBy(f => f.igclass_FI1.DESCRIPT);
                        break;
                    case "raakaaineluokkay_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.igclass_FI1.DESCRIPT);
                        break;
                    case "Elintarviketyyppi":
                        raakaAineet = raakaAineet.OrderBy(f => f.foodtype_FI.DESCRIPT);
                        break;
                    case "elintarviketyyppi_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.foodtype_FI.DESCRIPT);
                        break;
                    case "SyotavaOsuus":
                        raakaAineet = raakaAineet.OrderBy(f => f.EDPORT);
                        break;
                    case "syotavaosuus_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.EDPORT);
                        break;
                    default:
                        raakaAineet = raakaAineet.Where(f => f.fuclass_FI.THSCODE == para).OrderBy(f => f.FOODNAME);
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "ruokanimi_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.FOODNAME);
                        break;
                    case "RuoanKayttoLuokka":
                        raakaAineet = raakaAineet.OrderBy(f => f.fuclass_FI.DESCRIPT);
                        break;
                    case "ruoankayttoluokka_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.fuclass_FI.DESCRIPT);
                        break;
                    case "RaakaAineLuokka":
                        raakaAineet = raakaAineet.OrderBy(f => f.igclass_FI.DESCRIPT);
                        break;
                    case "raakaineluokka_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.igclass_FI.DESCRIPT);
                        break;
                    case "Valmistustapa":
                        raakaAineet = raakaAineet.OrderBy(f => f.process_FI.DESCRIPT);
                        break;
                    case "valmistustapa_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.process_FI.DESCRIPT);
                        break;
                    case "RuoanKayttoLuokkaY":
                        raakaAineet = raakaAineet.OrderBy(f => f.fuclass_FI1.DESCRIPT);
                        break;
                    case "ruoankayttoluokkay_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.fuclass_FI1.DESCRIPT);
                        break;
                    case "RaakaAineLuokkaY":
                        raakaAineet = raakaAineet.OrderBy(f => f.igclass_FI1.DESCRIPT);
                        break;
                    case "raakaaineluokkay_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.igclass_FI1.DESCRIPT);
                        break;
                    case "Elintarviketyyppi":
                        raakaAineet = raakaAineet.OrderBy(f => f.foodtype_FI.DESCRIPT);
                        break;
                    case "elintarviketyyppi_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.foodtype_FI.DESCRIPT);
                        break;
                    case "SyotavaOsuus":
                        raakaAineet = raakaAineet.OrderBy(f => f.EDPORT);
                        break;
                    case "syotavaosuus_desc":
                        raakaAineet = raakaAineet.OrderByDescending(f => f.EDPORT);
                        break;
                    default:
                        raakaAineet = raakaAineet.OrderBy(f => f.FOODNAME);
                        break;
                }
            }

            // haku kategorioittain
            List<fuclass_FI> lstRaakaAineLuokkaYlempi = new List<fuclass_FI>();

            var raakaaineluokkayList = from raa in db.fuclass_FI
                                       select raa;

            fuclass_FI tyhjaFuclass_FI = new fuclass_FI();
            tyhjaFuclass_FI.THSCODE = "";
            tyhjaFuclass_FI.DESCRIPT = "";
            tyhjaFuclass_FI.CategoryName = "";
            lstRaakaAineLuokkaYlempi.Add(tyhjaFuclass_FI);

            foreach (fuclass_FI raakaaineluokkay in raakaaineluokkayList)
            {
                fuclass_FI yksiFuclass_FI = new fuclass_FI();
                yksiFuclass_FI.THSCODE = raakaaineluokkay.THSCODE;
                yksiFuclass_FI.DESCRIPT = raakaaineluokkay.DESCRIPT;
                yksiFuclass_FI.CategoryName = raakaaineluokkay.DESCRIPT;
                // fuclass_FI.cs -taulun luokkamääritykseen Models-kansiossa lisätty tämä "uusi" kenttä = CategoryName
                lstRaakaAineLuokkaYlempi.Add(yksiFuclass_FI);
            }
            ViewBag.THSCODE = new SelectList(lstRaakaAineLuokkaYlempi, "THSCODE", "CategoryName", FuclassCategory);


            int pageSize = (pagesize ?? 100);
            int pageNumber = (page ?? 1);

            return View(raakaAineet.ToPagedList(pageNumber, pageSize));

        }

        // GET: foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: foods/Create
        public ActionResult Create()
        {
            ViewBag.FOODTYPE = new SelectList(db.foodtype_FI, "THSCODE", "DESCRIPT");
            ViewBag.FUCLASS = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT");
            ViewBag.FUCLASSP = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT");
            ViewBag.IGCLASS = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT");
            ViewBag.IGCLASSP = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT");
            ViewBag.PROCESS = new SelectList(db.process_FI, "THSCODE", "DESCRIPT");
            return View();
        }

        // POST: foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FOODID,FOODNAME,FOODTYPE,PROCESS,EDPORT,IGCLASS,IGCLASSP,FUCLASS,FUCLASSP")] food food)
        {
            if (ModelState.IsValid)
            {
                db.food.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FOODTYPE = new SelectList(db.foodtype_FI, "THSCODE", "DESCRIPT", food.FOODTYPE);
            ViewBag.FUCLASS = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASS);
            ViewBag.FUCLASSP = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASSP);
            ViewBag.IGCLASS = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASS);
            ViewBag.IGCLASSP = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASSP);
            ViewBag.PROCESS = new SelectList(db.process_FI, "THSCODE", "DESCRIPT", food.PROCESS);
            return View(food);
        }

        // GET: foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.FOODTYPE = new SelectList(db.foodtype_FI, "THSCODE", "DESCRIPT", food.FOODTYPE);
            ViewBag.FUCLASS = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASS);
            ViewBag.FUCLASSP = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASSP);
            ViewBag.IGCLASS = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASS);
            ViewBag.IGCLASSP = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASSP);
            ViewBag.PROCESS = new SelectList(db.process_FI, "THSCODE", "DESCRIPT", food.PROCESS);
            return View(food);
        }

        // POST: foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FOODID,FOODNAME,FOODTYPE,PROCESS,EDPORT,IGCLASS,IGCLASSP,FUCLASS,FUCLASSP")] food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FOODTYPE = new SelectList(db.foodtype_FI, "THSCODE", "DESCRIPT", food.FOODTYPE);
            ViewBag.FUCLASS = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASS);
            ViewBag.FUCLASSP = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASSP);
            ViewBag.IGCLASS = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASS);
            ViewBag.IGCLASSP = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASSP);
            ViewBag.PROCESS = new SelectList(db.process_FI, "THSCODE", "DESCRIPT", food.PROCESS);
            return View(food);
        }

        // GET: foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            food food = db.food.Find(id);
            db.food.Remove(food);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
