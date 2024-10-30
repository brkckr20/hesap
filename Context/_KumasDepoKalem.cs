using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Context
{
    public class _KumasDepoKalem
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public string KalemIslem { get; set; }
        public string SipNo { get; set; }
        public int KumasId { get; set; }
        public string KumasKodu { get; set; }
        public string KumasAdi { get; set; }
        public string BordurKodu { get; set; }
        public string Bordur { get; set; }
        public decimal GrM2 { get; set; }
        public decimal HamGr { get; set; }
        public int RenkId { get; set; }
        public string BoyahaneRenkKodu { get; set; }
        public string BoyahaneRenkAdi { get; set; }
        public decimal BrutKg { get; set; }
        public decimal NetKg { get; set; }
        public decimal BrutMt { get; set; }
        public decimal NetMt { get; set; }
        public int Adet { get; set; }
        public decimal Fire { get; set; }
        public int CuvalSayisi { get; set; }
        public int TopSayisi { get; set; }
        public string SatirAciklama { get; set; }
        public string UUID { get; set; }
        public int HataId { get; set; }
        public string IstenenEbat { get; set; }
        public string BoyaOzellik { get; set; }
        public int BaskiId { get; set; }
        public string Barkod { get; set; }
        public string HamKod { get; set; }
        public string HamFasonKod { get; set; }
        public string TakipNo { get; set; }
        public string PartiNo { get; set; }
        public string BoyaKod { get; set; }
        public int VaryantId { get; set; }
        public decimal Fiyat { get; set; }
        public bool Organik { get; set; }
        public int DesenId { get; set; }
        public int BoyaIslemId { get; set; }
    }
}
