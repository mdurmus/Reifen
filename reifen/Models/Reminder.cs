using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reifen.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bitte Name hinzufügen!")]
        [Display(Name="Name")]
        public string Name { get; set; }
        [Display(Name="Familienname")]
        public string Lastname { get; set; }

        [Display(Name = "Handy Nummer")]
        [Required(ErrorMessage = "Bitte handynummer hinzufügen!")]
        public string GSM { get; set; }

        [Display(Name="Email")]
        [Required(ErrorMessage = "Bitte Email hinzufügen! ")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name="Strasse")]
        public string Street { get; set; }
       
        [Display(Name = "PLZ")]
        public string Postal { get; set; }
        [Display(Name = "Ort")]
        public string City { get; set; }

        [Required(ErrorMessage = "Bitte KFZ-Kennzeichen hinzufügen!")]
        [Display(Name= "KFZ-Kennzeichen")]
        public string Plate { get; set; }
        [Display(Name = "Lager")]
        public int WareHouse { get; set; }
        [Display(Name = "Reihe")]
        public string Row { get; set; }
        [Display(Name = "Regal")]
        public string Column { get; set; }

        [Display(Name="Abholtermin")]
        [Required(ErrorMessage = "Bitte Abholtermin hinzufügen!")]
        public DateTime EndDate { get; set; }
    }
}
