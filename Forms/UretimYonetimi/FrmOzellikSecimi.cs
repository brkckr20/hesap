using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmOzellikSecimi : XtraForm
    {
        string _kullaniciSecimi, _ekranAdi, TableName = "FeatureCoding";
        public string aciklama, id, islemTipi;
        Bildirim bildirim = new Bildirim();
        CrudRepository crudRepository = new CrudRepository();
        int InventoryType = Convert.ToInt32(InventoryTypes.Kumas); // isteğe göre düzenlenebilir. - şuan Kumaş olarak gidiyor.
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            id = gridView1.GetFocusedRowCellValue("Id").ToString();
            aciklama = gridView1.GetFocusedRowCellValue("Explanation").ToString();
            Close();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            crudRepository.ConfirmAndDeleteCard(TableName, Id, Yukle);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }

        private void sütunSeçitimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            var _params = new Dictionary<string, object>
            {
                { "Type", _kullaniciSecimi },{ "Explanation", txtAciklama.Text },{ "PlaceOfUse", InventoryType.ToString() }, {"UsageScreen", _ekranAdi}
            };

            bool exist = crudRepository.IfExistRecord(TableName, "Explanation", txtAciklama.Text) > 0;
            if (!exist)
            {
                crudRepository.Insert(this.TableName, _params);
                Yukle();
                txtAciklama.Text = "";
            }
            else
            {
                bildirim.Uyari($"{txtAciklama.Text} daha önce kayıt edilmiş. Aynı kaydı tekrar yapmazsınız!");
            }
        }
        public FrmOzellikSecimi(string kullaniciSecimi, string ekranAdi)
        {
            InitializeComponent();
            _kullaniciSecimi = kullaniciSecimi;
            _ekranAdi = ekranAdi;
            this.Text = "Özellik Seçimi [ " + _kullaniciSecimi.Trim() + " ]";
        }
        private void FrmOzellikSecimi_Load(object sender, EventArgs e)
        {
            Yukle();
        }
        void Yukle()
        {
            gridControl1.DataSource = crudRepository.GetAll<FeatureCoding>(this.TableName).Where(fc => fc.PlaceOfUse == InventoryType.ToString() && fc.UsageScreen == _ekranAdi && fc.Type == _kullaniciSecimi);
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
    }
}