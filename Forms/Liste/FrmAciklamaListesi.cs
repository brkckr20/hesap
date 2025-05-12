using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FastColoredTextBoxNS;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.Liste
{
    public partial class FrmAciklamaListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        int _receiptType;
        public string Aciklama;
        int Id; // aktif olarak bir yerde kullanılmıyor
        CrudRepository crudRepository = new CrudRepository();

        public FrmAciklamaListesi(int ReceiptType)
        {
            InitializeComponent();
            this._receiptType = ReceiptType;
        }

        private void FrmAciklamaListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = $"SELECT Id,Explanation [Aciklama] FROM ReceiptExplanation where ReceiptType={_receiptType}";
            listele.Liste(sql, gridControl1);
            gridView1.Columns["Id"].Visible = false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "ReceiptType", _receiptType }, { "Explanation", txtAciklama.Text }
                };
            Id = crudRepository.Insert("ReceiptExplanation",parameters);
            Listele();
            txtAciklama.Text = "";            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Aciklama = gridView.GetFocusedRowCellValue("Aciklama").ToString();
            this.Close();
        }
    }
}