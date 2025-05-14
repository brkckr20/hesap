using System.ComponentModel.DataAnnotations;


namespace Hesap.Models
{
    public class Color
    {
        public int Id { get; set; }
        [Display(Name = "Boya Tipi")]
        public int Type{ get; set; }
        [Display(Name = "Boya Kodu")]
        public string Code { get; set; }
        [Display(Name = "Boya Adı")]
        public string Name { get; set; }
        [Display(Name = "Firma Id")]
        public int CompanyId { get; set; }
        [Display(Name = "Üst Id")]
        public int ParentId{ get; set; }
        // şuan kartta kullanılmak için eklendi, diğer alanlar ihtiyaca binaen db den eklencek
    }
}
