using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Utils
{
    public class KalemParametreleri
    {
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public Dictionary<string, object> KumasDepoParams(int rowIndex, int Id, GridView gridView1)
        {
            return new Dictionary<string, object>
            {
                { "RefNo", Id },
                { "KalemIslem", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "KalemIslem")) ?? "" },
                { "KumasId", gridView1.GetRowCellValue(rowIndex, "KumasId") ?? 0 },
                { "GrM2", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "GrM2")) },
                { "BrutKg", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "BrutKg")) },
                { "NetKg", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "NetKg")) },
                { "BrutMt", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "BrutMt")) },
                { "NetMt", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "NetMt")) },
                { "Adet", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "Adet")) },
                { "Fiyat", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "Fiyat")) },
                { "FiyatBirimi", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "FiyatBirim")) },
                { "DovizCinsi", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "DovizCinsi")) },
                { "RenkId", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "RenkId")) },
                { "Aciklama", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Aciklama")) },
                { "UUID", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "UUID")) },
                { "SatirTutari", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "SatirTutari")) },
                { "TakipNo", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "TakipNo")) },
                { "DesenId", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "DesenId")) },
                { "BoyaIslemId", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "BoyaIslemId")) }
            };
        }

        public Dictionary<string, object> GetGridViewData(int rowIndex, int id, GridView gridView, List<string> columnNames)
        {
            var data = new Dictionary<string, object> { { "RefNo", id } };

            foreach (var columnName in columnNames)
            {
                var cellValue = gridView.GetRowCellValue(rowIndex, columnName);
                data[columnName] = cellValue ?? GetDefaultValue(columnName);
            }

            return data;
        }
        private object GetDefaultValue(string columnName)
        {
            // İlgili kolona göre varsayılan değerleri döndüren bir metod yazabilirsiniz.
            // Örneğin, bazı kolonlar için 0, bazıları için boş string vb.
            if (columnName == "KumasId" || columnName.Contains("Id") || columnName.Contains("Kg") || columnName.Contains("Mt") || columnName.Contains("Adet") || columnName.Contains("Fiyat") || columnName.Contains("RenkId") || columnName.Contains("SatirTutari") || columnName.Contains("DesenId") || columnName.Contains("BoyaIslemId"))
                return 0;
            return "";
        }
    }
}

