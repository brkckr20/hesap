using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Helpers
{
    public class DateConvertHelper
    {
        Bildirim bildirim = new Bildirim();
        string databaseTuru;
        Ayarlar ayarlar = new Ayarlar();
        public DateConvertHelper()
        {
            if (ayarlar.VeritabaniTuru() == "mssql")
            {
                databaseTuru = "mssql";
            }
            else
            {
                databaseTuru = "sqlite";
            }
        }
        public void ConvertDateForDbType(DevExpress.XtraEditors.DateEdit dateEdit, string tarihStr)
        {
            if (this.databaseTuru == "sqlite")
            {
                DateTime tarih;
                bool success = DateTime.TryParseExact(tarihStr, "dd.MM.yyyy",
                                              System.Globalization.CultureInfo.InvariantCulture,
                                              System.Globalization.DateTimeStyles.None,
                                              out tarih);
                if (success)
                {
                    dateEdit.EditValue = tarih;
                }
                else
                {
                    bildirim.Uyari("Listeleme esnasında geçersiz tarih formatı");
                }
            }
            else
            {
                dateEdit.EditValue = Convert.ToDateTime(tarihStr);
            }
        }
        public object SaveFormattedDate(DevExpress.XtraEditors.DateEdit dateEdit)
        {
            DateTime date = (DateTime)dateEdit.EditValue;
            string formattedDate = date.ToString("dd.MM.yyyy");
            if (this.databaseTuru == "sqlite")
            {
                return formattedDate;
            }
            else
            {
                return dateEdit.EditValue;
            }
        }
    }
}
