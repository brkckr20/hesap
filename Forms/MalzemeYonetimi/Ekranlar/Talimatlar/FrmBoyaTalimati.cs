using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.Talimatlar
{
    public partial class FrmBoyaTalimati : DevExpress.XtraEditors.XtraForm
    {
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        Numarator numarator = new Numarator();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        int FirmaId = 0, Id = 0;
        CrudRepository crudRepository = new CrudRepository();
        private const string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        private readonly int ReceiptType = Convert.ToInt32(ReceiptTypes.BoyahaneTalimati);
        public FrmBoyaTalimati()
        {
            InitializeComponent();
        }

        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
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
                    { "ReceiptType", ReceiptType }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },{ "Explanation", rchAciklama.Text }, { "ReceiptNo", txtTalimatNo.Text },{ "Authorized", txtYetkili.Text },{"Maturity",txtVade.Text},{"PaymentType",comboBoxEdit1.Text},/*{"Approved",Onayli},*/{"SavedUser",CurrentUser.UserId},{"SavedDate",DateTime.Now}, {"IsFinished",0},{"Approved",0}
                };
                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName1, parameters);
                    var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        var item = itemList[i];
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "GrossWeight", item.GrossWeight }, { "NetWeight", item.NetWeight }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat }, { "TrackingNumber", item.TrackingNumber }, { "MeasurementUnit", item.MeasurementUnit }, { "ColorId", item.ColorId }, { "Wastage", item.Wastage }, { "Quantity", item.Quantity } };
                        var rec_id = crudRepository.Insert(TableName2, values);
                        gridView1.SetRowCellValue(i, "ReceiptItemId", rec_id);
                    }
                    lblOnayDurumu.Text = "Onaylanmadı";
                    lblOnayDurumu.ForeColor = System.Drawing.Color.Red;
                    lblOnayDurumu.Visible = true;
                    bildirim.Basarili();
                }
                else
                {
                    if (lblOnayDurumu.Text == "Onaylandı")
                    {
                        bildirim.Uyari("Onaylanmış talimat üzerinde değişiklik yapamazsınız!\nİşleme devam edebilmek için yetkili kişinden onayı kaldırmasını talep ediniz");
                        return;
                    }
                    else
                    {
                        parameters.Add("UpdatedDate", DateTime.Now);
                        parameters.Add("UpdatedUser", CurrentUser.UserId);
                        crudRepository.Update(TableName1, Id, parameters);
                        for (int i = 0; i < gridView1.RowCount - 1; i++)
                        {
                            var recIdObj = gridView1.GetRowCellValue(i, "ReceiptItemId");
                            int rec_id = recIdObj != null ? Convert.ToInt32(recIdObj) : 0;
                            var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "GrossWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "GrossWeight").ToString()) }, { "NetWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "NetWeight").ToString()) }, { "UnitPrice", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "UnitPrice").ToString()) }, { "RowAmount", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "RowAmount").ToString()) }, { "Vat", Convert.ToInt32(gridView1.GetRowCellValue(i, "Vat")) }, { "UUID", gridView1.GetRowCellValue(i, "UUID") }, { "Explanation", gridView1.GetRowCellValue(i, "Explanation") }, { "MeasurementUnit", gridView1.GetRowCellValue(i, "MeasurementUnit") }, { "ColorId", Convert.ToInt32(gridView1.GetRowCellValue(i, "ColorId")) }, { "Wastage", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "Wastage").ToString()) }, { "Quantity", Convert.ToInt32(gridView1.GetRowCellValue(i, "Quantity")) } };
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
                        lblOnayDurumu.Text = "Onaylanmadı";
                        lblOnayDurumu.ForeColor = System.Drawing.Color.Red;
                        lblOnayDurumu.Visible = true;
                        bildirim.GuncellemeBasarili();
                    }
                }
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
        }

        private void repoBoyaRenkKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.BoyahaneRenkBilgileriYansit(gridView1, "Kumaş");
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.MalzemeBilgileriniGrideYansit(gridView1, InventoryTypes.Kumas);
        }

        private void btnSiparisler_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Siparişe bağlı ihtiyaç miktarları listelenecek");
        }

        private void FrmBoyaTalimati_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
            crudRepository.GetUserColumns(gridView1, this.Text);
        }

        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            txtTalimatNo.Text = numarator.NumaraVer("Fiş", ReceiptType); 
            lblOnayDurumu.Visible = false;
        }
    }
}