using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class FeatureCoding
    {
        public int Id { get; set; }

        [Display(Name = "Açıklama")]
        public string Explanation { get; set; }

        [Display(Name = "Kullanım Yeri")]
        public string PlaceOfUse { get; set; }

        [Display(Name = "Kullanım Ekranı")]
        public string UsageScreen { get; set; }

        [Display(Name = "Tip")]
        public string Type { get; set; }
    }
}
