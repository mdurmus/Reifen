using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reifen.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Bitte BHZ hinzufügen")]
        [Display( Name ="BHZ")]
        public string Dimensions { get; set; }

        [Display(Name ="EK Netto")]
        public double EKNetto { get; set; }

        [Display(Name = "EK Brutto")]
        public double EKBrutto { get; set; }

        [Display(Name = "VK Netto")]
        public double VKNetto { get; set; }

        [Display(Name = "VK Brutto")]
        public double VKBrutto { get; set; }

        [Display(Name ="Menge")]
        public int StockQuantity { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
