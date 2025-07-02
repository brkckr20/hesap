namespace Hesap.Utils
{
    public enum ReceiptTypes
    {
        SatinAlmaTalimati = 0,
        MalzemeDepoGiris = 1,
        MusteriSiparisi = 2,
        MalzemeDepoCikis = 3,
        IplikSatinAlmaTalimati=4,
        IplikDepoGiris = 5,
        IplikDepoCikis = 6,
        KumasSatinAlmaTalimati = 7,
        KumasDepoGiris = 8,
        KumasDepoCikis = 9,
        BoyahaneTalimati = 10,
    }

    public enum InventoryTypes
    {
        Malzeme = 0,
        Kumas = 1,
        Iplik = 2,
    }

    public enum WareHouseTypes
    {
        Malzeme = 0,
    }
}
