using Dapper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Model;
using Hesap.Context;
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
    public partial class FrmMalzemeGiris : DevExpress.XtraEditors.XtraForm
    {
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        CRUD_Operations cRUD = new CRUD_Operations();
        KalemParametreleri parametreler = new KalemParametreleri();
        private string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        CrudRepository crudRepository = new CrudRepository();
        public FrmMalzemeGiris()
        {
            InitializeComponent();
        }
        int Id = 0;
        int FirmaId = 0;
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
        private decimal? UpdateUnitPrice(int rowIndex, string fieldName)
        {
            var value = gridView1.GetRowCellValue(rowIndex, fieldName);
            string valueString = value.ToString().Replace(",", ".");
            if (decimal.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal unitPrice))
            {
                MessageBox.Show(unitPrice.ToString());
                return unitPrice;
            }
            else
            {
                return null; // Dönüşüm başarısızsa null döndürüyoruz
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (this.FirmaId == 0)
            {
                bildirim.Uyari("Firma seçilmeden kayıt işlemi gerçekleştirilemez!!");
                return;
            }
            var parameters = new Dictionary<string, object>
            {
                { "ReceiptType", ReceiptTypes.MalzemeDepoGiris }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },
                { "Explanation", rchAciklama.Text }, { "WareHouseId", txtDepoKodu.Text }, { "InvoiceNo", txtFaturaNo.Text },
                { "InvoiceDate", dateFaturaTarihi.EditValue },{ "DispatchNo", txtIrsaliyeNo.Text },{ "DispatchDate", dateIrsaliyeTarihi.EditValue},
            };

            if (this.Id == 0)
            {
                this.Id = crudRepository.Insert(TableName1, parameters);
                var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                for (int i = 0; i < itemList.Count; i++)
                {
                    var item = itemList[i];
                    var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "Piece", item.Piece }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat } };
                    var rec_id = crudRepository.Insert(TableName2, values);
                    gridView1.SetRowCellValue(i, "ReceiptItemId", rec_id);
                }
                bildirim.Basarili();
            }
            else
            {

                crudRepository.Update(TableName1, Id, parameters);
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.GetRowCellValue(i, "ReceiptItemId") != null)
                    {
                        int rec_id = Convert.ToInt32(gridView1.GetRowCellValue(i, "ReceiptItemId"));
                        var values = new Dictionary<string, object> { { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "Piece", Convert.ToInt32(gridView1.GetRowCellValue(i, "Piece")) }, { "UnitPrice", UpdateUnitPrice(i, "UnitPrice") }/*{ "RowAmount", Convert.ToDouble(gridView1.GetRowCellValue(i, "RowAmount")) }*/ }; // decimal değerler hata veriyor
                        crudRepository.Update(TableName2, rec_id, values);
                    }
                }
                //var itemList = gridControl1.DataSource as BindingList<ReceiptItem>;
                //if (itemList.Count == 0)
                //{
                //    bildirim.Uyari("Liste boş, güncelleme işlemi yapılamaz.");
                //    return;
                //}
                //if (itemList == null)
                //{
                //    bildirim.Uyari("Veri kaynağı boş veya hatalı.");
                //    return;
                //}
                //for (int i = 0; i < itemList.Count; i++)
                //{
                //    var item = itemList[i];
                //    var rec_id = Convert.ToInt32(gridView1.GetRowCellValue(i, "ReceiptItemId"));
                //    var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "Piece", item.Piece }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat } };
                //    if (rec_id > 0)
                //    {
                //        crudRepository.Update(TableName2, rec_id, values);
                //    }
                //    else
                //    {
                //        var new_record = crudRepository.Insert(TableName2, values);
                //        gridView1.SetRowCellValue(i, "ReceiptItemId", new_record);
                //    }
                //}
                //bildirim.GuncellemeBasarili();
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
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            string uuid = Guid.NewGuid().ToString();
            view.SetRowCellValue(e.RowHandle, "UUID", uuid);
        }

        private void simpleButton2_Click(object sender, EventArgs e) // listeleme çalışmadı - düzeltilecek
        {
            Liste.FrmMalzemeGirisListesi frm = new Liste.FrmMalzemeGirisListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                dateTarih.EditValue = (DateTime)frm.veriler[0]["ReceiptDate"];
                dateFaturaTarihi.EditValue = (DateTime)frm.veriler[0]["InvoiceDate"];
                this.FirmaId = Convert.ToInt32(frm.veriler[0]["CompanyId"]);
                txtFirmaKodu.Text = frm.veriler[0]["CompanyCode"].ToString();
                txtFirmaUnvan.Text = frm.veriler[0]["CompanyName"].ToString();
                // txtDepoKodu.Text = frm.veriler[0]["DepoId"].ToString();
                txtFaturaNo.Text = frm.veriler[0]["InvoiceNo"].ToString();
                txtIrsaliyeNo.Text = frm.veriler[0]["DispatchNo"].ToString();
                rchAciklama.Text = frm.veriler[0]["Explanation"].ToString();
                Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                string[] columnNames = new string[]
                {
                    "OperationType", "InventoryId" ,"InventoryCode", "InventoryName", "Piece", "UUID", "TrackingNumber","UnitPrice","ReceiptItemId","RowAmount","Vat"
                };
                yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
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
            gridControl1.DataSource = new BindingList<_MalzemeKalem>();
            this.Id = 0;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Önceki");
        }
        public void KayitlariGetir(string OncemiSonrami)
        {
            int id = this.Id;
            int? istenenId = null;

            try
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql;

                    if (OncemiSonrami == "Önceki")
                    {
                        sql = "SELECT MAX(MalzemeDepo1.Id) FROM MalzemeDepo1 INNER JOIN MalzemeDepo2 ON MalzemeDepo1.Id = MalzemeDepo2.RefNo WHERE MalzemeDepo1.Id < @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(MalzemeDepo1.Id) FROM MalzemeDepo1 INNER JOIN MalzemeDepo2 ON MalzemeDepo1.Id = MalzemeDepo2.RefNo WHERE MalzemeDepo1.Id > @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    string fisquery = "SELECT * FROM MalzemeDepo1  WHERE Id = @Id";
                    string kalemquery = "SELECT * FROM MalzemeDepo2  WHERE RefNo = @Id";
                    var fis = connection.QueryFirstOrDefault(fisquery, new { Id = istenenId });
                    var kalemler = connection.Query(kalemquery, new { Id = istenenId });
                    if (fis != null && kalemler != null)
                    {
                        gridControl1.DataSource = null;
                        dateTarih.EditValue = (DateTime)fis.Tarih;
                        dateFaturaTarihi.EditValue = (DateTime)fis.FaturaTarihi;
                        txtFirmaKodu.Text = fis.FirmaKodu.ToString();
                        txtFirmaUnvan.Text = fis.FirmaUnvani.ToString();
                        txtDepoKodu.Text = fis.DepoId.ToString();
                        txtFaturaNo.Text = fis.FaturaNo.ToString();
                        txtIrsaliyeNo.Text = fis.IrsaliyeNo.ToString();
                        rchAciklama.Text = fis.Aciklama.ToString();
                        this.Id = Convert.ToInt32(fis.Id);
                        gridControl1.DataSource = kalemler.ToList();

                    }
                    else
                    {
                        bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Sonraki");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(TableName1, Id, null);
            crudRepository.DeleteRows(TableName2, this.Id);
            FormTemizle();
        }
        private void siparişFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                gridView1.SetRowCellValue(newRowHandle, "KalemIslem", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "MalzemeKodu", values[1]);
                gridView1.SetRowCellValue(newRowHandle, "MalzemeAdi", values[2]);
                gridView1.SetRowCellValue(newRowHandle, "Birim", values[3]);
                gridView1.SetRowCellValue(newRowHandle, "TakipNo", values[4]);
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