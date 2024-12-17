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

namespace Hesap.Forms.OrderYonetimi.Kartlar
{
    public partial class FrmKategoriKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmKategoriKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Adi", txtAd.Text },
                { "OrjAdi", txtOrjAd.Text },
                { "Kullanimda", chckKullanimda.Checked},
                { "Tip","0"},
                { "EkranAdi",this.Text},
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (this.Id == 0)
                {
                    this.Id = cRUD.InsertRecord("OzellikKarti", parameters);
                    bildirim.Basarili();
                }
                else
                {
                    cRUD.UpdateRecord("OzellikKarti", parameters, this.Id);
                    bildirim.GuncellemeBasarili();
                }
            }
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmListe frm = new Liste.FrmListe(this.Text);
            frm.ShowDialog();
        }
    }
}