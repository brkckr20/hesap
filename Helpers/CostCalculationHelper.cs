using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Helpers
{
    public static class CostCalculationHelper
    {
        public static void DirectReflection(TextBox kaynak, TextBox hedef)
        {
            hedef.Text = kaynak.Text;
        }
        public static void TextboxAndTextboxToTextbox(TextBox txt1, TextBox txt2, TextBox hedef, string islem)
        {
            if (double.TryParse(txt1.Text, out double deger1) && double.TryParse(txt2.Text, out double deger2))
            {
                double sonuc = 0;
                switch (islem)
                {
                    case "Bolme":
                        sonuc = deger1 / deger2;
                        break;
                    case "Carpma":
                        sonuc = deger1 * deger2;
                        break;
                    case "Topla":
                        sonuc = deger1 + deger2;
                        break;
                    case "BolCarp":
                        sonuc = (deger1 / deger2) * deger2;
                        break;
                    default:
                        sonuc = 0;
                        break;
                }
                hedef.Text = sonuc.ToString("0.##");
            }
        }
        public static void TextboxAndNumToTextbox(TextBox txt, double sayi, TextBox hedef)
        {
            if (double.TryParse(txt.Text, out double deger))
            {
                hedef.Text = (deger * sayi).ToString("0.##");
            }
        }

        public static void CalculateWithStaticNumber(TextBox kaynak, TextBox hedef)
        {
            if (double.TryParse(kaynak.Text, out double deger))
            {
                double sonuc = Math.Floor(deger / 1.05);
                hedef.Text = sonuc.ToString("0.##");
            }
        }
    }
}
