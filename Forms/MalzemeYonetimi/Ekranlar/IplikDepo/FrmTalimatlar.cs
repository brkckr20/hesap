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

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo
{
    public partial class FrmTalimatlar : DevExpress.XtraEditors.XtraForm
    {
		Listele listele = new Listele();
        public List<string> satinAlmaListesi = new List<string>();

        public FrmTalimatlar()
        {
            InitializeComponent();
        }

        private void FrmTalimatlar_Load(object sender, EventArgs e)
        {
			
			string sql = @"SELECT
	ISNULL(d1.TalimatNo, '') AS TalimatNo,	
	ISNULL(d1.Tarih, '') AS Tarih,
    ISNULL(d1.FirmaId, 0) AS FirmaId,
	ISNULL(fk.FirmaKodu, '') AS FirmaKodu,
    ISNULL(fk.FirmaUnvan, '') AS FirmaUnvan,
	ISNULL(d2.IplikId, '') AS IplikId,
	ISNULL(ik.IplikKodu, '') AS IplikKodu,
	ISNULL(ik.IplikAdi, '') AS IplikAdi,
	ISNULL(d2.Marka, '') AS Marka,
	ISNULL(d2.IplikRenkId, '') AS IplikRenkId,
	ISNULL(brk.BoyahaneRenkKodu, '') AS IplikRenkKodu,
	ISNULL(brk.BoyahaneRenkAdi, '') AS IplikRenkAdi,
    ISNULL(SUM(d2.BrutKg), 0) AS BrutTalimatKg,
    ISNULL(SUM(d2.NetKg), 0) AS NetTalimatKg,
	(select ISNULL(sum(y.BrutKg),0) from IplikDepo1 x inner join IplikDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) [BrutGiriş],
	(select ISNULL(sum(y.NetKg),0) from IplikDepo1 x inner join IplikDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) [NetGiriş],
	 ISNULL(SUM(d2.BrutKg), 0) - (select ISNULL(sum(y.BrutKg),0) from IplikDepo1 x inner join IplikDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) BrutKg,
	 ISNULL(SUM(d2.NetKg), 0) - (select ISNULL(sum(y.NetKg),0) from IplikDepo1 x inner join IplikDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) NetKg,
	 ISNULL(d2.Id,0) TakipNo
FROM 
    IplikDepo1 d1 
    INNER JOIN IplikDepo2 d2 ON d1.Id = d2.RefNo
    left JOIN FirmaKarti fk ON d1.FirmaId = fk.Id
	left join IplikKarti ik on ik.Id = d2.IplikId
	left join BoyahaneRenkKartlari brk on brk.Id = d2.IplikRenkId
	where d1.IslemCinsi = 'SaTal'
GROUP BY 
    ISNULL(d1.TalimatNo, ''),
	ISNULL(d1.Tarih, ''),
    ISNULL(d1.FirmaId, 0),
    ISNULL(fk.FirmaUnvan, ''),
    ISNULL(fk.FirmaKodu, ''),
    ISNULL(d2.IplikId, ''),
	ISNULL(ik.IplikKodu, ''),
	ISNULL(ik.IplikAdi, ''),
	ISNULL(d2.Marka, ''),
	ISNULL(d2.IplikRenkId, '') ,
	iSNULL(brk.BoyahaneRenkKodu, ''),
	iSNULL(brk.BoyahaneRenkAdi, '')
	,d2.Id
	,ISNULL(d2.Id,0)
	HAVING 
	 ISNULL(SUM(d2.NetKg), 0) - (select ISNULL(sum(y.NetKg),0) from IplikDepo1 x inner join IplikDepo2 y on x.Id = y.RefNo where x.IslemCinsi = 'Giriş' and y.TakipNo = d2.Id) > 0
";
            listele.Liste(sql, gridControl1);
        }

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
				int IplikId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikId"));
                string IplikKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikKodu"));
                string IplikAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikAdi"));
				decimal BrutKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "BrutKg"));
				decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetKg"));
				decimal Fiyat = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Fiyat"));
                string DovizCinsi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "DovizCinsi"));
                string OrganikSertifikaNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "OrganikSertifikaNo"));
                string Marka = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Marka"));
				int IplikRenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikRenkId"));
                string IplkiRenkKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkKodu"));
                string IplikRenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkAdi"));

				satinAlmaListesi.Add($"{TalimatNo};{FirmaId};{FirmaKodu};{FirmaUnvan};{TakipNo};{IplikId};{IplikKodu};{IplikAdi};{BrutKg};{Fiyat};{DovizCinsi};" +
					$"{OrganikSertifikaNo};{Marka};{IplikRenkId};{IplkiRenkKodu};{IplikRenkAdi};{NetKg}");
			}
            Close();
        }
    }
}