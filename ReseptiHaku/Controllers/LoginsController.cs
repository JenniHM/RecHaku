﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using ReseptiHaku.Models;

namespace ReseptiHaku.Controllers
{
    public class LoginsController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: Logins
        public ActionResult Index()
        {
            // kaiken listaavalle Index(3):lle oikea return View parametreineen:

            return View(db.Logins.ToList());

            // _CreateModal:in avaavan indexin return View
            //ViewBag.Message = ("Haluatko luoda uuden käyttäjätunnuksen?");
            //return View();

            // muutkaan CRUD:in osat eivät tällä return Viewillä varmaankaan toimi
        }

        // GET: Logins
        public ActionResult IndexUI()
        {
            
            // _CreateModal:in avaavan indexin return View
            ViewBag.Message = ("Haluatko luoda uuden käyttäjätunnuksen?");
            return View();

            // muutkaan CRUD:in osat eivät tällä return Viewillä varmaankaan toimi
        }

        // GET: Logins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logins logins = db.Logins.Find(id);
            if (logins == null)
            {
                return HttpNotFound();
            }
            return View(logins);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            return View();
        }
                
        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginID,UserName,PassWord")] Logins logins)
        {
            //ViewBag.Success = "";
            if (ModelState.IsValid)
            {
                db.Logins.Add(logins);
                db.SaveChanges();
                //MessageBox.Show("Käyttäjätunnus luotu onnistuneesti. Ole hyvä ja kirjaudu sisään käyttäjätunnuksella ja salasanalla.");                
                //ViewBag.Success = "Käyttäjätunnus ja salasana luotu onnistuneesti. Ole hyvä ja kirjaudu sisään käyttäjätunnuksella.";
                TempData["success"] = "Käyttäjätunnus ja salasana luotu onnistuneesti. Ole hyvä ja kirjaudu sisään käyttäjätunnuksella.";
                return RedirectToAction("IndexUI");
            }

            return View(logins);
        }

        // GET: Logins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logins logins = db.Logins.Find(id);
            if (logins == null)
            {
                return HttpNotFound();
            }
            return View(logins);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginID,UserName,PassWord")] Logins logins)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logins).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logins);
        }

        // GET: Logins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logins logins = db.Logins.Find(id);
            if (logins == null)
            {
                return HttpNotFound();
            }
            return View(logins);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logins logins = db.Logins.Find(id);
            db.Logins.Remove(logins);
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
