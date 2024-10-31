using DevExpress.XtraEditors;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraExport.Helpers;
using System.Text.RegularExpressions;

namespace Hesap.Utils
{
    public class HesaplaVeYansit
    {
        public void DogrudanYansit(TextBox alinan, TextBox hedef)
        {
            alinan.TextChanged += (s, e) => hedef.Text = alinan.Text;
        }
        public void GramajToplamYansit(TextBox t1, TextBox t2, TextBox t3, TextBox t4, TextBox t5, TextBox t6, TextBox t7)
        {
            void UpdateTotal(object sender, EventArgs e)
            {
                double sum = 0;
                TextBox[] textBoxes = { t1, t2, t3, t4, t5, t6 };

                foreach (var tb in textBoxes)
                {
                    if (double.TryParse(tb.Text, out double value))
                    {
                        sum += value;
                    }
                }

                t7.Text = sum.ToString();
            }

            // Subscribe to the TextChanged event for each TextBox
            t1.TextChanged += UpdateTotal;
            t2.TextChanged += UpdateTotal;
            t3.TextChanged += UpdateTotal;
            t4.TextChanged += UpdateTotal;
            t5.TextChanged += UpdateTotal;
            t6.TextChanged += UpdateTotal;
        }
        public void TextboxAndTextboxToTextbox(TextBox txtCozgu, TextBox txtBolen, TextBox lblSonuc, string Islem)
        {
            if (Islem == "Bolme")
            {
                txtCozgu.TextChanged += (s, e) => IkiSayiyiBolveYansit(txtCozgu, txtBolen, lblSonuc);
                txtBolen.TextChanged += (s, e) => IkiSayiyiBolveYansit(txtCozgu, txtBolen, lblSonuc);
            }
            else if (Islem == "Carpma")
            {
                txtCozgu.TextChanged += (s, e) => IkiSayiyiCarpVeYansit(txtCozgu, txtBolen, lblSonuc);
                txtBolen.TextChanged += (s, e) => IkiSayiyiCarpVeYansit(txtCozgu, txtBolen, lblSonuc);
            }
            else if (Islem == "BolCarp")
            {
                txtCozgu.TextChanged += (s, e) => IkiSayiyiBolve100leCarp(txtCozgu, txtBolen, lblSonuc);
                txtBolen.TextChanged += (s, e) => IkiSayiyiBolve100leCarp(txtCozgu, txtBolen, lblSonuc);
            }
            else if (Islem == "Topla")
            {
                txtCozgu.TextChanged += (s, e) => IkiSayiyiToplaveYansit(txtCozgu, txtBolen, lblSonuc);
                txtBolen.TextChanged += (s, e) => IkiSayiyiToplaveYansit(txtCozgu, txtBolen, lblSonuc);
            }
        }
        public void TextboxAndTextboxAndTextboxToTextbox(TextBox t1, TextBox t2, TextBox t3, TextBox t4)
        {
            t1.TextChanged += (s, e) => IplikMaliyet(t1, t2, t3, t4);
            t2.TextChanged += (s, e) => IplikMaliyet(t1, t2, t3, t4);
            t3.TextChanged += (s, e) => IplikMaliyet(t1, t2, t3, t4);
        }
        public void EightTextbox(TextBox t1, TextBox t2, TextBox t3, TextBox t4, TextBox t5, TextBox t6, TextBox t7, TextBox t8, TextBox lblSonuc)
        {
            t1.TextChanged += (s, e) => DokumaMaliyet(t1, t2, t3, t4, t5, t6, t7, t8, lblSonuc);
            t2.TextChanged += (s, e) => DokumaMaliyet(t1, t2, t3, t4, t5, t6, t7, t8, lblSonuc);
            t3.TextChanged += (s, e) => DokumaMaliyet(t1, t2, t3, t4, t5, t6, t7, t8, lblSonuc);
            t4.TextChanged += (s, e) => DokumaMaliyet(t1, t2, t3, t4, t5, t6, t7, t8, lblSonuc);
            t5.TextChanged += (s, e) => DokumaMaliyet(t1, t2, t3, t4, t5, t6, t7, t8, lblSonuc);
            t6.TextChanged += (s, e) => DokumaMaliyet(t1, t2, t3, t4, t5, t6, t7, t8, lblSonuc);
            t7.TextChanged += (s, e) => DokumaMaliyet(t1, t2, t3, t4, t5, t6, t7, t8, lblSonuc);
            t8.TextChanged += (s, e) => DokumaMaliyet(t1, t2, t3, t4, t5, t6, t7, t8, lblSonuc);
        }
        public void CozguMaliyetHesapla(TextBox t1, TextBox t2, TextBox lblSonuc)
        {
            t1.TextChanged += (s, e) => CozguMaliyet(t1, t2, lblSonuc);
            t2.TextChanged += (s, e) => CozguMaliyet(t1, t2, lblSonuc);
        }
        public void TextboxAndNumToTextbox(TextBox textbox, double sayi, TextBox lblSonuc)
        {
            textbox.TextChanged += (s, e) => SabitSayiIleBolVeYansit(textbox, sayi, lblSonuc);
        }
        public void Cozgu1UHYansit(TextBox textBox, TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4)
        {
            textBox.TextChanged += (s, e) => Cozgu1UH(textBox, textBox1, textBox2, textBox3, textBox4);
            textBox1.TextChanged += (s, e) => Cozgu1UH(textBox, textBox1, textBox2, textBox3, textBox4);
            textBox2.TextChanged += (s, e) => Cozgu1UH(textBox, textBox1, textBox2, textBox3, textBox4);
            textBox3.TextChanged += (s, e) => Cozgu1UH(textBox, textBox1, textBox2, textBox3, textBox4);
        }
        public void Cozgu2UHYansit(TextBox hamboy, TextBox cozgu2tel, TextBox cozgu2sonuc, TextBox yazdirilacakalan)
        {
            hamboy.TextChanged += (s, e) => Cozgu2UH(hamboy, cozgu2tel, cozgu2sonuc, yazdirilacakalan);
            cozgu2tel.TextChanged += (s, e) => Cozgu2UH(hamboy, cozgu2tel, cozgu2sonuc, yazdirilacakalan);
            cozgu2sonuc.TextChanged += (s, e) => Cozgu2UH(hamboy, cozgu2tel, cozgu2sonuc, yazdirilacakalan);
        }
        public void AtkiUHYansit(TextBox atki1siklik, TextBox taraken, TextBox ensacak, TextBox hamboy, TextBox atki1sonuc, TextBox yazdirilacakalan)
        {
            atki1siklik.TextChanged += (s, e) => AtkiUH(atki1siklik, taraken, ensacak, hamboy, atki1sonuc, yazdirilacakalan);
            taraken.TextChanged += (s, e) => AtkiUH(atki1siklik, taraken, ensacak, hamboy, atki1sonuc, yazdirilacakalan);
            ensacak.TextChanged += (s, e) => AtkiUH(atki1siklik, taraken, ensacak, hamboy, atki1sonuc, yazdirilacakalan);
            hamboy.TextChanged += (s, e) => AtkiUH(atki1siklik, taraken, ensacak, hamboy, atki1sonuc, yazdirilacakalan);
            atki1sonuc.TextChanged += (s, e) => AtkiUH(atki1siklik, taraken, ensacak, hamboy, atki1sonuc, yazdirilacakalan);
        }
        public void Sum3Texbox(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            t1.TextChanged += (s, e) => UretimMaliyetiToplam(t1, t2, t3, lblSonuc);
            t2.TextChanged += (s, e) => UretimMaliyetiToplam(t1, t2, t3, lblSonuc);
            t3.TextChanged += (s, e) => UretimMaliyetiToplam(t1, t2, t3, lblSonuc);
        }
        public void FireliUretimMaliyetiToplami(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            t1.TextChanged += (s, e) => UretimMaliyetiFireliToplam(t1, t2, t3, lblSonuc);
            t2.TextChanged += (s, e) => UretimMaliyetiFireliToplam(t1, t2, t3, lblSonuc);
            t3.TextChanged += (s, e) => UretimMaliyetiFireliToplam(t1, t2, t3, lblSonuc);
        }
        public void BoyanmisKumas(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            t1.TextChanged += (s, e) => BoyanmisKumasHesaplama(t1, t2, t3, lblSonuc);
            t2.TextChanged += (s, e) => BoyanmisKumasHesaplama(t1, t2, t3, lblSonuc);
            t3.TextChanged += (s, e) => BoyanmisKumasHesaplama(t1, t2, t3, lblSonuc);
        }
        public void FireliYikamaMaliyetiToplami(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            t1.TextChanged += (s, e) => FireliYikamaMaliyeti(t1, t2, t3, lblSonuc);
            t2.TextChanged += (s, e) => FireliYikamaMaliyeti(t1, t2, t3, lblSonuc);
            t3.TextChanged += (s, e) => FireliYikamaMaliyeti(t1, t2, t3, lblSonuc);
        }
        void IkiSayiyiBolveYansit(TextBox txtCozgu, TextBox sayi, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;
            if (decimal.TryParse(txtCozgu.Text, out decimal cozgulValue) && decimal.TryParse(sayi.Text, out decimal bolenValue))
            {
                if (bolenValue != 0)
                {
                    var sonuc = Math.Round(cozgulValue / bolenValue, 3);
                    lblSonuc.Text = sonuc.ToString();
                }
                else
                {
                    lblSonuc.Text = "Bölen sıfır olamaz.";
                }
            }
            else
            {
                // lblSonuc.Text = "Geçerli bir sayı giriniz.";
            }
        }
        void IkiSayiyiToplaveYansit(TextBox txtCozgu, TextBox sayi, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;
            if (decimal.TryParse(txtCozgu.Text, out decimal cozgulValue) && decimal.TryParse(sayi.Text, out decimal bolenValue))
            {
                if (bolenValue != 0)
                {
                    var sonuc = Math.Round(cozgulValue + bolenValue, 3);
                    lblSonuc.Text = sonuc.ToString();
                }
            }
            else
            {
                // lblSonuc.Text = "Geçerli bir sayı giriniz.";
            }
        }
        void IkiSayiyiCarpVeYansit(TextBox txtCozgu, TextBox sayi, TextBox lblSonuc)
        {
            if (decimal.TryParse(txtCozgu.Text, out decimal cozgulValue) && decimal.TryParse(sayi.Text, out decimal bolenValue))
            {
                if (bolenValue != 0)
                {
                    var sonuc = cozgulValue * bolenValue;
                    lblSonuc.Text = sonuc.ToString();
                }
                else
                {
                    //lblSonuc.Text = "Bölen sıfır olamaz.";
                }
            }
            else
            {
                //lblSonuc.Text = "Geçerli bir sayı giriniz.";
            }
        }
        void IkiSayiyiBolve100leCarp(TextBox txtCozgu, TextBox sayi, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;
            if (decimal.TryParse(txtCozgu.Text, out decimal cozgulValue) && decimal.TryParse(sayi.Text, out decimal bolenValue))
            {
                if (bolenValue != 0)
                {
                    var sonuc = Math.Round(cozgulValue / bolenValue * 100, 3);
                    lblSonuc.Text = sonuc.ToString("0.000");
                }
                else
                {
                    lblSonuc.Text = "Bölen sıfır olamaz.";
                }
            }
            else
            {
                // lblSonuc.Text = "Geçerli bir sayı giriniz.";
            }
        }
        void SabitSayiIleBolVeYansit(TextBox txtCozgu, double sayi, TextBox lblSonuc)
        {
            if (decimal.TryParse(txtCozgu.Text, out decimal cozgulValue))
            {
                decimal carpmaValue = (decimal)sayi; // float'tan decimal'a dönüştürme

                var sonuc = cozgulValue / carpmaValue;
                lblSonuc.Text = Math.Round(sonuc).ToString(); // İki ondalıklı formatta göster
            }
            else
            {
                lblSonuc.Text = "Geçerli bir sayı giriniz.";
            }
        }
        void Cozgu1UH(TextBox hamboy, TextBox boysacak, TextBox cozgu1telsayisi, TextBox cozgu1sonuc, TextBox yazdirilacakalan)
        {
            if (float.TryParse(hamboy.Text, out float hamboyval)
                && float.TryParse(boysacak.Text, out float boysacakval)
                && float.TryParse(cozgu1telsayisi.Text, out float cozgu1telsayisival)
                && float.TryParse(cozgu1sonuc.Text, out float cozgu1sonucval)

                )
            {
                var sonuc = Math.Round(((hamboyval + boysacakval) * cozgu1telsayisival * (60 / cozgu1sonucval) / 10000000), 3); //=((B16+B17)*G13*(60/E6)/10000000)
                yazdirilacakalan.Text = sonuc.ToString("0.000");
            }
        }
        void Cozgu2UH(TextBox hamboy, TextBox cozgu2tel, TextBox cozgu2sonuc, TextBox yazdirilacakalan)
        {
            if (float.TryParse(hamboy.Text, out float hamboyval)
                && float.TryParse(cozgu2tel.Text, out float cozgu2telval)
                && float.TryParse(cozgu2sonuc.Text, out float cozgu2sonucval)

                )
            {
                var sonuc = Math.Round((hamboyval * cozgu2telval * (60 / cozgu2sonucval) / 10000000), 3); //=(B16*G14*(60/E7)/10000000)
                yazdirilacakalan.Text = sonuc.ToString("0.000");
            }
        }
        void AtkiUH(TextBox atki1siklik, TextBox taraken, TextBox ensacak, TextBox hamboy, TextBox atki1sonuc, TextBox yazdirilacakalan)
        {

            if (float.TryParse(atki1siklik.Text, out float atki1siklikval)
            && float.TryParse(taraken.Text, out float tarakenval)
            && float.TryParse(ensacak.Text, out float ensacakval)
            && float.TryParse(hamboy.Text, out float hamboyval)
            && float.TryParse(atki1sonuc.Text, out float atki1sonucval)

            )
            {
                var sonuc = Math.Round((atki1siklikval * ((tarakenval + ensacakval) / 100) * (hamboyval / 100) * (60 / atki1sonucval)) / 1000, 3); // = (G8 * ((B15 + B18) / 100) * (B16 / 100) * (60 / E8)) / 1000
                yazdirilacakalan.Text = sonuc.ToString("0.000");
            }
        }
        void IplikMaliyet(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;

            if (float.TryParse(t1.Text, out float t1val) &&
                float.TryParse(t2.Text, out float t2val) &&
                float.TryParse(t3.Text, out float t3val))
            {
                var sonuc = Math.Round((t1val + t2val) * t3val, 3);
                lblSonuc.Text = sonuc.ToString("0.000");

            }

        }
        void DokumaMaliyet(TextBox t1, TextBox t2, TextBox t3, TextBox t4, TextBox t5, TextBox t6, TextBox t7, TextBox t8, TextBox lblSonuc)
        {
            t7.Text = t7.Text.Replace("₺", "").Trim();
            lblSonuc.Text = string.Empty;
            if (float.TryParse(t1.Text, out float t1val) &&
                float.TryParse(t2.Text, out float t2val) &&
                float.TryParse(t3.Text, out float t3val) &&
                float.TryParse(t4.Text, out float t4val) &&
                float.TryParse(t5.Text, out float t5val) &&
                float.TryParse(t6.Text, out float t6val) &&
                float.TryParse(t7.Text, out float t7val) &&
                float.TryParse(t8.Text, out float t8val)

                )
            {
                var sonuc = Math.Round((((t1val + t2val) / 100) * (t3val + t4val + t5val + t6val) * t7val) / t8val, 3); //=(((B16+B17)/100)*(G8+G9+G10+G11)*Q6)/Q20
                lblSonuc.Text = sonuc.ToString("0.000");
            }
        }
        void CozguMaliyet(TextBox t1, TextBox t2, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;
            t2.Text = t2.Text.Replace("₺", "").Trim();
            if (float.TryParse(t1.Text, out float t1val) &&
               float.TryParse(t2.Text, out float t2val)

               )
            {
                var sonuc = Math.Round((t1val / 100) * t2val, 3); //=(B16/100)*Q7
                lblSonuc.Text = sonuc.ToString("0.000");
            }
        }
        void UretimMaliyetiToplam(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;
            if (float.TryParse(t1.Text, out float t1val) &&
               float.TryParse(t2.Text, out float t2val) &&
               float.TryParse(t3.Text, out float t3val)

               )
            {
                var sonuc = Math.Round(t1val + t2val + t3val, 3); //=T6+T7+T8
                lblSonuc.Text = sonuc.ToString("0.000");
            }
        }
        void UretimMaliyetiFireliToplam(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;
            if (float.TryParse(t1.Text, out float t1val) &&
               float.TryParse(t2.Text, out float t2val) &&
               float.TryParse(t3.Text, out float t3val)

               )
            {
                float t2valAsPercentage = t2val / 100.0f;
                var sonuc = Math.Round((t1val * t2valAsPercentage) + t3val, 3); //=T6+T7+T8
                lblSonuc.Text = sonuc.ToString("0.00");
            }
        }
        void FireliYikamaMaliyeti(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;
            if (float.TryParse(t1.Text, out float t1val) &&
               float.TryParse(t2.Text, out float t2val) &&
               float.TryParse(t3.Text, out float t3val)

               )
            {
                float t2valAsPercentage = t3val / 100.0f;
                var sonuc = Math.Round(((t1val + t2val) * t2valAsPercentage) + (t1val + t2val), 3); //=((T15+T16)*Q11)+(T15+T16)
                lblSonuc.Text = sonuc.ToString("0.00");
            }
        }
        void BoyanmisKumasHesaplama(TextBox t1, TextBox t2, TextBox t3, TextBox lblSonuc)
        {
            lblSonuc.Text = string.Empty;
            if (float.TryParse(t1.Text, out float t1val) &&
               float.TryParse(t2.Text, out float t2val) &&
               float.TryParse(t3.Text, out float t3val)

               )
            {
                float t2valAsPercentage = t2val / 100.0f;
                var sonuc = Math.Round((t1val * t2val) + t3val, 3); //=T6+T7+T8
                lblSonuc.Text = sonuc.ToString("0.00");
            }
        }
        public void FirmaKoduVeAdiYansit(TextEdit fk, TextEdit fa, ref int FirmaId)
        {
            Forms.Liste.FrmFirmaKartiListesi frm = new Forms.Liste.FrmFirmaKartiListesi();
            frm.ShowDialog();
            if (frm.FirmaKodu != null)
            {
                fk.Text = frm.FirmaKodu;
                fa.Text = frm.FirmaUnvan;
                FirmaId = frm.Id;
            }

        }
        public void DepoKoduVeAdiYansit(TextEdit dk, Label da)
        {
            Forms.Liste.FrmDepoKartiListesi frm = new Forms.Liste.FrmDepoKartiListesi();
            frm.ShowDialog();
            if (frm.Kodu != null)
            {
                dk.Text = frm.Kodu;
                da.Text = frm.Adi;
            }


        }
        public List<Dictionary<string, object>> KartaYansit(object sender)
        {
            GridView gridView = sender as GridView;
            if (gridView == null)
                return new List<Dictionary<string, object>>(); // Hata durumunda boş bir liste dön

            int secilenId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            var veriler = new List<Dictionary<string, object>>(); // Burada yeni bir liste tanımlıyoruz

            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                int id = Convert.ToInt32(gridView.GetRowCellValue(i, "Id"));

                if (id == secilenId)
                {
                    var rowData = new Dictionary<string, object>();
                    foreach (GridColumn column in gridView.Columns)
                    {
                        var columnName = column.FieldName;
                        var cellValue = gridView.GetRowCellValue(i, columnName);
                        rowData[columnName] = cellValue;
                    }
                    veriler.Add(rowData);
                }
            }

            return veriler; // Verileri döndür
        }
        public void SayiyaNoktaKoy(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e, string fieldName)
        {
            {
                if (e.Column.FieldName == fieldName && e.Value != null)
                {
                    e.DisplayText = string.Format("{0:N4}", e.Value);
                }
            }
        }
        public void TasiyiciBilgileriYansit(TextEdit tu,TextEdit ta,TextEdit ts,TextEdit tt, TextEdit tp,TextEdit td,TextEdit ti,ref int Id)
        {
            Forms.Liste.FrmTasiyiciKartiListesi frm = new Forms.Liste.FrmTasiyiciKartiListesi();
            frm.ShowDialog();
            if (frm.Unvan != null)
            {
                tu.Text = frm.Unvan;
                ta.Text = frm.Ad;
                ts.Text = frm.Soyad;
                tt.Text = frm.TC;
                tp.Text = frm.Plaka;
                td.Text = frm.Dorse;
                ti.Text = frm.Id.ToString();
                Id = frm.Id;
            }
            
        }
        public void KumasBilgileriYansit(DevExpress.XtraGrid.Views.Grid.GridView gridView, int focusedRowHandle)
        {
            Forms.Liste.FrmUrunKartiListesi frm = new Forms.Liste.FrmUrunKartiListesi();
            frm.ShowDialog();

            if (frm.Id != 0)
            {
                gridView.SetRowCellValue(focusedRowHandle, "KumasId", Convert.ToInt32(frm.Id));
                gridView.SetRowCellValue(focusedRowHandle, "KumasKodu", frm.UrunKodu);
                gridView.SetRowCellValue(focusedRowHandle, "KumasAdi", frm.UrunAdi);
            }
        }
        public void BoyahaneRenkBilgileriYansit(GridView gridView, int focusedRowHandle)
        {
            Forms.Liste.FrmBoyahaneRenkKartlariListesi frm = new Forms.Liste.FrmBoyahaneRenkKartlariListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                int newRowHandle = gridView.FocusedRowHandle;
                gridView.SetRowCellValue(newRowHandle, "RenkId", Convert.ToInt32(frm.veriler[0]["Id"]));
                gridView.SetRowCellValue(newRowHandle, "BoyahaneRenkKodu", frm.veriler[0]["BoyahaneRenkKodu"].ToString());
                gridView.SetRowCellValue(newRowHandle, "BoyahaneRenkAdi", frm.veriler[0]["BoyahaneRenkAdi"].ToString());
            }
        }
        public string[] SorgudakiKolonIsimleriniAl(string sql)
        {
            var columnNames = new List<string>();
            var regex = new Regex(@"(?:AS\s+\[([^\]]+)\]|(?<=\s)\[([^\]]+)\])", RegexOptions.IgnoreCase);

            var matches = regex.Matches(sql);
            foreach (Match match in matches)
            {
                if (match.Groups[1].Success)
                    columnNames.Add(match.Groups[1].Value);
                else if (match.Groups[2].Success)
                    columnNames.Add(match.Groups[2].Value);
            }

            return columnNames.ToArray();
        }

    }
}
