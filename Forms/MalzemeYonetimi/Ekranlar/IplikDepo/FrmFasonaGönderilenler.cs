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
    public partial class FrmFasonaGönderilenler : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public List<string> listem = new List<string>();
        int _firmaId;
        public FrmFasonaGönderilenler(int FirmaId)
        {
            InitializeComponent();
            this._firmaId = FirmaId;
        }

        private void FrmFasonaGönderilenler_Load(object sender, EventArgs e)
        {
            string sql;
            if (this._firmaId !=0)
            {
                sql = $@"SELECT 
    ISNULL(d1.Tarih, '') AS Tarih,
    ISNULL(d2.Id, '') AS TakipNo,
    ISNULL(d2.KalemIslem, '') AS KalemIslem,
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
    ISNULL(SUM(d2.NetKg), 0) - 
        (SELECT ISNULL(SUM(y.BrutKg), 0) 
         FROM IplikDepo1 x 
         INNER JOIN IplikDepo2 y ON x.Id = y.RefNo 
         WHERE x.IslemCinsi = 'Giriş' AND y.TakipNo = ISNULL(d2.Id, '')) AS NetKg

FROM 
    IplikDepo1 d1 
INNER JOIN 
    IplikDepo2 d2 ON d1.Id = d2.RefNo
LEFT JOIN 
    FirmaKarti fk ON d1.FirmaId = fk.Id
LEFT JOIN 
    IplikKarti ik ON ik.Id = d2.IplikId
LEFT JOIN 
    BoyahaneRenkKartlari brk ON brk.Id = d2.IplikRenkId
WHERE 
    d1.IslemCinsi = 'Çıkış' 
    AND d2.KalemIslem NOT IN ('Satış','Fason İade','Alış İade') 
     AND d1.FirmaId = '{this._firmaId}'
GROUP BY
    ISNULL(d1.Tarih, ''),
    ISNULL(d2.Id, ''),
    ISNULL(d2.KalemIslem, ''),
    ISNULL(d1.FirmaId, 0),
    ISNULL(fk.FirmaKodu, ''),
    ISNULL(fk.FirmaUnvan, ''),
    ISNULL(d2.IplikId, ''),
    ISNULL(ik.IplikKodu, ''),
    ISNULL(ik.IplikAdi, ''),
    ISNULL(d2.Marka, ''),
    ISNULL(d2.IplikRenkId, ''),
    ISNULL(brk.BoyahaneRenkKodu, ''),
    ISNULL(brk.BoyahaneRenkAdi, '')
HAVING 
    ISNULL(SUM(d2.NetKg), 0) - 
    (SELECT ISNULL(SUM(y.BrutKg), 0) 
     FROM IplikDepo1 x 
     INNER JOIN IplikDepo2 y ON x.Id = y.RefNo 
     WHERE x.IslemCinsi = 'Giriş' AND y.TakipNo = ISNULL(d2.Id, '')) > 0

					";
            }
            else
            {
                sql = "select 'Lütfen firma seçimi yapınız' [Açıklama]";
            }
            
								listele.Liste(sql, gridControl1);
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int rowHandle in selectedRows)
            {
                int FirmaId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "FirmaId"));
                string FirmaKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaKodu"));
                string KalemIslem = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KalemIslem"));
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

                listem.Add($"{FirmaId};{FirmaKodu};{FirmaUnvan};{TakipNo};{IplikId};{IplikKodu};{IplikAdi};{BrutKg};{Fiyat};{DovizCinsi};" +
                    $"{OrganikSertifikaNo};{Marka};{IplikRenkId};{IplkiRenkKodu};{IplikRenkAdi};{NetKg};{KalemIslem}");
            }
            Close();
        }
    }
}