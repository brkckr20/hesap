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

namespace Hesap.Forms.Liste
{
    public partial class FrmKayitNoGoster : DevExpress.XtraEditors.XtraForm
    {
        int _id;
        string _kaydeden,_guncelleyen;
        DateTime _kayitTarihi, _guncellemeTarihi;
        public FrmKayitNoGoster(int id,string kaydeden=null,DateTime? kayitTarihi = null,string guncelleyen = null,DateTime? guncellemeTarihi = null)
        {
            InitializeComponent();
            _id = id;
            this._kaydeden = kaydeden;
            this._kayitTarihi = (DateTime)kayitTarihi;
            this._guncelleyen = guncelleyen;
            this._guncellemeTarihi = (DateTime)guncellemeTarihi;
        }

        private void FrmKayitNoGoster_Load(object sender, EventArgs e)
        {
            lblKayitNo.Text = this._id.ToString();
            lblKayitTarihi.Text = this._kayitTarihi.ToString();
            lblKayıtEden.Text = this._kaydeden.ToString();
            lblGuncelleyen.Text = this._guncelleyen.ToString();
            lblGuncellemeTarihi.Text = this._guncellemeTarihi.ToString();
        }
    }
}