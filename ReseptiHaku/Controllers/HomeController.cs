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
            if (Session["UserName"] == null) // Tämä tarkistaa kirjautumisen tilaan ja asettaa LoggedStatuksen sen mukaisesti (näkyy navbarissa)
            {
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("login", "home"); // johtaa login-ruutuun, jollei ole sisäänkirjautunut
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                ViewBag.UserName = Session["UserName"];
            }
            ViewBag.LoginError = 0; //ei virhettä...
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
        public ActionResult Authorize(Logins LoginModel)
        {
            ReseptiHakuEntities2 db = new ReseptiHakuEntities2();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnistetiedoilla tietokannasta LINQ-kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Succesfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa -> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccesfull";
                ViewBag.LoggedStatus = "Out";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginModel);
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