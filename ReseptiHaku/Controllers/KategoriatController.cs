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
    public class KategoriatController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        // GET: Kategoriat
        public ActionResult Index()
        {
            // Tämä johtaa /home/login/ vanhaan sisäänkirjautumis-näkymään.

            //if (Session["UserName"] == null)
            //{
            //    ViewBag.LoggedStatus = "Out";
            //    return RedirectToAction("login", "home");
            //}
            //else
            //{
            //    ViewBag.LoggedStatus = "In";
            //    ViewBag.UserName = Session["UserName"];
            //    return View(db.Kategoriat.ToList());
            //}
            return View(db.Kategoriat.ToList());
        }

        // Mikäli halutaan palauttaa sisäänkirjautumisen jälkeen ko. controllerin näkymään,
        // vaaditaan controllerille oma Authorize-metodi ja siellä korrektit määritellyt kohtaan RedirectToAction("Index", "Kategoriat")
        // sekä lisäksi jokaiselle tarvittavalle controllereille oma _LoginModal -partial view, joka vastaavasti ohjaa oikean kontrollerin Authorize-metodille.
        // Muutoin riittää /Shared/_LoginModal, joka ohjaa HomeControllerin Authorize-metodille aina, riippumatta mistä Login tehdään.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Authorize(Logins LoginModel)
        //{
        //    ReseptiHakuEntities2 db = new ReseptiHakuEntities2();
        //    //Haetaan käyttäjän/Loginin tiedot annetuilla tunnistetiedoilla tietokannasta LINQ-kyselyllä
        //    var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
        //    if (LoggedUser != null)
        //    {
        //        ViewBag.LoginMessage = "Succesfull login";
        //        ViewBag.LoggedStatus = "In";
        //        ViewBag.LoginError = 0; //Ei virhettä
        //        Session["UserName"] = LoggedUser.UserName;
        //        Session["LoginID"] = LoggedUser.LoginID;
        //        //Session["AccessLevel"] = LoggedUser.AccessLevel; // Tämä mielenkiintoinen! Jätän toistaiseksi tämän tähän, jos sitä voisi hyödyntää mahdollisesti
        //        return RedirectToAction("Index", "Kategoriat"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa -> Home/Index
        //    }
        //    else
        //    {
        //        ViewBag.LoginMessage = "Login unsuccesfull";
        //        ViewBag.LoggedStatus = "Out";
        //        ViewBag.LoginError = 1; //Pakotetaan modaali login-ruutu uudelleen koska kirjautumisyritys on epäonnistunut
        //        LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
        //        return View("Index", LoginModel);
        //    }
        //}

        // GET: Kategoriat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriat kategoriat = db.Kategoriat.Find(id);
            if (kategoriat == null)
            {
                return HttpNotFound();
            }
            return View(kategoriat);
        }

        // GET: Kategoriat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kategoriat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriaID,Kategoria")] Kategoriat kategoriat)
        {
            if (ModelState.IsValid)
            {
                db.Kategoriat.Add(kategoriat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategoriat);
        }

        // GET: Kategoriat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriat kategoriat = db.Kategoriat.Find(id);
            if (kategoriat == null)
            {
                return HttpNotFound();
            }
            return View(kategoriat);
        }

        // POST: Kategoriat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategoriaID,Kategoria")] Kategoriat kategoriat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategoriat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategoriat);
        }

        // GET: Kategoriat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriat kategoriat = db.Kategoriat.Find(id);
            if (kategoriat == null)
            {
                return HttpNotFound();
            }
            return View(kategoriat);
        }

        // POST: Kategoriat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategoriat kategoriat = db.Kategoriat.Find(id);
            db.Kategoriat.Remove(kategoriat);
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
