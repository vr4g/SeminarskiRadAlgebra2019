using SeminarskiRad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SeminarskiRad.Controllers
{
    public class AnonymousController : Controller
    {
        private readonly SeminarskiRadEntities Context = new SeminarskiRadEntities();

        public ActionResult PreRegistration()
        {
            List<Seminar> modelDB = Context.Seminar.Where(s => s.Popunjen == false).OrderBy(s => s.Datum).ToList();
            List<SeminarViewModel> model = new List<SeminarViewModel>();

            CopyDataToViewModel(modelDB, model);

            return View(model);
        }


        [HttpPost]
        public ActionResult PreRegistration(string search)
        {
            List<Seminar> modelDB = new List<Seminar>();
            List<SeminarViewModel> model = new List<SeminarViewModel>();

            if (!string.IsNullOrEmpty(search.Trim()))
            {
                modelDB = (from x in Context.Seminar
                         where x.Naziv.Contains(search.Trim()) && x.Popunjen == false
                         orderby x.Datum
                         select x).ToList();
                CopyDataToViewModel(modelDB, model);
            }
            else
            {
                modelDB = Context.Seminar.Where(s => s.Popunjen == false).OrderBy(s => s.Datum).ToList();
                CopyDataToViewModel(modelDB, model);
            }
            return View(model);
        }

        public ActionResult PreRegistrationInput(int id)
        {
            ViewBag.Seminar = (from s in Context.Seminar
                               where s.IdSeminar == id
                               select s.Naziv).FirstOrDefault();
            PredbiljezbaViewModel model = new PredbiljezbaViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PreRegistrationInput(PredbiljezbaViewModel model, int? id)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.Seminar = (from s in Context.Seminar
                                   where s.IdSeminar == id
                                   select s.Naziv).FirstOrDefault();
                return View();
            }

            Predbiljezba newPreRegistration = new Predbiljezba();
            newPreRegistration.Ime = model.Ime;
            newPreRegistration.Prezime = model.Prezime;
            newPreRegistration.Adresa = model.Adresa;
            newPreRegistration.Email = model.Email;
            newPreRegistration.Telefon = model.Telefon;
            newPreRegistration.Datum = DateTime.Now;
            newPreRegistration.IdSeminar = id;
            newPreRegistration.Seminar = model.Seminar;

            Context.Predbiljezba.Add(newPreRegistration);
            Context.SaveChanges();

            return RedirectToAction(nameof(PreRegistration));
        }

        private static void CopyDataToViewModel(List<Seminar> modelDB, List<SeminarViewModel> model)
        {
            foreach (var seminar in modelDB)
            {
                SeminarViewModel tempModel = new SeminarViewModel();
                tempModel.Naziv = seminar.Naziv;
                tempModel.Opis = seminar.Opis;
                tempModel.Popunjen = seminar.Popunjen;
                tempModel.Predavac = seminar.Predavac;
                tempModel.Datum = seminar.Datum;
                tempModel.IdSeminar = seminar.IdSeminar;
                model.Add(tempModel);
            }
        }

    }
}