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

namespace ReseptiHaku.Controllers
{
    public class MenutUIController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: MenutUI
        public ActionResult Index(string sortOrder, string currentFilter1, string searchString1, string MenuKategoria, string currentMenuKategoria, int? page, int? pagesize)
        {
            // huomioi nämä vielä mahdollisesti myöhemmässä vaiheessa!
            //var menuLista = db.MenuLista.Include(m => m.Logins);
            //return View(menuLista.ToList());

            ViewBag.CurrentSort = sortOrder;
            ViewBag.MenuNimiSortParm = String.IsNullOrEmpty(sortOrder) ? "menunimi_desc" : "";
            //ViewBag.AnnoskokoSortParm = sortOrder == "Annoskoko" ? "annoskoko_desc" : "Annoskoko";

            //MenuLista.cs piti lisätä: public virtual MenunKategoriat Menunkategoriat { get; set; }
            //jotta .Include tunnistaa sen

            //var menut = from m in db.MenuLista.Include(m => m.MenuVaihde.Select(n => n.MenunKategoriat))
            //            select m;

            var menut = from m in db.MenuLista
                        .Include(m => m.MenuVaihde)
                        //.Include(m => m.MenuVaihde.Select(n => n.MenuID))
                        //.Include(m => m.MenuVaihde.Select(n => n.MenunKategoriaID))
                        select m;

            //Menuhakufiltterin laitto muistiin
            if (searchString1 != null)
            {
                page = 1;
            }
            else
            {
                searchString1 = currentFilter1;
            }

            ViewBag.currentFilter1 = searchString1;

            //Menukategorian laitto muistiin
            if ((MenuKategoria != null) && (MenuKategoria != "0"))
            {
                page = 1;
            }
            else
            {
                MenuKategoria = currentMenuKategoria;
            }

            ViewBag.currentMenuKategoria = MenuKategoria;

            if (!String.IsNullOrEmpty(searchString1))
            {
                menut = menut.Where(r => r.MenunNimi.Contains(searchString1));
            }
            // Menukategorialla nolla (tyhjän valinnan oletusarvo) ei voi hakea, joten suljetaan se pois, jotta tuotehaku kannasta toimisi oikein
            if (!String.IsNullOrEmpty(MenuKategoria) && (MenuKategoria != "0"))
            {
                int para = int.Parse(MenuKategoria);
                menut = menut.Where(m => m.MenunKategoriat.MenunKategoriaID == para);
            }

            if (!String.IsNullOrEmpty(searchString1)) // Jos hakufiltteri on käytössä, niin käytetään sitä ja sen lisäksi lajitellaan tulokset
            {
                switch (sortOrder)
                {
                    case "menunimi_desc":
                        menut = menut.Where(r => r.MenunNimi.Contains(searchString1)).OrderByDescending(r => r.MenunNimi);
                        break;
                    default:
                        menut = menut.Where(r => r.MenunNimi.Contains(searchString1)).OrderBy(r => r.MenunNimi);
                        break;
                }
            }
            else if (!String.IsNullOrEmpty(MenuKategoria) && (MenuKategoria != "0")) // Jos käytössä on kategoriarajaus, niin käytetään sitä ja sen lisäksi lajitellaan
            {
                int para = int.Parse(MenuKategoria);
                switch (sortOrder)
                {
                    case "menunimi_desc":
                        menut = menut.Where(m => m.MenunKategoriat.MenunKategoriaID == para).OrderByDescending(r => r.MenunNimi);
                        break;
                    default:
                        menut = menut.Where(m => m.MenunKategoriat.MenunKategoriaID == para).OrderBy(r => r.MenunNimi);
                        break;
                }
            }
            else
            { // Tässä hakufiltteri EI OLE käytössä, joten lajitellaan koko tulosjoukko ilman suodatuksia
                switch (sortOrder)
                {
                    case "menunimi_desc":
                        menut = menut.OrderByDescending(r => r.MenunNimi);
                        break;
                    default:
                        menut = menut.OrderBy(r => r.MenunNimi);
                        break;
                }
            }
            

            // Selaus menujen kategorioittain
            List<MenunKategoriat> lstCategories = new List<MenunKategoriat>();

            var categoryList = from cat in db.MenunKategoriat
                               select cat;

            MenunKategoriat tyhjaCategory = new MenunKategoriat();
            tyhjaCategory.MenunKategoriaID = 0;
            tyhjaCategory.MenunKategoria = "";
            tyhjaCategory.MenunKategoriaIDKategorianNimi = "";
            lstCategories.Add(tyhjaCategory);

            foreach (MenunKategoriat category in categoryList)
            {
                MenunKategoriat yksiCategory = new MenunKategoriat();
                yksiCategory.MenunKategoriaID = category.MenunKategoriaID;
                yksiCategory.MenunKategoria = category.MenunKategoria;
                yksiCategory.MenunKategoriaIDKategorianNimi = category.MenunKategoriaID.ToString() + " - " + category.MenunKategoria;
                // Tämä uusi kenttä lisätty Models-kansioon MenunKategoriat
                lstCategories.Add(yksiCategory);
            }
            ViewBag.MenunKategoriaID = new SelectList(lstCategories, "MenunKategoriaID", "MenunKategoriaIDKategorianNimi", MenuKategoria);


            int pageSize = (pagesize ?? 10); // Tämä palauttaa sivukoon taikka, jos pagesize on null, niin palauttaa koon 10 riviä per sivu
            int pageNumber = (page ?? 1); // Tämä palauttaa sivunumeron taikka, jos page on null, niin palauttaa numeron yksi

            //List < Reseptit > reseptit = db.Reseptit.ToList();
            return View(menut.ToPagedList(pageNumber, pageSize));
        }

        // GET: MenutUI
        public ActionResult Index2()
        {
            var menuLista = db.MenuLista.Include(m => m.Logins);
            return View(menuLista.ToList());
        }

        // GET: MenutUI/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuLista menuLista = db.MenuLista.Find(id);
            if (menuLista == null)
            {
                return HttpNotFound();
            }
            return View(menuLista);
        }

        // GET: MenutUI/Create
        public ActionResult Create()
        {
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName");
            return View();
        }

        // POST: MenutUI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuID,MenunNimi,LoginID,Julkinen")] MenuLista menuLista)
        {
            if (ModelState.IsValid)
            {
                db.MenuLista.Add(menuLista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", menuLista.LoginID);
            return View(menuLista);
        }

        // GET: MenutUI/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuLista menuLista = db.MenuLista.Find(id);
            if (menuLista == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", menuLista.LoginID);
            return View(menuLista);
        }

        // POST: MenutUI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuID,MenunNimi,LoginID,Julkinen")] MenuLista menuLista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuLista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", menuLista.LoginID);
            return View(menuLista);
        }

        // GET: MenutUI/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuLista menuLista = db.MenuLista.Find(id);
            if (menuLista == null)
            {
                return HttpNotFound();
            }
            return View(menuLista);
        }

        // POST: MenutUI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuLista menuLista = db.MenuLista.Find(id);
            db.MenuLista.Remove(menuLista);
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
