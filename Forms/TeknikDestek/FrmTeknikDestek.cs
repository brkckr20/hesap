using Dapper;
using DevExpress.XtraEditors;
using Hesap.Context;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.TeknikDestek
{
    public partial class FrmTeknikDestek : DevExpress.XtraEditors.XtraForm
    {
        public FrmTeknikDestek()
        {
            InitializeComponent();
        }
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();

        public int Id = 0;
        private void FrmTeknikDestek_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTalepTarihi.Properties.EditMask = "dd.MM.yyyy";
            dateTalepTarihi.Properties.Mask.UseMaskAsDisplayFormat = true;
            dateTalepTarihi.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            dateTalepTarihi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

            dateTamamlanmaTarihi.Properties.EditMask = "dd.MM.yyyy";
            dateTamamlanmaTarihi.Properties.Mask.UseMaskAsDisplayFormat = true;
            dateTamamlanmaTarihi.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            dateTamamlanmaTarihi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

            dateTalepTarihi.EditValue = DateTime.Now;
            dateTamamlanmaTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<_TaleplerGorusme>();

        }
        #region satir kayitlari
        private Dictionary<string, object> CreateKalemParameters(int rowIndex)
        {
            return new Dictionary<string, object>
            {
                { "SiraNo", Convert.ToInt32(gridView1.GetRowCellValue(rowIndex, "SiraNo")) },
                { "GorusmeNotu", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "GorusmeNotu"))},
                { "Not1", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Not1"))},
                { "Not2", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Not2"))},
                { "Not3", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Not3"))},
                { "DosyaEk", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "DosyaEk"))},
                { "RefNo", this.Id},
            };
        }
        #endregion
        private void btnKaydet_Click(object sender, EventArgs e)
        {

            var parameters = new Dictionary<string, object>
                            {
                                { "Tarih", dateTalepTarihi.EditValue },
                                { "Departman", txtDepartman.Text },
                                { "Baslik", txtBaslik.Text },
                                { "Aciklama", memoAciklama.Text },
                                { "Ek", txtDosyaEk.Text },
                                { "Durum", cmbDurum.Text },
                                { "TamamlanmaTarihi", dateTamamlanmaTarihi.Text },
                                { "Kullanici", txtKullanici.Text }
                            };

            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord("Talepler", parameters);
                bildirim.Basarili();
            }
            else
            {
                cRUD.UpdateRecord("Talepler", parameters, this.Id);
                bildirim.GuncellemeBasarili();
            }
        }


        private void txtDepartman_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmDepartmanListesi frm = new FrmDepartmanListesi();
            frm.ShowDialog();
            if (frm.Departman != null)
            {
                txtDepartman.Text = frm.Departman;
            }
        }

        private void txtDepartman_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmTalepListesi frm = new FrmTalepListesi();
            frm.ShowDialog();

            if (frm.veriler.Count > 0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                dateTalepTarihi.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                dateTamamlanmaTarihi.EditValue = (DateTime)frm.veriler[0]["TamamlanmaTarihi"];
                txtDepartman.Text = frm.veriler[0]["Departman"].ToString();
                txtBaslik.Text = frm.veriler[0]["Baslik"].ToString();
                memoAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                txtDosyaEk.Text = frm.veriler[0]["Ek"].ToString();
                cmbDurum.EditValue = frm.veriler[0]["Durum"].ToString();
                txtKullanici.Text = frm.veriler[0]["Kullanici"].ToString();
                string resimYolu = frm.veriler[0]["Ek"] as string;
                if (!string.IsNullOrEmpty(resimYolu) && File.Exists(resimYolu))
                {
                    pictureEdit1.Image = Image.FromFile(resimYolu);
                }
                else
                {
                    pictureEdit1.Image = null;
                }
            }
        }


        private void btnSil_Click(object sender, EventArgs e)
        {
            cRUD.KartSil(this.Id, "Talepler", true, pictureEdit1); // true resim varsa
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from Talepler where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from FirmaKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        dateTalepTarihi.EditValue = veri.Tarih.ToString();
                        dateTamamlanmaTarihi.EditValue = veri.TamamlanmaTarihi.ToString();
                        txtDepartman.Text = veri.Departman.ToString();
                        txtBaslik.Text = veri.Baslik.ToString();
                        memoAciklama.Text = veri.Aciklama.ToString();
                        txtDosyaEk.Text = veri.Ek.ToString();
                        txtKullanici.Text = veri.Kullanici.ToString();
                        cmbDurum.EditValue = veri.Durum.ToString();
                        this.Id = Convert.ToInt32(veri.Id);
                        string resimYolu = veri.Ek as string;
                        if (!string.IsNullOrEmpty(resimYolu) && File.Exists(resimYolu))
                        {
                            pictureEdit1.Image = Image.FromFile(resimYolu);
                        }
                        else
                        {
                            pictureEdit1.Image = null;
                        }
                    }
                    else
                    {
                        bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                    }
                }
            }
            else
            {
                bildirim.Uyari("Kayıt gösterebilmek için öncelikle listeden bir kayıt getirmelisiniz!");
            }

        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }

        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }
        void FormTemizle()
        {
            object[] bilgiler = { dateTalepTarihi, txtDepartman, txtBaslik, memoAciklama, txtDosyaEk };
            yardimciAraclar.KartTemizle(bilgiler);
            this.Id = 0;
            cmbDurum.SelectedIndex = 0;
            dateTamamlanmaTarihi.EditValue = DateTime.Now;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        private void txtDosyaEk_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Resim Dosyaları (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    string uniqueId = Guid.NewGuid().ToString();
                    CopyImageToApplicationFolder(selectedFilePath, uniqueId);
                    txtDosyaEk.Text = Path.Combine(Application.StartupPath, "Resim", "Talepler", $"{uniqueId}.png");
                    ShowImageInPictureEdit(txtDosyaEk.Text);
                }
            }
        }
        public void CopyImageToApplicationFolder(string selectedFilePath, string uniqueId)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Resim", "Talepler");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string destinationFilePath = Path.Combine(folderPath, $"{uniqueId}.png");
                File.Copy(selectedFilePath, destinationFilePath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Resim kopyalama sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowImageInPictureEdit(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    pictureEdit1.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureEdit1.Image = null; // Dosya yoksa resmi boş bırak
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Resim gösterme sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}