using System.ComponentModel.DataAnnotations;


namespace Hesap.Models
{
    public class AuthVisibleItems
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Id")]
        public int UserId { get; set; }

        [Display(Name = "Buton Name")]
        public string ButtonName { get; set; }        

        [Display(Name = "Grup Adı")]
        public string GroupName { get; set; }

        [Display(Name = "Gizle/Göster")]
        public bool IsVisible { get; set; }

        [Display(Name = "Buton Text")]
        public string ButtonText { get; set; }
    }
}
