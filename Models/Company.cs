using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Display(Name = "Firma Kodu")]
        public string CompanyCode { get; set; }
        [Display(Name = "Firma Adı")]
        public string CompanyName { get; set; }
        [Display(Name = "Adres 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Adres 2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "Adres 3")]
        public string AddressLine3 { get; set; }
    }
}
