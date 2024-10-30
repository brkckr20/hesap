using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmUrunTipiSecimi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        Bildirim bildirim = new Bildirim();
        public FrmUrunTipiSecimi()
        {
            InitializeComponent();
        }
        public string OnEk,yeniNumaraStr;
        private void FrmUrunTipiSecimi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            OnEk = gridView.GetFocusedRowCellValue("Ek").ToString();
            string strNum = gridView.GetFocusedRowCellValue("YeniNumara").ToString();
            int num = Convert.ToInt32(strNum);
            yeniNumaraStr = num.ToString("D3");
            this.Close();
        }

        private void btnListeyeEkle_Click(object sender, EventArgs e)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string prefix = textEdit1.Text.ToUpper().Substring(0, 3);
                string checkQuery = "SELECT COUNT(*) FROM UrunKarti WHERE UrunKodu LIKE @Prefix";
                int count = connection.ExecuteScalar<int>(checkQuery, new { Prefix = prefix + "%" });

                if (count == 0)
                {
                    string insertQuery = "INSERT INTO UrunKarti (UrunKodu, Pasif) VALUES (@UrunKodu, @Pasif)";
                    connection.Execute(insertQuery, new { UrunKodu = textEdit1.Text.ToUpper() + "000", Pasif = true });
                }
                else
                {
                    bildirim.Uyari($"{prefix} için daha önce kayıt yapılmış.");
                }
                Listele();
            }
        }

        void Listele()
        {
            string sql = @"WITH CTE AS (
                        SELECT 
                            LEFT(UrunKodu, 3) AS Ek,
                            RIGHT(UrunKodu, 3) AS Numara,
                            ROW_NUMBER() OVER (PARTITION BY LEFT(UrunKodu, 3) ORDER BY Id DESC) AS rn
                        FROM 
                            UrunKarti
                    )
                    SELECT 
                        Ek,
                        RIGHT('000' + CAST(CAST(Numara AS INT) + 1 AS VARCHAR(3)), 3) AS YeniNumara

                    FROM 
                        CTE
                    WHERE 
                        rn = 1;";
            listele.Liste(sql, gridControl1);
        }
    }
}