using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeminarskiRad.Models
{
    public class SeminarViewModel
    {
        public int IdSeminar { get; set; }

        [Display(Name ="Naziv seminara:")]
        [Required(ErrorMessage = "obavezan naziv seminara")]
        public string Naziv { get; set; }

        [Display(Name = "Opis seminara:")]
        [Required(ErrorMessage ="obavezan opis seminara")]
        public string Opis { get; set; }

        [Display(Name = "Datum:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "obavezan datum seminara")]
        public Nullable<System.DateTime> Datum { get; set; }
        public bool Popunjen { get; set; }

        [Display(Name = "Predavač:")]
        [Required(ErrorMessage = "obavezan predavač")]
        public string Predavac { get; set; }
    }
}