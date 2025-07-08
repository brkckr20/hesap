using System.ComponentModel.DataAnnotations;

namespace Hesap.Models
{
    public class Lookup
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Orijinal Adı")]
        public string OriginalName { get; set; }
        [Display(Name = "Tipi")]
        public int Type { get; set; }
    }
}
