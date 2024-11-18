using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using FastReport.DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.UretimYonetimi.Barkod
{
    public partial class FrmSiparisSecimi : DevExpress.XtraEditors.XtraForm
    {
        public FrmSiparisSecimi()
        {
            InitializeComponent();
        }

        private void FrmSiparisSecimi_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=192.168.1.65,1433;Database=ExtremeNakosan;User Id=sa;Password=aaBB123@;";
            string sorgu = $@"select 
isnull(W.WorkOrderNo,0) [OrderNo]
,isnull(I.InventoryName,'') [ModelAdi]
,isnull(WOIV.Explanation,'') [PartOrderNo]
,isnull(WI.OperationCode,'') [Varyant1]
,dbo.SubNoToSizeCode(W.RecId,W.SText,WOIV.SubNo) [Ebat] 
,isnull(WOIV.Barcode,0) [Barkod]
,isnull(WOIV.Quantity,0) [Adet]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker1+'#@#',WOIV.SubNo)[Sticker1]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker2+'#@#',WOIV.SubNo)[Sticker2]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker3+'#@#',WOIV.SubNo)[Sticker3]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker4+'#@#',WOIV.SubNo)[Sticker4]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker5+'#@#',WOIV.SubNo)[Sticker5]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker6+'#@#',WOIV.SubNo)[Sticker6]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker7+'#@#',WOIV.SubNo)[Sticker7]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker8+'#@#',WOIV.SubNo)[Sticker8]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker9+'#@#',WOIV.SubNo)[Sticker9]
,dbo.SubNoToSizeCode(0,WI.UD_CSSticker10+'#@#',WOIV.SubNo)[Sticker10]
 from MA_WorkOrder W with (nolock)
 left join IM_Item I with (nolock) on W.InventoryId = I.RecId
left join MA_WorkOrderItem WI with (nolock) on WI.WorkOrderId = W.RecId
left join MA_WorkOrderItemVariant WOIV with(nolock) on WI.RecId = WOIV.WorkOrderItemId
LEFT JOIN MA_RecipeItem FRI with (nolock) ON FRI.RecId=(SELECT TOP 1 RecId from MA_RecipeItem with (nolock) WHERE OwnerInventoryId=I.RecId AND ItemType=1  AND IsMaster=1) --ItemType=1 kumaş ıs master=1 ana kuması veriyor.
LEFT JOIN IM_Item F with (nolock) on F.RecId=FRI.InventoryId
LEFT JOIN MA_RecipeItem KRI with (nolock) ON KRI.RecId=(select top 1 RecId from MA_RecipeItem RI with (nolock) where InventoryId in (select RecId from IM_Item with (nolock) where TrimClassCardId = 74) and OwnerInventoryId = I.RecId)
left join MA_RecipeItemSizeQuantity RISQ with (nolock) on RISQ.RecipeItemId = KRI.RecId and RISQ.SizeItemNo = WOIV.SubNo - 1
LEFT JOIN MA_RecipeItem PRI with (nolock) ON PRI.RecId=(select top 1 RecId from MA_RecipeItem RI with (nolock) where InventoryId in (select RecId from IM_Item with (nolock) where TrimClassCardId = 80) and OwnerInventoryId = I.RecId)
left join MA_RecipeItemSizeQuantity PRISQ with (nolock) on PRISQ.RecipeItemId = PRI.RecId and PRISQ.SizeItemNo = WOIV.SubNo - 1
--reçete model kumas kartı
where /* W.WorkOrderNo='EIH24-0085-01'and*/
WI.WorkOrderSubType = 2 and WI.ParentItemId is null
and WOIV.SubNo < 1000 and  W.CompanyId = 1 --_ActiveCompanyId_  
and (W.WorkOrderNo like 'EIH%' or W.WorkOrderNo like 'TR%') and WOIV.Barcode is not null and WOIV.Quantity > 0  order by W.WorkOrderNo

--MA_WorkOrder_SQLPLACEHOLDER_W

"; try
            {
                // SqlConnection ile veritabanı bağlantısını aç
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SqlCommand ile sorguyu hazırla
                    SqlCommand command = new SqlCommand(sorgu, connection);

                    // SqlDataAdapter ile veri alımı
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                    // Veriyi alacağımız DataTable
                    DataTable dataTable = new DataTable();

                    // Veriyi DataTable'a doldur
                    dataAdapter.Fill(dataTable);

                    // GridControl'a veri yansıt (örneğin DataGridView kullanıyoruz)
                    // Eğer GridControl kullanıyorsanız, onu da bağlayabilirsiniz.
                    gridControl1.DataSource = dataTable; // DataGridView'i kullanıyorsanız

                    // Eğer GridControl kullanıyorsanız, onu şu şekilde yapabilirsiniz:
                    // gridControl1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        public List<string> itemList = new List<string>();
        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int rowHandle in selectedRows)
            {
                string PartOrderNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "PartOrderNo"));
                string OrderNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "OrderNo"));
                string UrunKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "ModelAdi")); // ürün adi
                string Sticker1 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker1"));
                string Sticker2 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker2"));
                string Sticker3 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker3"));
                string Sticker4 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker4"));
                string Sticker5 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker5"));
                string Sticker6 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker6"));
                string Sticker7 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker7"));
                string Sticker8 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker8"));
                string Sticker9 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker9"));
                string Sticker10 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Sticker10"));
                string Barkod = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Barkod"));
                string EbatBeden = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Ebat"));
                string Varyant1 = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Varyant1"));
                int Miktar = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Adet"));

                itemList.Add($"{PartOrderNo};{OrderNo};{UrunKodu};{Sticker1};{Sticker2};{Sticker3};{Sticker4};{Sticker5};{Sticker6};{Sticker7};{Sticker8};{Sticker9};{Sticker10};{Barkod};" +
                    $"{EbatBeden};{Varyant1};{Miktar}");
            }
            Close();
        }
    }
}