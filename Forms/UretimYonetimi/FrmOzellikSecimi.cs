using Dapper;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Doc;
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

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmOzellikSecimi : XtraForm
    {
        string _kullaniciSecimi, KullanimYeri = "Kumaş", _ekranAdi, TableName = "FeatureCoding";
        public string aciklama, id, islemTipi;
        Bildirim bildirim = new Bildirim();
        CrudRepository crudRepository = new CrudRepository();
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            var _params = new Dictionary<string, object>
            {
                { "Type", _kullaniciSecimi },{ "Explanation", txtAciklama.Text },{ "PlaceOfUse", KullanimYeri }, {"UsageScreen", _ekranAdi}
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
            gridControl1.DataSource = crudRepository.GetAll<FeatureCoding>(this.TableName).Where(fc => fc.PlaceOfUse == KullanimYeri && fc.UsageScreen == _ekranAdi && fc.Type == _kullaniciSecimi);
        }
    }
}