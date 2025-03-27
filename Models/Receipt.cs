using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
        public int ReceiptType { get; set; }
        public DateTime ReceiptDate  { get; set; }
        public int CompanyId { get; set; }
        public string Explanation { get; set; }
        public int WareHouseId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string DispatchNo { get; set; }
        public DateTime DispatchDate  { get; set; }
        public DateTime DuaDate { get; set; }
        public string Authorized { get; set; }
        public string Approved { get; set; }
        public bool IsFinished { get; set; }
        public string CustomerOrderNo { get; set; }
        public string OrderNo { get; set; }
        public int Maturity { get; set; }
    }
}
