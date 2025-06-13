using DevExpress.XtraEditors;
using Hesap.DataAccess;
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
        CrudRepository crudRepository = new CrudRepository();
        Bildirim bildirim = new Bildirim();
        private int Id=0;
        
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Code", txtKodu.Text },
                { "Name", txtAdi.Text},
                { "Date",DateTime.Now },
                { "IsParent",true},
                { "Explanation", txtAciklama.Text},
                { "IsUse",chckKullanimda.Checked},
                { "EmployeeId",1},
            };
            if (this.Id == 0)
            {
                this.Id = crudRepository.Insert("Color", parameters);
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update("Color", this.Id, parameters);
                bildirim.GuncellemeBasarili();
            }
        }
    }
}