using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.Talimatlar
{
    public partial class FrmKumasSaTalimati : DevExpress.XtraEditors.XtraForm
    {
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        Numarator numarator = new Numarator();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations cRUD = new CRUD_Operations();
        int FirmaId = 0, Id = 0;
        KalemParametreleri parametreler = new KalemParametreleri();
        public FrmKumasSaTalimati()
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
        private void FrmKumasSaTalimati_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<_KumasDepoKalem>();
            txtTalimatNo.Text = numarator.NumaraVer("HamKSaTal");
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "IslemCinsi", "SaTal" },
                { "TalimatNo", txtTalimatNo.Text },
                { "Tarih", dateTarih.EditValue },
                { "FirmaId", this.FirmaId },
                { "Aciklama", rchAciklama.Text },
                { "Yetkili", txtYetkili.Text },
                { "Vade", txtVade.Text },
                { "OdemeSekli", comboBoxEdit1.Text }
            };
            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord("HamDepo1", parameters);
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var kalemParameters = parametreler.KumasDepoParams(i, this.Id, gridView1);
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
                    var kalemParameters = parametreler.KumasDepoParams(i, this.Id, gridView1);
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

        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }
        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1, this.Text);
        }
        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);

        }
        private void repoBoyaRenkKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int newRowHandle = gridView1.FocusedRowHandle;
            yansit.BoyahaneRenkBilgileriYansit(gridView1, newRowHandle);
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        void FormTemizle()
        {
            txtTalimatNo.Text = numarator.NumaraVer("HamKSaTal");
            object[] bilgiler = { dateTarih, txtFirmaKodu, txtFirmaUnvan, rchAciklama, txtYetkili, txtVade, comboBoxEdit1 };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<_IplikDepoKalem>();
            this.Id = 0;
            this.FirmaId = 0;
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            HamDepo.FrmHamDepoListe frm = new HamDepo.FrmHamDepoListe("SaTal");
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                dateTarih.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                txtFirmaKodu.Text = frm.veriler[0]["FirmaKodu"].ToString();
                txtFirmaUnvan.Text = frm.veriler[0]["FirmaUnvan"].ToString();
                this.FirmaId = Convert.ToInt32(frm.veriler[0]["FirmaId"]);
                rchAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                txtTalimatNo.Text = frm.veriler[0]["TalimatNo"].ToString();
                txtYetkili.Text = frm.veriler[0]["Yetkili"].ToString();
                txtVade.Text = frm.veriler[0]["Vade"].ToString();
                comboBoxEdit1.Text = frm.veriler[0]["OdemeSekli"].ToString();
                string[] columnNames = yansit.SorgudakiKolonIsimleriniAl(frm.sql);
                yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, "D2Id", 0);

        }

        private void talimatFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rapor.FrmRaporSecimEkrani frm = new Rapor.FrmRaporSecimEkrani(this.Text, this.Id);
            frm.ShowDialog();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {

        }

        private void btnIleri_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (e.KeyCode == Keys.Delete)
            {
                cRUD.SatirSil(gridView, "HamDepo2");
            }
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int newRowHandle = gridView1.FocusedRowHandle;
            yansit.KumasBilgileriYansit(gridView1, newRowHandle);
        }

    }
}