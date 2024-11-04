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

namespace Hesap.Forms.TeknikDestek
{
    public partial class FrmDepartmanListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmDepartmanListesi()
        {
            InitializeComponent();
        }
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public string Departman;
        void Listele()
        {
            string sql = "SELECT distinct Departman [Departman] FROM Talepler";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
        }
        private void FrmDepartmanListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Departman = gridView.GetFocusedRowCellValue("Departman").ToString();
            this.Close();
        }
    }
}