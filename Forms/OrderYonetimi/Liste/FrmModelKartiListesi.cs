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

namespace Hesap.Forms.OrderYonetimi.Liste
{
    public partial class FrmModelKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmModelKartiListesi()
        {
            InitializeComponent();
        }
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public int Id;
        public string Kodu,Adi, OrjAdi;

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Adi = gridView.GetFocusedRowCellValue("ModelAdi").ToString();
            OrjAdi = gridView.GetFocusedRowCellValue("OrjModelAdi").ToString();
            Kodu = gridView.GetFocusedRowCellValue("ModelKodu").ToString();
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }

        private void FrmModelKartiListesi_Load(object sender, EventArgs e)
        {
            string sql = $"select ModelKodu,ModelAdi,OrjModelAdi,Id from ModelKarti where PazarlamaciId = '{Properties.Settings.Default.Id}'";
            listele.Liste(sql, gridControl1);
        }
    }
}