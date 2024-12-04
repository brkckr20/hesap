using DevExpress.XtraEditors;
using Hesap.Context;
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
        Metotlar metotlar = new Metotlar();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }

        private void FrmHamDepoCikis_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void btnAciklamaGetir_Click(object sender, EventArgs e)
        {
            Liste.FrmAciklamaListesi frm = new Liste.FrmAciklamaListesi(this.Text);
            frm.ShowDialog();
            rchAciklama.EditValue = frm.Aciklama;
        }

        private void txtNakliyeci_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.TasiyiciBilgileriYansit(txtUnvan,txtAd,txtSoyad,txtTC,txtPlaka,txtDorse,txtNakliyeci,ref this.TasiyiciId);
        }

        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateIrsaliyeTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<_KumasDepoKalem>();
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "IslemCinsi", "Çıkış" },
                { "Tarih", dateTarih.EditValue },
                { "IrsaliyeTarihi", dateIrsaliyeTarihi.EditValue },
                { "IrsaliyeNo", txtIrsaliyeNo.Text },
                { "FirmaId", this.FirmaId },
                { "Aciklama", rchAciklama.Text },
                { "TasiyiciId", txtNakliyeci.Text },
                { "TalimatId", TalimatId },

            };
            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord("HamDepo1", parameters);
                txtKayitNo.Text = this.Id.ToString();
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var kalemParameters = metotlar.CreateHameDepo2KalemParameters(i, this.Id, gridView1);
                    var d2Id = cRUD.InsertRecord("HamDepo2", kalemParameters);
                    gridView1.SetRowCellValue(i, "D2Id", d2Id);
                }
                bildirim.Basarili();
            }
            else
            {
                cRUD.UpdateRecord("HamDepo1", parameters, this.Id);
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var d2Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
                    var kalemParameters = metotlar.CreateHameDepo2KalemParameters(i, this.Id, gridView1);
                    if (d2Id > 0)
                    {
                        cRUD.UpdateRecord("HamDepo2", kalemParameters, d2Id);
                    }
                    else
                    {
                        var yeniId = cRUD.InsertRecord("HamDepo2", kalemParameters);
                        gridView1.SetRowCellValue(i, "D2Id", yeniId);
                    }
                }
                bildirim.GuncellemeBasarili();
            }
        }
    }
}