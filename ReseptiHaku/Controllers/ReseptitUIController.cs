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
using ReseptiHaku.ViewModels;

namespace ReseptiHaku.Controllers
{
    public class ReseptitUIController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: ReseptitUI
        public ActionResult Index(/*string sortOrder, */string currentFilter1, string searchString1, int? page, int? pagesize)
        {

            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.ReseptiNimiSortParm = String.IsNullOrEmpty(sortOrder) ? "reseptinimi_desc" : "";
            //ViewBag.AnnoskokoSortParm = sortOrder == "Annoskoko" ? "annoskoko_desc" : "Annoskoko";

            
            var reseptit = from r in db.Reseptit/*.Include(r => r.Logins)*/
                           select r;

            if (searchString1 != null)
            {
                page = 1;
            }
            else
            {
                searchString1 = currentFilter1;
            }

            ViewBag.currentFilter1 = searchString1;

            if (!String.IsNullOrEmpty(searchString1))
            {
                reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1));
            }

            //if (!String.IsNullOrEmpty(searchString1)) // Jos hakufiltteri on käytössä, niin käytetään sitä ja sen lisäksi lajitellaan tulokset
            //{
            //    switch (sortOrder)
            //    {
            //        case "reseptinimi_desc":
            //            reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1)).OrderByDescending(r => r.ReseptinNimi);
            //            break;
            //        case "Annoskoko":
            //            reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1)).OrderBy(r => r.AnnosKoko);
            //            break;
            //        case "annoskoko_desc":
            //            reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1)).OrderByDescending(r => r.AnnosKoko);
            //            break;
            //        default:
            //            reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1)).OrderBy(r => r.ReseptinNimi);
            //            break;
            //    }
            //}
            //else
            //{ // Tässä hakufiltteri EI OLE käytössä, joten lajitellaan koko tulosjoukko ilman suodatuksia
            //    switch (sortOrder)
            //    {
            //        case "reseptinimi_desc":
            //            reseptit = reseptit.OrderByDescending(r => r.ReseptinNimi);
            //            break;
            //        case "Annoskoko":
            //            reseptit = reseptit.OrderBy(r => r.AnnosKoko);
            //            break;
            //        case "annoskoko_desc":
            //            reseptit = reseptit.OrderByDescending(r => r.AnnosKoko);
            //            break;
            //        default:
            //            reseptit = reseptit.OrderBy(r => r.ReseptinNimi);
            //            break;
            //    }
            //}


            int pageSize = (pagesize ?? 10); // Tämä palauttaa sivukoon taikka, jos pagesize on null, niin palauttaa koon 10 riviä per sivu
            int pageNumber = (page ?? 1); // Tämä palauttaa sivunumeron taikka, jos page on null, niin palauttaa numeron yksi

            return View(reseptit.OrderBy(q => q.ReseptinNimi).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult _ReseptinAinesosat(int? reseptiid) //int? mahdollistaa null:in vastaanottamisen
        {
            var reseptiAinesosaLista = from r in db.Reseptit
                
                //from rvl in db.ReseptienVaiheidenLista
                //                       join rev in db.ReseptienVaiheet on rvl.ReseptiVaiheID equals rev.ReseptiVaiheID
                                       
                                       //join rv in db.ReseptienAinesosienVaihde on rl.ReseptiAinesosaListaID equals rv.ReseptiAinesosaListaID
                                       //join rl in db.ReseptienAinesosaLista on rv.ReseptiAinesosaListaID equals rl.ReseptiAinesosaListaID
                                       //join ra in db.RaakaAineet on rl.RaakaAineID equals ra.RaakaAineID
                                       //join ml in db.MittayksikkoLista on rl.MittayksikkoID equals ml.MittayksikkoID
                                       //join k in db.Kategoriat on ra.KategoriaID equals k.KategoriaID
                                       where r.ReseptiID == reseptiid
                                       //orderby-lause
                                       select new ReseptitData
                                       {
                                           ReseptiID = (int)r.ReseptiID
                                           //ReseptiVaiheID = (int)rev.ReseptiVaiheID,
                                           //ReseptiVaihe = rev.ReseptiVaihe
                                           //Maara = rl.Maara
                                           //RaakaAine = ra.RaakaAine,
                                           //Mittayksikko = ml.Mittayksikko,
                                           //Kategoria = k.Kategoria
                                       };

            //var reseptiAinesosaLista = from r in db.Reseptit
            //                           join rv in db.ReseptienAinesosienVaihde on r.ReseptiID equals rv.ReseptiID
            //                           join rl in db.ReseptienAinesosaLista on rv.ReseptiAinesosaListaID equals rl.ReseptiAinesosaListaID
            //                           join ra in db.RaakaAineet on rl.RaakaAineID equals ra.RaakaAineID
            //                           join ml in db.MittayksikkoLista on rl.MittayksikkoID equals ml.MittayksikkoID
            //                           join k in db.Kategoriat on ra.KategoriaID equals k.KategoriaID
            //                           join rvl in db.ReseptienVaiheidenLista on r.ReseptiID equals rvl.ReseptiID
            //                           join rev in db.ReseptienVaiheet on rvl.ReseptiVaiheID equals rev.ReseptiVaiheID
            //                           where rv.ReseptiID == reseptiid
            //                           //orderby-lause
            //                           select new ReseptitData
            //                           {
            //                               ReseptiID = rv.ReseptiID,
            //                               ReseptinNimi = r.ReseptinNimi,
            //                               AnnosKoko = r.AnnosKoko,
            //                               LoginID = r.LoginID,
            //                               Julkinen = r.Julkinen,
            //                               RiviID = rv.RiviID,
            //                               ReseptiAinesosaListaID = rl.ReseptiAinesosaListaID,
            //                               RaakaAineID = ra.RaakaAineID,
            //                               Maara = rl.Maara,
            //                               MittayksikkoID = ml.MittayksikkoID,
            //                               RaakaAine = ra.RaakaAine,
            //                               KategoriaID = k.KategoriaID,
            //                               Mittayksikko = ml.Mittayksikko,
            //                               MittayksikkoSelite = ml.MittayksikkoSelite,
            //                               Kategoria = k.Kategoria,
            //                               ReseptiVaiheID = rev.ReseptiVaiheID,
            //                               ReseptiVaihe = rev.ReseptiVaihe
            //                           };

            return PartialView(reseptiAinesosaLista);
        }



        // GET: ReseptitUI
        public ActionResult Index3(string sortOrder, string currentFilter1, string searchString1, int? page, int? pagesize)
        {
            // huomioi nämä myöhemmin! include tulee käyttöön jossain vaiheessa
            //var reseptit = db.Reseptit.Include(r => r.Logins);
            //return View(reseptit.ToList());

            ViewBag.CurrentSort = sortOrder;
            ViewBag.ReseptiNimiSortParm = String.IsNullOrEmpty(sortOrder) ? "reseptinimi_desc" : "";
            ViewBag.AnnoskokoSortParm = sortOrder == "Annoskoko" ? "annoskoko_desc" : "Annoskoko";

            var reseptit = from r in db.Reseptit
                           select r;

            if (searchString1 != null)
            {
                page = 1;
            }
            else
            {
                searchString1 = currentFilter1;
            }

            ViewBag.currentFilter1 = searchString1;

            if (!String.IsNullOrEmpty(searchString1))
            {
                reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1));
            }

            if (!String.IsNullOrEmpty(searchString1)) // Jos hakufiltteri on käytössä, niin käytetään sitä ja sen lisäksi lajitellaan tulokset
            {
                switch (sortOrder)
                {
                    case "reseptinimi_desc":
                        reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1)).OrderByDescending(r => r.ReseptinNimi);
                        break;
                    case "Annoskoko":
                        reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1)).OrderBy(r => r.AnnosKoko);
                        break;
                    case "annoskoko_desc":
                        reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1)).OrderByDescending(r => r.AnnosKoko);
                        break;
                    default:
                        reseptit = reseptit.Where(r => r.ReseptinNimi.Contains(searchString1)).OrderBy(r => r.ReseptinNimi);
                        break;
                }
            } else { // Tässä hakufiltteri EI OLE käytössä, joten lajitellaan koko tulosjoukko ilman suodatuksia
                switch (sortOrder)
                {
                    case "reseptinimi_desc":
                        reseptit = reseptit.OrderByDescending(r => r.ReseptinNimi);
                        break;
                    case "Annoskoko":
                        reseptit = reseptit.OrderBy(r => r.AnnosKoko);
                        break;
                    case "annoskoko_desc":
                        reseptit = reseptit.OrderByDescending(r => r.AnnosKoko);
                        break;
                    default:
                        reseptit = reseptit.OrderBy(r => r.ReseptinNimi);
                        break;
                }
            }
            

            int pageSize = (pagesize ?? 10); // Tämä palauttaa sivukoon taikka, jos pagesize on null, niin palauttaa koon 10 riviä per sivu
            int pageNumber = (page ?? 1); // Tämä palauttaa sivunumeron taikka, jos page on null, niin palauttaa numeron yksi

            //List < Reseptit > reseptit = db.Reseptit.ToList();
            return View(reseptit.ToPagedList(pageNumber, pageSize));
        }

        // GET: ReseptitUI
        public ActionResult Index2()
        {
            var reseptit = db.Reseptit.Include(r => r.Logins);
            return View(reseptit.ToList());
        }

        // GET: ReseptitUI/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseptit reseptit = db.Reseptit.Find(id);
            if (reseptit == null)
            {
                return HttpNotFound();
            }
            return View(reseptit);
        }

        // GET: ReseptitUI/Create
        public ActionResult Create()
        {
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName");
            return View();
        }

        // POST: ReseptitUI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReseptiID,ReseptinNimi,AnnosKoko,LoginID,Julkinen")] Reseptit reseptit)
        {
            if (ModelState.IsValid)
            {
                db.Reseptit.Add(reseptit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", reseptit.LoginID);
            return View(reseptit);
        }

        // GET: ReseptitUI/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseptit reseptit = db.Reseptit.Find(id);
            if (reseptit == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", reseptit.LoginID);
            return View(reseptit);
        }

        // POST: ReseptitUI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReseptiID,ReseptinNimi,AnnosKoko,LoginID,Julkinen")] Reseptit reseptit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reseptit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", reseptit.LoginID);
            return View(reseptit);
        }

        // GET: ReseptitUI/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseptit reseptit = db.Reseptit.Find(id);
            if (reseptit == null)
            {
                return HttpNotFound();
            }
            return View(reseptit);
        }

        // POST: ReseptitUI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reseptit reseptit = db.Reseptit.Find(id);
            db.Reseptit.Remove(reseptit);
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
