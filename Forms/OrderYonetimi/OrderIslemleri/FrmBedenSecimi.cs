using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmBedenSecimi : DevExpress.XtraEditors.XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        Listele listele = new Listele();
        Bildirim bildirim = new Bildirim();
        int _malzemeId;
        public FrmBedenSecimi(int malzemeId)
        {
            InitializeComponent();
            _malzemeId = malzemeId;
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            gridControl1.DataSource = new BindingList<InventoryRequirement>();
        }
        private void FrmBedenSecimi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        void Listele()
        {
            string sql = $@"
                        select 
                        IR.Id [Id],
                        S.Id [SizeId],
                        S.SizeName [Size],
                        IR.Quantity [Footage] from InventoryRequirement IR 
                        left join Inventory I on I.Id = IR.InventoryId
                        left join Size S on S.Id = IR.SizeId
                        where IR.InventoryId = {_malzemeId} ";
            listele.Liste(sql, gridControl1);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }
        void Kaydet()
        {
            try
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.GetRowCellValue(i, "Size") != null)
                    {
                        var obj_id = gridView1.GetRowCellValue(i, "Id");
                        var values = new Dictionary<string, object>
                    {
                        {"Quantity", Convert.ToDecimal(gridView1.GetRowCellValue(i,"Footage")) }
                    };
                        crudRepository.Update("InventoryRequirement", obj_id, values);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
        }
    }
}