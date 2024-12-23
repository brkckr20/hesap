using DevExpress.XtraEditors;
using Hesap.Forms.OrderYonetimi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmOrderGirisi : DevExpress.XtraEditors.XtraForm
    {
        public FrmOrderGirisi()
        {
            InitializeComponent();
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void repoBtnModelKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmModelKartiListesi frm = new Liste.FrmModelKartiListesi();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.Kodu))
            {
                gridVModelBilgi.AddNewRow();
                int newRowHandle = gridVModelBilgi.FocusedRowHandle;
                gridVModelBilgi.SetRowCellValue(newRowHandle, "ModelKodu", frm.Kodu);
                gridVModelBilgi.SetRowCellValue(newRowHandle, "ModelAdi", frm.Adi);
                gridVModelBilgi.SetRowCellValue(newRowHandle, "OrjModelAdi", frm.OrjAdi);
                gridVModelBilgi.SetRowCellValue(newRowHandle, "Id", frm.Id);
            }
        }

        private void FrmOrderGirisi_Load(object sender, EventArgs e)
        {
            gridModelBilgi.DataSource = new BindingList<ModelBilgileri>();
        }
    }
}