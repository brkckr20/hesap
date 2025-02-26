using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
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
    public partial class FrmMaliyetHesaplamaListesi :XtraForm
    {
        Listele listele = new Listele();
        string TableName1 = "Cost", TableName2 = "CostProductionInformation", TableName3 = "CostProductionCalculate", TableName4 = "CostCostCalculate";
        CrudRepository crudRepository = new CrudRepository();
        public FrmMaliyetHesaplamaListesi()
        {
            InitializeComponent();
        }

        private void FrmMaliyetHesaplamaListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            //var costList = crudRepository.GetAll<Cost>(TableName1).ToList();
            //var companyList = crudRepository.GetAll<Company>("Company").ToList();
            //var result = from cost in costList 
            //             join company in companyList on cost.CompanyId equals company.Id
            //             select new { 
            //                 cost.Id,cost.Date,cost.OrderNo,cost.InventoryId
            //                ,company.CompanyName,company.CompanyCode };
            //gridControl1.DataSource = result.ToList();
            //gridControl1.DataSource = crudRepository.GetAll(TableName1); // tablolar model ile oluşturulup eklenebilir.
            //            string sql = @"SELECT co.Id,
            //	   co.CompanyId [Firma Id]
            //      ,co.Date [Tarih]
            //      ,co.InventoryId [Malzeme Id] -- alan eklenince silinecek
            //      ,co.RecipeId [Reçete Id] -- alan eklenince silinecek
            //      ,co.OrderNo [Sipariş No]
            //      ,co.ProductImage [Ürün Resmi]
            //	  ,c.CompanyName [Firma Adı]
            //	  ,c.CompanyCode [Firma Kodu]
            //	  -- üretim bilgileri -- iplik bilgileri
            //	  ,CPI.YI_Warp1 [Çözgü 1 İp.Bil.],CPI.YI_Warp1Divider [Çözgü 1 Bölen İp.Bil.],CPI.YI_Warp1Result [Çözgü 1 Sonuç İp.Bil.]
            //	  ,CPI.YI_Warp2 [Çözgü 2 İp.Bil.],CPI.YI_Warp2Divider [Çözgü 2 Bölen İp.Bil.],CPI.YI_Warp2Result [Çözgü 2 Sonuç İp.Bil.]
            //	  ,CPI.YI_Scarf1 [Atkı 1 İp.Bil.],CPI.YI_Scarf1Divider [Atkı 1 Bölen İp.Bil.],CPI.YI_Scarf1Result [Atkı 1 Sonuç İp.Bil.]
            //	  ,CPI.YI_Scarf2 [Atkı 2 İp.Bil.],CPI.YI_Scarf2Divider [Atkı 2 Bölen İp.Bil.],CPI.YI_Scarf2Result [Atkı 2 Sonuç İp.Bil.]
            //	  ,CPI.YI_Scarf3 [Atkı 3 İp.Bil.],CPI.YI_Scarf3Divider [Atkı 3 Bölen İp.Bil.],CPI.YI_Scarf3Result [Atkı 3 Sonuç İp.Bil.]
            //	  ,CPI.YI_Scarf4 [Atkı 4 İp.Bil.],CPI.YI_Scarf4Divider [Atkı 4 Bölen İp.Bil.],CPI.YI_Scarf3Result [Atkı 4 Sonuç İp.Bil.]
            //	  ,CPI.WI_CombNo1 [Tar.No 1],CPI.WI_CombNo1Multiplier [Tar.No 1 Çarpan],CPI.WI_CombNo1Result [Tar.No 1 Sonuç]
            //	  ,CPI.WI_CombNo2 [Tar.No 2],CPI.WI_CombNo2Multiplier [Tar.No 2 Çarpan],CPI.WI_CombNo2Result [Tar.No 1 Sonuç]
            //	  ,CPI.WI_CombWidth [Tar. En],CPI.WI_RawHeight [Ham Boy],CPI.WI_HeightEaves [Boy Saçak],CPI.WI_WidthEaves [En Saçak],CPI.WI_RawWidth [Ham En],CPI.WI_ProductHeight [Mamul Boy],CPI.WI_ProductWidth [Mamul En]
            //	  ,CPI.D_Warp1 [Çözgü 1 Sıklık],CPI.D_Warp2 [Çözgü 2 Sıklık],CPI.D_Scarf1 [Atkı 1 Sıklık],CPI.D_Scarf2 [Atkı 2 Sıklık],CPI.D_Scarf3 [Atkı 3 Sıklık],CPI.D_Scarf3 [Atkı 4 Sıklık]
            //	  ,CPI.NW_Warp1 [Çözgü 1 Tel.Say],CPI.NW_Warp2 [Çözgü 2 Tel.Say],CPI.NW_Scarf1 [Atkı 1 Tel.Say],CPI.NW_Scarf2 [Atkı 2 Tel.Say],CPI.NW_Scarf3 [Atkı 3 Tel.Say],CPI.NW_Scarf4 [Atkı 4 Tel.Say]
            //	  --,CPI.*
            //  FROM Cost co inner join Company c on co.CompanyId = c.Id
            //  left join CostProductionInformation CPI on co.Id = CPI.CostId
            //";
            string sql = "select * from MaliyetCalismaListesi";
            listele.Liste(sql, gridControl1);
        }

        public List<Dictionary<string, object>> veriler = new List<Dictionary<string, object>>();
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null)
                return;
            int secilenId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            veriler.Clear();
            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                int id = Convert.ToInt32(gridView.GetRowCellValue(i, "Id"));

                if (id == secilenId)
                {
                    var rowData = new Dictionary<string, object>();
                    foreach (GridColumn column in gridView.Columns)
                    {
                        var columnName = column.FieldName;
                        var cellValue = gridView.GetRowCellValue(i, columnName);
                        rowData[columnName] = cellValue;
                    }
                    veriler.Add(rowData);
                }
            }
            Close();
        }
    }
}