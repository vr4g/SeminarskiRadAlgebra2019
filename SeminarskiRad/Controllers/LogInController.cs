using SeminarskiRad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SeminarskiRad.Controllers
{
    public class LogInController : Controller
    {
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ZaposlenikViewModel model, bool remember)
        {
            //if (string.IsNullOrEmpty(model.KorisnickoIme)) {
            //    ModelState.AddModelError("KorisnickoIme", "unesite korisničko ime");
            //}
            //if (string.IsNullOrEmpty(model.Password))
            //{
            //    ModelState.AddModelError("Password", "unesite password");
            //}

            if (ModelState.IsValid)
            {
                using (SeminarskiRadEntities Context = new SeminarskiRadEntities())
                {
                    var employee = Context.Zaposlenik.Where(a => a.KorisnickoIme.Equals(model.KorisnickoIme) && a.Password.Equals(model.Password)).FirstOrDefault();

                    if (employee != null)
                    {
                        Session["UserID"] = employee.IdZaposlenik.ToString();
                        Session["UserName"] = employee.KorisnickoIme.ToString();
                        if (remember == false)
                        {
                            FormsAuthentication.SetAuthCookie(model.KorisnickoIme, false);
                        }
                        else if(remember == true) {
                            FormsAuthentication.SetAuthCookie(model.KorisnickoIme, true);
                        }
                        return RedirectToAction("PreRegistration", "Employee");
                    }
                    else {
            
                        ViewBag.UnsuccesfulLogin = "Netočni podaci za prijavu";
                    }
                }
            }
          
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("PreRegistration", "Anonymous");
        }

    }
}