using DevExpress.XtraEditors.Filtering.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Context
{
    internal class _Etiket
    {
        public int Id { get; set; }
        public Date Tarih{ get; set; }
        public string Aciklama{ get; set; }
        public int BasimSayisi{ get; set; }
        public int Yuzde{ get; set; }
        public bool Tekli{ get; set; }
        public string UrunKodu { get; set; }
        public string ArtNo { get; set; }
        public string Sticker1{ get; set; }
        public string Sticker2 { get; set; }
        public string Sticker3 { get; set; }
        public string Sticker4 { get; set; }
        public string Sticker5 { get; set; }
        public string Sticker6 { get; set; }
        public string Sticker7 { get; set; }
        public string Sticker8 { get; set; }
        public string Sticker9 { get; set; }
        public string Sticker10 { get; set; }
        public string MusteriOrderNo { get; set; }
        public string OrderNo { get; set; }
        public string Barkod { get; set; }
        public string Varyant1{ get; set; }
        public string Varyant2 { get; set; }
        public string EbatBeden { get; set; }
        public int Miktar { get; set; }
        public int D2Id { get; set; }
    }
}
