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

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.HamDepo
{
    public partial class FrmTalimatlar : DevExpress.XtraEditors.XtraForm
    {
        public FrmTalimatlar()
        {
            InitializeComponent();
        }
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public string sql = "",baslik;
        private void FrmTalimatlar_Load(object sender, EventArgs e)
        {
            sql= @"select 
ISNULL(d1.TalimatNo, '') AS TalimatNo,	
ISNULL(d1.Tarih, '') AS Tarih,
ISNULL(d1.FirmaId, 0) AS FirmaId,
ISNULL(fk.FirmaKodu, '') AS FirmaKodu,
ISNULL(fk.FirmaUnvan, '') AS FirmaUnvan,
ISNULL(uk.Id, '') AS KumasId,
ISNULL(uk.UrunKodu, '') AS KumasKodu,
ISNULL(uk.UrunAdi, '') AS KumasAdi,
ISNULL(d2.GrM2, '') AS GrM2,
ISNULL(SUM(d2.BrutKg), 0) AS BrutTalimatKg,
ISNULL(SUM(d2.NetKg), 0) AS NetTalimatKg,
(select ISNULL(sum(y.BrutKg),0) from HamDepo1 x inner join HamDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) [BrutGiriş],
(select ISNULL(sum(y.NetKg),0) from HamDepo1 x inner join HamDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) [NetGiriş],
 --ISNULL(SUM(d2.BrutKg), 0) - (select ISNULL(sum(y.BrutKg),0) from HamDepo1 x inner join HamDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) BrutKg,
 ISNULL(SUM(d2.NetKg), 0) - (select ISNULL(sum(y.NetKg),0) from HamDepo1 x inner join HamDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) NetKg,
 ISNULL(d2.Id,0) TakipNo
 from HamDepo1 d1
inner join HamDepo2 d2 on d1.Id = d2.RefNo
left join FirmaKarti fk on fk.Id = d1.FirmaId
left join UrunKarti uk on uk.Id = d2.KumasId
where d1.IslemCinsi = 'SaTal'

group by
ISNULL(d1.TalimatNo, ''),	
d1.Tarih,
d1.FirmaId,
fk.FirmaKodu,
ISNULL(fk.FirmaUnvan, ''),
d2.Id,d1.Id,d1.IslemCinsi,d1.Aciklama,
ISNULL(uk.UrunKodu, ''),ISNULL(uk.UrunAdi, ''),ISNULL(d2.GrM2, ''),ISNULL(uk.Id, '')
HAVING 
 ISNULL(SUM(d2.NetKg), 0) - (select ISNULL(sum(y.NetKg),0) from HamDepo1 x inner join HamDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) > 0

                ";
            listele.Liste(sql, gridControl1);
            this.Text += "[Ham Depo]";
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
            
        }
        public List<string> satinAlmaListesi = new List<string>();

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int rowHandle in selectedRows)
            {
                string TalimatNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "TalimatNo"));
                int FirmaId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "FirmaId"));
                string FirmaKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaKodu"));
                string FirmaUnvan = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaUnvan"));
                int TakipNo = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "TakipNo"));
                int KumasId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "KumasId"));
                string KumasKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KumasKodu"));
                string KumasAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KumasAdi"));
                decimal BrutKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "BrutKg"));
                decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetKg"));
                decimal Fiyat = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Fiyat"));
                string DovizCinsi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "DovizCinsi"));
                int GrM2 = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "GrM2"));

                satinAlmaListesi.Add($"{TalimatNo};{FirmaId};{FirmaKodu};{FirmaUnvan};{TakipNo};{KumasId};{KumasKodu};{KumasAdi};{BrutKg};{Fiyat};{DovizCinsi};{NetKg};{GrM2}");
            }
            Close();
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1, this.Text);
        }
    }
}