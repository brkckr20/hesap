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
    public partial class FrmIplikKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public string IplikKodu, IplikAdi, IplikNo, IplikNoAciklama, IplikCinsi, IplikCinsiAciklama;
        public int Id;
        public bool Organik;
        public FrmIplikKartiListesi()
        {
            InitializeComponent();
        }

        private void FrmIplikKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = @"SELECT 
                            IK.Id AS [Id],
                            IK.IplikKodu AS [IplikKodu],
                            IK.IplikAdi AS [IplikAdi],
                            (SELECT OK.Id FROM OzellikKodlama OK WHERE IK.IplikNo = OK.Id) AS [IplikNo],
                            (SELECT OK.Aciklama FROM OzellikKodlama OK WHERE IK.IplikNo = OK.Id) AS [IplikNoAciklama],
                            (SELECT OK.Id FROM OzellikKodlama OK WHERE IK.IplikCinsi = OK.Id) AS [IplikCinsi],
                            (SELECT OK.Aciklama FROM OzellikKodlama OK WHERE IK.IplikCinsi = OK.Id) AS [IplikCinsiAciklama],
	                        IK.Numara [Numara],
	                        IK.Organik [Organik]
                        FROM 
                            IplikKarti IK";
            listele.Liste(sql, gridControl1);
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            IplikKodu = gridView.GetFocusedRowCellValue("IplikKodu").ToString();
            IplikAdi = gridView.GetFocusedRowCellValue("IplikAdi").ToString();
            IplikNo = gridView.GetFocusedRowCellValue("IplikNo").ToString();
            IplikNoAciklama = gridView.GetFocusedRowCellValue("IplikNoAciklama").ToString();
            IplikCinsi = gridView.GetFocusedRowCellValue("IplikCinsi").ToString();
            IplikCinsiAciklama = gridView.GetFocusedRowCellValue("IplikCinsiAciklama").ToString();
            Organik = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Organik"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}