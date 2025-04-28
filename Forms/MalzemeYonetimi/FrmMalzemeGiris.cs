using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmMalzemeGiris : DevExpress.XtraEditors.XtraForm
    {
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        CRUD_Operations cRUD = new CRUD_Operations();
        KalemParametreleri parametreler = new KalemParametreleri();
        private readonly string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        CrudRepository crudRepository = new CrudRepository();
        public FrmMalzemeGiris()
        {
            InitializeComponent();
            gridControl1.ContextMenuStrip = contextMenuStrip1;
        }
        int Id = 0, FirmaId = 0;
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }
        private void FrmMalzemeGiris_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateFaturaTarihi.EditValue = DateTime.Now;
            dateIrsaliyeTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            crudRepository.GetUserColumns(gridView1, this.Text);
        }
        decimal ConvertDecimal(string strVal)
        {
            if (decimal.TryParse(strVal, NumberStyles.Any, CultureInfo.GetCultureInfo("tr-TR"), out decimal unitPriceValue))
            {
                return unitPriceValue;
            }
            return 0;
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
                var parameters = new Dictionary<string, object>
                {
                    { "ReceiptType", ReceiptTypes.MalzemeDepoGiris }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },
                    { "Explanation", rchAciklama.Text }, { "InvoiceNo", txtFaturaNo.Text },
                    { "InvoiceDate", dateFaturaTarihi.EditValue },{ "DispatchNo", txtIrsaliyeNo.Text },{ "DispatchDate", dateIrsaliyeTarihi.EditValue},{ "WareHouseId", Convert.ToInt32(WareHouseTypes.Malzeme)},
                };

                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName1, parameters);
                    var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        var item = itemList[i];
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "Piece", item.Piece }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat }, { "TrackingNumber", item.TrackingNumber } };
                        var rec_id = crudRepository.Insert(TableName2, values);
                        gridView1.SetRowCellValue(i, "ReceiptItemId", rec_id);
                    }
                    bildirim.Basarili();
                }
                else
                {
                    crudRepository.Update(TableName1, Id, parameters);
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        var recIdObj = gridView1.GetRowCellValue(i, "ReceiptItemId");
                        int rec_id = recIdObj != null ? Convert.ToInt32(recIdObj) : 0;
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "Piece", ConvertDecimal(gridView1.GetRowCellValue(i, "Piece").ToString()) }, { "UnitPrice", ConvertDecimal(gridView1.GetRowCellValue(i, "UnitPrice").ToString()) }, { "RowAmount", ConvertDecimal(gridView1.GetRowCellValue(i, "RowAmount").ToString()) }, { "Vat", Convert.ToInt32(gridView1.GetRowCellValue(i, "Vat")) }, { "UUID", gridView1.GetRowCellValue(i, "UUID") }, { "Explanation", gridView1.GetRowCellValue(i, "Explanation") } };
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
                bildirim.Uyari("Bir hata oluştu : " + ex.Message);
            }
        }
        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmMalzemeKartiListesi frm = new Liste.FrmMalzemeKartiListesi(Convert.ToInt32(InventoryTypes.Malzeme));
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.Kodu) && !string.IsNullOrEmpty(frm.Adi))
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "InventoryId", frm.Id);
                gridView1.SetRowCellValue(newRowHandle, "InventoryCode", frm.Kodu);
                gridView1.SetRowCellValue(newRowHandle, "InventoryName", frm.Adi);
            }
        }
        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            string uuid = Guid.NewGuid().ToString();
            view.SetRowCellValue(e.RowHandle, "UUID", uuid);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmMalzemeGirisListesi frm = new Liste.FrmMalzemeGirisListesi();
            frm.ShowDialog();
            DateTime tarih = DateTime.MinValue;
            if (frm.liste.Count > 0)
            {
                yardimciAraclar.ClearGridViewRows(gridView1);
                var firstItem = frm.liste[0];
                var values1 = firstItem.Split(';');
                this.Id = Convert.ToInt32(values1[6]);
                tarih = Convert.ToDateTime(values1[8]);
                dateTarih.EditValue = (DateTime)tarih;
                this.FirmaId = Convert.ToInt32(values1[9]);
                txtFirmaKodu.Text = values1[10];
                txtFirmaUnvan.Text = values1[11];
                dateFaturaTarihi.EditValue= (DateTime)Convert.ToDateTime(values1[12]);
                txtFaturaNo.Text = values1[13];
                dateIrsaliyeTarihi.EditValue= (DateTime)Convert.ToDateTime(values1[14]);
                txtIrsaliyeNo.Text = values1[15];
                rchAciklama.Text = values1[16];
                //depo eklenecek
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
                    gridView1.SetRowCellValue(newRowHandle, "InventoryId", values[5]);
                    gridView1.SetRowCellValue(newRowHandle, "UnitPrice", values[17]);
                    gridView1.SetRowCellValue(newRowHandle, "Vat", values[18]);
                    gridView1.SetRowCellValue(newRowHandle, "ReceiptItemId", values[19]);
                    gridView1.SetRowCellValue(newRowHandle, "Explanation", values[20]);
                    gridView1.SetRowCellValue(newRowHandle, "TrackingNumber", values[21]);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        void FormTemizle()
        {
            object[] bilgiler = { dateTarih, dateFaturaTarihi, txtFirmaKodu, txtFirmaUnvan, txtDepoKodu, txtFaturaNo, txtIrsaliyeNo, rchAciklama };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            this.Id = 0;
            this.FirmaId = 0;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Önceki");
        }
        public void KayitlariGetir(string KayitTipi)
        {
            try
            {
                int id = this.Id;
                int? istenenId = crudRepository.GetIdForAfterOrBeforeRecord(KayitTipi, TableName1, id,TableName2,"ReceiptId",Convert.ToInt32(ReceiptTypes.MalzemeDepoGiris));

                if (istenenId == null)
                {
                    bildirim.Uyari("Başka bir kayıt bulunamadı.");
                    return;
                }

                string query = $@"SELECT 
                    ISNULL(R.Id,0) Id, ISNULL(R.ReceiptDate,'') ReceiptDate, ISNULL(R.CompanyId,0) CompanyId,
                    ISNULL(R.InvoiceDate,'') InvoiceDate, ISNULL(R.InvoiceNo,'') InvoiceNo, ISNULL(R.DispatchDate,'') DispatchDate,
                    ISNULL(R.DispatchNo,'') DispatchNo, ISNULL(R.Explanation,'') ExplanationFis,
                    ISNULL(RI.Id,0) [ReceiptItemId], ISNULL(RI.OperationType,'') OperationType,
                    ISNULL(RI.InventoryId,0) InventoryId, ISNULL(RI.Piece,0) Piece, ISNULL(RI.UnitPrice,0) UnitPrice,
                    ISNULL(RI.UUID,'') UUID, ISNULL(RI.RowAmount,0) RowAmount, ISNULL(RI.Vat,0) Vat, ISNULL(RI.Explanation,'') Explanation, ISNULL(RI.TrackingNumber,'') TrackingNumber,
                    ISNULL(C.CompanyCode,'') CompanyCode, ISNULL(C.CompanyName,'') CompanyName,
                    ISNULL(I.InventoryCode,'') InventoryCode, ISNULL(I.InventoryName,'') InventoryName
                    FROM Receipt R
                    INNER JOIN ReceiptItem RI ON R.Id = RI.ReceiptId
                    LEFT JOIN Company C ON C.Id = R.CompanyId
                    LEFT JOIN Inventory I ON I.Id = RI.InventoryId
                    WHERE R.ReceiptType = 1 AND R.Id = @Id";

                var liste = crudRepository.GetAfterOrBeforeRecord(query, istenenId.Value);

                if (liste != null && liste.Count > 0)
                {
                    yardimciAraclar.ClearGridViewRows(gridView1);
                    var item = liste[0];
                    //dateTarih.Text = item.ReceiptDate.ToString();
                    this.Id = Convert.ToInt32(item.Id);
                    this.FirmaId = Convert.ToInt32(item.CompanyId);
                    txtFirmaKodu.Text = item.CompanyCode.ToString();
                    txtFirmaUnvan.Text = item.CompanyName.ToString();
                    dateFaturaTarihi.Text = item.InvoiceDate.ToString();
                    txtFaturaNo.Text = item.InvoiceNo.ToString();
                    dateIrsaliyeTarihi.Text = item.DispatchDate.ToString();
                    txtIrsaliyeNo.Text = item.DispatchNo.ToString();
                    rchAciklama.Text = item.ExplanationFis.ToString();
                    //gridControl1.DataSource = liste;
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


        private void simpleButton4_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Sonraki");
        }
        void DeleteRows()
        {
            crudRepository.DeleteRows(TableName2, this.Id);
            FormTemizle();
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(TableName1, Id, DeleteRows);
        }
        private void siparişFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.OpenFormSelectScreen(this.Id,this.Text);            
        }

        private void btnIslemBekleyenler_Click(object sender, EventArgs e)
        {
            FrmIslemBekleyenler frm = new FrmIslemBekleyenler();
            frm.ShowDialog();
            foreach (var item in frm.malzemeBilgileri)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "OperationType", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryCode", values[1]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryName", values[2]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryId", values[3]);
                gridView1.SetRowCellValue(newRowHandle, "Piece", values[4]);
                gridView1.SetRowCellValue(newRowHandle, "TrackingNumber", values[5]);
            }
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("custom button");
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }
    }
}