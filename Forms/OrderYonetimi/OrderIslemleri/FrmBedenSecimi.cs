using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmBedenSecimi : DevExpress.XtraEditors.XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        Listele listele = new Listele();
        Bildirim bildirim = new Bildirim();
        int _malzemeId;
        public List<int> SecilenIdler { get; private set; } = new List<int>();
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
                SecilenIdler.Clear();

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    var footage = gridView1.GetRowCellValue(i, "Footage");
                    if (footage != null && Convert.ToDecimal(footage) > 0)
                    {
                        var obj_id = gridView1.GetRowCellValue(i, "SizeId");
                        if (obj_id != null)
                        {
                            var values = new Dictionary<string, object>
                                        {
                                            { "Quantity", Convert.ToDecimal(footage) }
                                        };
                            crudRepository.Update("InventoryRequirement", obj_id, values);
                            SecilenIdler.Add(Convert.ToInt32(obj_id));
                        }
                    }
                }
                //MessageBox.Show(SecilenIdler.Count.ToString());
                this.Close();
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
        }
    }
}