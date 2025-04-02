using System.ComponentModel.DataAnnotations;

namespace Hesap.Models
{
    public class Numerator
    {
        public int Id { get; set; }
        [Display(Name = "Ön Ek")]
        public string Prefix { get; set; }
        [Display(Name = "Numara")]
        public int Number { get; set; }
        [Display(Name = "Malzeme İsmi")]
        public string Name { get; set; }
        [Display(Name = "Aktif Mi?")]
        public bool IsActive { get; set; }
        [Display(Name = "Malzeme Tipi")]
        public int InventoryType { get; set; }
    }
}
