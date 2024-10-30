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
    public partial class FrmTasiyiciKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public int Id;
        public string Unvan, Ad, Soyad, TC, Plaka, Dorse;

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kartlar.FrmTasiyiciKarti frm = new Kartlar.FrmTasiyiciKarti();
            frm.ShowDialog();
        }

        public FrmTasiyiciKartiListesi()
        {
            InitializeComponent();
        }

        private void FrmTasiyiciKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = "SELECT * FROM TasiyiciKarti";
            listele.Liste(sql, gridControl1);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Unvan = gridView.GetFocusedRowCellValue("Unvan").ToString();
            Ad = gridView.GetFocusedRowCellValue("Ad").ToString();
            Soyad = gridView.GetFocusedRowCellValue("Soyad").ToString();
            TC = gridView.GetFocusedRowCellValue("TC").ToString();
            Plaka = gridView.GetFocusedRowCellValue("Plaka").ToString();
            Dorse = gridView.GetFocusedRowCellValue("Dorse").ToString();
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}