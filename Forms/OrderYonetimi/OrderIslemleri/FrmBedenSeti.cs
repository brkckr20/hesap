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

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmBedenSeti : DevExpress.XtraEditors.XtraForm
    {
        Bildirim bildirim = new Bildirim();
        public FrmBedenSeti()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBedenSeti.Text.Trim()))
            {
                if (!lstBedenler.Items.Contains(txtBedenSeti.Text.Trim()))
                {
                    lstBedenler.Items.Add(txtBedenSeti.Text.Trim());
                    txtBedenSeti.Text = string.Empty;
                    txtBedenSeti.Focus();
                }
                else
                {
                    bildirim.Uyari($"{txtBedenSeti.Text.Trim()} bedeni daha önce eklenmiş!");
                }
                
            }
            else
            {
                bildirim.Uyari("Lütfen bir metin giriniz!");
            }
        }

        private void txtBedenSeti_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnKaydet.PerformClick();
                e.Handled = true;
            }
        }
    }
}