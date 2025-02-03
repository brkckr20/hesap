using System.ComponentModel.DataAnnotations;

namespace Hesap.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Kodu")]
        public string Code { get; set; }

        [Display(Name = "Adı")]
        public string Name { get; set; }
        
        [Display(Name = "Soyadı")]
        public string Surname { get; set; }

        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Kullanımda")]
        public bool IsUse { get; set; }
    }
}
