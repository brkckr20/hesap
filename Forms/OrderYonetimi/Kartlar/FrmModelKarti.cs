using DevExpress.XtraEditors;
using Hesap.Context;
using Hesap.Forms.MalzemeYonetimi.Ekranlar.HamDepo;
using Hesap.Forms.OrderYonetimi.Models;
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

namespace Hesap.Forms.OrderYonetimi
{
    public partial class FrmModelKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmModelKarti()
        {
            InitializeComponent();
        }
        int Id = 0, FirmaId = 0, KategoriId = 0, CinsiId = 0, PazarlamaciId = 0, KullaniciId;

        private void txtKategori_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.KategoriYansit(txtKategori,txtOrjKategoriAdi,ref this.KategoriId,"Kategori Kartı");
        }

        private void txtCinsi_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.KategoriYansit(txtCinsi, txtOrjCinsiAdi, ref this.CinsiId, "Cinsi Kartı");
        }

        void BaslangicVerileri()
        {
            gridBedenler.DataSource = new BindingList<Bedenler>();
        }

        private void FrmModelKarti_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }

        HesaplaVeYansit yansit = new HesaplaVeYansit();
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "ModelKodu", txtModelKodu.Text },
                { "ModelAdi", txtModelAdi.Text },
                { "OrjModelAdi", txtOrjModelAdi.Text },
                { "FirmaId", this.FirmaId },
                { "KategoriId", this.KategoriId},
                { "CinsiId", this.CinsiId},
                { "GrM2", txtGrM2.Text},
                { "OzelKod",txtOzelKod.Text},
                { "OzelKod2",txtOzelKod2.Text},
                //{ "Fiyat",},
                //{ "DovizCinsi",},
                { "PazarlamaciId",this.PazarlamaciId},
                { "KayitTarihi",DateTime.Now},
                { "KayitEdenId",this.KullaniciId},
                { "KumasOK",chckKumasOK.Checked},
                { "BoyaOK",chckBoyaOK.Checked},
                { "NakisOK",chckNakisOK.Checked},
                { "AksesuarOK",chckAksesuarOK.Checked},
                { "IplikOK",chckIplikOK.Checked},
                { "GTIP",txtGTIP.Text},
                //{ "GTIP",txtGTIP.Text},
        };
            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord("ModelKarti", parameters);
                bildirim.Basarili();
            }
            else
            {
                cRUD.UpdateRecord("ModelKarti", parameters, this.Id);
            }
        }


        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtMusteriKodu, txtMusteriAdi, ref this.FirmaId);
        }
    }
}