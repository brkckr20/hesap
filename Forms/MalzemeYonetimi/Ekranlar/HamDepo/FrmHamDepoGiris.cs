using DevExpress.XtraEditors;
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
        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
        public int FirmaId = 0, Id = 0;

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
                //for (int i = 0; i < gridView1.RowCount - 1; i++)
                //{
                //    var kalemParameters = CreateKalemParameters(i);
                //    var d2Id = cRUD.InsertRecord("HamDepo2", kalemParameters);
                //    gridView1.SetRowCellValue(i, "D2Id", d2Id);
                //}
                bildirim.Basarili();
            }
            else
            {
                cRUD.UpdateRecord("HamDepo1", parameters, this.Id);
                //for (int i = 0; i < gridView1.RowCount - 1; i++)
                //{
                //    var d2Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
                //    var kalemParameters = CreateKalemParameters(i);
                //    if (d2Id > 0) // Eğer D2Id varsa güncelle
                //    {
                //        cRUD.UpdateRecord("HamDepo2", kalemParameters, d2Id);
                //    }
                //    else // Yeni kayıt ekle
                //    {
                //        //kalemParameters["RefNo"] = this.Id; // RefNo ekle
                //        var yeniId = cRUD.InsertRecord("HamDepo2", kalemParameters);
                //        gridView1.SetRowCellValue(i, "D2Id", yeniId); // Yeni Id'yi gridView'a set et
                //    }
                //}
                bildirim.GuncellemeBasarili();
            }
        }
    }
}