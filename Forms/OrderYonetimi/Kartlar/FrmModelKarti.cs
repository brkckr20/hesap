using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;

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
            //yansit.KategoriYansit(txtKategori, txtOrjKategoriAdi, ref this.KategoriId, "Kategori Kartı");
        }

        private void txtCinsi_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //yansit.KategoriYansit(txtCinsi, txtOrjCinsiAdi, ref this.CinsiId, "Cinsi Kartı");
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

        private void bedenSetiGirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderIslemleri.FrmBedenSeti frm = new OrderIslemleri.FrmBedenSeti();
            frm.ShowDialog();
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
                this.Id = crudRepository.Insert("Inventory", parameters);
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update("Inventory", this.Id, parameters);
                bildirim.GuncellemeBasarili();
            }
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