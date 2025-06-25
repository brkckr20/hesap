using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.Talimatlar
{
    public partial class FrmTalimatOnaylama : DevExpress.XtraEditors.XtraForm
    {
        public int _types;
        CrudRepository crudRepository = new CrudRepository();
        Listele listele = new Listele();
        public FrmTalimatOnaylama()
        {
            InitializeComponent();
        }

        public FrmTalimatOnaylama(int types)
        {
            InitializeComponent();
            _types = types;
        }

        private void FrmTalimatOnaylama_Load(object sender, EventArgs e)
        {
            Listele(null);
            gridControl1.ContextMenuStrip = contextMenuStrip1;
        }

        void Listele(int? onay)
        {
            string sql;
            if (onay == null)
            {
                sql = $@"
                        Select 
                        ISNULL(R.Id,'') [Id],ISNULL(R.ReceiptNo,'') [Talimat No],ISNULL(R.ReceiptDate,'') [Talimat Tarihi],
                        ISNULL(C.CompanyCode,'') [Firma Kodu],ISNULL(C.CompanyName,'') [Firma Adı],
                        CASE
	                        WHEN R.Approved = 1 THEN 'Onaylı'
	                        WHEN R.Approved = 0 THEN 'Onaysız'
	                        END AS [Onay Durumu],
                        SUM(RI.RowAmount) [Toplam Tutar]

                        from Receipt R left join ReceiptItem RI on R.Id = RI.ReceiptId
                        left join Company C on C.Id = R.CompanyId
                        where R.ReceiptType = {_types} and ISNULL(IsFinished,0) <> 1
                        group by
                        ISNULL(R.Id,''),ISNULL(R.ReceiptNo,''),ISNULL(R.ReceiptDate,''),ISNULL(C.CompanyCode,''),ISNULL(C.CompanyName,''),R.Approved
";
            }
            else
            {
                sql = $@"
                        Select 
                        ISNULL(R.Id,'') [Id],ISNULL(R.ReceiptNo,'') [Talimat No],ISNULL(R.ReceiptDate,'') [Talimat Tarihi],
                        ISNULL(C.CompanyCode,'') [Firma Kodu],ISNULL(C.CompanyName,'') [Firma Adı],
                        CASE
	                        WHEN R.Approved = 1 THEN 'Onaylı'
	                        WHEN R.Approved = 0 THEN 'Onaysız'
	                        END AS [Onay Durumu],
                        SUM(RI.RowAmount) [Toplam Tutar]

                        from Receipt R left join ReceiptItem RI on R.Id = RI.ReceiptId
                        left join Company C on C.Id = R.CompanyId
                        where R.ReceiptType = {_types} and R.Approved = {onay} and ISNULL(IsFinished,0) <> 1
                        group by
                        ISNULL(R.Id,''),ISNULL(R.ReceiptNo,''),ISNULL(R.ReceiptDate,''),ISNULL(C.CompanyCode,''),ISNULL(C.CompanyName,''),R.Approved
";
            }
            listele.Liste(sql, gridControl1);
        }

        private void btnTumu_Click(object sender, EventArgs e)
        {
            Listele(null);
        }

        private void btnOnayli_Click(object sender, EventArgs e)
        {
            Listele(1);
        }

        private void btnOnaysiz_Click(object sender, EventArgs e)
        {
            Listele(0);
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        void OnayDurumuDegistir(int durum)
        {
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            crudRepository.Update("Receipt", id, new Dictionary<string, object> { { "Approved", durum } });
            Listele(durum);
        }

        private void onaylaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnayDurumuDegistir(1);
        }

        private void onayıKaldırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnayDurumuDegistir(0);

        }
    }
}