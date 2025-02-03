using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Linq;

namespace Hesap.Forms.Liste
{
    public partial class FrmKullaniciListesi : DevExpress.XtraEditors.XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        string TableName = "Users";
        public FrmKullaniciListesi()
        {
            InitializeComponent();
        }
        public string Kodu, Ad, Sifre, Soyad;
        public int Id;
        public bool Kullanimda;

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void excelOlarakAktarxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Kullanıcı Listesi");
        }

        private void FrmKullaniciListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            gridControl1.DataSource = crudRepository.GetAll<User>(this.TableName).ToList();
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
        
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Kodu = gridView.GetFocusedRowCellValue("Code").ToString();
            Ad = gridView.GetFocusedRowCellValue("Name").ToString();
            Sifre = gridView.GetFocusedRowCellValue("Password").ToString();
            Soyad = gridView.GetFocusedRowCellValue("Surname").ToString();
            Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("IsUse"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}