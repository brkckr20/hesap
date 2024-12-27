using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Forms.OrderYonetimi
{
    public class Metotlar
    {
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public Dictionary<string, object> ModelBeden(int rowIndex, int Id, DevExpress.XtraGrid.Views.Grid.GridView gridView1)
        {
            return new Dictionary<string, object>
            {
                { "ModelId", Id },
                { "Beden", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Beden")) ?? "" },
            };
        }
        public Dictionary<string, object> BedenRenkAdetParametreleri(int rowIndex, int SiparisId, DevExpress.XtraGrid.Views.Grid.GridView gridView1, int ModelId, int RenkId)
        {
            return new Dictionary<string, object>
            {
                { "SiparisId", SiparisId },
                { "ModelId", ModelId },
                { "BedenId", gridView1.GetRowCellValue(rowIndex, "BedenId") ?? 0 },
                { "RenkId", RenkId },
                { "Miktar", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "Miktar")) },
            };
        }
    }
}
