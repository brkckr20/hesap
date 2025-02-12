using DevExpress.XtraEditors;
using FastReport.Data;
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
                    case "CozguMaliyet":
                        sonuc = (deger1 / 100) * deger2;
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

        public static void CalculateGrammageCozgu1(TextBox tbTarakBoy, TextBox tbBoySacak, TextBox tbCozgu1TelSayisi, TextBox tbCozgu1, TextBox hedef)
        {
            if (double.TryParse(tbTarakBoy.Text, out double b16) &&
                double.TryParse(tbBoySacak.Text, out double b17) &&
                double.TryParse(tbCozgu1TelSayisi.Text, out double g13) &&
                double.TryParse(tbCozgu1.Text, out double e6) &&
                e6 != 0)
            {
                double sonuc = ((b16 + b17) * g13 * (60 / e6)) / 10000000;
                hedef.Text = sonuc.ToString("0.###");
            }
        }
        public static void CalculateGrammageCozgu2(TextBox tbHamBoy, TextBox tbCozgu2TelSayisi, TextBox tbCozgu2, TextBox hedef)
        {
            if (double.TryParse(tbHamBoy.Text, out double b16) &&
                double.TryParse(tbCozgu2TelSayisi.Text, out double g14) &&
                double.TryParse(tbCozgu2.Text, out double e7) &&
                e7 != 0) // Bölme işleminde sıfıra bölmeyi önlemek için
            {
                double sonuc = (b16 * g14 * (60 / e7)) / 10000000;
                hedef.Text = sonuc.ToString("0.###"); // Sonucu en yakın tam sayıya yuvarlama
            }
        }
        public static void CalculateGrammageAtki(TextBox tbAtki1Siklik, TextBox tbTarakEn, TextBox tbEnSacak, TextBox tbHamBoy, TextBox tbAtki1Uretim, TextBox hedef)
        {
            if (double.TryParse(tbAtki1Siklik.Text, out double g8) &&
                double.TryParse(tbTarakEn.Text, out double b15) &&
                double.TryParse(tbEnSacak.Text, out double b18) &&
                double.TryParse(tbHamBoy.Text, out double b16) &&
                double.TryParse(tbAtki1Uretim.Text, out double e8) &&
                e8 != 0) // Bölme işleminde sıfıra bölmeyi önlemek için
            {
                double sonuc = (g8 * ((b15 + b18) / 100) * (b16 / 100) * (60 / e8)) / 1000;
                hedef.Text = sonuc.ToString("0.###"); // Sonucu en yakın tam sayıya yuvarlama
            }
        }

        public static void CalculateGrammageSum(TextBox tbCozgu1, TextBox tbCozgu2, TextBox tbAtki1, TextBox tbAtki2, TextBox tbAtki3, TextBox tbAtki4, TextBox hedef)
        {
            if (double.TryParse(tbCozgu1.Text, out double j6) &&
                double.TryParse(tbCozgu2.Text, out double j7) &&
                double.TryParse(tbAtki1.Text, out double j8) &&
                double.TryParse(tbAtki2.Text, out double j9) &&
                double.TryParse(tbAtki3.Text, out double j10) &&
                double.TryParse(tbAtki4.Text, out double j11))
            {
                double sonuc = j6 + j7 + j8 + j9 + j10 + j11;
                hedef.Text = sonuc.ToString("0.###"); // Sonucu en yakın tam sayıya yuvarlama
            }
        }
        public static void CalculateYarnGrammage(TextBox tb1, TextBox tb2, TextBox hedef)
        {
            if (double.TryParse(tb1.Text, out double j6) &&
                double.TryParse(tb2.Text, out double j7))
            {
                double sonuc = j6 / j7 * 100;
                hedef.Text = sonuc.ToString("0.###");
            }
        }
        public static void CalculateCostYarnAmount(TextBox tbCozgu1, TextBox tbCozgu2, TextBox tbAtki1, TextBox tbAtki2, TextBox tbAtki3, TextBox tbAtki4, TextBox hedef)
        {
            if (double.TryParse(tbCozgu1.Text, out double j6) &&
                double.TryParse(tbCozgu2.Text, out double j7) &&
                double.TryParse(tbAtki1.Text, out double j8) &&
                double.TryParse(tbAtki2.Text, out double j9) &&
                double.TryParse(tbAtki3.Text, out double j10) &&
                double.TryParse(tbAtki4.Text, out double j11))
            {
                double sonuc = j6 + j7 + j8 + j9 + j10 + j11;
                hedef.Text = sonuc.ToString("0.###");
            }
        }
        public static void CalculateYarnAmount(TextBox tb1, TextBox tb2, TextBox tb3, TextBox hedef)
        {
            if (double.TryParse(tb1.Text, out double j6) &&
                double.TryParse(tb2.Text, out double j7) &&
                double.TryParse(tb3.Text, out double j8))
            {
                double sonuc = (j6 + j7) * j8;
                hedef.Text = sonuc.ToString("0.##");
            }
        }
        public static void CalculateCostWeaving(TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, TextBox tb6, TextBox tb7, TextBox tb8, TextBox hedef)
        {
            if (double.TryParse(tb1.Text, out double j1) &&
                double.TryParse(tb2.Text, out double j2) &&
                double.TryParse(tb3.Text, out double j3) &&
                double.TryParse(tb4.Text, out double j4) &&
                double.TryParse(tb5.Text, out double j5) &&
                double.TryParse(tb6.Text, out double j6) &&
                double.TryParse(tb7.Text, out double j7) &&
                double.TryParse(tb8.Text, out double j8))
            {
                double sonuc = (((j1 + j2) / 100) * (j3 + j4 + j5 + j6) * j7) / j8;
                hedef.Text = sonuc.ToString("0.##");
            }
        }
        public static void CalculateCostProductionSum(TextBox tb1, TextBox tb2, TextBox tb3, TextBox hedef)
        {
            if (double.TryParse(tb1.Text, out double j1) &&
                double.TryParse(tb2.Text, out double j2) &&
                double.TryParse(tb3.Text, out double j3))
            {
                double sonuc = j1 + j2 + j3;
                hedef.Text = sonuc.ToString("0.##");
            }
        }
        public static void CalculateCostProductionSumWithWastage(TextBox tb1, TextBox tb2, TextBox tb3, TextBox hedef) // fireli üretim maliyeti - 
        {
            if (double.TryParse(tb1.Text, out double j1) &&
                double.TryParse(tb2.Text, out double j2) &&
                double.TryParse(tb3.Text, out double j3))
            {
                double sonuc = (j1 * (j2 / 100)) + j3;
                hedef.Text = sonuc.ToString("0.##");
            }
        }
        public static void CalculatePaintedFabric(TextBox tb1, TextBox tb2, TextBox tb3, TextBox hedef)
        {
            if (double.TryParse(tb1.Text, out double j1) &&
                double.TryParse(tb2.Text, out double j2) &&
                double.TryParse(tb3.Text, out double j3))
            {
                var sonuc = Math.Round((j1 * j2) + j3, 3); //=T6+T7+T8
                hedef.Text = sonuc.ToString("0.##");
            }
        }
        public static void CalculatePaintedWastage(TextBox tb1, TextBox tb2, TextBox tb3, TextBox hedef) // yıkama ve boyahane maliyet - fireli
        {
            if (double.TryParse(tb1.Text, out double j1) &&
                double.TryParse(tb2.Text, out double j2) &&
                double.TryParse(tb3.Text, out double j3))
            {
                var sonuc = ((j1 + j2) * (j3 / 100)) + (j1 + j2); //=((T15+T16)*Q11)+(T15+T16)
                hedef.Text = sonuc.ToString("0.##");
            }
        }
        public static void CalculatePaintedBeneficial(TextBox tb1, TextBox tb2, TextBox hedef) // yıkama ve boyahane maliyet - Kârlı - ayrıca dikilmiş ürün 2.kalite maliyeti hesaplamada da kullanıldı
        {
            if (double.TryParse(tb1.Text, out double j1) &&
                double.TryParse(tb2.Text, out double j2))
            {
                var sonuc = (j1 * (j2/100)) + j1; //=(T17*Q14)+T17
                hedef.Text = sonuc.ToString("0.##");
            }
        }
        //dikilmiş ürün - karlıdan devam edilecektir.
    }
}

