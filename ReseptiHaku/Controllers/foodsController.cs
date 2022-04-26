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
    public class foodsController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: foods
        public ActionResult Index()
        {
            var food = db.food.Include(f => f.foodtype_FI).Include(f => f.fuclass_FI).Include(f => f.fuclass_FI1).Include(f => f.igclass_FI).Include(f => f.igclass_FI1).Include(f => f.process_FI);
            return View(food.ToList());
        }

        // GET: foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: foods/Create
        public ActionResult Create()
        {
            ViewBag.FOODTYPE = new SelectList(db.foodtype_FI, "THSCODE", "DESCRIPT");
            ViewBag.FUCLASS = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT");
            ViewBag.FUCLASSP = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT");
            ViewBag.IGCLASS = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT");
            ViewBag.IGCLASSP = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT");
            ViewBag.PROCESS = new SelectList(db.process_FI, "THSCODE", "DESCRIPT");
            return View();
        }

        // POST: foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FOODID,FOODNAME,FOODTYPE,PROCESS,EDPORT,IGCLASS,IGCLASSP,FUCLASS,FUCLASSP")] food food)
        {
            if (ModelState.IsValid)
            {
                db.food.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FOODTYPE = new SelectList(db.foodtype_FI, "THSCODE", "DESCRIPT", food.FOODTYPE);
            ViewBag.FUCLASS = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASS);
            ViewBag.FUCLASSP = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASSP);
            ViewBag.IGCLASS = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASS);
            ViewBag.IGCLASSP = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASSP);
            ViewBag.PROCESS = new SelectList(db.process_FI, "THSCODE", "DESCRIPT", food.PROCESS);
            return View(food);
        }

        // GET: foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.FOODTYPE = new SelectList(db.foodtype_FI, "THSCODE", "DESCRIPT", food.FOODTYPE);
            ViewBag.FUCLASS = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASS);
            ViewBag.FUCLASSP = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASSP);
            ViewBag.IGCLASS = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASS);
            ViewBag.IGCLASSP = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASSP);
            ViewBag.PROCESS = new SelectList(db.process_FI, "THSCODE", "DESCRIPT", food.PROCESS);
            return View(food);
        }

        // POST: foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FOODID,FOODNAME,FOODTYPE,PROCESS,EDPORT,IGCLASS,IGCLASSP,FUCLASS,FUCLASSP")] food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FOODTYPE = new SelectList(db.foodtype_FI, "THSCODE", "DESCRIPT", food.FOODTYPE);
            ViewBag.FUCLASS = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASS);
            ViewBag.FUCLASSP = new SelectList(db.fuclass_FI, "THSCODE", "DESCRIPT", food.FUCLASSP);
            ViewBag.IGCLASS = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASS);
            ViewBag.IGCLASSP = new SelectList(db.igclass_FI, "THSCODE", "DESCRIPT", food.IGCLASSP);
            ViewBag.PROCESS = new SelectList(db.process_FI, "THSCODE", "DESCRIPT", food.PROCESS);
            return View(food);
        }

        // GET: foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            food food = db.food.Find(id);
            db.food.Remove(food);
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
