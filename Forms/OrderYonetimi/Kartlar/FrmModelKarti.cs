using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.XtraEditors;
using Hesap.Context;
using Hesap.DataAccess;
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
        int Id = 0, FirmaId = 0, KategoriId = 0, CinsiId = 0, PazarlamaciId = Properties.Settings.Default.Id, KullaniciId;
        Metotlar metotlar = new Metotlar();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CrudRepository crudRepository = new CrudRepository();

        private void txtKategori_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.KategoriYansit(txtKategori, txtOrjKategoriAdi, ref this.KategoriId, "Kategori Kartı");
        }

        private void txtCinsi_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.KategoriYansit(txtCinsi, txtOrjCinsiAdi, ref this.CinsiId, "Cinsi Kartı");
        }

        void BaslangicVerileri()
        {
            //gridBedenler.DataSource = new BindingList<Bedenler>();
            //yardimciAraclar.KolonlariGetir(gridViewBedenler, this.Text + " [Bedenler]");
            //gridBedenler.ContextMenuStrip = contextBedenler;
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //yardimciAraclar.KolonDurumunuKaydet(gridViewBedenler,this.Text + " [Bedenler]");
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //yardimciAraclar.KolonSecici(gridBedenler);
        }

        private void FrmModelKarti_Load(object sender, EventArgs e)
        {
            
            BaslangicVerileri();
        }
        


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "InventoryCode", txtModelKodu.Text},
                { "InventoryName", txtModelAdi.Text},
                { "InventoryOriginalName", txtOrjModelAdi.Text},
                { "CompanyId", this.FirmaId},
                { "CategoryId",0},
                { "GenusId",0},
                { "SpecialCode",txtOzelKod.Text},
                { "SpecialCode2",txtOzelKod2.Text},
                { "GrM2",txtGrM2.Text},
                { "UserId",CurrentUser.UserId},
                { "FabricOK",false},
                { "ColorOK",false},
                { "EmbroideryOK",false},
                { "YarnOK",false},
                { "AccessoriesOK",false},
                { "GTIPNo",txtGTIP.Text},
                { "IsUse",chckKullanimda.Checked},//bradan devam edilecek
            };
            if (this.Id == 0)
            {
                this.Id = crudRepository.Insert("Inventory", parameters);
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update("Inventory", this.Id, parameters);
                bildirim.GuncellemeBasarili();
            }
            //    var parameters = new Dictionary<string, object>
            //    {
            //        { "ModelKodu", txtModelKodu.Text },
            //        { "ModelAdi", txtModelAdi.Text },
            //        { "OrjModelAdi", txtOrjModelAdi.Text },
            //        { "FirmaId", this.FirmaId },
            //        { "KategoriId", this.KategoriId},
            //        { "CinsiId", this.CinsiId},
            //        { "GrM2", txtGrM2.Text},
            //        { "OzelKod",txtOzelKod.Text},
            //        { "OzelKod2",txtOzelKod2.Text},
            //        { "Fiyat",},
            //        { "DovizCinsi",},
            //        { "PazarlamaciId",this.PazarlamaciId},
            //        { "KayitTarihi",DateTime.Now},
            //        { "KayitEdenId",this.KullaniciId},
            //        { "KumasOK",chckKumasOK.Checked},
            //        { "BoyaOK",chckBoyaOK.Checked},
            //        { "NakisOK",chckNakisOK.Checked},
            //        { "AksesuarOK",chckAksesuarOK.Checked},
            //        { "IplikOK",chckIplikOK.Checked},
            //        { "GTIP",txtGTIP.Text},
            //        { "GTIP",txtGTIP.Text},
            //};
            //    if (this.Id == 0)
            //    {
            //        this.Id = cRUD.InsertRecord("ModelKarti", parameters);
            //        BedenleriKaydet();
            //        bildirim.Basarili();
            //    }
            //    else
            //    {
            //        cRUD.UpdateRecord("ModelKarti", parameters, this.Id);
            //        BedenleriGuncelle();
            //        /*
            //         for (int i = 0; i < gridView1.RowCount - 1; i++)
            //        {
            //            var d2Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
            //            var kalemParameters = metotlar.CreateHameDepo2KalemParameters(i, this.Id, gridView1);
            //            if (d2Id > 0)
            //            {
            //                cRUD.UpdateRecord("HamDepo2", kalemParameters, d2Id);
            //            }
            //            else
            //            {
            //                var yeniId = cRUD.InsertRecord("HamDepo2", kalemParameters);
            //                gridView1.SetRowCellValue(i, "D2Id", yeniId);
            //            }
            //        }
            //         */
            //    }
        }

        void BedenleriKaydet()
        {
            //for (int i = 0; i < gridViewBedenler.RowCount - 1; i++)
            //{
            //    var parameters = metotlar.ModelBeden(i, this.Id, gridViewBedenler);
            //    var BedenId = cRUD.InsertRecord("ModelBedenSeti", parameters);
            //    gridViewBedenler.SetRowCellValue(i, "Id", BedenId);
            //}
        }
        void BedenleriGuncelle()
        {
            //for (int i = 0; i < gridViewBedenler.RowCount - 1; i++)
            //{
            //    var d2Id = Convert.ToInt32(gridViewBedenler.GetRowCellValue(i, "Id"));
            //    var kalemParameters = metotlar.ModelBeden(i, this.Id, gridViewBedenler);
            //    if (d2Id > 0)
            //    {
            //        cRUD.UpdateRecord("ModelBedenSeti", kalemParameters, d2Id);
            //    }
            //    else
            //    {
            //        var yeniId = cRUD.InsertRecord("ModelBedenSeti", kalemParameters);
            //        gridViewBedenler.SetRowCellValue(i, "Id", yeniId);
            //    }
            //}
        }


        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtMusteriKodu, txtMusteriAdi, ref this.FirmaId);
        }
    }
}