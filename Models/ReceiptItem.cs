using System.ComponentModel;

namespace Hesap.Models
{
    public class ReceiptItem : INotifyPropertyChanged
    {
        private decimal _piece;
        private decimal _grossWeight;
        private decimal _netWeight;
        private decimal _unitPrice;
        private decimal _rowAmount;
        private int _vat;
        private string _measurementUnit;
        public int Id { get; set; }
        public int ReceiptItemId { get; set; }
        public int ReceiptId { get; set; }
        public string OperationType { get; set; }
        public int InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public int GrM2 { get; set; }
        public decimal GrossWeight
        {
            get => _grossWeight;
            set
            {
                if (_grossWeight != value)
                {
                    _grossWeight = value;
                    OnPropertyChanged(nameof(GrossWeight));
                    UpdateRowAmount();
                }
            }
        }
        public decimal NetWeight
        {
            get => _netWeight;
            set
            {
                if (_netWeight != value)
                {
                    _netWeight = value;
                    OnPropertyChanged(nameof(NetWeight));
                    UpdateRowAmount();
                }
            }
        }
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
                    UpdateRowAmount();
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
                    UpdateRowAmount();
                }
            }
        }
        public int VariantId { get; set; }
        public int ColorId { get; set; } // fişlerde Boyahane renk kodunu - siparişlerde ise varyantı temsil edecektir.
        public string ColorCode { get; set; }
        public string ColorName { get; set; }
        public string Explanation { get; set; }
        public string UUID { get; set; }
        public int TrackingNumber { get; set; }
        public int PatternId { get; set; }
        public int ProcessId { get; set; }
        public string Authorized { get; set; }
        public string Receiver { get; set; }
        public decimal RowAmount
        {
            get => _rowAmount;
            private set
            {
                if (_rowAmount != value)
                {
                    _rowAmount = value;
                    OnPropertyChanged(nameof(RowAmount));
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
                    UpdateRowAmount();
                }
            }
        }

        public string MeasurementUnit
        {
            get => _measurementUnit;
            set
            {
                if (_measurementUnit != value)
                {
                    _measurementUnit = value;
                    OnPropertyChanged(nameof(MeasurementUnit));
                    UpdateRowAmount();
                }
            }
        }

        public string ReceiptNo { get; set; }
        public string Brand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void UpdateRowAmount()
        {
            var netAmount = Piece * UnitPrice;

            switch (MeasurementUnit)
            {
                case "Brüt Kg":
                    RowAmount = CalculateGrossWeight() + (CalculateGrossWeight() * Vat / 100m);
                    break;
                case "Net Kg":
                    RowAmount = CalculateNetWeight() + (CalculateNetWeight() * Vat / 100m);
                    break;
                default:
                    RowAmount = netAmount + (netAmount * Vat / 100m);
                    break;
            }
        }

        private decimal CalculateGrossWeight()
        {
            return _unitPrice * _grossWeight;
        }
        private decimal CalculateNetWeight()
        {
            return _unitPrice * _netWeight;
        }
        //private void UpdateRowAmount()
        //{
        //    // Piece ve UnitPrice'ı çarparak net tutarı buluyoruz
        //    var netAmount = Piece * UnitPrice;

        //    // KDV hesaplaması ve RowAmount'a eklenmesi
        //    RowAmount = netAmount + (netAmount * Vat / 100m);  // KDV'yi ekliyoruz
        //}
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
