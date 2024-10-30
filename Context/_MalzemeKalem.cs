using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Context
{
    public class _MalzemeKalem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int RefNo { get; set; }
        public string KalemIslem { get; set; }
        public string MalzemeKodu { get; set; }
        public string MalzemeAdi { get; set; }
        public float Miktar
        {
            get => _miktar;
            set
            {
                if (_miktar != value)
                {
                    _miktar = value;
                    OnPropertyChanged(nameof(Miktar));
                    UpdateSatirTutari();
                }
            }
        }
        public string Birim { get; set; }
        public string UUID { get; set; }
        public string TeslimAlan { get; set; }
        public int KayitNo { get; set; }
        public int TakipNo { get; set; }
        public float BirimFiyat
        {
            get => _birimFiyat;
            set
            {
                if (_birimFiyat != value)
                {
                    _birimFiyat = value;
                    OnPropertyChanged(nameof(BirimFiyat));
                    UpdateSatirTutari();
                }
            }
        }
        public float SatirTutari
        {
            get => _satirTutari;
            private set
            {
                if (_satirTutari != value)
                {
                    _satirTutari = value;
                    OnPropertyChanged(nameof(SatirTutari));
                }
            }
        }

        private float _miktar;
        private float _birimFiyat;
        private float _satirTutari;

        public event PropertyChangedEventHandler PropertyChanged;
        private void UpdateSatirTutari()
        {
            SatirTutari = Miktar * BirimFiyat;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
