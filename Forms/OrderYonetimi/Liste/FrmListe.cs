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

namespace Hesap.Forms.OrderYonetimi.Liste
{
    public partial class FrmListe : DevExpress.XtraEditors.XtraForm
    {
        public FrmListe()
        {
            InitializeComponent();
        }
        string _ekranAdi;
        public FrmListe(string ekranAdi)
        {
            this.Text = ekranAdi + " Listesisssssssssssssss";
            this._ekranAdi = ekranAdi;
        }

        private void FrmListe_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
    }
}