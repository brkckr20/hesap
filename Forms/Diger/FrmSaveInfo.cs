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

namespace Hesap.Forms.Diger
{
    public partial class FrmSaveInfo : XtraForm
    {
        int _recId;
        string _tableName1;
        CrudRepository crudRepository = new CrudRepository();
        Bildirim bildirim = new Bildirim();
        public FrmSaveInfo(int RecId,string TableName1)
        {
            _recId = RecId;
            _tableName1 = TableName1;

            InitializeComponent();
        }
        void GetSaveInfo()
        {
            try
            {       // eğer güncelleme bilgisi yok ise açılırken boş gelmektedir. kontrol edilecek
                    var item = crudRepository.GetById<Receipt>(_tableName1, _recId);
                    var userinfo = crudRepository.GetById<User>("Users", item.SavedUser);
                    var updateinfo = crudRepository.GetById<User>("Users", item.UpdatedUser);
                    if (item != null && userinfo != null & updateinfo != null)
                    {
                        lblKayitTarihi.Text = item.SavedDate.ToString().Substring(0, 10);
                        lblKayitEden.Text = userinfo.Code + " - " + userinfo.Name + " " + userinfo.Surname;
                        lblGuncellemeTarihi.Text = item.UpdatedDate.ToString().Substring(0, 10);
                        lblGuncelleyen.Text = updateinfo.Code + " - " + updateinfo.Name + " " + updateinfo.Surname;

                    }
                    else
                    {
                        lblKayitTarihi.Text = "";
                        lblKayitEden.Text = "";
                        lblGuncellemeTarihi.Text = "";
                        lblGuncelleyen.Text = "";
                    }
                
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
                throw;
            }
        }
        private void FrmSaveInfo_Load(object sender, EventArgs e)
        {
            GetSaveInfo();
        }
    }
}