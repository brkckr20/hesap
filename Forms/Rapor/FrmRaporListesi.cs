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
    public partial class FrmRaporListesi : XtraForm
    {
        public FrmRaporListesi(bool etiketMi = false)
        {
            InitializeComponent();
            EtiketMi = etiketMi;
        }
        Listele listele = new Listele();
        public int Id;
        public string RaporAdi,EkranAdi, Sorgu1, Sorgu2, Sorgu3, Sorgu4, Sorgu5, Sorgu6, Sorgu7, Sorgu8, Sorgu9, FormGrubu;
        bool EtiketMi = false;
        private void FrmRaporListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql;
            if (EtiketMi)
            {
                sql = "SELECT * FROM Rapor where FormAdi = 'Etiket Basımı'";
                listele.Liste(sql, gridControl1);

            }
            else
            {
                sql = "SELECT * FROM Report";
            listele.Liste(sql, gridControl1);
            }
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            RaporAdi = gridView.GetFocusedRowCellValue("ReportName").ToString();
            EkranAdi = gridView.GetFocusedRowCellValue("FormName").ToString();
            Sorgu1 = gridView.GetFocusedRowCellValue("Query1").ToString();
            Sorgu2 = gridView.GetFocusedRowCellValue("Query2").ToString();
            Sorgu3 = gridView.GetFocusedRowCellValue("Query3").ToString();
            Sorgu4 = gridView.GetFocusedRowCellValue("Query4").ToString();
            FormGrubu = gridView.GetFocusedRowCellValue("FormGroup").ToString();
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}