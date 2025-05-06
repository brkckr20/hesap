using DevExpress.XtraGrid.Views.Grid;
using Hesap.Context;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.Talimatlar
{
    public partial class FrmIplikSaTalimati : DevExpress.XtraEditors.XtraForm
    {
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        Numarator numarator = new Numarator();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations cRUD = new CRUD_Operations();
        CrudRepository crudRepository = new CrudRepository();
        int FirmaId = 0, Id = 0,ReceiptType = Convert.ToInt32(ReceiptTypes.IplikSatinAlmaTalimati);
        private const string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        private bool Onayli = false;
        public FrmIplikSaTalimati()
        {
            InitializeComponent();
            SayiFormati();

        }
        void SayiFormati()
        {
            gridView1.CustomColumnDisplayText += (sender, e) => yansit.SayiyaNoktaKoy(sender, e, "SatirTutari");
            gridView1.CustomColumnDisplayText += (sender, e) => yansit.SayiyaNoktaKoy(sender, e, "NetKg");
            gridView1.CustomColumnDisplayText += (sender, e) => yansit.SayiyaNoktaKoy(sender, e, "BrutKg");
        }
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }
        private void FrmIplikSaTalimati_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            txtTalimatNo.Text = crudRepository.GetNumaratorWithCondition(TableName1, "ReceiptNo", Convert.ToInt32(ReceiptTypes.IplikSatinAlmaTalimati));
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmIplikKartiListesi frm = new Liste.FrmIplikKartiListesi();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.IplikKodu))
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "InventoryCode", frm.IplikKodu);
                gridView1.SetRowCellValue(newRowHandle, "InventoryName", frm.IplikAdi);
                gridView1.SetRowCellValue(newRowHandle, "InventoryId", frm.Id);
            }
        }
        private void repoBtnMarka_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //Liste.FrmMarkaSecimListesi frm = new Liste.FrmMarkaSecimListesi("IplikDepo2");
            //frm.ShowDialog();
            //if (frm._marka != null)
            //{
            //    int newRowHandle = gridView1.FocusedRowHandle;
            //    gridView1.SetRowCellValue(newRowHandle, "Marka", frm._marka);
            //}
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
                    { "ReceiptType", ReceiptTypes.IplikSatinAlmaTalimati }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },{ "Explanation", rchAciklama.Text }, { "ReceiptNo", txtTalimatNo.Text },{ "Authorized", txtYetkili.Text },{"Maturity",txtVade.Text},{"PaymentType",comboBoxEdit1.Text},{"Approved",Onayli}
                };
                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName1, parameters);
                    var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        var item = itemList[i];
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "GrossWeight", item.GrossWeight }, { "NetWeight", item.NetWeight }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat }, { "TrackingNumber", item.TrackingNumber }, { "MeasurementUnit", item.MeasurementUnit } };
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
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "GrossWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "GrossWeight").ToString()) }, { "NetWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "NetWeight").ToString()) }, { "UnitPrice", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "UnitPrice").ToString()) }, { "RowAmount", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "RowAmount").ToString()) }, { "Vat", Convert.ToInt32(gridView1.GetRowCellValue(i, "Vat")) }, { "UUID", gridView1.GetRowCellValue(i, "UUID") }, { "Explanation", gridView1.GetRowCellValue(i, "Explanation") }, { "MeasurementUnit", gridView1.GetRowCellValue(i, "MeasurementUnit") } };
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
        private void satınAlmaTalimatFormuToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmIplikDepoListe frm = new FrmIplikDepoListe(ReceiptType);
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
                txtTalimatNo.Text = values1[22];
                txtYetkili.Text = values1[26];
                txtVade.Text = values1[27];
                comboBoxEdit1.Text = values1[28];
                Onayli = Convert.ToBoolean(values1[29]); // sağ click ile değiştirmek için
                chckOnayli.Checked = Convert.ToBoolean(values1[29]);
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
                    gridView1.SetRowCellValue(newRowHandle, "BrutWeight", values[23]);
                    gridView1.SetRowCellValue(newRowHandle, "NetWeight", values[24]);
                    gridView1.SetRowCellValue(newRowHandle, "MeasurementUnit", values[25]);
                }
            }
        }
        void DeleteRows()
        {
            crudRepository.DeleteRows(TableName2, this.Id);
            FormTemizle();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(TableName1, Id, DeleteRows);
        }
        void FormTemizle()
        {
            object[] bilgiler = { txtTalimatNo, dateTarih, txtFirmaKodu, txtFirmaUnvan, rchAciklama, txtYetkili, txtVade, comboBoxEdit1 };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<_IplikDepoKalem>();
            this.Id = 0;
            this.FirmaId = 0;
        }
        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, "TrackingNumber", 0);
            string uuid = Guid.NewGuid().ToString();
            view.SetRowCellValue(e.RowHandle, "UUID", uuid);
        }
        public void KayitlariGetir(string KayitTipi)
        {
            try
            {
                int id = this.Id;
                int? istenenId = crudRepository.GetIdForAfterOrBeforeRecord(KayitTipi, TableName1, id, TableName2, "ReceiptId", ReceiptType);

                if (istenenId == null)
                {
                    bildirim.Uyari("Başka bir kayıt bulunamadı.");
                    return;
                }

                string query = $@"SELECT 
                    ISNULL(R.Id,0) Id, ISNULL(R.ReceiptDate,'') ReceiptDate, ISNULL(R.CompanyId,0) CompanyId,
                    ISNULL(R.InvoiceDate,'') InvoiceDate, ISNULL(R.InvoiceNo,'') InvoiceNo, ISNULL(R.DispatchDate,'') DispatchDate,
                    ISNULL(R.DispatchNo,'') DispatchNo, ISNULL(R.Explanation,'') ExplanationFis,ISNULL(R.ReceiptNo,'') ReceiptNo,ISNULL(R.Authorized,'') Authorized,ISNULL(R.Maturity,0) Maturity,
					ISNULL(R.PaymentType,0) PaymentType,
                    ISNULL(RI.Id,0) [ReceiptItemId], ISNULL(RI.OperationType,'') OperationType,ISNULL(RI.InventoryId,0) InventoryId, ISNULL(RI.Piece,0) Piece, ISNULL(RI.UnitPrice,0) UnitPrice,
                    ISNULL(RI.UUID,'') UUID, ISNULL(RI.RowAmount,0) RowAmount, ISNULL(RI.Vat,0) Vat, ISNULL(RI.Explanation,'') Explanation, ISNULL(RI.TrackingNumber,'') TrackingNumber,
					ISNULL(RI.GrossWeight,0) GrossWeight,ISNULL(RI.NetWeight,0) NetWeight,ISNULL(RI.MeasurementUnit,'') MeasurementUnit,
                    ISNULL(C.CompanyCode,'') CompanyCode, ISNULL(C.CompanyName,'') CompanyName,
                    ISNULL(I.InventoryCode,'') InventoryCode, ISNULL(I.InventoryName,'') InventoryName
                    FROM Receipt R
                    INNER JOIN ReceiptItem RI ON R.Id = RI.ReceiptId
                    LEFT JOIN Company C ON C.Id = R.CompanyId
                    LEFT JOIN Inventory I ON I.Id = RI.InventoryId
                    WHERE R.ReceiptType = {ReceiptType} AND R.Id = @Id";

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
                    //dateFaturaTarihi.Text = item.InvoiceDate.ToString();
                    //txtFaturaNo.Text = item.InvoiceNo.ToString();
                    //dateIrsaliyeTarihi.Text = item.DispatchDate.ToString();
                    //txtIrsaliyeNo.Text = item.DispatchNo.ToString();
                    rchAciklama.Text = item.ExplanationFis.ToString();
                    txtYetkili.Text = item.Authorized.ToString();
                    txtVade.Text = item.Maturity.ToString();
                    comboBoxEdit1.Text = item.PaymentType.ToString();
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
        private void btnGeri_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Önceki");
        }
        private void btnIleri_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Sonraki");
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }
        void TalimatAcKapat(bool acKapa)
        {
            crudRepository.Update(TableName1, this.Id, new Dictionary<string, object> { { "IsFinished", acKapa } }); // talimat kapatma işlemi tamamlandı. giriş içerisinde talimat seçimini düzenle 29.04.2025 17:35
        }
        private void talimatKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TalimatAcKapat(true);
        }

        private void talimatAçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TalimatAcKapat(false);
        }

        private void kayıtBilgisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Diger.FrmSaveInfo frm = new Diger.FrmSaveInfo(100,"tablo adı gelecek");
            frm.ShowDialog();
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }

        private void repoBoyaRenkKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmBoyahaneRenkKartlariListesi frm = new Liste.FrmBoyahaneRenkKartlariListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkId", Convert.ToInt32(frm.veriler[0]["Id"]));
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkKodu", frm.veriler[0]["BoyahaneRenkKodu"].ToString());
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkAdi", frm.veriler[0]["BoyahaneRenkAdi"].ToString());
            }
        }
    }
}