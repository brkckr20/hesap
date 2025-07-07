using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmBedenSeti : DevExpress.XtraEditors.XtraForm
    {
        Bildirim bildirim = new Bildirim();
        CrudRepository crudRepository = new CrudRepository();
        int modelId = 0,secilenBedenId;
        public FrmBedenSeti(int _modelId)
        {
            InitializeComponent();
            this.modelId = _modelId;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "SizeName", txtBedenSeti.Text},
                { "InventoryType", Convert.ToInt32(InventoryTypes.Kumas)},
                { "InventoryId", modelId}
            };
            if (!string.IsNullOrEmpty(txtBedenSeti.Text.Trim()))
            {
                if (!lstBedenler.Items.Contains(txtBedenSeti.Text.Trim()))
                {
                    lstBedenler.Items.Add(txtBedenSeti.Text.Trim());
                    int size_id = crudRepository.Insert("Size", parameters);
                    var size_params = new Dictionary<string, object> { { "SizeId",size_id }, { "InventoryId", modelId } }; // beden eklendikten sonra ihtiyaç tablosuna ilgili bedenin eklenmesi
                    crudRepository.Insert("InventoryRequirement", size_params);
                    txtBedenSeti.Text = string.Empty;
                    txtBedenSeti.Focus();
                }
                else
                {
                    bildirim.Uyari($"{txtBedenSeti.Text.Trim()} bedeni daha önce eklenmiş!");
                }

            }
            else
            {
                bildirim.Uyari("Lütfen bir metin giriniz!");
            }
        }

        private void txtBedenSeti_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnKaydet.PerformClick();
                e.Handled = true;
            }
        }

        private void lstBedenler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lstBedenler.SelectedItem != null)
                {
                    var seciliBeden = lstBedenler.SelectedItem.ToString();

                    if (bildirim.SilmeOnayı())
                    {
                        lstBedenler.Items.Remove(seciliBeden);
                        crudRepository.Delete("Size",secilenBedenId);
                        BedenleriGetir();
                    }
                }
                e.Handled = true;
            }
        }
        void BedenleriGetir()
        {
            lstBedenler.DisplayMember = "SizeName";
            lstBedenler.DataSource = crudRepository.GetAll<Size>("Size").ToList();
        }

        private void FrmBedenSeti_Load(object sender, EventArgs e)
        {
            BedenleriGetir();
        }

        private void lstBedenler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBedenler.SelectedItem is Hesap.Models.Size secilenBeden)
            {
                secilenBedenId = secilenBeden.Id;
            }
        }
    }
}