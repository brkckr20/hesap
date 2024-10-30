using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Context
{
    public class _UrunReceteUB
    {
        public int Id { get; set; }
        public int RefNo { get; set; }
        public string KalemIslem { get; set; }
        public string IplikKodu { get; set; }
        public string IplikAdi { get; set; }
        public float Miktar { get; set; }
        public string Birim { get; set; }
        public float BirimFiyat { get; set; }
        public string Doviz { get; set; }
        public string DovizFiyat { get; set; }
    }
}
