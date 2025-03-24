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
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Ayarlar ayarlar = new Ayarlar();
        public FrmUrunTipiSecimi()
        {
            InitializeComponent();
        }
        public string OnEk, yeniNumaraStr, KumasAdiOzellik;
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
            KumasAdiOzellik = gridView.GetFocusedRowCellValue("KumasAdiOzellik").ToString();
            this.Close();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void btnListeyeEkle_Click(object sender, EventArgs e)
        {
            var TypeParams = new Dictionary<string, object>
            {
                {"InventoryCode", textEdit1.Text.ToUpper() + "000"},
                {"SubType",textEdit2.Text},
                {"InventoryName",""},
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
            string sql = $@"WITH CTE AS (
                        SELECT 
                            LEFT(InventoryCode, 3) AS Ek,
                            RIGHT(InventoryCode, 3) AS Numara,
							ISNULL(SubType,'') [KumasAdiOzellik],
                            ROW_NUMBER() OVER (PARTITION BY LEFT(InventoryCode, 3) ORDER BY Id DESC) AS rn
                        FROM 
                            Inventory
						where Type = {Convert.ToInt32(InventoryTypes.Kumas)}
                    )
                    SELECT 
                        Ek,
                        RIGHT('000' + CAST(CAST(Numara AS INT) + 1 AS VARCHAR(3)), 3) AS YeniNumara,
						KumasAdiOzellik

                    FROM 
                        CTE
                    WHERE 
                        rn = 1;";
            //            string sqlite = @"
            //                    WITH CTE AS (
            //                    SELECT 
            //                        substr(InventoryCode, 1, 3) AS Ek,  -- LEFT(InventoryCode, 3)
            //                        substr(InventoryCode, -3) AS Numara,  -- RIGHT(InventoryCode, 3)
            //                        COALESCE(SubType, '') AS KumasAdiOzellik,  -- ISNULL yerine COALESCE kullanıldı
            //                        (SELECT COUNT(*) 
            //                         FROM Inventory AS I 
            //                         WHERE substr(I.InventoryCode, 1, 3) = substr(Inventory.InventoryCode, 1, 3) 
            //                         AND I.Id >= Inventory.Id) AS rn
            //                    FROM 
            //                        Inventory
            //                )
            //                SELECT 
            //                    Ek,
            //                    printf('%03d', CAST(Numara AS INT) + 1) AS YeniNumara,  -- RIGHT('000' + CAST(CAST(Numara AS INT) + 1 AS VARCHAR(3)), 3)
            //                    KumasAdiOzellik
            //                FROM 
            //                    CTE
            //                WHERE 
            //                    rn = 1;
            //";
            //            if (ayarlar.VeritabaniTuru() != "sqlite")
            //            {
            //                listele.Liste(sqlite, gridControl1);
            //                crudRepository.GetUserColumns(gridView1, this.Text);
            //            }
            //            else
            //            {
            listele.Liste(sql, gridControl1);
            crudRepository.GetUserColumns(gridView1, this.Text);
            //}

        }
    }
}