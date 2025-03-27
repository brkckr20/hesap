using System.ComponentModel;

namespace Hesap.Models
{
    public class ReceiptItem : INotifyPropertyChanged
    {
        private decimal _piece;
        private decimal _unitPrice;
        private decimal _rowAmount;
        private int _vat;

        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public string OperationType { get; set; }
        public int InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public int GrM2 { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetWeight { get; set; }
        public decimal GrossMeter { get; set; }
        public decimal NetMeter { get; set; }
        public decimal Piece
        {
            get => _piece;
            set
            {
                if (_piece != value)
                {
                    _piece = value;
                    OnPropertyChanged(nameof(Piece));
                    UpdateRowAmount();  // Piece değiştiğinde RowAmount güncellensin
                }
            }
        }
        public decimal ForexPrice { get; set; }
        public string Forex { get; set; }
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                    UpdateRowAmount();  // UnitPrice değiştiğinde RowAmount güncellensin
                }
            }
        }
        public int VariantId { get; set; }
        public int ColorId { get; set; }
        public string Explanation { get; set; }
        public string UUID { get; set; }
        public int TrackingNumber { get; set; }
        public int PatternId { get; set; }
        public int ProcessId { get; set; }
        public decimal RowAmount
        {
            get => _rowAmount;
            private set
            {
                if (_rowAmount != value)
                {
                    _rowAmount = value;
                    OnPropertyChanged(nameof(RowAmount)); // RowAmount güncellendikçe bildirim yapıyoruz
                }
            }
        }
        public int Vat
        {
            get => _vat;
            set
            {
                if (_vat != value)
                {
                    _vat = value;
                    OnPropertyChanged(nameof(Vat));
                    UpdateRowAmount();  // Vat değiştiğinde RowAmount güncellensin
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateRowAmount()
        {
            // Piece ve UnitPrice'ı çarparak net tutarı buluyoruz
            var netAmount = Piece * UnitPrice;

            // KDV hesaplaması ve RowAmount'a eklenmesi
            RowAmount = netAmount + (netAmount * Vat / 100m);  // KDV'yi ekliyoruz
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
