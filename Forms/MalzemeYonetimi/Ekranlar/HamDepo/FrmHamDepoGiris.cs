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
    public partial class FrmHamDepoGiris : DevExpress.XtraEditors.XtraForm
    {
        public FrmHamDepoGiris()
        {
            InitializeComponent();
        }
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Metotlar metotlar = new Metotlar();
        public int FirmaId = 0, Id = 0;

        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateIrsaliyeTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<_KumasDepoKalem>();
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }
        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }

        private void btnListe_Click(object sender, EventArgs e)
        {

        }

        private void FrmHamDepoGiris_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.KumasBilgileriYansit(gridView1);
        }

        private void btnTalimatlar_Click(object sender, EventArgs e)
        {
            FrmTalimatlar frm = new FrmTalimatlar();
            frm.ShowDialog();
            if (frm.satinAlmaListesi.Count > 0)
            {
                var parts = frm.satinAlmaListesi[0].Split(';');
                this.FirmaId = Convert.ToInt32(parts[1]);
                txtFirmaKodu.Text = parts[2].ToString();
                txtFirmaUnvan.Text = parts[3].ToString();
            }
            foreach (var item in frm.satinAlmaListesi)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "KalemIslem", "Satın Alma");
                gridView1.SetRowCellValue(newRowHandle, "TalimatNo", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "TakipNo", values[4]);
                gridView1.SetRowCellValue(newRowHandle, "KumasId", values[5]);
                gridView1.SetRowCellValue(newRowHandle, "KumasKodu", values[6]);
                gridView1.SetRowCellValue(newRowHandle, "KumasAdi", values[7]);
                gridView1.SetRowCellValue(newRowHandle, "BrutKg", values[11]); // otomatik doldurmada hesaplamada kullanılması için eklendi
                gridView1.SetRowCellValue(newRowHandle, "Fiyat", values[9]);
                gridView1.SetRowCellValue(newRowHandle, "DovizCinsi", values[10]);
                gridView1.SetRowCellValue(newRowHandle, "NetKg", values[11]);
                gridView1.SetRowCellValue(newRowHandle, "GrM2", values[12]);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "IslemCinsi", "Giriş" },
                { "Tarih", dateTarih.EditValue },
                { "IrsaliyeTarihi", dateIrsaliyeTarihi.EditValue },
                { "IrsaliyeNo", txtIrsaliyeNo.Text },
                { "FirmaId", this.FirmaId },
                { "Aciklama", rchAciklama.Text },
            };
            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord("HamDepo1", parameters);
                txtKayitNo.Text = this.Id.ToString();
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var kalemParameters = metotlar.CreateHameDepo2KalemParameters(i,this.Id,gridView1);
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