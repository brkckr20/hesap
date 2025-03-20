using System.ComponentModel.DataAnnotations;


namespace Hesap.Models
{
    public class Authorization
    {
        public int Id { get; set; }

        [Display(Name = "Ekran Adı")]
        public string ScreenName { get; set; }

        [Display(Name = "Giriş")]
        public bool CanAccess { get; set; }

        [Display(Name = "Kullanıcı Id")]
        public int UserId { get; set; }

        [Display(Name = "Kayıt")]
        public bool CanSave { get; set; }

        [Display(Name = "Silme")]
        public bool CanDelete { get; set; }

        [Display(Name = "Tag")]
        public string Tag{ get; set; }

        [Display(Name = "Güncelleme")]
        public bool CanUpdate { get; set; }

        [Display(Name = "Açan Button Adı")]
        public string OpenButtonName { get; set; }
    }
}
