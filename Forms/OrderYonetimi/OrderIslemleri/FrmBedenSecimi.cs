using Hesap.DataAccess;
using Hesap.Models;
using System.ComponentModel;
using System.Linq;

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmBedenSecimi : DevExpress.XtraEditors.XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        public FrmBedenSecimi()
        {
            InitializeComponent();
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            gridControl1.DataSource = new BindingList<InventoryRequirement>(); //new BindingList<ReceiptItem>();
        }
        private void FrmBedenSecimi_Load(object sender, System.EventArgs e)
        {
            Listele();
        }

        void Listele()
        {
            gridControl1.DataSource = crudRepository.GetAll<InventoryRequirement>("InventoryRequirement").ToList();
        }
    }
}