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
    public class ReseptienAinesosaListaController : Controller
    {
        private ReseptiHakuEntities db = new ReseptiHakuEntities();

        // GET: ReseptienAinesosaLista
        public ActionResult Index()
        {
            var reseptienAinesosaLista = db.ReseptienAinesosaLista.Include(r => r.MittayksikkoLista).Include(r => r.RaakaAineet);
            return View(reseptienAinesosaLista.ToList());
        }

        // GET: ReseptienAinesosaLista/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienAinesosaLista reseptienAinesosaLista = db.ReseptienAinesosaLista.Find(id);
            if (reseptienAinesosaLista == null)
            {
                return HttpNotFound();
            }
            return View(reseptienAinesosaLista);
        }

        // GET: ReseptienAinesosaLista/Create
        public ActionResult Create()
        {
            ViewBag.MittayksikkoID = new SelectList(db.MittayksikkoLista, "MittayksikkoID", "Mittayksikko");
            ViewBag.RaakaAineID = new SelectList(db.RaakaAineet, "RaakaAineID", "RaakaAine");
            return View();
        }

        // POST: ReseptienAinesosaLista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReseptiAinesosaListaID,RaakaAineID,Maara,MittayksikkoID")] ReseptienAinesosaLista reseptienAinesosaLista)
        {
            if (ModelState.IsValid)
            {
                db.ReseptienAinesosaLista.Add(reseptienAinesosaLista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MittayksikkoID = new SelectList(db.MittayksikkoLista, "MittayksikkoID", "Mittayksikko", reseptienAinesosaLista.MittayksikkoID);
            ViewBag.RaakaAineID = new SelectList(db.RaakaAineet, "RaakaAineID", "RaakaAine", reseptienAinesosaLista.RaakaAineID);
            return View(reseptienAinesosaLista);
        }

        // GET: ReseptienAinesosaLista/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienAinesosaLista reseptienAinesosaLista = db.ReseptienAinesosaLista.Find(id);
            if (reseptienAinesosaLista == null)
            {
                return HttpNotFound();
            }
            ViewBag.MittayksikkoID = new SelectList(db.MittayksikkoLista, "MittayksikkoID", "Mittayksikko", reseptienAinesosaLista.MittayksikkoID);
            ViewBag.RaakaAineID = new SelectList(db.RaakaAineet, "RaakaAineID", "RaakaAine", reseptienAinesosaLista.RaakaAineID);
            return View(reseptienAinesosaLista);
        }

        // POST: ReseptienAinesosaLista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReseptiAinesosaListaID,RaakaAineID,Maara,MittayksikkoID")] ReseptienAinesosaLista reseptienAinesosaLista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reseptienAinesosaLista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MittayksikkoID = new SelectList(db.MittayksikkoLista, "MittayksikkoID", "Mittayksikko", reseptienAinesosaLista.MittayksikkoID);
            ViewBag.RaakaAineID = new SelectList(db.RaakaAineet, "RaakaAineID", "RaakaAine", reseptienAinesosaLista.RaakaAineID);
            return View(reseptienAinesosaLista);
        }

        // GET: ReseptienAinesosaLista/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienAinesosaLista reseptienAinesosaLista = db.ReseptienAinesosaLista.Find(id);
            if (reseptienAinesosaLista == null)
            {
                return HttpNotFound();
            }
            return View(reseptienAinesosaLista);
        }

        // POST: ReseptienAinesosaLista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReseptienAinesosaLista reseptienAinesosaLista = db.ReseptienAinesosaLista.Find(id);
            db.ReseptienAinesosaLista.Remove(reseptienAinesosaLista);
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
