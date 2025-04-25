using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Utils;
using System;

namespace Hesap.Forms.Liste
{
    public partial class FrmIplikKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public string IplikKodu, IplikAdi, IplikNo, IplikNoAciklama, IplikCinsi, IplikCinsiAciklama;
        public int Id;
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void excelOlarakAktarxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"İplik Kartı Listesi");
        }

        public bool Organik;
        public FrmIplikKartiListesi()
        {
            InitializeComponent();
        }

        private void FrmIplikKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = @"Select 
	                        ISNULL(I.Id,'') [Id],
	                        ISNULL(I.InventoryCode,'') [IplikKodu],
	                        ISNULL(I.InventoryName,'') [IplikAdi],
	                        (select FC.Id from FeatureCoding FC where FC.Id = I.InventoryNo) [IplikNoId],
	                        (select FC.Explanation from FeatureCoding FC where FC.Id = I.InventoryNo) [IplikNoAdi],
	                        (select FC.Id from FeatureCoding FC where FC.Id = I.InventoryCinsi) [IplikCinsiId],
	                        (select FC.Explanation from FeatureCoding FC where FC.Id = I.InventoryCinsi) [IplikCinsiAdi]
                        from Inventory I 
	                        where I.Type = 2";
            listele.Liste(sql, gridControl1);
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            IplikKodu = gridView.GetFocusedRowCellValue("IplikKodu").ToString();
            IplikAdi = gridView.GetFocusedRowCellValue("IplikAdi").ToString();
            IplikNo = gridView.GetFocusedRowCellValue("IplikNoId").ToString();
            IplikNoAciklama = gridView.GetFocusedRowCellValue("IplikNoAdi").ToString();
            IplikCinsi = gridView.GetFocusedRowCellValue("IplikCinsiId").ToString();
            IplikCinsiAciklama = gridView.GetFocusedRowCellValue("IplikCinsiAdi").ToString();
            //Organik = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Organik")); // tabloya organik fieldi eklenecek
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}