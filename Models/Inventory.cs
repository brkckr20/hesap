﻿using System.ComponentModel.DataAnnotations;

namespace Hesap.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Display(Name = "Malzeme Kodu")]
        public string InventoryCode { get; set; }

        [Display(Name = "Malzeme Adı")]
        public string InventoryName { get; set; }

        [Display(Name = "Birim")]
        public string Unit { get; set; }

        [Display(Name = "Tip")]
        public int Type { get; set; }

        [Display(Name = "Cins Adı")]
        public string SubType{ get; set; }

        [Display(Name = "Ön Ek Mi?")]
        public bool IsPrefix{ get; set; }

        [Display(Name = "Kullanımda")]
        public bool IsUse { get; set; }

        [Display(Name = "Birleşik Kod")]
        public string CombinedCode { get; set; }

        [Display(Name = "Stok Mu?")]
        public bool IsStock { get; set; }

    }
}
