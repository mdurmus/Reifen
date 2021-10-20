using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reifen.Models
{
    public class Personal
    {
        //burayi degistirdim

        public int PersonalId { get; set; }
        
        [Display(Name ="Name")]
        [Required(ErrorMessage = "Bitte Name hinzufügen!")]
        public string Name { get; set; }

        [Display(Name = "Familienname")]
        [Required(ErrorMessage = "Bitte famillienname hinzufügen!")]
        public string LastName { get; set; }
        
        [Display(Name ="Benutzername")]
        [Required(ErrorMessage = "Bitte benutzername hinzufügen!")]
        public string UserName { get; set; }
        
        [Display(Name="E-mail")]
        [EmailAddress]
        [Required(ErrorMessage = "Bitte E-mail hinzufügen!")]
        public string Email { get; set; }

        [Display(Name ="Kennwort")]
        public string Password { get; set; }
        
        [Display(Name="Benutzer Aktiv?")]
        public bool isActive { get; set; }

        [Display(Name ="Admin?")]
        public bool isManager { get; set; }
    }
}
