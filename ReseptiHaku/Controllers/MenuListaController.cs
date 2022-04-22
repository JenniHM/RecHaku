using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReseptiHaku.Models;

namespace ReseptiHaku.Controllers
{
    public class MenuListaController : Controller
    {
        private ReseptiHakuEntities db = new ReseptiHakuEntities();

        // GET: MenuLista
        public ActionResult Index()
        {
            var menuLista = db.MenuLista.Include(m => m.MenunKategoriat).Include(m => m.Reseptit);
            return View(menuLista.ToList());
        }

        // GET: MenuLista/Details/5
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

        // GET: MenuLista/Create
        public ActionResult Create()
        {
            ViewBag.MenunKategoriaID = new SelectList(db.MenunKategoriat, "MenunKategoriaID", "MenunKategoria");
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi");
            return View();
        }

        // POST: MenuLista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuID,MenunNimi,ReseptiID,MenunKategoriaID")] MenuLista menuLista)
        {
            if (ModelState.IsValid)
            {
                db.MenuLista.Add(menuLista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenunKategoriaID = new SelectList(db.MenunKategoriat, "MenunKategoriaID", "MenunKategoria", menuLista.MenunKategoriaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", menuLista.ReseptiID);
            return View(menuLista);
        }

        // GET: MenuLista/Edit/5
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
            ViewBag.MenunKategoriaID = new SelectList(db.MenunKategoriat, "MenunKategoriaID", "MenunKategoria", menuLista.MenunKategoriaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", menuLista.ReseptiID);
            return View(menuLista);
        }

        // POST: MenuLista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuID,MenunNimi,ReseptiID,MenunKategoriaID")] MenuLista menuLista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuLista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenunKategoriaID = new SelectList(db.MenunKategoriat, "MenunKategoriaID", "MenunKategoria", menuLista.MenunKategoriaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", menuLista.ReseptiID);
            return View(menuLista);
        }

        // GET: MenuLista/Delete/5
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

        // POST: MenuLista/Delete/5
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
