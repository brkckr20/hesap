using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Import.Doc;
using FastReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo.FrmIplikCikis;

namespace Hesap.Utils
{
    public class YardimciAraclar
    {
        private readonly string _forex;
        private readonly TextBox[] _textBoxes;

        public YardimciAraclar(string forex = "$", params TextBox[] textBoxes)
        {
            _forex = forex;
            _textBoxes = textBoxes;

            foreach (var textBox in _textBoxes)
            {
                textBox.Enter += TextBox_Enter;
                textBox.Leave += TextBox_Leave;
            }
        }
        private void HandleTextBoxFocus(TextBox textBox)
        {
            // Odağa girildiğinde, sadece sayısal kısmı gösterir, döviz sembolünü kaldırır
            if (textBox.Text.StartsWith(_forex))
            {
                textBox.Text = textBox.Text.Substring(_forex.Length);
            }
        }
        private void HandleTextBoxLeave(TextBox textBox)
        {
            // Odağı kaybettiğinde, metni döviz sembolü ile formatlar
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Eğer metin döviz sembolü içeriyorsa, işlemi yapma
                if (!textBox.Text.StartsWith(_forex))
                {
                    textBox.Text = _forex + textBox.Text;
                }
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }
        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                HandleTextBoxFocus(textBox);
            }
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                HandleTextBoxLeave(textBox);
            }
        }
        public decimal KurBilgisiniYansit(string kur)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                DateTime date = DateTime.Now.AddDays(-1).Date;
                var sorgu = $"select top 1 {kur} from Kur where TARIH = @Date order by TARIH desc";
                var rate = connection.QuerySingleOrDefault<decimal>(sorgu, new { Date = date });
                if (rate != null)
                {
                    return rate;
                }
                return 0;

            }
        }
        public string GetStringValue(object value)
        {
            return value == null ? "" : value.ToString();
        }
        public double GetDoubleValue(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return 0;
            }

            double result;
            if (double.TryParse(value.ToString(), out result))
            {
                return result;
            }

            return 0;
        }
        public decimal GetDecimalValue(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return 0;
            }

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
            {
                return result;
            }

            return 0;
        }
        public DateTime GetDateValue(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return default(DateTime); // Varsayılan tarih değeri (01/01/0001 00:00:00)
            }

            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result))
            {
                return result;
            }

            return default(DateTime); // Varsayılan tarih değeri (01/01/0001 00:00:00)
        }

        public void ListedenGrideYansit(GridControl gridControl, string[] columnNames, List<Dictionary<string, object>> data)
        {
            DataTable dt = new DataTable();
            foreach (string columnName in columnNames)
            {
                dt.Columns.Add(columnName);
            }
            foreach (var item in data)
            {
                DataRow row = dt.NewRow();
                foreach (string columnName in columnNames)
                {
                    if (item.ContainsKey(columnName))
                    {
                        var value = item[columnName];
                        row[columnName] = value == null || value == DBNull.Value ? string.Empty : value.ToString();
                    }
                    else
                    {
                        row[columnName] = string.Empty;
                    }
                }
                dt.Rows.Add(row);
            }
            gridControl.DataSource = dt;
        }

        public void FisVeHavuzuTemizle(object[] controls, GridControl gridControl)
        {
            KartTemizle(controls);
            gridControl.DataSource = null;
        }
        public void KartTemizle(object[] controls)
        {
            foreach (var control in controls)
            {
                if (control is DateEdit)
                {
                    DateEdit dateEdit = (DateEdit)control;
                    dateEdit.EditValue = DateTime.Now;
                }
                else if (control is TextEdit)
                {
                    TextEdit txtEdit = (TextEdit)control;
                    txtEdit.Text = string.Empty;
                }
                else if (control is ButtonEdit)
                {
                    ButtonEdit txtEdit = (ButtonEdit)control;
                    txtEdit.Text = string.Empty;
                }
                else if (control is MemoExEdit)
                {
                    MemoExEdit txtEdit = (MemoExEdit)control;
                    txtEdit.Text = string.Empty;
                }
                else if (control is LabelControl)
                {
                    LabelControl label = (LabelControl)control;
                    label.Text = string.Empty;
                }
                else if (control is Label)
                {
                    Label label = (Label)control;
                    label.Text = string.Empty;
                }
                else if (control is CheckEdit)
                {
                    CheckEdit check = (CheckEdit)control;
                    check.Checked = false;
                }
                else if (control is RadioButton)
                {
                    RadioButton radio = (RadioButton)control;
                    radio.Checked = false;
                }
                else if (control is ComboBoxEdit)
                {
                    ComboBoxEdit comboBox = (ComboBoxEdit)control;
                    comboBox.Text = string.Empty;
                }
            }
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public void ExcelOlarakAktar(GridControl grid, string dosyaAdi)
        {
            saveFileDialog.Filter = $"Excel Dosyası (*.xlsx)|*.xlsx";
            saveFileDialog.Title = $"Excel Dosyasını Kaydet";
            saveFileDialog.FileName = dosyaAdi;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;
                    grid.ExportToXlsx(filePath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Aktar sırasında hata :" + ex.Message);
                }
            }

        }

        public string TabloAdiniAl(string sorgu)
        {
            var match = System.Text.RegularExpressions.Regex.Match(sorgu, @"FROM\s+(\w+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : null;
        }
        public void Y1(object[] controls) // silme işlemi sonrasi 1 olarak yansıtmak için
        {
            foreach (var control in controls)
            {
                if (control is TextEdit)
                {
                    TextEdit txtEdit = (TextEdit)control;
                    txtEdit.Text = "1";
                }
                else if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Text = "1";
                }
            }
        }
        public void Y0(object[] controls) // silme işlemi sonrasi 0 olarak yansıtmak için
        {
            foreach (var control in controls)
            {
                if (control is TextEdit)
                {
                    TextEdit txtEdit = (TextEdit)control;
                    txtEdit.Text = "0";
                }
                else if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Text = "0";
                }
            }
        }
        public void ArkaPlaniDegistir(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e, string columnName)
        {
            if (e.Column.FieldName == columnName)
            {
                var cellValue = Convert.ToDouble(e.CellValue);

                if (cellValue < 2)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
            }
        }

        public void KolonDurumunuKaydet(GridView gridView, string EkranAdi)
        {
            /* using (var connection = new Baglanti().GetConnection())
             {
                 foreach (GridColumn kolon in gridView.Columns)
                 {
                     // Kolonun mevcut kaydını kontrol et
                     string checkQuery = "SELECT COUNT(*) FROM KolonDuzenleme WHERE KullaniciId = @KullaniciId AND EkranAdi = @EkranAdi AND KolonAdi = @KolonAdi";
                     var exists = connection.ExecuteScalar<int>(checkQuery, new
                     {
                         KullaniciId = Properties.Settings.Default.Id,
                         EkranAdi = EkranAdi,
                         KolonAdi = kolon.FieldName
                     });

                     if (exists > 0)
                     {
                         // Eğer kayıt varsa, güncelle
                         string updateQuery = @"
                     UPDATE KolonDuzenleme 
                     SET Genişlik = @Genişlik, Gizli = @Gizli, Konum = @Konum 
                     WHERE KullaniciId = @KullaniciId AND EkranAdi = @EkranAdi AND KolonAdi = @KolonAdi";

                         var updateParameters = new
                         {
                             KullaniciId = Properties.Settings.Default.Id,
                             EkranAdi = EkranAdi,
                             KolonAdi = kolon.FieldName,
                             Genişlik = kolon.Width,
                             Gizli = !kolon.Visible, // Gizli kolon için tersini alıyoruz
                             Konum = kolon.VisibleIndex
                         };

                         connection.Execute(updateQuery, updateParameters);
                     }
                     else
                     {
                         // Eğer kayıt yoksa, ekle
                         string insertQuery = @"
                     INSERT INTO KolonDuzenleme (KullaniciId, EkranAdi, KolonAdi, Genişlik, Gizli, Konum)
                     VALUES (@KullaniciId, @EkranAdi, @KolonAdi, @Genişlik, @Gizli, @Konum)";

                         var insertParameters = new
                         {
                             KullaniciId = Properties.Settings.Default.Id,
                             EkranAdi = EkranAdi,
                             KolonAdi = kolon.FieldName,
                             Genişlik = kolon.Width,
                             Gizli = !kolon.Visible, // Gizli kolon için tersini alıyoruz
                             Konum = kolon.VisibleIndex
                         };

                         connection.Execute(insertQuery, insertParameters);
                     }
                 }
             }*/
            using (var connection = new Baglanti().GetConnection())
            {
                foreach (GridColumn kolon in gridView.Columns)
                {
                    string checkQuery = "SELECT COUNT(*) FROM KolonDuzenleme WHERE KullaniciId = @KullaniciId AND EkranAdi = @EkranAdi AND KolonAdi = @KolonAdi";
                    var exists = connection.ExecuteScalar<int>(checkQuery, new
                    {
                        KullaniciId = Properties.Settings.Default.Id,
                        EkranAdi = EkranAdi,
                        KolonAdi = kolon.FieldName
                    });

                    if (exists > 0)
                    {
                        string updateQuery = @"
            UPDATE KolonDuzenleme 
            SET Genişlik = @Genişlik, Gizli = @Gizli, Konum = @Konum 
            WHERE KullaniciId = @KullaniciId AND EkranAdi = @EkranAdi AND KolonAdi = @KolonAdi";

                        var updateParameters = new
                        {
                            KullaniciId = Properties.Settings.Default.Id,
                            EkranAdi = EkranAdi,
                            KolonAdi = kolon.FieldName,
                            Genişlik = kolon.Width,
                            Gizli = !kolon.Visible,
                            Konum = kolon.VisibleIndex
                        };

                        connection.Execute(updateQuery, updateParameters);
                    }
                    else
                    {
                        string insertQuery = @"
            INSERT INTO KolonDuzenleme (KullaniciId, EkranAdi, KolonAdi, Genişlik, Gizli, Konum)
            VALUES (@KullaniciId, @EkranAdi, @KolonAdi, @Genişlik, @Gizli, @Konum)";

                        var insertParameters = new
                        {
                            KullaniciId = Properties.Settings.Default.Id,
                            EkranAdi = EkranAdi,
                            KolonAdi = kolon.FieldName,
                            Genişlik = kolon.Width,
                            Gizli = !kolon.Visible,
                            Konum = kolon.VisibleIndex
                        };

                        connection.Execute(insertQuery, insertParameters);
                    }
                }
            }

        }
        public class KolonBilgisi
        {
            public string KolonAdi { get; set; }
            public int Genişlik { get; set; }
            public bool Gizli { get; set; }
            public int Konum { get; set; }
        }
        public void KolonlariGetir(GridView gridView, string EkranAdi)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string selectQuery = "SELECT KolonAdi, Genişlik, Gizli, Konum FROM KolonDuzenleme WHERE KullaniciId = @KullaniciId AND EkranAdi = @EkranAdi ORDER BY Konum";
                var kolonlar = connection.Query<KolonBilgisi>(selectQuery, new
                {
                    KullaniciId = Properties.Settings.Default.Id,
                    EkranAdi = EkranAdi
                }).ToList();

                foreach (GridColumn kolon in gridView.Columns)
                {
                    var kolonBilgisi = kolonlar.FirstOrDefault(k => k.KolonAdi == kolon.FieldName);
                    if (kolonBilgisi != null)
                    {
                        kolon.Visible = !kolonBilgisi.Gizli;
                        kolon.Width = kolonBilgisi.Genişlik;
                    }
                    else
                    {
                        kolon.Visible = true;
                    }
                }
            }
        }
        public void KolonSecici(DevExpress.XtraGrid.GridControl grid)
        {
            var gridView = grid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView != null)
            {
                // Sütun Seçici'yi açın
                gridView.OptionsCustomization.AllowColumnMoving = true; // Sütunların taşınmasına izin ver
                gridView.OptionsCustomization.AllowFilter = true; // Filtrelemeye izin ver


                // Column Chooser'ı aç
                gridView.ShowCustomization();
            }
        }

        public string GetActiveUser()
        {
            return Properties.Settings.Default.KullaniciAdi.ToString();
        }
    }
}
