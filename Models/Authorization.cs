using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
