using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hesap.Forms.OrderYonetimi.Liste
{
    public partial class FrmModelKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmModelKartiListesi()
        {
            InitializeComponent();
        }
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CrudRepository crudRepository = new CrudRepository();
        public int Id, FirmaId, KategoriId, CinsiId, PazarlamaciId;
        public string Kodu, Adi, OrjAdi, FirmaKodu, FirmaAdi, KategoriAdi, KategorOrjAdi, CinsiAdi, CinsiOrjAdi, OzelKod, OzelKod2, GrM2, Pazarlamaci, GTIPNo;
        public bool KumasOk, BoyaOk, NakisOk, IplikOk, AksesuarOk;
        bool KullanicininKendiModelleriListelenecek = false; // bu alan parametre olarak seçilebilecek

        public List<dynamic> fabricRecipe = new List<dynamic>();

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Kodu = gridView.GetFocusedRowCellValue("Model Kodu").ToString();
            Adi = gridView.GetFocusedRowCellValue("Model Adı").ToString();
            OrjAdi = gridView.GetFocusedRowCellValue("Orj. Model Adı").ToString();
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            FirmaId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Firma Id"));
            FirmaKodu = gridView.GetFocusedRowCellValue("Firma Kodu").ToString();
            FirmaAdi = gridView.GetFocusedRowCellValue("Firma Adı").ToString();
            KategoriId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Kategori Id"));
            KategoriAdi = gridView.GetFocusedRowCellValue("Kategori Adı").ToString();
            KategorOrjAdi = gridView.GetFocusedRowCellValue("Orj. Kategori Adı").ToString();
            CinsiId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Cinsi Id"));
            CinsiAdi = gridView.GetFocusedRowCellValue("Cinsi Adı").ToString();
            CinsiOrjAdi = gridView.GetFocusedRowCellValue("Orj. Cinsi Adı").ToString();
            OzelKod = gridView.GetFocusedRowCellValue("Özel Kod").ToString();
            OzelKod2 = gridView.GetFocusedRowCellValue("Özel Kod 2").ToString();
            GrM2 = gridView.GetFocusedRowCellValue("GrM2").ToString();
            PazarlamaciId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Kullanıcı Id"));
            Pazarlamaci = gridView.GetFocusedRowCellValue("Kullanıcı").ToString();
            KumasOk = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kumaş Ok"));
            BoyaOk = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Boya Ok"));
            NakisOk = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Nakış Ok"));
            IplikOk = Convert.ToBoolean(gridView.GetFocusedRowCellValue("İplik Ok"));
            AksesuarOk = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Aksesuar Ok"));
            GTIPNo = gridView.GetFocusedRowCellValue("GTIP No").ToString();
            //var kumasRecetesi = crudRepository.GetAll<InventoryReceipt>("InventoryReceipt")
            //    .Where(x => x.InventoryId == Id)
            //    .ToList();
            var sql = $"SELECT I1.Id AS [RecipeInventoryId], I1.InventoryName, I1.InventoryCode, IR.PlaceOfUse, IR.Genus, IR.GrM2, IR.IsOrganic, IR.EmbroideryRef, STRING_AGG(CAST(IRQ.SizeId AS NVARCHAR(MAX)), ', ') AS SizeText FROM Inventory I LEFT JOIN InventoryReceipt IR WITH(NOLOCK) ON I.Id = IR.InventoryId LEFT JOIN Inventory I1 ON I1.Id = IR.RecipeInventoryId LEFT JOIN InventoryRequirement IRQ ON I.Id = IRQ.InventoryId WHERE I.Id = {Id} GROUP BY I1.Id, I1.InventoryName, I1.InventoryCode, IR.PlaceOfUse, IR.Genus, IR.GrM2, IR.IsOrganic, IR.EmbroideryRef";
            var kumasRecetesi = crudRepository.GetListWithCustomQuery(sql);

            fabricRecipe.Clear();
            if (kumasRecetesi != null && kumasRecetesi.Any())
            {
                fabricRecipe.AddRange(kumasRecetesi);
            }
            this.Close();
        }

        private void FrmModelKartiListesi_Load(object sender, EventArgs e)
        {
            string sql = $@"
                            select 
                            I.Id,
                            I.InventoryCode [Model Kodu],
                            I.InventoryName [Model Adı],
                            I.InventoryOriginalName [Orj. Model Adı],
                            ISNULL(C.Id,'') [Firma Id],
                            ISNULL(C.CompanyCode,'') [Firma Kodu],
                            ISNULL(C.CompanyName,'') [Firma Adı],
                            ISNULL(L1.Id,'') [Kategori Id],
                            ISNULL(L1.Name,'') [Kategori Adı],
                            ISNULL(L1.OriginalName,'') [Orj. Kategori Adı],
                            ISNULL(L2.Id,'') [Cinsi Id],
                            ISNULL(L2.Name,'') [Cinsi Adı],
                            ISNULL(L2.OriginalName,'') [Orj. Cinsi Adı],
                            ISNULL(I.SpecialCode,'') [Özel Kod],
                            ISNULL(I.SpecialCode2,'') [Özel Kod 2],
                            ISNULL(I.GrM2,'') [GrM2],
                            ISNULL(U.Id,'') [Kullanıcı Id],
                            ISNULL(U.Name,'') + ' ' +ISNULL(U.Surname,'') [Kullanıcı],
                            ISNULL(I.FabricOK,'') [Kumaş Ok],
                            ISNULL(I.ColorOK,'') [Boya Ok],
                            ISNULL(I.EmbroideryOK,'') [Nakış Ok],
                            ISNULL(I.YarnOK,'') [İplik Ok],
                            ISNULL(I.AccessoriesOK,'') [Aksesuar Ok],
                            ISNULL(I.GTIPNo,'') [GTIP No],
                            ISNULL(IR.PlaceOfUse,'') [Kullanım Yeri],
                            ISNULL(IR.GrM2,'') [GrM2],
                            ISNULL(IR.Genus,'') [Cinsi]
                            from Inventory I 
                            left join Company C on C.Id = I.CompanyId
                            left join Lookup L1 on I.CategoryId = L1.Id
                            left join Lookup L2 on I.GenusId = L2.Id
                            left join Users U on I.UserId = U.Id
                            left join InventoryReceipt IR on I.Id = IR.InventoryId
                            where ISNULL(I.Type,0)=3";
            //if (!KullanicininKendiModelleriListelenecek) // bu alan order yönetimi parametrelerinden gelecek
            //{
            //    sql += $" and U.Id = {CurrentUser.UserId}";
            //}
            listele.Liste(sql, gridControl1);
        }
    }
}