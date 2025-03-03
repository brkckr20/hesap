using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class Cost
    {
        public int Id { get; set; }
        [Display(Name = "Firma Id")]
        public int CompanyId { get; set; }

        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        [Display(Name = "Malzeme Id")]
        public int InventoryId { get; set; }

        [Display(Name = "Reçete Id")]
        public int RecipeId { get; set; }

        [Display(Name = "Sipariş No")]
        public string OrderNo{ get; set; }

        [Display(Name = "Resim")]
        public byte[] ProductImage{ get; set; }

        [Display(Name = "Kayıt Eden")]
        public string InsertedBy { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public DateTime InsertedDate { get; set; }

        [Display(Name = "Güncelleyen")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Güncelleme Tarihi")]
        public DateTime UpdatedDate { get; set; }

    }
}
