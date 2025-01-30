using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
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
    public partial class FrmDepoKartiListesi : XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        string TableName = "WareHouse";
        public FrmDepoKartiListesi()
        {
            InitializeComponent();
        }
        public string Kodu, Adi;
        public int Id;
        public bool Kullanimda;

        private void FrmDepoKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            gridControl1.DataSource = crudRepository.GetAll<WareHouse>(this.TableName).ToList();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Kodu = gridView.GetFocusedRowCellValue("Code").ToString();
            Adi = gridView.GetFocusedRowCellValue("Name").ToString();
            Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("IsUse"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}