using DevExpress.XtraEditors;
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
    public partial class FrmRenkBedenAdetleri : DevExpress.XtraEditors.XtraForm
    {
        public string SecilenRenk;
        public FrmRenkBedenAdetleri()
        {
            InitializeComponent();
           
        }

        private void FrmRenkBedenAdetleri_Load(object sender, EventArgs e)
        {
            this.Text += " " + SecilenRenk;
        }
    }
}