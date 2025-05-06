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

namespace Hesap.Forms.Diger
{
    public partial class FrmSaveInfo : XtraForm
    {
        int _recId;
        string _tableName1;
        public FrmSaveInfo(int RecId,string TableName1)
        {
            _recId = RecId;
            _tableName1 = TableName1;

            InitializeComponent();
        }

        private void FrmSaveInfo_Load(object sender, EventArgs e)
        {
            lblKayitTarihi.Text = _recId.ToString(); // bu alanlar değişecek
            lblKayitEden.Text = _tableName1;
        }
    }
}