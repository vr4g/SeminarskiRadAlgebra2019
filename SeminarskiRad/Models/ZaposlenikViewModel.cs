using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeminarskiRad.Models
{
    public class ZaposlenikViewModel
    {
        public int IdZaposlenik { get; set; }

        
        public string Ime { get; set; }
        public string Prezime { get; set; }

        [Display(Name = "Korisnik: ")]
        [Required(ErrorMessage ="unesite korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Display(Name = "Lozinka: ")]
        [Required(ErrorMessage = "unesite lozinku")]
        public string Password { get; set; }
    }
}