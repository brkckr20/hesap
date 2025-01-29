using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Context
{
    public class _ReceiptItem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public string OperationType { get; set; }
        public int InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public decimal Piece
        {
            get => _miktar;
            set
            {
                if (_miktar != value)
                {
                    _miktar = value;
                    OnPropertyChanged(nameof(Piece));
                    UpdateSatirTutari();
                }
            }
        }
        public string UUID { get; set; }
        //public string TeslimAlan { get; set; }
        public int TrackingNumber { get; set; }
        public decimal UnitPrice
        {
            get => _birimFiyat;
            set
            {
                if (_birimFiyat != value)
                {
                    _birimFiyat = value;
                    OnPropertyChanged(nameof(UnitPrice));
                    UpdateSatirTutari();
                }
            }
        }
        public decimal RowAmount
        {
            get => _satirTutari;
            private set
            {
                if (_satirTutari != value)
                {
                    _satirTutari = value;
                    OnPropertyChanged(nameof(RowAmount));
                }
            }
        }

        private decimal _miktar;
        private decimal _birimFiyat;
        private decimal _satirTutari;

        public event PropertyChangedEventHandler PropertyChanged;
        private void UpdateSatirTutari()
        {
            RowAmount = Piece * UnitPrice;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
