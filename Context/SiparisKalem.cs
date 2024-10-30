using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Context
{
    public class SiparisKalem
    {
        public int Id { get; set; }
        public int RefNo { get; set; }
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public string BirlesikKod { get; set; }
        public string Varyant { get; set; }
        public string Birim{ get; set; }
        public float PesinFiyat{ get; set; }
        public float VadeFiyat{ get; set; }
        public int Vade{ get; set; }
        public string VadeSuresi{ get; set; }
        public string UUID{ get; set; }
        public float Miktar { get; set; }
        public string ReceteNo { get; set; }
    }
}
