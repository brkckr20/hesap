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
        public string InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public float Piece
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
        public string TeslimAlan { get; set; }
        public int TrackingNumber { get; set; }
        public float UnitPrice
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
        public float RowAmount
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

        private float _miktar;
        private float _birimFiyat;
        private float _satirTutari;

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
