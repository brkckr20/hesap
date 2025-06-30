using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.HamDepo
{
    public partial class FrmHamDepoCikis : DevExpress.XtraEditors.XtraForm
    {
        public FrmHamDepoCikis()
        {
            InitializeComponent();
        }
        int Id = 0, FirmaId = 0, TalimatId = 0, TasiyiciId = 0;
        CRUD_Operations cRUD = new CRUD_Operations();
        CrudRepository crudRepository = new CrudRepository();
        Metotlar metotlar = new Metotlar();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        private readonly string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        private readonly int ReceiptType = Convert.ToInt32(ReceiptTypes.KumasDepoCikis);
        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }
        //gridcontrol üzerindeki field isimlerinden devam edilecek - 27.06.2025 
        private void FrmHamDepoCikis_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void btnAciklamaGetir_Click(object sender, EventArgs e)
        {
            Liste.FrmAciklamaListesi frm = new Liste.FrmAciklamaListesi(Convert.ToInt32(ReceiptTypes.KumasDepoCikis));
            frm.ShowDialog();
            rchAciklama.EditValue = frm.Aciklama;
        }

        private void btnStokSecimi_Click(object sender, EventArgs e)
        {
            FrmStokSecimi frm = new FrmStokSecimi(ReceiptTypes.KumasDepoGiris);
            frm.ShowDialog();
            foreach (var item in frm.stokListesi)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "InventoryId", values[5]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryCode", values[6]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryName", values[7]);
                //gridView1.SetRowCellValue(newRowHandle, "Organik", values[4]);
                gridView1.SetRowCellValue(newRowHandle, "Brand", values[12]);
                //gridView1.SetRowCellValue(newRowHandle, "PartiNo", values[6]);
                gridView1.SetRowCellValue(newRowHandle, "ColorId", values[13]);
                gridView1.SetRowCellValue(newRowHandle, "ColorCode", values[14]);
                gridView1.SetRowCellValue(newRowHandle, "ColorName", values[15]);
                gridView1.SetRowCellValue(newRowHandle, "NetWeight", values[8]);
                gridView1.SetRowCellValue(newRowHandle, "GrossWeight", values[8]);
                gridView1.SetRowCellValue(newRowHandle, "TrackingNumber", values[4]);
            }
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            LstIslemListesi frm = new LstIslemListesi(ReceiptType);
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
                //dateFaturaTarihi.EditValue = (DateTime)Convert.ToDateTime(values1[12]);
                //txtFaturaNo.Text = values1[13];
                //dateIrsaliyeTarihi.EditValue = (DateTime)Convert.ToDateTime(values1[14]);
                //txtIrsaliyeNo.Text = values1[15];
                rchAciklama.Text = values1[16];
                //txtTalimatNo.Text = values1[22];
                //txtYetkili.Text = values1[26];
                //txtVade.Text = values1[27];
                //comboBoxEdit1.Text = values1[28];
                //Onayli = Convert.ToBoolean(values1[29]); // sağ click ile değiştirmek için
                //chckOnayli.Checked = Convert.ToBoolean(values1[29]);
                //lblOnayDurumu.Text = Convert.ToBoolean(values1[29]) ? "Onaylandı" : "Onaylanmadı";
                //lblOnayDurumu.ForeColor = Convert.ToBoolean(values1[29]) ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                //lblOnayDurumu.Visible = true;
                txtNakliyeci.Text = values1[31].ToString();
                txtAd.Text = values1[33].ToString();
                txtSoyad.Text = values1[34].ToString();
                txtTC.Text = values1[37].ToString();
                txtPlaka.Text = values1[35].ToString();
                txtDorse.Text = values1[36].ToString();
                txtUnvan.Text = values1[32].ToString();

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
                    gridView1.SetRowCellValue(newRowHandle, "TrackingNumber", values[21]); // 22 receiptNo yukarıda
                    gridView1.SetRowCellValue(newRowHandle, "GrossWeight", values[23]);
                    gridView1.SetRowCellValue(newRowHandle, "NetWeight", values[24]);
                    gridView1.SetRowCellValue(newRowHandle, "MeasurementUnit", values[25]);
                    gridView1.SetRowCellValue(newRowHandle, "ColorId", values[38]);
                    gridView1.SetRowCellValue(newRowHandle, "ColorCode", values[39]);
                    gridView1.SetRowCellValue(newRowHandle, "ColorName", values[40]);
                }
            }
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.MalzemeBilgileriniGrideYansit(gridView1, InventoryTypes.Kumas);
        }

        private void txtNakliyeci_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.TasiyiciBilgileriYansit(txtUnvan, txtAd, txtSoyad, txtTC, txtPlaka, txtDorse, txtNakliyeci, ref this.TasiyiciId);
        }

        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateIrsaliyeTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            crudRepository.GetUserColumns(gridView1, this.Text);
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
                    { "ReceiptType", ReceiptType }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },{ "Explanation", rchAciklama.Text }, { "DispatchDate", dateIrsaliyeTarihi.EditValue},{ "DispatchNo", txtIrsaliyeNo.Text },{ "TransporterId", TasiyiciId}
                };
                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName1, parameters);
                    var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        var item = itemList[i];
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "GrossWeight", item.GrossWeight }, { "NetWeight", item.NetWeight }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat }, { "TrackingNumber", item.TrackingNumber }, { "MeasurementUnit", item.MeasurementUnit }, { "Brand", item.Brand }, { "ReceiptNo", item.ReceiptNo }, { "ColorId", item.ColorId } };
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
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "GrossWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "GrossWeight").ToString()) }, { "NetWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "NetWeight").ToString()) }, { "UnitPrice", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "UnitPrice").ToString()) }, { "RowAmount", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "RowAmount").ToString()) }, { "Vat", Convert.ToInt32(gridView1.GetRowCellValue(i, "Vat")) }, { "UUID", gridView1.GetRowCellValue(i, "UUID") }, { "Explanation", gridView1.GetRowCellValue(i, "Explanation") }, { "MeasurementUnit", gridView1.GetRowCellValue(i, "MeasurementUnit") }, { "Brand", gridView1.GetRowCellValue(i, "Brand") }, { "ReceiptNo", gridView1.GetRowCellValue(i, "ReceiptNo") }, { "ColorId", Convert.ToInt32(gridView1.GetRowCellValue(i, "ColorId")) } };
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
    }
}