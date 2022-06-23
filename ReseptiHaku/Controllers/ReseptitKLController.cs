using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReseptiHaku.Models;
using ReseptiHaku.ViewModels;

namespace ReseptiHaku.Controllers
{
    public class ReseptitKLController : Controller
    {
        private ReseptiHakuEntities2 db = new ReseptiHakuEntities2();

        public ActionResult Reseptit()
        {
            var reseptit = from r in db.Reseptit
                           select r;

            return View(reseptit.ToList());
        }

        //// GET: Reseptit/
        //public ActionResult _ModalEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Reseptit reseptit = db.Reseptit.Find(id);
        //    if (reseptit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ReseptiVaiheID = new SelectList(db.ReseptienVaiheet, "ReseptiVaiheID", "ReseptiVaihe", reseptit.ReseptiID);
        //    //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
        //    //ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
        //    return PartialView("_ModalEdit", reseptit);
        //}

        //// POST: Orders/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult _ModalEdit([Bind(Include = "ReseptiID,ReseptienVaiheidenLista.ReseptiVaiheID,ReseptienVaiheet.ReseptiVaihe")] Reseptit reseptit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(reseptit).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ReseptiVaiheID = new SelectList(db.ReseptienVaiheet, "ReseptiVaiheID", "ReseptiVaihe", reseptit.ReseptiID);
        //    //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
        //    //ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
        //    return PartialView("_ModalEdit", reseptit);
        //}

        public ActionResult ReseptitYhteenveto()
        {
            var reseptitYhteenveto = from r in db.Reseptit
                                     join rv in db.ReseptienAinesosienVaihde on r.ReseptiID equals rv.ReseptiID
                                     join rl in db.ReseptienAinesosaLista on rv.ReseptiAinesosaListaID equals rl.ReseptiAinesosaListaID
                                     join ra in db.RaakaAineet on rl.RaakaAineID equals ra.RaakaAineID
                                     join ml in db.MittayksikkoLista on rl.MittayksikkoID equals ml.MittayksikkoID
                                     join k in db.Kategoriat on ra.KategoriaID equals k.KategoriaID
                                     join rvl in db.ReseptienVaiheidenLista on r.ReseptiID equals rvl.ReseptiID
                                     join rev in db.ReseptienVaiheet on rvl.ReseptiVaiheID equals rev.ReseptiVaiheID
                                     //where-lause
                                     //orderby-lause
                                     select new ReseptitData
                                     {
                                         ReseptiID = r.ReseptiID,
                                         ReseptinNimi = r.ReseptinNimi,
                                         AnnosKoko = r.AnnosKoko,
                                         LoginID = r.LoginID,
                                         Julkinen = r.Julkinen,
                                         RiviID = rv.RiviID,
                                         ReseptiAinesosaListaID = rl.ReseptiAinesosaListaID,
                                         RaakaAineID = ra.RaakaAineID,
                                         Maara = rl.Maara,
                                         MittayksikkoID = ml.MittayksikkoID,
                                         RaakaAine = ra.RaakaAine,
                                         KategoriaID = k.KategoriaID,
                                         Mittayksikko = ml.Mittayksikko,
                                         MittayksikkoSelite = ml.MittayksikkoSelite,
                                         Kategoria = k.Kategoria,
                                         ReseptiVaiheID = rev.ReseptiVaiheID,
                                         ReseptiVaihe = rev.ReseptiVaihe
                                     };
            return View(reseptitYhteenveto);

        }


        // GET: ReseptitKL
        public ActionResult Index()
        {
            var reseptit = db.Reseptit.Include(r => r.Logins)/*.Include(r => r.ReseptienVaiheidenLista.Select(q => q.ReseptienVaiheet))*/;
            return View(reseptit.ToList());
        }

        // GET: ReseptitKL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseptit reseptit = db.Reseptit.Find(id);
            if (reseptit == null)
            {
                return HttpNotFound();
            }

            //------Raaka-aineet, määrät ja mittayksiköt------
            // Sijoitetaan ReseptienAineet taulusta kyseisen ReseptiID:n mukaiset vaiheet ViewBagiin
            List<ReseptienAinesosaLista> lstAineet = new List<ReseptienAinesosaLista>();

            var aineLista = from ral in db.ReseptienAinesosaLista
                            join rav in db.ReseptienAinesosienVaihde on ral.ReseptiAinesosaListaID equals rav.ReseptiAinesosaListaID
                            join ml in db.MittayksikkoLista on ral.MittayksikkoID equals ml.MittayksikkoID
                            join ra in db.RaakaAineet on ral.RaakaAineID equals ra.RaakaAineID
                            where rav.ReseptiID == id
                            select ral;



            foreach (ReseptienAinesosaLista aine in aineLista)
            {
                ReseptienAinesosaLista yksiAine = new ReseptienAinesosaLista();
                yksiAine.RaakaAineID = aine.RaakaAineID;
                //yksiAine.RaakaAine = aine.RaakaAineet.RaakaAine;
                yksiAine.Maara = aine.Maara;
                yksiAine.MittayksikkoID = aine.MittayksikkoID;
                //yksiAine.MittayksikkoLista.Mittayksikko = aine.MittayksikkoLista.Mittayksikko;
                lstAineet.Add(yksiAine);
            }

            ViewBag.ReseptiAineetBag = lstAineet;

            // ------Reseptien vaiheet------
            // Sijoitetaan ReseptienVaiheet taulusta kyseisen ReseptiID:n mukaiset vaiheet ViewBagiin
            List<ReseptienVaiheet> lstVaiheet = new List<ReseptienVaiheet>();

            var vaiheLista = from resv in db.ReseptienVaiheet
                             join resvl in db.ReseptienVaiheidenLista on resv.ReseptiVaiheID equals resvl.ReseptiVaiheID
                             where resvl.ReseptiID == id
                             select resv;

            //ReseptienVaiheet tyhjaVaiheet = new ReseptienVaiheet();
            //tyhjaVaiheet.ReseptiVaiheID = 0;
            //tyhjaVaiheet.ReseptiVaihe = "";
            //lstVaiheet.Add(tyhjaVaiheet);

            foreach (ReseptienVaiheet vaihe in vaiheLista)
            {
                ReseptienVaiheet yksiVaihe = new ReseptienVaiheet();
                yksiVaihe.ReseptiVaiheID = vaihe.ReseptiVaiheID;
                yksiVaihe.ReseptiVaihe = vaihe.ReseptiVaihe;
                lstVaiheet.Add(yksiVaihe);
            }
            
            ViewBag.ReseptiVaiheetBag = lstVaiheet;

            return View(reseptit);
        }

        // GET: ReseptitKL/Create
        public ActionResult Create()
        {
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName");
            return View();
        }

        // POST: ReseptitKL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReseptiID,ReseptinNimi,AnnosKoko,LoginID,Julkinen")] Reseptit reseptit)
        {
            if (ModelState.IsValid)
            {
                db.Reseptit.Add(reseptit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", reseptit.LoginID);
            return View(reseptit);
        }

        // GET: ReseptitKL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseptit reseptit = db.Reseptit.Find(id);
            if (reseptit == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", reseptit.LoginID);
            return View(reseptit);
        }

        // POST: ReseptitKL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReseptiID,ReseptinNimi,AnnosKoko,LoginID,Julkinen")] Reseptit reseptit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reseptit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", reseptit.LoginID);
            return View(reseptit);
        }

        // GET: ReseptitKL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseptit reseptit = db.Reseptit.Find(id);
            if (reseptit == null)
            {
                return HttpNotFound();
            }
            return View(reseptit);
        }

        // POST: ReseptitKL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reseptit reseptit = db.Reseptit.Find(id);
            db.Reseptit.Remove(reseptit);
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
