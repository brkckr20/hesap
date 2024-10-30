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

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmIslemBekleyenler : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public FrmIslemBekleyenler()
        {
            InitializeComponent();
        }

        private void FrmIslemBekleyenler_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT 
                        d1.Tarih,
                        d1.FirmaKodu,
                        d1.FirmaUnvani,
                        d1.DepoId,
                        d2.KalemIslem,
                        d2.MalzemeKodu,
                        d2.MalzemeAdi,
                        ISNULL(SUM(d2.Miktar) - COALESCE((
                            SELECT SUM(y.Miktar)
                            FROM MalzemeDepo1 x
                            INNER JOIN MalzemeDepo2 y ON x.Id = y.RefNo
                            WHERE x.IslemCinsi = 'Giriş' AND d2.Id = y.TakipNo
                        ), 0), 0) AS Kalan,
                        d2.Birim,
                        d2.RefNo AS KayitNo,
                        d2.Id [TakipNo]
                    FROM MalzemeDepo1 d1
                    LEFT JOIN MalzemeDepo2 d2 ON d1.Id = d2.RefNo
					inner join MalzemeKarti MK on MK.Kodu = d2.MalzemeKodu
                    WHERE d1.IslemCinsi = 'Çıkış' AND d2.KalemIslem IN ('Dolum','Tamir') and MK.Tip <> 1
                    GROUP BY
                        d1.Tarih,
                        d1.FirmaKodu,
                        d1.FirmaUnvani,
                        d1.DepoId,
                        d2.KalemIslem,
                        d2.MalzemeKodu,
                        d2.MalzemeAdi,
                        d2.Birim,
                        d2.RefNo,
                        d2.TakipNo,
						d2.Id
                    HAVING ISNULL(SUM(d2.Miktar) - COALESCE((
                        SELECT SUM(y.Miktar)
                        FROM MalzemeDepo1 x
                        INNER JOIN MalzemeDepo2 y ON x.Id = y.RefNo
                        WHERE x.IslemCinsi = 'Giriş' AND d2.Id = y.TakipNo
                    ), 0), 0) <> 0;";
            listele.Liste(sql, gridControl1);
        }
        public List<string> malzemeBilgileri = new List<string>();

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int rowHandle in selectedRows)
            {
                string KalemIslem = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KalemIslem"));
                string MalzemeKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "MalzemeKodu"));
                string MalzemeAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "MalzemeAdi"));
                string birim = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Birim"));
                int TakipNo = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "TakipNo"));
                malzemeBilgileri.Add($"{KalemIslem};{MalzemeKodu};{MalzemeAdi};{birim};{TakipNo}");
            }
            Close();
        }
    }
}