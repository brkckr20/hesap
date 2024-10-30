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

namespace Hesap.Forms.Liste
{
    public partial class FrmBoyahaneRenkKartlariListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmBoyahaneRenkKartlariListesi()
        {
            InitializeComponent();
        }
        Listele listele = new Listele();
        HesaplaVeYansit hesaplaVeYansit = new HesaplaVeYansit();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public List<Dictionary<string, object>> veriler;
        private void FrmBoyahaneRenkKartlariListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = @"select BRK.Id,
		case 
			when RenkTuru = 1 then 'Kumaş'
			when RenkTuru = 2 then 'İplik'
			end as [RenkTuru]
		,BoyahaneRenkKodu
		,BoyahaneRenkAdi
		,ISNULL(FK.Id,0) [CariId]
		,ISNULL(FK.FirmaKodu,'') [FirmaKodu]
		,ISNULL(FK.FirmaUnvan,'') [FirmaUnvan]
		,ISNULL(PantoneNo,'') [PantoneNo]
		,ISNULL(Fiyat,0) [Fiyat]
		,ISNULL(DovizCinsi,'') [DovizCinsi]
		,ISNULL(Kullanimda,0) [Kullanimda]
from BoyahaneRenkKartlari BRK left join FirmaKarti FK on BRK.CariId = FK.Id";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            veriler = hesaplaVeYansit.KartaYansit(sender);
            this.Close();
        }

        private void excelDosyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Boyahane Renk Kartları");
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }
    }
}