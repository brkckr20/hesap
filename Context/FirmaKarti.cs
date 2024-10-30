using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Context
{
    public class FirmaKarti
    {
        public int Id { get; set; }
        public string FirmaKodu { get; set; }
        public string FirmaUnvan { get; set; } 
        public string Adres1 { get; set; }
        public string Adres2 { get; set; }
        public string Adres3 { get; set; }
    }
}
