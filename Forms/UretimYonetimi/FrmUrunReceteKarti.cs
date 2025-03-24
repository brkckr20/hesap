using Hesap.DataAccess;
using Hesap.Forms.Liste;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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
            try
            {
                byte[] resimData = yardimciAraclar.GetPictureData(pictureBox1);
                var _params = new Dictionary<string, object>
            {
                {"ReceiptNo",txtReceteNo.Text},{"RawWidth",txtHamEn.Text},{"RawHeight",txtHamBoy.Text},{"ProductWidth",txtMamulEn.Text},{"ProductHeight",txtMamulBoy.Text},{"RawGrammage",txtGrm2.Text},{"ProductGrammage",txtMamulGrM2.Text},{"YarnDyed",chckIpligiBoyali.Checked},{"Explanation",txtReceteAciklama.Text},{"ReceiptType",Convert.ToInt32(InventoryTypes.Kumas)},{"InventoryId",InventoryId}
            };
                if (resimData != null && resimData.Length > 0)
                {
                    _params.Add("ReceiptImage1", resimData);
                }
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
            catch (Exception ex)
            {
                bildirim.Uyari(ex.Message);
            }


        }
        private void FrmUrunReceteKarti_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            txtReceteNo.Text = numarator.GetNumaratorNotCondition(this.TableName, "ReceiptNo");
            // gridControl1.DataSource = new BindingList<_UrunReceteUB>();
        }
        private void repoKalemIslem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
        private void btnUrunResmiSec_Click(object sender, EventArgs e)
        {
            yardimciAraclar.SelectImage(pictureBox1);
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            yardimciAraclar.OpenPicture(pictureBox1);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmUrunReceteKartiListesi frm = new FrmUrunReceteKartiListesi(0);
            frm.ShowDialog();
            if (frm.ReceteNo != null)
            {
                if (frm.UrunResmi != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream(frm.UrunResmi))
                    {
                        pictureBox1.Image = Image.FromStream(memoryStream);
                    }
                    string tempFilePath = Path.GetTempFileName() + ".png";
                    File.WriteAllBytes(tempFilePath, frm.UrunResmi);
                    pictureBox1.Tag = tempFilePath;

                }
                txtReceteNo.Text = frm.ReceteNo;
                this.InventoryId = frm.InventoryId;
                var item = crudRepository.GetById<Inventory>("Inventory", this.InventoryId);
                txtUrun.Text = item.InventoryCode;
                lblUrunAdi.Text = item.InventoryName;
                txtHamEn.Text = frm.HamEn.ToString();
                txtHamBoy.Text = frm.HamBoy.ToString();
                txtMamulEn.Text = frm.MamulEn.ToString();
                txtGrm2.Text = frm.HamGr_M2.ToString();
                txtMamulGrM2.Text = frm.MamulGr_M2.ToString();
                chckIpligiBoyali.Checked = Convert.ToBoolean(frm.IpligiBoyali);
                txtReceteAciklama.Text = frm.Aciklama;
                this.Id = frm.Id;
            }
        }
        private void FrmUrunReceteKarti_FormClosing(object sender, FormClosingEventArgs e)
        {
            yardimciAraclar.DeleteTempFile(pictureBox1);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(this.TableName,this.Id, Temizle);
        }
        void Temizle()
        {
            object[] kart = { txtReceteNo, txtUrun,lblUrunAdi, txtHamEn, txtHamBoy, txtMamulEn,txtMamulBoy,txtGrm2,txtMamulGrM2,chckIpligiBoyali,txtReceteAciklama};
            yardimciAraclar.KartTemizle(kart);
            pictureBox1.Image = null;
            this.Id = 0;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                var list = crudRepository.GetByList<InventoryReceipt>(this.TableName, KayitTipi, this.Id);
                if (list != null)
                {
                    this.Id = list.Id;
                    txtReceteNo.Text = list.ReceiptNo;
                    this.InventoryId = list.InventoryId;
                    var item = crudRepository.GetById<Inventory>("Inventory", this.InventoryId);
                    txtUrun.Text = item.InventoryCode;
                    lblUrunAdi.Text = item.InventoryName;
                    txtHamEn.Text = list.RawWidth.ToString();
                    txtHamBoy.Text = list.RawHeight.ToString();
                    txtMamulEn.Text = list.ProductWidth.ToString();
                    txtMamulBoy.Text = list.ProductHeight.ToString();
                    txtGrm2.Text = list.RawGrammage.ToString();
                    txtMamulGrM2.Text = list.ProductGrammage.ToString();
                    chckIpligiBoyali.Checked = list.YarnDyed;
                    txtReceteAciklama.Text = list.Explanation;
                    if (list.ReceiptImage1 != null)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(list.ReceiptImage1))
                        {
                            pictureBox1.Image = Image.FromStream(memoryStream);
                        }
                        string tempFilePath = Path.GetTempFileName() + ".png";
                        File.WriteAllBytes(tempFilePath, list.ReceiptImage1);
                        pictureBox1.Tag = tempFilePath;
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
                else
                {
                    bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                }
            }
            else
            {
                bildirim.Uyari("Kayıt gösterebilmek için öncelikle listeden bir kayıt getirmelisiniz!");
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
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