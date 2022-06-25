using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeminarskiRad.Models
{
    public class PredbiljezbaViewModel
    {
        public int IdPredbiljezba { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Datum { get; set; }

        [Display(Name ="Ime:")]
        [Required(ErrorMessage ="ime je obavezno")]
        public string Ime { get; set; }

        [Display(Name = "Prezime:")]
        [Required(ErrorMessage = "prezime je obavezno")]
        public string Prezime { get; set; }

        [Display(Name = "Adresa:")]
        [Required(ErrorMessage = "adresa je obavezna")]
        public string Adresa { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "email je obavezan")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "unesite ispravnu email adresu")]
        public string Email { get; set; }

        [Display(Name = "Telefon:")]
        [Required(ErrorMessage = "telefon je obavezan")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "unesite ispravan broj telefona (format: 09*-***-****)")]
        public string Telefon { get; set; }

        public Nullable<int> IdSeminar { get; set; }

        [Display(Name = "Status:")]
        public Nullable<bool> Status { get; set; }

        public virtual Seminar Seminar { get; set; }
    }
}