using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Helpers
{
    public static class SemiColonHelper
    {
        public static string RemoveSemiColon(string metin)
        {
            string veri = metin.Split(':')[0];
            return veri;
        }
    }
}
