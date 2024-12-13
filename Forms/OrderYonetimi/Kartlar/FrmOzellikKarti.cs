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

namespace Hesap.Forms.OrderYonetimi.Kartlar
{
    public partial class FrmOzellikKarti : DevExpress.XtraEditors.XtraForm
    {
        public string _type;
        public FrmOzellikKarti()
        {
            InitializeComponent();
        }
        public FrmOzellikKarti(string tip)
        {
            this._type = tip;
        }

        private void FrmOzellikKarti_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            this.Text = TipGetir();
            MessageBox.Show("gelen tip" + this._type);
        }

        string TipGetir()
        {
            if (_type == "sifir")
            {
                return "aaaa";
            }
            else
            {
                return "bbb";
            }
        }
    }
}