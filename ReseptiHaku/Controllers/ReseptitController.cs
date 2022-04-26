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
    public class ReseptitController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: Reseptit
        public ActionResult Index()
        {
            return View(db.Reseptit.ToList());
        }

        // GET: Reseptit/Details/5
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

        // GET: Reseptit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reseptit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReseptiID,ReseptinNimi,AnnosKoko")] Reseptit reseptit)
        {
            if (ModelState.IsValid)
            {
                db.Reseptit.Add(reseptit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reseptit);
        }

        // GET: Reseptit/Edit/5
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
            return View(reseptit);
        }

        // POST: Reseptit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReseptiID,ReseptinNimi,AnnosKoko")] Reseptit reseptit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reseptit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reseptit);
        }

        // GET: Reseptit/Delete/5
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

        // POST: Reseptit/Delete/5
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
