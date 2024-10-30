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
    public partial class FrmMarkaSecimListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        string _tabloAdi;
        public string _marka;
        public FrmMarkaSecimListesi(string tabloAdi)
        {
            InitializeComponent();
            this._tabloAdi = tabloAdi;
        }

        private void FrmMarkaSecimListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = $"select distinct ISNULL(Marka,'')  [Marka] from {_tabloAdi} where ISNULL(Marka,'') <> ''";
            listele.Liste(sql, gridControl1);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            _marka = gridView.GetFocusedRowCellValue("Marka").ToString();
            this.Close();
        }
    }
}