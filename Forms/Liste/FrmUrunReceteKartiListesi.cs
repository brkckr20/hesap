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
    public partial class FrmUrunReceteKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public int Id;
        public float Gr_M2, HamEn, HamBoy, MamulEn, MamulBoy;
        public bool IpligiBoyali;
        public string ReceteNo;
        private string _UrunKodu;
        public FrmUrunReceteKartiListesi(string UrunKodu)
        {
            InitializeComponent();
            this._UrunKodu = UrunKodu;
        }
        private void FrmUrunReceteKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = $"select * from UrunRecete where UrunKodu = @UrunKodu";
            listele.ListeWithParams(sql, gridControl1,_UrunKodu);
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            ReceteNo = gridView.GetFocusedRowCellValue("ReceteNo").ToString();
            Gr_M2 = Convert.ToSingle(gridView.GetFocusedRowCellValue("Gr_M2"));
            HamEn = Convert.ToSingle(gridView.GetFocusedRowCellValue("HamEn"));
            HamBoy = Convert.ToSingle(gridView.GetFocusedRowCellValue("HamBoy"));
            MamulEn = Convert.ToSingle(gridView.GetFocusedRowCellValue("MamulEn"));
            MamulBoy = Convert.ToSingle(gridView.GetFocusedRowCellValue("MamulBoy"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}