using System.ComponentModel;

namespace Hesap.Context
{
    public class _KumasDepoKalem : INotifyPropertyChanged
    {
        private decimal _brutKg, _netKg, _brutMt, _netMt, _satirTutari,_fiyat;
        private int _adet;
        public string _fiyatBirim;
        public int Id { get; set; }
        public int D2Id { get; set; }
        public string RefNo { get; set; }
        public string KalemIslem { get; set; }
        public string SipNo { get; set; }
        public int KumasId { get; set; }
        public string KumasKodu { get; set; }
        public string KumasAdi { get; set; }
        public string BordurKodu { get; set; }
        public string Bordur { get; set; }
        public decimal GrM2 { get; set; }
        public decimal HamGr { get; set; }
        public int RenkId { get; set; }
        public string BoyahaneRenkKodu { get; set; }
        public string BoyahaneRenkAdi { get; set; }
        public decimal BrutKg
        {
            get => _brutKg; set
            {
                if (_brutKg != value)
                {
                    _brutKg = value;
                    OnPropertyChanged(nameof(BrutKg));
                    UpdateSatirTutari();
                }
            }
        }
        public decimal NetKg
        {
            get => _netKg; set
            {
                if (_netKg != value)
                {
                    _netKg = value;
                    OnPropertyChanged(nameof(NetKg));
                    UpdateSatirTutari();
                }
            }
        }
        public decimal BrutMt
        {
            get => _brutMt; set
            {
                if (_brutMt != value)
                {
                    _brutMt = value;
                    OnPropertyChanged(nameof(BrutMt));
                    UpdateSatirTutari();
                }
            }
        }
        public decimal NetMt
        {
            get => _netMt; set
            {
                if (_netMt != value)
                {
                    _netMt = value;
                    OnPropertyChanged(nameof(NetKg));
                    UpdateSatirTutari();
                }
            }
        }
        public int Adet
        {
            get => _adet; set
            {
                if (_adet != value)
                {
                    _adet = value;
                    OnPropertyChanged(nameof(Adet));
                    UpdateSatirTutari();
                }
            }
        }
        public string FiyatBirim
        {
            get => _fiyatBirim; set
            {
                if (_fiyatBirim != value)
                {
                    _fiyatBirim = value;
                    OnPropertyChanged(nameof(FiyatBirim));
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
        public decimal Fire { get; set; }
        public int CuvalSayisi { get; set; }
        public int TopSayisi { get; set; }
        public string SatirAciklama { get; set; }
        public string UUID { get; set; }
        public int HataId { get; set; }
        public string IstenenEbat { get; set; }
        public string BoyaOzellik { get; set; }
        public int BaskiId { get; set; }
        public string Barkod { get; set; }
        public string HamKod { get; set; }
        public string HamFasonKod { get; set; }
        public string TakipNo { get; set; }
        public string PartiNo { get; set; }
        public string BoyaKod { get; set; }
        public int VaryantId { get; set; }
        public decimal Fiyat {
            get => _fiyat;
            set
            {
                if (_fiyat != value)
                {
                    _fiyat = value;
                    OnPropertyChanged(nameof(Fiyat));
                    UpdateSatirTutari(); // Fiyat değiştiğinde tutarı güncelle
                }
            }
        }
        public bool Organik { get; set; }
        public int DesenId { get; set; }
        public int BoyaIslemId { get; set; }
        private void UpdateSatirTutari()
        {
            switch (FiyatBirim)
            {
                case "Brüt Kg":
                    SatirTutari = BrutKg * Fiyat;
                    break;
                case "Net Kg":
                    SatirTutari = NetKg * Fiyat; // Örnek: net kg = %90 brut
                    break;
                case "Brüt Mt":
                    SatirTutari = BrutMt * Fiyat; // Örnek: 1 mt = 1000 kg
                    break;
                case "Net Mt":
                    SatirTutari = NetMt * Fiyat; // Örnek: net mt = %90 brut
                    break;
                case "Adet":
                    SatirTutari = Adet * Fiyat; // Örnek: adet için brut kg kullanılıyor
                    break;
                default:
                    SatirTutari = 0; // Geçersiz birim
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
