using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReseptiHaku.Models;

namespace ReseptiHaku.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //if (Session["UserName"] == null) // Tämä tarkistaa kirjautumisen tilaan ja asettaa LoggedStatuksen sen mukaisesti (näkyy navbarissa)
            //{
            //    ViewBag.LoggedStatus = "Out";                
            //    return RedirectToAction("login", "home"); // johtaa login-ruutuun, jollei ole sisäänkirjautunut
            //}
            //else
            //{
            //    ViewBag.LoggedStatus = "In";
            //    ViewBag.UserName = Session["UserName"];
            //}
            //ViewBag.LoginError = 0; //ei virhettä...
            return View();            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(Logins LoginModel)
        {
            ReseptiHakuEntities2 db = new ReseptiHakuEntities2();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnistetiedoilla tietokannasta LINQ-kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Succesfull login";
                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //Ei virhettä
                Session["UserName"] = LoggedUser.UserName;
                Session["LoginID"] = LoggedUser.LoginID;
                //Session["AccessLevel"] = LoggedUser.AccessLevel; // Tämä mielenkiintoinen! Jätän toistaiseksi tämän tähän, jos sitä voisi hyödyntää mahdollisesti
                //return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa -> Home/Index
                return RedirectToAction("Index"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa -> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccesfull";
                ViewBag.LoggedStatus = "Out";
                ViewBag.LoginError = 1; //Pakotetaan modaali login-ruutu uudelleen koska kirjautumisyritys on epäonnistunut
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Index", LoginModel);
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
    }
}