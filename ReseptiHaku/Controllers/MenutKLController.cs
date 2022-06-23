using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReseptiHaku.Models;
using ReseptiHaku.ViewModels;

namespace ReseptiHaku.Controllers
{
    public class MenutKLController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        public ActionResult MenutYhteenveto()
        {
            var menutYhteenveto = from m in db.MenuLista
                                  join mv in db.MenuVaihde on m.MenuID equals mv.MenuID
                                  join mk in db.MenunKategoriat on mv.MenunKategoriaID equals mk.MenunKategoriaID
                                  //where-lause
                                  //orderby-lause
                                  select new MenutData
                                  {
                                      MenuID = m.MenuID,
                                      MenunNimi = m.MenunNimi,
                                      LoginID = m.LoginID,
                                      Julkinen = m.Julkinen,
                                      RiviID = mv.RiviID,
                                      ReseptiID = mv.ReseptiID,
                                      MenunKategoriaID = mk.MenunKategoriaID,
                                      MenunKategoria = mk.MenunKategoria
                                  };

            return View(menutYhteenveto);
        }


        // GET: MenutKL
        public ActionResult Index()
        {
            var menuLista = db.MenuLista.Include(m => m.Logins);
            return View(menuLista.ToList());
        }

        // GET: MenutKL/Details/5
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

        // GET: MenutKL/Create
        public ActionResult Create()
        {
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName");
            return View();
        }

        // POST: MenutKL/Create
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

        // GET: MenutKL/Edit/5
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

        // POST: MenutKL/Edit/5
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

        // GET: MenutKL/Delete/5
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

        // POST: MenutKL/Delete/5
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
