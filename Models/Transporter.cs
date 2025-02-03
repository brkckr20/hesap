using System.ComponentModel.DataAnnotations;

namespace Hesap.Models
{
    public class Transporter
    {
        public int Id { get; set; }

        [Display(Name = "Ünvan")]
        public string Title { get; set; }

        [Display(Name = "Adı")]
        public string Name { get; set; }

        [Display(Name = "Soyadı")]
        public string Surname { get; set; }

        [Display(Name = "T.C.")]
        public string TCKN { get; set; }

        [Display(Name = "Plaka")]
        public string NumberPlate{ get; set; }

        [Display(Name = "Dorse")]
        public string TrailerNumber{ get; set; }

        [Display(Name = "Kullanımda")]
        public bool IsUse { get; set; }
    }
}
