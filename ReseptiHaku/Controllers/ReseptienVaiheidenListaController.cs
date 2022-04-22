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
    public class ReseptienVaiheidenListaController : Controller
    {
        private ReseptiHakuEntities db = new ReseptiHakuEntities();

        // GET: ReseptienVaiheidenLista
        public ActionResult Index()
        {
            var reseptienVaiheidenLista = db.ReseptienVaiheidenLista.Include(r => r.ReseptienVaiheet).Include(r => r.Reseptit);
            return View(reseptienVaiheidenLista.ToList());
        }

        // GET: ReseptienVaiheidenLista/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienVaiheidenLista reseptienVaiheidenLista = db.ReseptienVaiheidenLista.Find(id);
            if (reseptienVaiheidenLista == null)
            {
                return HttpNotFound();
            }
            return View(reseptienVaiheidenLista);
        }

        // GET: ReseptienVaiheidenLista/Create
        public ActionResult Create()
        {
            ViewBag.ReseptiVaiheID = new SelectList(db.ReseptienVaiheet, "ReseptiVaiheID", "ReseptiVaihe");
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi");
            return View();
        }

        // POST: ReseptienVaiheidenLista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RiviID,ReseptiID,ReseptiVaiheID")] ReseptienVaiheidenLista reseptienVaiheidenLista)
        {
            if (ModelState.IsValid)
            {
                db.ReseptienVaiheidenLista.Add(reseptienVaiheidenLista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReseptiVaiheID = new SelectList(db.ReseptienVaiheet, "ReseptiVaiheID", "ReseptiVaihe", reseptienVaiheidenLista.ReseptiVaiheID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", reseptienVaiheidenLista.ReseptiID);
            return View(reseptienVaiheidenLista);
        }

        // GET: ReseptienVaiheidenLista/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienVaiheidenLista reseptienVaiheidenLista = db.ReseptienVaiheidenLista.Find(id);
            if (reseptienVaiheidenLista == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReseptiVaiheID = new SelectList(db.ReseptienVaiheet, "ReseptiVaiheID", "ReseptiVaihe", reseptienVaiheidenLista.ReseptiVaiheID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", reseptienVaiheidenLista.ReseptiID);
            return View(reseptienVaiheidenLista);
        }

        // POST: ReseptienVaiheidenLista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiviID,ReseptiID,ReseptiVaiheID")] ReseptienVaiheidenLista reseptienVaiheidenLista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reseptienVaiheidenLista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReseptiVaiheID = new SelectList(db.ReseptienVaiheet, "ReseptiVaiheID", "ReseptiVaihe", reseptienVaiheidenLista.ReseptiVaiheID);
            ViewBag.ReseptiID = new SelectList(db.Reseptit, "ReseptiID", "ReseptinNimi", reseptienVaiheidenLista.ReseptiID);
            return View(reseptienVaiheidenLista);
        }

        // GET: ReseptienVaiheidenLista/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReseptienVaiheidenLista reseptienVaiheidenLista = db.ReseptienVaiheidenLista.Find(id);
            if (reseptienVaiheidenLista == null)
            {
                return HttpNotFound();
            }
            return View(reseptienVaiheidenLista);
        }

        // POST: ReseptienVaiheidenLista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReseptienVaiheidenLista reseptienVaiheidenLista = db.ReseptienVaiheidenLista.Find(id);
            db.ReseptienVaiheidenLista.Remove(reseptienVaiheidenLista);
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
