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

namespace Hesap.Forms.UretimYonetimi.Barkod
{
    public partial class FrmEtiketBasim : DevExpress.XtraEditors.XtraForm
    {
        public FrmEtiketBasim()
        {
            InitializeComponent();
        }
        public int Id;
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnTalimatlar_Click(object sender, EventArgs e)
        {
            FrmSiparisSecimi frm = new FrmSiparisSecimi(); // SİPARİŞ AKTARIMINDAN DEVAM EDİLECEK
            frm.ShowDialog();
            if (frm.itemList.Count > 0)
            {
                foreach (var item in frm.itemList)
                {
                    gridView1.AddNewRow();
                    int newRowHandle = gridView1.FocusedRowHandle;
                    var values = item.Split(';');
                    gridView1.SetRowCellValue(newRowHandle, "ArtNo", values[0]);
                    gridView1.SetRowCellValue(newRowHandle, "OrderNo", values[1]);
                    gridView1.SetRowCellValue(newRowHandle, "UrunKodu", values[2]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker1", values[3]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker2", values[4]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker3", values[5]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker4", values[6]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker5", values[7]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker6", values[8]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker7", values[9]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker8", values[10]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker9", values[11]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker10", values[12]);
                    gridView1.SetRowCellValue(newRowHandle, "Barkod", values[13]);
                    gridView1.SetRowCellValue(newRowHandle, "EbatBeden", values[14]);
                    gridView1.SetRowCellValue(newRowHandle, "Varyant1", values[15]);
                    gridView1.SetRowCellValue(newRowHandle, "Miktar", Convert.ToInt32(values[16]));
                    


                }
            }
            /*
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
             */
        }
        private Dictionary<string, object> CreateKalemParameters(int rowIndex)
        {
            return new Dictionary<string, object>
            {
                { "RefNo", this.Id },
                { "UrunKodu", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "UrunKodu")) },
                { "ArtNo", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "ArtNo")) },
                { "Sticker1", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker1")) },
                { "Sticker2", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker2")) },
                { "Sticker3", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker3")) },
                { "Sticker4", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker4")) },
                { "Sticker5", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker5")) },
                { "Sticker6", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker6")) },
                { "Sticker7", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker7")) },
                { "Sticker8", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker8")) },
                { "Sticker9", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker9")) },
                { "Sticker10", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker10")) },
                { "MusteriOrderNo", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "MusteriOrderNo")) },
                { "OrderNo", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "OrderNo")) },
                { "Barkod", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Barkod")) },
                { "Varyant1", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Varyant1")) },
                { "Varyant2", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Varyant2")) },
                { "EbatBeden", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "EbatBeden")) },
                { "Miktar", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Miktar")) },
            };
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Tarih", dateTarih.EditValue },
                { "Aciklama", txtAciklama.Text},
                { "BasimSayisi", txtBasimSayisi.Text},
                { "Yuzde", txtYuzde.Text},
                { "Tekli", chckTekli.Checked},
            };
            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord("Etiket1", parameters);
                txtKayitNo.Text = this.Id.ToString();
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var kalemParameters = CreateKalemParameters(i);
                    var d2Id = cRUD.InsertRecord("Etiket", kalemParameters);
                    //gridView1.SetRowCellValue(i, "D2Id", d2Id);
                }
                bildirim.Basarili();
            }
        }

        private void FrmEtiketBasim_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = new BindingList<_Etiket>();
        }

        private void barkodBasımıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Rapor.FrmRaporSecimEkrani frm = new Rapor.FrmRaporSecimEkrani(this.Text,this.Id);
            frm.ShowDialog();
        }
    }
}