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
    public partial class FrmRenkKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmRenkKarti()
        {
            InitializeComponent();
        }
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        private int Id=0;
        
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Kodu", txtKodu.Text },
                { "Adi", txtAdi.Text },
                { "Kullanimda", chckKullanimda.Checked},
                { "Aciklama", txtAciklama.Text},
                { "ErisimKodu", txtErisimKodu.Text},
                { "PazarlamaciId", 1 },
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (this.Id == 0)
                {
                    this.Id = cRUD.InsertRecord("RenkKarti", parameters);
                    bildirim.Basarili();
                }
            }
        }
    }
}