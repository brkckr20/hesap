using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmIplikKarti : DevExpress.XtraEditors.XtraForm
    {
        Bildirim bildirim = new Bildirim();
        private int Id = 0;
        private string TableName = "Inventory", IplikAdiOzellik = "İplik";
        Ayarlar ayarlar = new Ayarlar();
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();

        public FrmIplikKarti()
        {
            InitializeComponent();
        }
        private void txtUrun_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OzellikSecikEkrani(lblIplikNo.Text);
        }

        void OzellikSecikEkrani(string labelName)
        {
            FrmOzellikSecimi frm = new FrmOzellikSecimi(NoktaliVirgulKaldir(labelName), this.Name);
            frm.ShowDialog();
            switch (NoktaliVirgulKaldir(labelName).Trim())
            {
                case "İplik No":
                    txtIplikNo.Text = frm.id;
                    lblIplikNoAciklama.Text = frm.aciklama;
                    break;
                case "İplik Cinsi":
                    txtIplikCinsi.Text = frm.id;
                    lblIplikCinsiAciklama.Text = frm.aciklama;
                    break;
                default:
                    break;
            }
        }
        string NoktaliVirgulKaldir(string metin)
        {
            string veri = metin.Split(':')[0];
            return veri;
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OzellikSecikEkrani(lblIplikCinsi.Text);
        }
        private void lblIplikNoAciklama_TextChanged(object sender, EventArgs e)
        {
            string iplikNo = lblIplikNoAciklama.Text;
            string[] parts = iplikNo.Split('/');
            if (parts.Length == 2)
            {
                int birinci = Convert.ToInt32(parts[0].Trim());
                int ikinci = Convert.ToInt32(parts[1].Trim());
                txtNumara.Text = (birinci / ikinci).ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string CombinedCode = txtIplikNo.Text + txtIplikCinsi.Text;
            string InventoryName = IplikAdiOzellik + " " + lblIplikNoAciklama.Text + " " + lblIplikCinsiAciklama.Text + (checkEdit1.Checked ? " Organik" : "");
            if (crudRepository.IfExistRecord(TableName, "CombinedCode", CombinedCode) > 0)
            {
                string code = crudRepository.GetByCode("InventoryCode", TableName, CombinedCode);
                bildirim.Uyari($"Seçtiğiniz özelliklere ait bir kayıt bulunmaktadır.\nLütfen {code} numaralı kaydı kontrol ediniz!!");
                return;
            }
            var InvParams = new Dictionary<string, object>
            {
                { "InventoryCode", txtIplikKodu.Text }, { "InventoryName", InventoryName },
                {"Type" , InventoryTypes.Iplik}, { "IsUse", checkEdit2.Checked }, {"Unit",""},{"CombinedCode",CombinedCode},{"SubType" , IplikAdiOzellik},{"IsPrefix" , false},{"InventoryNo" , txtIplikNo.Text},{"InventoryCinsi" , txtIplikCinsi.Text}
            };
            if (this.Id == 0)
            {
                Id = crudRepository.Insert(TableName, InvParams);
                lblIplikAdi.Text = InventoryName;
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update(TableName, this.Id, InvParams);
                bildirim.GuncellemeBasarili();
            }
        }

        private void FrmIplikKarti_Load(object sender, EventArgs e)
        {
            txtIplikKodu.Text = crudRepository.GetInventoryNumerator(TableName, "InventoryCode", Convert.ToInt32(InventoryTypes.Iplik), "IPL");
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }
        void Temizle()
        {
            object[] kart = { txtIplikCinsi, txtIplikCinsi, lblIplikCinsiAciklama, txtNumara,checkEdit1,checkEdit2,lblIplikAdi, txtIplikNo,lblIplikNoAciklama };
            yardimciAraclar.KartTemizle(kart);
            this.Id = 0;
            txtIplikKodu.Text = crudRepository.GetInventoryNumerator(TableName, "InventoryCode", Convert.ToInt32(InventoryTypes.Iplik), "IPL");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(TableName, this.Id, Temizle);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmIplikKartiListesi frm = new Liste.FrmIplikKartiListesi();
            frm.ShowDialog();
            if (frm.IplikKodu != null)
            {
                this.Id = frm.Id;
                txtIplikKodu.Text = frm.IplikKodu;
                lblIplikAdi.Text = frm.IplikAdi;
                txtIplikNo.Text = frm.IplikNo;
                lblIplikNoAciklama.Text = frm.IplikNoAciklama;
                txtIplikCinsi.Text = frm.IplikCinsi;
                lblIplikCinsiAciklama.Text = frm.IplikCinsiAciklama;
                //checkEdit1.Checked = frm.Organik;
            }
        }
    }
}