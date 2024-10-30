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
    public partial class FrmIplikDepoStok : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public List<string> stokListesi = new List<string>();
        public FrmIplikDepoStok()
        {
            InitializeComponent();
        }

        private void FrmIplikDepoStok_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT 
                            ISNULL(d2.Id, 0) AS [TakipNo],
                            ISNULL(d2.KalemIslem, '') AS [KalemIslem],
                            ISNULL(d2.IplikId, '') AS [IplikId],
                            ISNULL(ik.IplikKodu, '') AS [IplikKodu],
                            ISNULL(ik.IplikAdi, '') AS [IplikAdi],
                            ISNULL(ik.Organik, '') AS [Organik],
                            ISNULL(d2.Marka, '') AS [Marka],
                            ISNULL(d2.PartiNo, '') AS [PartiNo],
                            ISNULL(d2.IplikRenkId, 0) AS [IplikRenkId],
                            ISNULL(brk.BoyahaneRenkKodu, '') AS [IplikRenkKodu],
                            ISNULL(brk.BoyahaneRenkAdi, '') AS [IplikRenkAdi],
                            --SUM(ISNULL(d2.NetKg, 0)) AS [NetKg],
                            ISNULL(SUM(d2.NetKg), 0) - 
                                (SELECT ISNULL(SUM(y.NetKg), 0) 
                                 FROM IplikDepo1 x 
                                 INNER JOIN IplikDepo2 y ON x.Id = y.RefNo 
                                 WHERE x.IslemCinsi = 'Çıkış' AND y.TakipNo = ISNULL(d2.Id, 0)) AS NetKg
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
                        LEFT JOIN 
                            OzellikKodlama ok ON ok.Id = ik.IplikNo
                        WHERE 
                            d1.IslemCinsi = 'Giriş'
                        GROUP BY
                            ISNULL(d2.KalemIslem, ''),
                            ISNULL(ik.IplikKodu, ''),
                            ISNULL(d2.IplikId, ''),
                            ISNULL(ik.IplikAdi, ''),
                            ISNULL(ik.Organik, ''),
                            ISNULL(d2.Marka, ''),
                            ISNULL(d2.PartiNo, ''),
                            ISNULL(d2.IplikRenkId, 0),
                            ISNULL(brk.BoyahaneRenkKodu, ''),
                            ISNULL(brk.BoyahaneRenkAdi, ''),
                            ISNULL(d2.Id, 0)
                        HAVING
                            ISNULL(SUM(d2.NetKg), 0) - 
                            (SELECT ISNULL(SUM(y.NetKg), 0) 
                             FROM IplikDepo1 x 
                             INNER JOIN IplikDepo2 y ON x.Id = y.RefNo 
                             WHERE x.IslemCinsi = 'Çıkış' AND y.TakipNo = ISNULL(d2.Id, 0)) <> 0

";
            listele.Liste(sql, gridControl1);
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                string KalemIslem = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KalemIslem"));
                string IplikKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikKodu"));
                int IplikId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikId"));
                string IplikAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikAdi"));
                bool Organik = Convert.ToBoolean(gridView1.GetRowCellValue(rowHandle, "Organik"));
                string Marka = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Marka"));
                string PartiNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "PartiNo"));
                int IplikRenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikRenkId"));
                string IplkiRenkKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkKodu"));
                string IplikRenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkAdi"));
                decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetKg"));
                string TakipNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "TakipNo"));


                stokListesi.Add($"{KalemIslem};{IplikKodu};{IplikId};{IplikAdi};{Organik};{Marka};{PartiNo};{IplikRenkId};{IplkiRenkKodu};" +
                    $"{IplikRenkAdi};{NetKg};{TakipNo}");
            }
            Close();
        }
    }
}