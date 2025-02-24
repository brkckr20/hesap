using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class Currency
    {
        [Display(Name = "Tarih")]
        public DateTime TARIH { get; set; }

        [Display(Name = "Usd Alış")]
        public decimal USD_ALIS { get; set; }

        [Display(Name = "Usd Satış")]
        public decimal USD_SATIS { get; set; }

        [Display(Name = "Eur Alış")]
        public decimal EUR_ALIS { get; set; }

        [Display(Name = "Eur Satış")]
        public decimal EUR_SATIS { get; set; }

        [Display(Name = "Usd / Eur")]
        public decimal USD_EUR { get; set; }
    }
}
