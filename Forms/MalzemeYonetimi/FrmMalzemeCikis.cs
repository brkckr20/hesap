using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmMalzemeCikis : XtraForm
    {
        public FrmMalzemeCikis()
        {
            InitializeComponent();
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
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "Piece", item.Piece }, { "UUID", item.UUID },{ "Receiver", item.Receiver } };
                        var rec_id = crudRepository.Insert(TableName2, values);
                        gridView1.SetRowCellValue(i, "ReceiptItemId", rec_id);
                    }
                    bildirim.Basarili();
                }
                else
                {
                    crudRepository.Update(TableName1, Id, out_params);
                }
                /*
                 else
                {

                    crudRepository.Update(TableName1, Id, parameters);
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (gridView1.GetRowCellValue(i, "ReceiptItemId") != null)
                        {
                            int rec_id = Convert.ToInt32(gridView1.GetRowCellValue(i, "ReceiptItemId"));
                            var unitPriceStr = gridView1.GetRowCellValue(i, "Piece").ToString();
                            var values = new Dictionary<string, object> { { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "Piece", ConvertDecimal(gridView1.GetRowCellValue(i, "Piece").ToString()) }, { "UnitPrice", ConvertDecimal(gridView1.GetRowCellValue(i, "UnitPrice").ToString()) }, { "RowAmount", ConvertDecimal(gridView1.GetRowCellValue(i, "RowAmount").ToString()) } };
                            crudRepository.Update(TableName2, rec_id, values);
                        }
                    }
                    bildirim.GuncellemeBasarili();
                }
                 
                 */
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmMalzemeKartiListesi frm = new Liste.FrmMalzemeKartiListesi(Convert.ToInt32(InventoryTypes.Malzeme));
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.Kodu) && !string.IsNullOrEmpty(frm.Adi))
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "MalzemeKodu", frm.Kodu);
                gridView1.SetRowCellValue(newRowHandle, "MalzemeAdi", frm.Adi);
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
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
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
            if (frm.liste.Count > 0)
            {
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
                    this.Id = Convert.ToInt32(values[6]);
                    gridView1.SetRowCellValue(newRowHandle, "Receiver", values[7]);
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
            crudRepository.DeleteRows(TableName2,this.Id);
            FormTemizle();
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
                        sql = "SELECT MAX(MalzemeDepo1.Id) FROM MalzemeDepo1 INNER JOIN MalzemeDepo2 ON MalzemeDepo1.Id = MalzemeDepo2.RefNo WHERE MalzemeDepo1.Id < @Id and MalzemeDepo1.IslemCinsi='Çıkış'";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(MalzemeDepo1.Id) FROM MalzemeDepo1 INNER JOIN MalzemeDepo2 ON MalzemeDepo1.Id = MalzemeDepo2.RefNo WHERE MalzemeDepo1.Id > @Id and MalzemeDepo1.IslemCinsi='Çıkış'";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    string fisquery = "SELECT * FROM MalzemeDepo1 WHERE Id = @Id";
                    string kalemquery = "SELECT *,Id [KayitNo] FROM MalzemeDepo2  WHERE RefNo = @Id";
                    var fis = connection.QueryFirstOrDefault(fisquery, new { Id = istenenId });
                    var kalemler = connection.Query(kalemquery, new { Id = istenenId });
                    if (fis != null && kalemler != null)
                    {
                        gridControl1.DataSource = null;
                        dateTarih.EditValue = (DateTime)fis.Tarih;
                        txtFirmaKodu.Text = fis.FirmaKodu?.ToString();
                        txtFirmaUnvan.Text = fis.FirmaUnvani?.ToString();
                        txtDepoKodu.Text = fis.DepoId?.ToString();
                        txtIrsaliyeNo.Text = fis.IrsaliyeNo?.ToString();
                        rchAciklama.Text = fis.Aciklama?.ToString();
                        txtYetkili.Text = fis.Yetkili?.ToString();
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
            if (this.Id == 0)
            {
                bildirim.Uyari("Rapor alabilmek için bir kayıt seçmelisiniz!");
            }
            else
            {
                Rapor.FrmRaporSecimEkrani frm = new Rapor.FrmRaporSecimEkrani(this.Text, this.Id);
                frm.ShowDialog();
            }
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1, this.Text);
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