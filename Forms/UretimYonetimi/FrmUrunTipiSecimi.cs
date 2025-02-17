using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmUrunTipiSecimi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        Bildirim bildirim = new Bildirim();
        CrudRepository crudRepository = new CrudRepository();
        public FrmUrunTipiSecimi()
        {
            InitializeComponent();
        }
        public string OnEk, yeniNumaraStr;
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
            var TypeParams = new Dictionary<string, object>
            {
                {"InventoryCode", textEdit1.Text.ToUpper() + "000"}, 
                {"SubType",textEdit1.Text.ToUpper() + "000"}, 
                {"InventoryName",textEdit2.Text}, 
                {"Unit",""},
                {"IsPrefix",true},
                {"Type" , InventoryTypes.Kumas}
            };
            string prefix = textEdit1.Text.ToUpper().Substring(0, 3);
            int count = crudRepository.GetCountByPrefix("Inventory", "SubType", prefix);
            if (count == 0)
            {
                crudRepository.Insert("Inventory", TypeParams);
            }
            else
            {
                bildirim.Uyari($"{prefix} için daha önce kayıt yapılmış.");
            }
            Listele();
        }

        void Listele()
        {
            string sql = @"WITH CTE AS (
                        SELECT 
                            LEFT(InventoryCode, 3) AS Ek,
                            RIGHT(InventoryCode, 3) AS Numara,
                            ROW_NUMBER() OVER (PARTITION BY LEFT(InventoryCode, 3) ORDER BY Id DESC) AS rn
                        FROM 
                            Inventory
						--where IsPrefix = 1
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