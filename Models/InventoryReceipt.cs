using System.ComponentModel.DataAnnotations;

namespace Hesap.Models
{
    public class InventoryReceipt
    {
        public int Id { get; set; }

        [Display(Name = "Reçete No")]
        public string ReceiptNo { get; set; }

        [Display(Name = "Ham En")]
        public decimal RawWidth { get; set; }

        [Display(Name = "Ham Boy")]
        public decimal RawHeight { get; set; }

        [Display(Name = "Mamül En")]
        public decimal ProductWidth { get; set; }

        [Display(Name = "Mamül Boy")]
        public decimal ProductHeight { get; set; }

        [Display(Name = "Ham Gramaj")]
        public decimal RawGrammage { get; set; }

        [Display(Name = "Mamül Gramaj")]
        public decimal ProductGrammage { get; set; }

        [Display(Name = "İpliği Boyalı?")]
        public bool YarnDyed { get; set; }

        [Display(Name = "Açıklama")]
        public string Explanation { get; set; }

        [Display(Name = "Reçete Tipi")]
        public int ReceiptType { get; set; }

        [Display(Name = "Malzeme Id")]
        public int InventoryId { get; set; }

        [Display(Name = "Ürün Resmi")]
        public byte[] ReceiptImage1 { get; set; }

        [Display(Name = "Kullanım Yeri")]
        public string PlaceOfUse { get; set; }

        [Display(Name = "Nakış Referans")]
        public int EmbroideryRef { get; set; }

        [Display(Name = "Cinsi")]
        public string Genus { get; set; }

        [Display(Name = "GrM2")]
        public int GrM2 { get; set; }

        [Display(Name = "Organik Mi?")]
        public bool IsOrganic { get; set; }

        [Display(Name = "Malzeme Kodu")]
        public string InventoryCode { get; set; }

        [Display(Name = "Malzeme Adı")]
        public string InventoryName { get; set; }

    }
}
