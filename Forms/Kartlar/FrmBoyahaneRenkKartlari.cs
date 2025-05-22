using Dapper;
using DevExpress.XtraEditors;
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
using static DevExpress.Xpo.Helpers.CommandChannelHelper;

namespace Hesap.Forms
{
    public partial class FrmBoyahaneRenkKartlari : DevExpress.XtraEditors.XtraForm
    {
        public FrmBoyahaneRenkKartlari()
        {
            InitializeComponent();
        }
        Bildirim bildirim = new Bildirim();
        HesaplaVeYansit hesaplaVeYansit = new HesaplaVeYansit();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CRUD_Operations cRUD = new CRUD_Operations();
        Ayarlar ayarlar = new Ayarlar();
        int Tur = 0, Id = 0, FirmaId = 0, RenkId = 0;
        CrudRepository crudRepository = new CrudRepository();
        private void FrmBoyahaneRenkKartlari_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            object[] kart = { txtRenkKodu,txtRenkAdi,btnCari,txtCariAdi,btnVaryant,dateTarih,dateTalepTarihi,dateOkeyTarihi,
                txtPantoneNo,txtFiyat,radioIplik,radioKumas,cmbDoviz };
            yardimciAraclar.KartTemizle(kart);
            chckKullanimda.Checked = true;
            this.Id = 0; this.FirmaId = 0;
        }
        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmBoyahaneRenkKartlariListesi frm = new Liste.FrmBoyahaneRenkKartlariListesi(true);
            frm.ShowDialog();
            this.Id = frm.Id;
            Tur = frm.TypeNo;
            radioKumas.Checked = Tur == 1 ? true : false;
            radioIplik.Checked = Tur == 1 ? false : true;
            txtRenkKodu.Text = frm.Code;
            txtRenkAdi.Text = frm.Namee;
            FirmaId = frm.CompanyId;
            btnCari.Text = frm.CompanyCode;
            txtCariAdi.Text = frm.CompanyName;
            txtPantoneNo.Text = frm.PantoneNo;
            txtFiyat.Text = frm.Price;
            cmbDoviz.Text = frm.Forex;
            chckKullanimda.Checked = frm.IsUse;

        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard("Color", Id, Temizle);
        }
        private void btnCari_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            hesaplaVeYansit.FirmaKoduVeAdiYansit(btnCari, txtCariAdi, ref this.FirmaId);
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }
        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }
        private void radioIplik_CheckedChanged(object sender, EventArgs e)
        {
            this.Tur = 2;
        }
        private void radioKumas_CheckedChanged(object sender, EventArgs e)
        {
            this.Tur = 1;
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateTalepTarihi.EditValue = DateTime.Now;
            dateOkeyTarihi.EditValue = DateTime.Now;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!radioKumas.Checked && !radioIplik.Checked)
            {
                bildirim.Uyari("Kayıt yapabilmek için renk türü seçmelisiniz!");
                return;
            }
            var parameters = new Dictionary<string, object>
            {
                { "Type", Tur},
                { "Code", txtRenkKodu.Text },
                { "Name", txtRenkAdi.Text},
                { "CompanyId", this.FirmaId},
                { "ParentId",0},
                { "Date",dateTarih.EditValue},
                { "RequestDate",dateTalepTarihi.EditValue},
                { "ConfirmDate",dateOkeyTarihi.EditValue},
                { "PantoneNo",txtPantoneNo.Text},
                { "Price",yardimciAraclar.GetDecimalValue(txtFiyat.Text)},
                { "Forex",cmbDoviz.Text},
                { "IsParent",false},
                { "IsUse",chckKullanimda.Checked},
            };
            if (this.Id == 0)
            {
                this.Id = crudRepository.Insert("Color", parameters);
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update("Color", this.Id, parameters);
                bildirim.GuncellemeBasarili();
            }
        }
        void ListeGetir(string KayitTipi)
        {
            /*
             Listeleme işlemi tamamlanamadı!!! bilgiler araçlara yansımıyor
            kontrol edilmelidir.
            22.05.2025
             */
            try
            {
                int id = this.Id;
                int? istenenId = crudRepository.GetIdTwoJoinTable(KayitTipi, "Color", id, "Company", "CompanyId", 0);
                if (istenenId == null)
                {
                    bildirim.Uyari("Başka bir kayıt bulunamadı.");
                    return;
                }
                string query = $@"Select 
                                COL.Id [Id],
                                ISNULL(Type,0) [Boya Tipi],
                                ISNULL(Code,'') [Code],
                                ISNULL(Name,'') [Name],
                                ISNULL(COM.Id,0) [CompanyId],
                                ISNULL(COM.CompanyCode,0) [CompanyCode],
                                ISNULL(COM.CompanyName,0) [CompanyName],
                                ISNULL(COL.Date,'') [Date],
                                ISNULL(COL.RequestDate,'') [RequestDate],
                                ISNULL(COL.ConfirmDate,'') [ConfirmDate],
                                ISNULL(COL.PantoneNo,'') [Pantone No],
                                ISNULL(COL.Price,0) [Fiyat],
                                ISNULL(COL.Forex,0) [Döviz],
                                ISNULL(COL.IsUse,0) [Kullanımda]
                                from Color COL
                                left join Company COM on COL.CompanyId = COM.Id";
                var liste = crudRepository.GetAfterOrBeforeRecord(query, istenenId.Value);
                if (liste != null && liste.Count > 0)
                {
                    //yardimciAraclar.ClearGridViewRows(gridView1);
                    var item = liste[0];
                    dateTarih.Text = item.Date.ToString();
                    this.Id = Convert.ToInt32(item.Id);
                    txtRenkKodu.Text = item.Code.ToString();
                    MessageBox.Show(txtRenkKodu.Text);
                    txtRenkAdi.Text = item.Name.ToString();
                    this.FirmaId = Convert.ToInt32(item.CompanyId);
                    btnCari.Text = item.CompanyCode.ToString();
                    txtCariAdi.Text = item.CompanyName.ToString();
                    dateTalepTarihi.Text = item.RequestDate.ToString();
                    dateOkeyTarihi.Text = item.ConfirmDate.ToString();
                    //txtYetkili.Text = item.Authorized.ToString();
                    //txtVade.Text = item.Maturity.ToString();
                    //comboBoxEdit1.Text = item.PaymentType.ToString();
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
            /*
             try
            {


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
                    dateIrsaliyeTarihi.Text = item.DispatchDate.ToString();
                    txtIrsaliyeNo.Text = item.DispatchNo.ToString();
                    rchAciklama.Text = item.ExplanationFis.ToString();
                    //txtYetkili.Text = item.Authorized.ToString();
                    //txtVade.Text = item.Maturity.ToString();
                    //comboBoxEdit1.Text = item.PaymentType.ToString();
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
             
             */
        }

    }
}