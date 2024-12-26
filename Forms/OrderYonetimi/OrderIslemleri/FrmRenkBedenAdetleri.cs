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
    public partial class FrmRenkBedenAdetleri : DevExpress.XtraEditors.XtraForm
    {
        public string SecilenRenk;
        public int ModelId;
        Listele listele = new Listele();
        public FrmRenkBedenAdetleri()
        {
            InitializeComponent();
           
        }

        private void FrmRenkBedenAdetleri_Load(object sender, EventArgs e)
        {
            this.Text += " " + SecilenRenk;
            Listele();
        }
        void Listele()
        {
            string sql = $"select Beden,Id from ModelBedenSeti where ModelId = '{ModelId}'";
            listele.Liste(sql, gridControl1);
        }
    }
}