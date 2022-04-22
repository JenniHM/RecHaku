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
    public class RaakaAineetController : Controller
    {
        private ReseptiHakuEntities db = new ReseptiHakuEntities();

        // GET: RaakaAineet
        public ActionResult Index()
        {
            var raakaAineet = db.RaakaAineet.Include(r => r.Kategoriat);
            return View(raakaAineet.ToList());
        }

        // GET: RaakaAineet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaakaAineet raakaAineet = db.RaakaAineet.Find(id);
            if (raakaAineet == null)
            {
                return HttpNotFound();
            }
            return View(raakaAineet);
        }

        // GET: RaakaAineet/Create
        public ActionResult Create()
        {
            ViewBag.KategoriaID = new SelectList(db.Kategoriat, "KategoriaID", "Kategoria");
            return View();
        }

        // POST: RaakaAineet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RaakaAineID,RaakaAine,KategoriaID")] RaakaAineet raakaAineet)
        {
            if (ModelState.IsValid)
            {
                db.RaakaAineet.Add(raakaAineet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriaID = new SelectList(db.Kategoriat, "KategoriaID", "Kategoria", raakaAineet.KategoriaID);
            return View(raakaAineet);
        }

        // GET: RaakaAineet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaakaAineet raakaAineet = db.RaakaAineet.Find(id);
            if (raakaAineet == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriaID = new SelectList(db.Kategoriat, "KategoriaID", "Kategoria", raakaAineet.KategoriaID);
            return View(raakaAineet);
        }

        // POST: RaakaAineet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RaakaAineID,RaakaAine,KategoriaID")] RaakaAineet raakaAineet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raakaAineet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriaID = new SelectList(db.Kategoriat, "KategoriaID", "Kategoria", raakaAineet.KategoriaID);
            return View(raakaAineet);
        }

        // GET: RaakaAineet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaakaAineet raakaAineet = db.RaakaAineet.Find(id);
            if (raakaAineet == null)
            {
                return HttpNotFound();
            }
            return View(raakaAineet);
        }

        // POST: RaakaAineet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RaakaAineet raakaAineet = db.RaakaAineet.Find(id);
            db.RaakaAineet.Remove(raakaAineet);
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
