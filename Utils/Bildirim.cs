using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Utils
{
    public class Bildirim
    {
        public void Basarili(string tip = "Kayıt")
        {
            XtraMessageBox.Show($"{tip} işlemi başarılı bir şekilde gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void GuncellemeBasarili()
        {
            XtraMessageBox.Show("Güncelleme başarılı bir şekilde gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void SilmeBasarili()
        {
            XtraMessageBox.Show("Silme başarılı bir şekilde gerçekleştirildi.", "Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public bool SilmeOnayı()
        {
            if (XtraMessageBox.Show("Seçilen kayıt silinecek! Bu işlem geri alınamaz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
        public void Uyari(string tip)
        {
            XtraMessageBox.Show($"{tip}", "❗ Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
