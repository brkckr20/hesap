using DevExpress.XtraEditors;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using Hesap.DataAccess;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using Hesap.Helpers;
using System.Drawing.Drawing2D;
using Hesap.Models;

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmUrunKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmUrunKarti()
        {
            InitializeComponent();
        }

        int Id = 0, InventoryType = Convert.ToInt32(InventoryTypes.Kumas), InventoryNumeratorId=0;
        Bildirim bildirim = new Bildirim();
        CrudRepository crudRepository = new CrudRepository();
        string TableName = "Inventory", KumasAdiOzellik = "";

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUrunKodu.Text) && btnCinsiId.Text != null)
            {
                string CombinedCode = txtUrunKodu.Text.Substring(0, 3) + btnCinsiId.Text;
                string InventoryName = KumasAdiOzellik + " " + lblCinsiAciklama.Text;
                if (crudRepository.IfExistRecord(TableName, "CombinedCode", CombinedCode) > 0)
                {
                    string code = crudRepository.GetByCode("InventoryCode", this.TableName, CombinedCode);
                    bildirim.Uyari($"Seçtiğiniz özelliklere ait bir kayıt bulunmaktadır.\nLütfen {code} numaralı kaydı kontrol ediniz!!");
                    return;
                }

                var InvParams = new Dictionary<string, object>
            {
                { "InventoryCode", txtUrunKodu.Text }, { "InventoryName", InventoryName },
                {"Type" , InventoryTypes.Kumas}, { "IsUse", chckPasif.Checked }, {"Unit",""},{"CombinedCode",CombinedCode},{"SubType" , KumasAdiOzellik},{"IsPrefix" , false}//,{"IsStock" , true}
            };
                if (this.Id == 0)
                {
                    Id = crudRepository.Insert(TableName, InvParams);
                    crudRepository.Update("FeatureCoding", Convert.ToInt32(btnCinsiId.Text), new Dictionary<string, object> { { "InventoryId", this.Id } });
                    txtUrunAdi.Text = InventoryName;
                    var item = crudRepository.GetById<Numerator>("Numerator", InventoryNumeratorId);
                    crudRepository.Update("Numerator", item.Id, new Dictionary<string, object> { { "Number", item.Number + 1 } });
                    bildirim.Basarili();
                }
                else
                {
                    crudRepository.Update(this.TableName, this.Id, InvParams);
                    bildirim.GuncellemeBasarili();
                }
            }
            else
            {
                bildirim.Uyari("Kumaş kodu veya kumaş özelliği boş bırakılamaz!");
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmUrunKartiListesi frm = new Liste.FrmUrunKartiListesi(InventoryType);
            frm.ShowDialog();
            if (frm.UrunKodu != null && frm.UrunAdi != null)
            {
                txtUrunKodu.Text = frm.UrunKodu;
                txtUrunAdi.Text = frm.UrunAdi;
                chckPasif.Checked = frm.Pasif;
                this.Id = frm.Id;
                var item = crudRepository.GetByOtherCondition<FeatureCoding>("FeatureCoding", "InventoryId", this.Id);
                if (item != null)
                {
                    btnCinsiId.Text = item.Id.ToString() ?? "";
                    lblCinsiAciklama.Text = item.Explanation ?? "";
                }
            }
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(this.TableName, this.Id, Temizle);
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                var list = crudRepository.GetByList<Inventory>(this.TableName, KayitTipi, this.Id);

                if (list != null && list.Type == this.InventoryType && !list.IsPrefix)
                {
                    this.Id = list.Id;
                    txtUrunKodu.Text = list.InventoryCode;
                    txtUrunAdi.Text = list.InventoryName;
                    chckPasif.Checked = list.IsUse;
                    var cinsiKod = crudRepository.GetByOtherCondition<FeatureCoding>("FeatureCoding", "InventoryId", this.Id);
                    if (cinsiKod != null)
                    {
                        btnCinsiId.Text = cinsiKod.Id.ToString();
                        lblCinsiAciklama.Text = cinsiKod.Explanation;
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
        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }
        void Temizle()
        {
            txtUrunKodu.Text = "";
            txtUrunAdi.Text = "";
            chckPasif.Checked = false;
            btnCinsiId.Text = "";
            lblCinsiAciklama.Text = "";
            this.Id = 0;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        private void FrmUrunKarti_Load(object sender, EventArgs e)
        {

        }
        private void buttonEdit1_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmUrunTipiSecimi frm = new FrmUrunTipiSecimi();
            frm.ShowDialog();
            txtUrunKodu.Text = frm.OnEk + frm.yeniNumaraStr;
            KumasAdiOzellik = frm.KumasAdiOzellik;
            InventoryNumeratorId = frm.Id;
        }
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OzellikSecikEkrani(lblCinsi.Text);
        }
        void OzellikSecikEkrani(string labelName)
        {
            FrmOzellikSecimi frm = new FrmOzellikSecimi(SemiColonHelper.RemoveSemiColon(labelName), this.Name, txtUrunKodu.Text.Substring(0,3));
            frm.ShowDialog();
            if (frm.id == null)
            {
                return;
            }
            switch (SemiColonHelper.RemoveSemiColon(labelName).Trim())
            {
                case "Cinsi":
                    btnCinsiId.Text = frm.id;
                    lblCinsiAciklama.Text = frm.aciklama;
                    break;
                default:
                    break;
            }
        }
    }
}