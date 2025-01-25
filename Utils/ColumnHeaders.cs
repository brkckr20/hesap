using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Utils
{
    public static class ColumnHeaders
    {
        public static Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            { "InventoryCode", "Malzeme Kodu" },
            { "ReceiptId", "Fatura ID" }, // burası fatura id değil kayıt no -- değiştirilecek
            { "CompanyCode", "Firma Kodu" },
            { "CompanyName", "Firma Adı" },
            { "Explanation", "Açıklama" },
            { "InvoiceNo", "Fatura No" },
            { "InvoiceDate", "Fatura Tarihi" },
            { "DispatchNo", "İrsaliye No" },
            { "DispatchDate", "İrsaliye Tarihi" },
            { "TrackingNumber", "Takip No" },
            { "OperationType", "İşlem Türü" },
            { "InventoryName", "Malzeme Adı" },
            { "Piece", "Adet" },
            { "UnitPrice", "Birim Fiyat" },
            { "D2Id", "D2 Id" }
        };
    }
}
