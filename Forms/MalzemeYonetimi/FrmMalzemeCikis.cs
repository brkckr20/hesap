using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmMalzemeCikis : XtraForm
    {
        public FrmMalzemeCikis()
        {
            InitializeComponent();
            gridControl1.ContextMenuStrip = contextMenuStrip1;
        }
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        int Id = 0, FirmaId = 0;
        CrudRepository crudRepository = new CrudRepository();
        private const string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmFirmaKartiListesi frm = new Liste.FrmFirmaKartiListesi();
            frm.ShowDialog();
            txtFirmaKodu.Text = frm.FirmaKodu;
            txtFirmaUnvan.Text = frm.FirmaUnvan;
            this.FirmaId = frm.Id;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        void FormTemizle()
        {
            object[] bilgiler = { dateTarih, dateSevkTarihi, txtFirmaKodu, txtFirmaUnvan, txtDepoKodu, txtIrsaliyeNo, rchAciklama, txtYetkili };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            this.Id = 0;
            this.FirmaId = 0;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FirmaId == 0)
                {
                    bildirim.Uyari("Firma seçilmeden kayıt işlemi gerçekleştirilemez!!");
                    return;
                }
                var out_params = new Dictionary<string, object>
                                    {
                                    { "ReceiptType", ReceiptTypes.MalzemeDepoCikis }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },{ "Explanation", rchAciklama.Text }, { "WareHouseId", txtDepoKodu.Text },{ "DispatchNo", txtIrsaliyeNo.Text },{ "DispatchDate", dateSevkTarihi.EditValue},{ "Authorized", txtYetkili.Text}                                    };
                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName1, out_params);
                    var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        var item = itemList[i];
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "Piece", item.Piece }, { "UUID", item.UUID }, { "Receiver", item.Receiver } };
                        var rec_id = crudRepository.Insert(TableName2, values);
                        gridView1.SetRowCellValue(i, "ReceiptItemId", rec_id);
                    }
                    bildirim.Basarili();
                }
                else
                {
                    crudRepository.Update(TableName1, Id, out_params);
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        var receipItemId = gridView1.GetRowCellValue(i, "ReceiptItemId");
                        var rec_id = receipItemId != null ? Convert.ToInt32(receipItemId) : 0;
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "Piece", ConvertDecimal(gridView1.GetRowCellValue(i, "Piece").ToString()) }, { "UUID", gridView1.GetRowCellValue(i, "UUID") }, { "Explanation", gridView1.GetRowCellValue(i, "Explanation") }, { "Receiver", gridView1.GetRowCellValue(i, "Receiver") } };
                        if (rec_id != 0)
                        {
                            crudRepository.Update(TableName2, rec_id, values);
                        }
                        else
                        {
                            var new_rec_id = crudRepository.Insert(TableName2, values);
                            gridView1.SetRowCellValue(i, "ReceiptItemId", new_rec_id);
                        }
                    }
                    bildirim.GuncellemeBasarili();
                }
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
        }
        decimal ConvertDecimal(string strVal)
        {
            if (decimal.TryParse(strVal, NumberStyles.Any, CultureInfo.GetCultureInfo("tr-TR"), out decimal unitPriceValue))
            {
                return unitPriceValue;
            }
            return 0;
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmMalzemeKartiListesi frm = new Liste.FrmMalzemeKartiListesi(Convert.ToInt32(InventoryTypes.Malzeme));
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.Kodu) && !string.IsNullOrEmpty(frm.Adi))
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "InventoryCode", frm.Kodu);
                gridView1.SetRowCellValue(newRowHandle, "InventoryName", frm.Adi);
                gridView1.SetRowCellValue(newRowHandle, "InventoryId", frm.Id);
            }
        }

        private void FrmMalzemeCikis_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateSevkTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            crudRepository.GetUserColumns(gridView1, this.Text);
        }

        private void btnIslemBekleyenler_Click(object sender, EventArgs e)
        {
            FrmMalzemeDepoStok frm = new FrmMalzemeDepoStok();
            frm.ShowDialog();
            foreach (var item in frm.malzemeBilgileri)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "InventoryCode", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryName", values[1]);
                gridView1.SetRowCellValue(newRowHandle, "UUID", values[2]);
                gridView1.SetRowCellValue(newRowHandle, "Piece", values[3]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryId", values[4]);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmMalzemeCikisListesi frm = new Liste.FrmMalzemeCikisListesi();
            frm.ShowDialog();
            DateTime tarih = DateTime.MinValue;
            if (frm.liste.Count > 0)
            {
                yardimciAraclar.ClearGridViewRows(gridView1);
                var firstItem = frm.liste[0];
                var _item = firstItem.Split(';');
                Id = Convert.ToInt32(_item[6]);
                tarih = Convert.ToDateTime(_item[9]);
                dateTarih.EditValue = (DateTime)tarih;
                FirmaId = Convert.ToInt32(_item[10]);
                txtFirmaKodu.Text = _item[11];
                txtFirmaUnvan.Text = _item[12];
                txtDepoKodu.Text = _item[13];
                txtIrsaliyeNo.Text = _item[14];
                rchAciklama.Text = _item[15];
                txtYetkili.Text = _item[16];

                foreach (var item in frm.liste)
                {
                    gridView1.AddNewRow();
                    int newRowHandle = gridView1.FocusedRowHandle;
                    var values = item.Split(';');
                    gridView1.SetRowCellValue(newRowHandle, "InventoryCode", values[0]);
                    gridView1.SetRowCellValue(newRowHandle, "InventoryName", values[1]);
                    gridView1.SetRowCellValue(newRowHandle, "Piece", values[2]);
                    gridView1.SetRowCellValue(newRowHandle, "OperationType", values[3]);
                    gridView1.SetRowCellValue(newRowHandle, "UUID", values[4]);
                    gridView1.SetRowCellValue(newRowHandle, "InventoryId", values[5]); // 6 = id
                    gridView1.SetRowCellValue(newRowHandle, "Receiver", values[7]);
                    gridView1.SetRowCellValue(newRowHandle, "ReceiptItemId", values[8]);
                }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gridView = sender as GridView;

            if (e.KeyCode == Keys.Delete)
            {
                //if (bildirim.SilmeOnayı())
                //{
                //    int selectedRowHandle = gridView.FocusedRowHandle;
                //    int kayitNo = Convert.ToInt32(gridView.GetRowCellValue(selectedRowHandle, "KayitNo"));
                //    using (var connection = new Baglanti().GetConnection())
                //    {
                //        string sql = "delete from MalzemeDepo2 where Id = @Id";
                //        connection.Execute(sql, new { Id = kayitNo });
                //        gridView.DeleteRow(selectedRowHandle);
                //        bildirim.SilmeBasarili();
                //    }
                //}
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(TableName1, this.Id, null);
            crudRepository.DeleteRows(TableName2, this.Id);
            FormTemizle();
        }
        public void KayitlariGetir(string KayitTipi)
        {
            try
            {
                int id = this.Id;
                int? istenenId = crudRepository.GetIdForAfterOrBeforeRecord(KayitTipi, TableName1, id, TableName2, "ReceiptId", Convert.ToInt32(ReceiptTypes.MalzemeDepoCikis));

                if (istenenId == null)
                {
                    bildirim.Uyari("Başka bir kayıt bulunamadı.");
                    return;
                }

                string query = $@"SELECT 
                    ISNULL(R.Id,0) Id, ISNULL(R.ReceiptDate,'') ReceiptDate, ISNULL(R.CompanyId,0) CompanyId,
                    ISNULL(R.DispatchDate,'') DispatchDate, ISNULL(R.DispatchNo,'') DispatchNo, ISNULL(R.Explanation,'') ExplanationFis,ISNULL(R.Authorized,'') Authorized,
                    ISNULL(RI.Id,0) [ReceiptItemId], ISNULL(RI.OperationType,'') OperationType,
                    ISNULL(RI.InventoryId,0) InventoryId, ISNULL(RI.Piece,0) Piece, ISNULL(RI.UnitPrice,0) UnitPrice,
                    ISNULL(RI.UUID,'') UUID, ISNULL(RI.Explanation,'') Explanation,ISNULL(RI.Receiver,'') Receiver,
                    ISNULL(C.CompanyCode,'') CompanyCode, ISNULL(C.CompanyName,'') CompanyName,
                    ISNULL(I.InventoryCode,'') InventoryCode, ISNULL(I.InventoryName,'') InventoryName
                    FROM Receipt R
                    INNER JOIN ReceiptItem RI ON R.Id = RI.ReceiptId
                    LEFT JOIN Company C ON C.Id = R.CompanyId
                    LEFT JOIN Inventory I ON I.Id = RI.InventoryId
                    WHERE R.ReceiptType = 3 AND R.Id = @Id";

                var liste = crudRepository.GetAfterOrBeforeRecord(query, istenenId.Value);

                if (liste != null && liste.Count > 0)
                {
                    yardimciAraclar.ClearGridViewRows(gridView1);
                    var item = liste[0];
                    dateTarih.Text = item.ReceiptDate.ToString();
                    this.Id = Convert.ToInt32(item.Id);
                    this.FirmaId = Convert.ToInt32(item.CompanyId);
                    txtFirmaKodu.Text = item.CompanyCode.ToString();
                    txtFirmaUnvan.Text = item.CompanyName.ToString();
                    dateSevkTarihi.Text = item.DispatchDate.ToString();
                    txtIrsaliyeNo.Text = item.DispatchNo.ToString();
                    rchAciklama.Text = item.ExplanationFis.ToString();
                    txtYetkili.Text = item.Authorized.ToString();
                    gridControl1.DataSource = liste;
                }
                else
                {
                    bildirim.Uyari("Kayıt bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata: " + ex.Message);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Önceki");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Sonraki");
        }

        private void siparişFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.OpenFormSelectScreen(this.Id,this.Text);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, "KayitNo", 0);
        }
    }
}