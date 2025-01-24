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

namespace Hesap.Forms.Liste
{
    public partial class FrmMalzemeKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        int _Type;
        public FrmMalzemeKartiListesi(int type)
        {
            InitializeComponent();
            _Type = type;
        }
        Listele listele = new Listele();


        public string Kodu, Adi, GrupKodu;
        public bool Kullanimda;

        private void FrmMalzemeKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = $@"SELECT 
                        Id,
	                    ISNULL(InventoryCode,'') [Kodu]
	                    ,ISNULL(InventoryName,'') [Adi]
                    FROM Inventory where Type = {this._Type}";
            listele.Liste(sql, gridControl1);
        }

        public int Id;
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Kodu = gridView.GetFocusedRowCellValue("Kodu").ToString();
            Adi = gridView.GetFocusedRowCellValue("Adi").ToString();
            Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kullanimda"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}