using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Models;
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

namespace Hesap.Forms.Parametreler
{
    public partial class frmUretimYonetimiParams : DevExpress.XtraEditors.XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        Bildirim bildirim = new Bildirim();
        public frmUretimYonetimiParams()
        {
            InitializeComponent();
        }

        private void frmUretimYonetimiParams_Load(object sender, EventArgs e)
        {
            VerileriGetir();
        }
        void VerileriGetir()
        {
            var data = crudRepository.GetAll<ProductionManagementParams>("ProductionManagementParams").FirstOrDefault();
            if (data == null)
            {
                crudRepository.Insert("ProductionManagementParams", new Dictionary<string, object> { { "KasmaPayi", 0 } });
            }
            else
            {
                txtKasmaPayi.Text = data.KasmaPayi.ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var update_params = new Dictionary<string, object> { { "KasmaPayi", txtKasmaPayi.Text } };
            crudRepository.Update("ProductionManagementParams", 1, update_params);
            bildirim.GuncellemeBasarili();
        }
    }
}