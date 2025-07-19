using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmBedenSecimi : DevExpress.XtraEditors.XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        Listele listele = new Listele();
        Bildirim bildirim = new Bildirim();
        int _malzemeId;
        string _sizeText;
        public List<int> SecilenIdler { get; private set; } = new List<int>();
        public FrmBedenSecimi(int malzemeId)
        {
            InitializeComponent();
            _malzemeId = malzemeId;
            BaslangicVerileri();
        }
        public FrmBedenSecimi(string sizeText)
        {
            InitializeComponent();
            _sizeText = sizeText;
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
        string sql;
        void Listele()
        {
            if (_sizeText.Length > 0)
            {
                // Virgülle ayrılmış ID'leri diziye çevir ve SQL IN clause için formatla
                var sizeIds = _sizeText.Split(',')
                                      .Select(x => x.Trim())
                                      .Where(x => !string.IsNullOrEmpty(x));

                // IN clause için uygun format: '40','41','42'
                var formattedSizeIds = string.Join(",", sizeIds.Select(x => $"'{x}'"));

                        sql = $@"
                SELECT 
                    IR.Id [Id],
                    S.Id [SizeId],
                    S.SizeName [Size],
                    IR.Quantity [Footage] 
                FROM InventoryRequirement IR 
                LEFT JOIN Inventory I ON I.Id = IR.InventoryId
                LEFT JOIN Size S ON S.Id = IR.SizeId
                WHERE S.Id IN ({formattedSizeIds})";
                    }
                    else
                    {
                        sql = $@"
                SELECT 
                    IR.Id [Id],
                    S.Id [SizeId],
                    S.SizeName [Size],
                    IR.Quantity [Footage] 
                FROM InventoryRequirement IR 
                LEFT JOIN Inventory I ON I.Id = IR.InventoryId
                LEFT JOIN Size S ON S.Id = IR.SizeId
                WHERE IR.InventoryId = {_malzemeId}";
            }

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
                        var obj_id = gridView1.GetRowCellValue(i, "Id");
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