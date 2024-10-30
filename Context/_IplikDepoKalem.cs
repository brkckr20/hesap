using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Context
{
    public class _IplikDepoKalem : INotifyPropertyChanged
    { 
        private decimal _brutMiktar; 
        private decimal _birimFiyat;
        private decimal _satirTutari;

        public int Id { get; set; }
        public int RefNo { get; set; }
        public string KalemIslem { get; set; }
        public int IplikId { get; set; }
        public string IplikKodu { get; set; } //tabloda yok
        public string IplikAdi { get; set; } // tabloda yok
        public decimal NetKg { get; set; }
        public decimal BrutKg { get => _brutMiktar; 
            set
            {
                if (_brutMiktar != value)
                {
                    _brutMiktar = value;
                    OnPropertyChanged(nameof(BrutKg));
                    UpdateSatirTutari();
                } 
            } 
        }
       
        //public decimal Fiyat { get; set; }
        public string DovizCinsi { get; set; }
        public string OrganikSertifikaNo { get; set; }
        public string Marka { get; set; }
        public string KullanimYeri { get; set; }
        public int IplikRenkId { get; set; }
        public string IplikRenkKodu { get; set; } //tabloda yok
        public string IplikRenkAdi { get; set; } //tabloda yok
        public string PartiNo { get; set; }
        public string SatirAciklama { get; set; }
        public string Barkod { get; set; }
        public string TalimatNo { get; set; }
        public string UUID { get; set; }
        public int TakipNo { get; set; }
        public int D2Id { get; set; }

        public decimal Fiyat
        {
            get => _birimFiyat;
            set
            {
                if (_birimFiyat != value)
                {
                    _birimFiyat = value;
                    OnPropertyChanged(nameof(Fiyat));
                    UpdateSatirTutari();
                }
            }
        }
        public decimal SatirTutari
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void UpdateSatirTutari()
        {
            SatirTutari = BrutKg * Fiyat;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
