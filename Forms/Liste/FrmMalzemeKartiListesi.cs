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
        public FrmMalzemeKartiListesi()
        {
            InitializeComponent();
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
            string sql = @"SELECT *,case 
			                when Tip = 0 then 'Malzeme'
			                when Tip = 1 then 'Hizmet'
			                when Tip = 2 then 'Sabit Kıymet'
			                end as 'Tip Adı'
			                FROM MalzemeKarti";
            listele.Liste(sql, gridControl1);
        }

        public int Id;
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Kodu = gridView.GetFocusedRowCellValue("Kodu").ToString();
            Adi = gridView.GetFocusedRowCellValue("Adi").ToString();
            GrupKodu = gridView.GetFocusedRowCellValue("GrupKodu").ToString();
            Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kullanimda"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}