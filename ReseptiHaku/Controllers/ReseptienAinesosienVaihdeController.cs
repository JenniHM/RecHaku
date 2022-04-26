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
    public class ReseptienAinesosienVaihdeController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: ReseptienAinesosienVaihde
        public ActionResult Index()
        {
            var reseptienAinesosienVaihde = db.ReseptienAinesosienVaihde.Include(r => r.ReseptienAinesosaLista).Include(r => r.Reseptit);
            return View(reseptienAinesosienVaihde.ToList());
        }

        // GET: ReseptienAinesosienVaihde/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienAinesosienVaihde reseptienAinesosienVaihde = db.ReseptienAinesosienVaihde.Find(id);
            if (reseptienAinesosienVaihde == null)
            {
                return HttpNotFound();
            }
            return View(reseptienAinesosienVaihde);
        }

        // GET: ReseptienAinesosienVaihde/Create
        public ActionResult Create()
        {
            ViewBag.ReseptiAinesosaListaID = new SelectList(db.ReseptienAinesosaLista, "ReseptiAinesosaListaID", "ReseptiAinesosaListaID");
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi");
            return View();
        }

        // POST: ReseptienAinesosienVaihde/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiviID,ReseptiID,ReseptiAinesosaListaID")] ReseptienAinesosienVaihde reseptienAinesosienVaihde)
        {
            if (ModelState.IsValid)
            {
                db.ReseptienAinesosienVaihde.Add(reseptienAinesosienVaihde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReseptiAinesosaListaID = new SelectList(db.ReseptienAinesosaLista, "ReseptiAinesosaListaID", "ReseptiAinesosaListaID", reseptienAinesosienVaihde.ReseptiAinesosaListaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", reseptienAinesosienVaihde.ReseptiID);
            return View(reseptienAinesosienVaihde);
        }

        // GET: ReseptienAinesosienVaihde/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienAinesosienVaihde reseptienAinesosienVaihde = db.ReseptienAinesosienVaihde.Find(id);
            if (reseptienAinesosienVaihde == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReseptiAinesosaListaID = new SelectList(db.ReseptienAinesosaLista, "ReseptiAinesosaListaID", "ReseptiAinesosaListaID", reseptienAinesosienVaihde.ReseptiAinesosaListaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", reseptienAinesosienVaihde.ReseptiID);
            return View(reseptienAinesosienVaihde);
        }

        // POST: ReseptienAinesosienVaihde/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiviID,ReseptiID,ReseptiAinesosaListaID")] ReseptienAinesosienVaihde reseptienAinesosienVaihde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reseptienAinesosienVaihde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReseptiAinesosaListaID = new SelectList(db.ReseptienAinesosaLista, "ReseptiAinesosaListaID", "ReseptiAinesosaListaID", reseptienAinesosienVaihde.ReseptiAinesosaListaID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", reseptienAinesosienVaihde.ReseptiID);
            return View(reseptienAinesosienVaihde);
        }

        // GET: ReseptienAinesosienVaihde/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienAinesosienVaihde reseptienAinesosienVaihde = db.ReseptienAinesosienVaihde.Find(id);
            if (reseptienAinesosienVaihde == null)
            {
                return HttpNotFound();
            }
            return View(reseptienAinesosienVaihde);
        }

        // POST: ReseptienAinesosienVaihde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReseptienAinesosienVaihde reseptienAinesosienVaihde = db.ReseptienAinesosienVaihde.Find(id);
            db.ReseptienAinesosienVaihde.Remove(reseptienAinesosienVaihde);
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
