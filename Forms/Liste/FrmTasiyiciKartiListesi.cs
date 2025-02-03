using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
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
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public int Id;
        public string Unvan, Ad, Soyad, TC, Plaka, Dorse;
        public bool Kullanimda;
        private string TableName = "Transporter";

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void satırİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
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
            gridControl1.DataSource = crudRepository.GetAll<Transporter>(this.TableName).ToList();
            crudRepository.GetUserColumns(gridView1,this.Text);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Unvan = gridView.GetFocusedRowCellValue("Title").ToString();
            Ad = gridView.GetFocusedRowCellValue("Name").ToString();
            Soyad = gridView.GetFocusedRowCellValue("Surname").ToString();
            TC = gridView.GetFocusedRowCellValue("TCKN").ToString();
            Plaka = gridView.GetFocusedRowCellValue("NumberPlate").ToString();
            Dorse = gridView.GetFocusedRowCellValue("TrailerNumber").ToString();
            Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("IsUse"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}