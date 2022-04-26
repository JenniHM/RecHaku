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
    public class MenuVaihdeController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: MenuVaihde
        public ActionResult Index()
        {
            var menuVaihde = db.MenuVaihde.Include(m => m.MenuLista).Include(m => m.MenunKategoriat).Include(m => m.Reseptit);
            return View(menuVaihde.ToList());
        }

        // GET: MenuVaihde/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuVaihde menuVaihde = db.MenuVaihde.Find(id);
            if (menuVaihde == null)
            {
                return HttpNotFound();
            }
            return View(menuVaihde);
        }

        // GET: MenuVaihde/Create
        public ActionResult Create()
        {
            ViewBag.MenuID = new SelectList(db.MenuLista, "MenuID", "MenunNimi");
            ViewBag.MenunKategoriaID = new SelectList(db.MenunKategoriat, "MenunKategoriaID", "MenunKategoria");
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi");
            return View();
        }

        // POST: MenuVaihde/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiviID,MenuID,ReseptiID,MenunKategoriaID")] MenuVaihde menuVaihde)
        {
            if (ModelState.IsValid)
            {
                db.MenuVaihde.Add(menuVaihde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuID = new SelectList(db.MenuLista, "MenuID", "MenunNimi", menuVaihde.MenuID);
            ViewBag.MenunKategoriaID = new SelectList(db.MenunKategoriat, "MenunKategoriaID", "MenunKategoria", menuVaihde.MenunKategoriaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", menuVaihde.ReseptiID);
            return View(menuVaihde);
        }

        // GET: MenuVaihde/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuVaihde menuVaihde = db.MenuVaihde.Find(id);
            if (menuVaihde == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuID = new SelectList(db.MenuLista, "MenuID", "MenunNimi", menuVaihde.MenuID);
            ViewBag.MenunKategoriaID = new SelectList(db.MenunKategoriat, "MenunKategoriaID", "MenunKategoria", menuVaihde.MenunKategoriaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", menuVaihde.ReseptiID);
            return View(menuVaihde);
        }

        // POST: MenuVaihde/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiviID,MenuID,ReseptiID,MenunKategoriaID")] MenuVaihde menuVaihde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuVaihde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuID = new SelectList(db.MenuLista, "MenuID", "MenunNimi", menuVaihde.MenuID);
            ViewBag.MenunKategoriaID = new SelectList(db.MenunKategoriat, "MenunKategoriaID", "MenunKategoria", menuVaihde.MenunKategoriaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", menuVaihde.ReseptiID);
            return View(menuVaihde);
        }

        // GET: MenuVaihde/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuVaihde menuVaihde = db.MenuVaihde.Find(id);
            if (menuVaihde == null)
            {
                return HttpNotFound();
            }
            return View(menuVaihde);
        }

        // POST: MenuVaihde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuVaihde menuVaihde = db.MenuVaihde.Find(id);
            db.MenuVaihde.Remove(menuVaihde);
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
