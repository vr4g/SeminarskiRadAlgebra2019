using SeminarskiRad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeminarskiRad.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly SeminarskiRadEntities Context = new SeminarskiRadEntities();

        public ActionResult PreRegistration()
        {
            List<Predbiljezba>  model = Context.Predbiljezba.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult PreRegistration(int? id, string search, string filter)
        {
            List<Predbiljezba> model = new List<Predbiljezba>();
            if (!string.IsNullOrEmpty(search.Trim()))
            {
                model = (from x in Context.Predbiljezba
                         where x.Ime.Contains(search.Trim()) || x.Prezime.Contains(search.Trim()) ||
                         x.Telefon.Contains(search.Trim()) || x.Adresa.Contains(search.Trim()) ||
                         x.Email.Contains(search.Trim()) || x.Datum.ToString().Contains(search.Trim()) ||
                         x.Seminar.Naziv.Contains(search.Trim())
                         select x).ToList();
            }
            else
            {
                model = Context.Predbiljezba.ToList();
            }

            if (filter == "1")
            {
                model = Context.Predbiljezba.Where(x => x.Status == true).ToList();
                return View(model);
            }
            else if (filter == "2")
            {
                model = Context.Predbiljezba.Where(x => x.Status == false).ToList();
                return View(model);
            }
            else if (filter == "3")
            {
                model = Context.Predbiljezba.Where(x => x.Status == null).ToList();
                return View(model);
            }

            return View(model);

        }

        public ActionResult EditPreRegistration(int id)
        {
            PredbiljezbaViewModel model = new PredbiljezbaViewModel();
            var preRegistration = Context.Predbiljezba.SingleOrDefault(p => p.IdPredbiljezba == id);
            model.Seminar = preRegistration.Seminar;
            model.Ime = preRegistration.Ime;
            model.Prezime = preRegistration.Prezime;
            model.Adresa = preRegistration.Adresa;
            model.Datum = preRegistration.Datum;
            model.Email = preRegistration.Email;
            model.Telefon = preRegistration.Telefon;
            model.Status = preRegistration.Status;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditPreRegistration(PredbiljezbaViewModel model, int? id)
        {
            var preRegistration = Context.Predbiljezba.Single(p => p.IdPredbiljezba == id);


            if (model.Datum == null)
            {
                ModelState.AddModelError("Datum", "datum je obavezan");
            }
            if (!ModelState.IsValid)
            {
                model.Seminar = preRegistration.Seminar;
                return View(model);
            }

            ViewBag.Seminar = preRegistration.Seminar.Naziv;
            
            preRegistration.Ime = model.Ime;
            preRegistration.Prezime = model.Prezime;
            preRegistration.Adresa = model.Adresa;
            preRegistration.Datum = model.Datum;
            preRegistration.Email = model.Email;
            preRegistration.Telefon = model.Telefon;
            preRegistration.Status = model.Status;
          
            Context.SaveChanges();

            return RedirectToAction(nameof(PreRegistration), model);
        }

        public ActionResult Seminars()
        {
            List<Seminar> modelDB = Context.Seminar.OrderBy(x => x.Datum).ToList();
            List<SeminarViewModel> model = new List<SeminarViewModel>();

            CopyDataToViewModel(modelDB, model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Seminars(string search)
        {
            List<Seminar> modelDB = Context.Seminar.OrderBy(x => x.Datum).ToList();
            List<SeminarViewModel> model = new List<SeminarViewModel>();

            if (!string.IsNullOrEmpty(search.Trim()))
            {
                modelDB = (from x in Context.Seminar
                         where x.Naziv.Contains(search.Trim()) || x.Opis.Contains(search.Trim())
                         select x).ToList();

                CopyDataToViewModel(modelDB, model);

                return View(model);
            }

            CopyDataToViewModel(modelDB, model);

            return View(model);
        }

        public ActionResult Upsert(int? id)
        {
            SeminarViewModel model = new SeminarViewModel();
            if (id.HasValue)
            {
                var seminar = Context.Seminar.Where(s => s.IdSeminar == id).Single();
                model.Naziv = seminar.Naziv;
                model.Opis = seminar.Opis;
                model.Predavac = seminar.Predavac;
                model.Datum = seminar.Datum;
                model.Popunjen = seminar.Popunjen;
                return View(model);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Upsert(int? id, SeminarViewModel model)
        {
            
            if (!ModelState.IsValid) {
                return View(model);
            }

            if (id.HasValue)
            {
                var seminar = Context.Seminar.Single(s => s.IdSeminar == id);
                seminar.Naziv = model.Naziv;
                seminar.Opis = model.Opis;
                seminar.Predavac = model.Predavac;
                seminar.Datum = model.Datum;
                seminar.Popunjen = model.Popunjen;

            }
            else
            {          
                Seminar newSeminar = new Seminar();
                newSeminar.Naziv = model.Naziv;
                newSeminar.Opis = model.Opis;
                newSeminar.Predavac = model.Predavac;
                newSeminar.Datum = model.Datum;
                newSeminar.Popunjen = model.Popunjen;
                Context.Seminar.Add(newSeminar);
            }

            Context.SaveChanges();
            return RedirectToAction(nameof(Seminars), model);
        }

        public ActionResult Delete(int? id)
        {
            var predbiljezba = Context.Predbiljezba.SingleOrDefault(p => p.IdSeminar == id);
            var seminar = Context.Seminar.SingleOrDefault(s => s.IdSeminar == id);
            Context.Seminar.Remove(seminar);
            if (predbiljezba != null) {

            Context.Predbiljezba.Remove(predbiljezba);
            }
            Context.SaveChanges();


            return RedirectToAction(nameof(Seminars));
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