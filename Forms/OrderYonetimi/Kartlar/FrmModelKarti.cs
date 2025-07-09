using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Forms.OrderYonetimi.Liste;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hesap.Forms.OrderYonetimi
{
    public partial class FrmModelKarti : XtraForm
    {
        public FrmModelKarti()
        {
            InitializeComponent();
        }
        int Id = 0, FirmaId = 0, KategoriId = 0, CinsiId = 0, PazarlamaciId = Properties.Settings.Default.Id, KullaniciId;
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CrudRepository crudRepository = new CrudRepository();

        private void txtKategori_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //
        }

        private void txtCinsi_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //yansit.KategoriYansit(txtCinsi, txtOrjCinsiAdi, ref this.CinsiId, "Cinsi Kartı");
        }

        void BaslangicVerileri()
        {
            gridKumasBilgileri.DataSource = new BindingList<InventoryReceipt>();
            repoCinsi.Items.Add("Ana Kumaş");
            repoCinsi.Items.Add("Biye");
            repoCinsi.Items.Add("Desen");
            repoCinsi.Items.Add("Aplika 1");
            repoCinsi.Items.Add("Aplika 2");
            repoCinsi.Items.Add("Aplika 3");
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //yardimciAraclar.KolonDurumunuKaydet(gridViewBedenler,this.Text + " [Bedenler]");
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //yardimciAraclar.KolonSecici(gridBedenler);
        }

        private void bedenSetiGirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtModelKodu.Text.Trim() != "")
            {
                Kaydet();
                OrderIslemleri.FrmBedenSeti frm = new OrderIslemleri.FrmBedenSeti(this.Id);
                frm.ShowDialog();
            }
            else
            {
                bildirim.Uyari("Kayıt yapabilmek için Model Kodu alanını doldurunuz!");
            }
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.MalzemeBilgileriniGrideYansit(gridView1, InventoryTypes.Kumas);
        }

        private void FrmModelKarti_Load(object sender, EventArgs e)
        {

            BaslangicVerileri();
        }

        private void repoSutunSecimi_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OrderIslemleri.FrmBedenSecimi frm = new OrderIslemleri.FrmBedenSecimi(this.Id);
            frm.ShowDialog();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmModelKartiListesi frm = new FrmModelKartiListesi();
            frm.ShowDialog();
            if (frm.Id != null)
            {
                this.Id = frm.Id;
                txtModelKodu.Text = frm.Kodu;
                txtModelAdi.Text = frm.Adi;
                txtOrjModelAdi.Text = frm.OrjAdi;
                this.FirmaId = frm.FirmaId;
                txtMusteriKodu.Text = frm.FirmaKodu;
                txtMusteriAdi.Text = frm.FirmaAdi;
                this.KategoriId = frm.KategoriId;
                txtKategori.Text = frm.KategoriAdi;
                txtOrjKategoriAdi.Text = frm.KategorOrjAdi;
                this.CinsiId = frm.CinsiId;
                txtCinsi.Text = frm.CinsiAdi;
                txtOrjCinsiAdi.Text = frm.CinsiOrjAdi;
                txtOzelKod.Text = frm.OzelKod;
                txtOzelKod2.Text = frm.OzelKod2;
                txtGrM2.Text = frm.GrM2;
                this.PazarlamaciId = frm.PazarlamaciId;
                txtPazarlamaci.Text = frm.Pazarlamaci;
                chckKumasOK.Checked = frm.KumasOk;
                chckBoyaOK.Checked = frm.BoyaOk;
                chckNakisOK.Checked = frm.NakisOk;
                chckIplikOK.Checked = frm.IplikOk;
                chckAksesuarOK.Checked = frm.AksesuarOk;
                txtGTIP.Text = frm.GTIPNo;
            }
        }

        private void txtKategori_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.KategoriYansit(txtKategori, txtOrjKategoriAdi, ref this.KategoriId, "Kategori Kartı");
        }

        private void txtCinsi_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.KategoriYansit(txtCinsi, txtOrjCinsiAdi, ref this.CinsiId, "Cinsi Kartı");
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Kaydet(true);
        }

        void Kaydet(bool showNotification = false)
        {
            var parameters = new Dictionary<string, object>
            {
                { "InventoryCode", txtModelKodu.Text},
                { "InventoryName", txtModelAdi.Text},
                { "InventoryOriginalName", txtOrjModelAdi.Text},
                { "CompanyId", this.FirmaId},
                { "CategoryId",this.KategoriId},
                { "GenusId",this.CinsiId},
                { "SpecialCode",txtOzelKod.Text},
                { "SpecialCode2",txtOzelKod2.Text},
                { "GrM2",txtGrM2.Text},
                { "UserId",CurrentUser.UserId},
                { "FabricOK",chckKumasOK.Checked},
                { "ColorOK",chckBoyaOK.Checked},
                { "EmbroideryOK",chckNakisOK.Checked},
                { "YarnOK",chckIplikOK.Checked},
                { "AccessoriesOK",chckAksesuarOK.Checked},
                { "GTIPNo",txtGTIP.Text},
                { "IsUse",chckKullanimda.Checked},
                { "Unit",""},
                { "Type",InventoryTypes.Model},
            };
            if (this.Id == 0)
            {
                if (txtModelKodu.Text.Trim()=="")
                {
                    bildirim.Uyari("Kayıt yapabilmek için Model Kodu alanını doldurunuz!");
                    return;
                }
                else
                {
                    this.Id = crudRepository.Insert("Inventory", parameters);
                    if (showNotification)
                    {
                        bildirim.Basarili();
                    }
                }
            }
            else
            {
                crudRepository.Update("Inventory", this.Id, parameters);
                if (showNotification)
                {
                    bildirim.GuncellemeBasarili();
                }
            }
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtMusteriKodu, txtMusteriAdi, ref this.FirmaId);
        }
    }
}