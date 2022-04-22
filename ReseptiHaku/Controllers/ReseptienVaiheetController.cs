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
    public class ReseptienVaiheetController : Controller
    {
        private ReseptiHakuEntities db = new ReseptiHakuEntities();

        // GET: ReseptienVaiheet
        public ActionResult Index()
        {
            return View(db.ReseptienVaiheet.ToList());
        }

        // GET: ReseptienVaiheet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienVaiheet reseptienVaiheet = db.ReseptienVaiheet.Find(id);
            if (reseptienVaiheet == null)
            {
                return HttpNotFound();
            }
            return View(reseptienVaiheet);
        }

        // GET: ReseptienVaiheet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReseptienVaiheet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReseptiVaiheID,ReseptiVaihe")] ReseptienVaiheet reseptienVaiheet)
        {
            if (ModelState.IsValid)
            {
                db.ReseptienVaiheet.Add(reseptienVaiheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reseptienVaiheet);
        }

        // GET: ReseptienVaiheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienVaiheet reseptienVaiheet = db.ReseptienVaiheet.Find(id);
            if (reseptienVaiheet == null)
            {
                return HttpNotFound();
            }
            return View(reseptienVaiheet);
        }

        // POST: ReseptienVaiheet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReseptiVaiheID,ReseptiVaihe")] ReseptienVaiheet reseptienVaiheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reseptienVaiheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reseptienVaiheet);
        }

        // GET: ReseptienVaiheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienVaiheet reseptienVaiheet = db.ReseptienVaiheet.Find(id);
            if (reseptienVaiheet == null)
            {
                return HttpNotFound();
            }
            return View(reseptienVaiheet);
        }

        // POST: ReseptienVaiheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReseptienVaiheet reseptienVaiheet = db.ReseptienVaiheet.Find(id);
            db.ReseptienVaiheet.Remove(reseptienVaiheet);
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
