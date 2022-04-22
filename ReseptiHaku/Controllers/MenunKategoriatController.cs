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
    public class MenunKategoriatController : Controller
    {
        private ReseptiHakuEntities db = new ReseptiHakuEntities();

        // GET: MenunKategoriat
        public ActionResult Index()
        {
            return View(db.MenunKategoriat.ToList());
        }

        // GET: MenunKategoriat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenunKategoriat menunKategoriat = db.MenunKategoriat.Find(id);
            if (menunKategoriat == null)
            {
                return HttpNotFound();
            }
            return View(menunKategoriat);
        }

        // GET: MenunKategoriat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenunKategoriat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenunKategoriaID,MenunKategoria")] MenunKategoriat menunKategoriat)
        {
            if (ModelState.IsValid)
            {
                db.MenunKategoriat.Add(menunKategoriat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menunKategoriat);
        }

        // GET: MenunKategoriat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenunKategoriat menunKategoriat = db.MenunKategoriat.Find(id);
            if (menunKategoriat == null)
            {
                return HttpNotFound();
            }
            return View(menunKategoriat);
        }

        // POST: MenunKategoriat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenunKategoriaID,MenunKategoria")] MenunKategoriat menunKategoriat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menunKategoriat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menunKategoriat);
        }

        // GET: MenunKategoriat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenunKategoriat menunKategoriat = db.MenunKategoriat.Find(id);
            if (menunKategoriat == null)
            {
                return HttpNotFound();
            }
            return View(menunKategoriat);
        }

        // POST: MenunKategoriat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenunKategoriat menunKategoriat = db.MenunKategoriat.Find(id);
            db.MenunKategoriat.Remove(menunKategoriat);
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
