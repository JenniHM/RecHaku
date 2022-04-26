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
    public class MittayksikkoListaController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: MittayksikkoLista
        public ActionResult Index()
        {
            return View(db.MittayksikkoLista.ToList());
        }

        // GET: MittayksikkoLista/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MittayksikkoLista mittayksikkoLista = db.MittayksikkoLista.Find(id);
            if (mittayksikkoLista == null)
            {
                return HttpNotFound();
            }
            return View(mittayksikkoLista);
        }

        // GET: MittayksikkoLista/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MittayksikkoLista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MittayksikkoID,Mittayksikko,MittayksikkoSelite")] MittayksikkoLista mittayksikkoLista)
        {
            if (ModelState.IsValid)
            {
                db.MittayksikkoLista.Add(mittayksikkoLista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mittayksikkoLista);
        }

        // GET: MittayksikkoLista/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MittayksikkoLista mittayksikkoLista = db.MittayksikkoLista.Find(id);
            if (mittayksikkoLista == null)
            {
                return HttpNotFound();
            }
            return View(mittayksikkoLista);
        }

        // POST: MittayksikkoLista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MittayksikkoID,Mittayksikko,MittayksikkoSelite")] MittayksikkoLista mittayksikkoLista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mittayksikkoLista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mittayksikkoLista);
        }

        // GET: MittayksikkoLista/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MittayksikkoLista mittayksikkoLista = db.MittayksikkoLista.Find(id);
            if (mittayksikkoLista == null)
            {
                return HttpNotFound();
            }
            return View(mittayksikkoLista);
        }

        // POST: MittayksikkoLista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MittayksikkoLista mittayksikkoLista = db.MittayksikkoLista.Find(id);
            db.MittayksikkoLista.Remove(mittayksikkoLista);
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
