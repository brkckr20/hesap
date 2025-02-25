using Dapper;
using DevExpress.XtraEditors;
using Hesap.Context;
using Hesap.DataAccess;
using Hesap.Forms.Liste;
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
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmUrunReceteKarti : DevExpress.XtraEditors.XtraForm
    {
        Numarator numarator = new Numarator();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        int InventoryType = Convert.ToInt32(InventoryTypes.Kumas);
        CrudRepository crudRepository = new CrudRepository();
        string TableName = "InventoryReceipt";
        public FrmUrunReceteKarti()
        {
            InitializeComponent();
        }
        int Id = 0, InventoryId;
        private void txtUrun_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmUrunKartiListesi frm = new Liste.FrmUrunKartiListesi(InventoryType);
            frm.ShowDialog();
            txtUrun.Text = frm.UrunKodu;
            lblUrunAdi.Text = frm.UrunAdi;
            InventoryId = frm.Id;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                bildirim.Uyari("Lütfen bir resim seçin.");
                return;
            }

            string filePath = pictureBox1.Tag?.ToString();
            if (string.IsNullOrEmpty(filePath))
            {
                bildirim.Uyari("Resim yolu alınamadı.");
                return;
            }
            byte[] resimData = File.ReadAllBytes(filePath);

            var _params = new Dictionary<string, object>
            {
                {"ReceiptNo",txtReceteNo.Text},{"RawWidth",txtHamEn.Text},{"RawHeight",txtHamBoy.Text},{"ProductWidth",txtMamulEn.Text},{"ProductHeight",txtMamulBoy.Text},{"RawGrammage",txtGrm2.Text},{"ProductGrammage",txtMamulGrM2.Text},{"YarnDyed ",chckIpligiBoyali.Checked},{"Explanation ",txtReceteAciklama.Text},{"ReceiptType ",Convert.ToInt32(InventoryTypes.Kumas)},{"InventoryId ",InventoryId},{"ReceiptImage1", resimData}
            };
            if (this.Id == 0)
            {
                Id = crudRepository.Insert(this.TableName, _params);
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update(this.TableName, this.Id, _params);
                bildirim.GuncellemeBasarili();
            }
        }

        private void FrmUrunReceteKarti_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            txtReceteNo.Text = numarator.NumaraVer("UrunRecete");
            // gridControl1.DataSource = new BindingList<_UrunReceteUB>();

        }

        private void repoKalemIslem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnUrunResmiSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    pictureBox1.Tag = ofd.FileName;
                }
            }
        }
        byte[] imageData;
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                string dosyaYolu = pictureBox1.Tag?.ToString();

                if (!string.IsNullOrEmpty(dosyaYolu))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(dosyaYolu);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Resim açılamadı: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Resmin dosya yolu bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("Görüntülenecek bir resim yok.");
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmUrunReceteKartiListesi frm = new FrmUrunReceteKartiListesi(0);
            frm.ShowDialog();
            if (frm.ReceteNo != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(frm.UrunResmi))
                {
                    pictureBox1.Image = Image.FromStream(memoryStream);
                }
                pictureBox1.Tag = imageData;
            }
        }
        private void repoIplikKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmIplikKartiListesi frm = new FrmIplikKartiListesi();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.IplikKodu) && !string.IsNullOrEmpty(frm.IplikAdi))
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "IplikKodu", frm.IplikKodu);
                gridView1.SetRowCellValue(newRowHandle, "IplikAdi", frm.IplikAdi);
            }
        }
    }
}