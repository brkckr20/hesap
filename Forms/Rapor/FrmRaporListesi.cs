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

namespace Hesap.Forms.Rapor
{
    public partial class FrmRaporListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmRaporListesi()
        {
            InitializeComponent();
        }
        Listele listele = new Listele();
        public int Id;
        public string RaporAdi,EkranAdi, Sorgu1, Sorgu2, Sorgu3, Sorgu4, Sorgu5, Sorgu6, Sorgu7, Sorgu8, Sorgu9, FormGrubu;
        private void FrmRaporListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = "SELECT * FROM Rapor";
            listele.Liste(sql, gridControl1);
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            RaporAdi = gridView.GetFocusedRowCellValue("RaporAdi").ToString();
            EkranAdi = gridView.GetFocusedRowCellValue("FormAdi").ToString();
            Sorgu1 = gridView.GetFocusedRowCellValue("Sorgu1").ToString();
            Sorgu2 = gridView.GetFocusedRowCellValue("Sorgu2").ToString();
            Sorgu3 = gridView.GetFocusedRowCellValue("Sorgu3").ToString();
            Sorgu4 = gridView.GetFocusedRowCellValue("Sorgu4").ToString();
            Sorgu5 = gridView.GetFocusedRowCellValue("Sorgu5").ToString();
            Sorgu6 = gridView.GetFocusedRowCellValue("Sorgu6").ToString();
            Sorgu7 = gridView.GetFocusedRowCellValue("Sorgu7").ToString();
            Sorgu8 = gridView.GetFocusedRowCellValue("Sorgu8").ToString();
            Sorgu9 = gridView.GetFocusedRowCellValue("Sorgu9").ToString();
            FormGrubu = gridView.GetFocusedRowCellValue("FormGrubu").ToString();
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("KayitNo"));
            this.Close();
        }
    }
}