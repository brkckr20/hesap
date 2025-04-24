using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Models;
using System;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using Hesap.Utils;
using System.Linq;

namespace Hesap.Forms.Diger
{
    public partial class FrmNumerator : XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();

        int Id = 0;
        private string TableName = "Numerator";
        public FrmNumerator()
        {
            InitializeComponent();            
        }
        private void FrmNumerator_Load(object sender, EventArgs e)
        {
            Yukle();
        }
        void Yukle()
        {
            gridControl1.DataSource = crudRepository.GetAll<Numerator>(TableName).Select(s => new {
                s.Id,
                s.Prefix,
                s.Number,
                s.Name,
                s.IsActive,
                InventoryType = GetInventoryName((InventoryTypes)s.InventoryType)
            });            
        }
        void Temizle()
        {
            object[] kart = { txtOnEk,txtNumara,txtIsim,cmbType,chckIsActive };
            yardimciAraclar.KartTemizle(kart);
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            
        }

        string GetInventoryName(InventoryTypes types)
        {
            switch (types)
            {
                case InventoryTypes.Malzeme:
                    return "Malzeme";
                case InventoryTypes.Kumas:
                    return "Kumaş";
                case InventoryTypes.Iplik:
                    return "İplik";
                default:
                    return "";
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var numerator_params = new Dictionary<string, object> { { "Prefix", txtOnEk.Text }, { "Number", txtNumara.Text }, { "Name", txtIsim.Text }, { "IsActive", chckIsActive.Checked }, { "InventoryType", cmbType.SelectedIndex } };
            if (this.Id == 0)
            {
                Id = crudRepository.Insert(TableName, numerator_params);
                bildirim.Basarili();
                Yukle();
            }
            else
            {
                crudRepository.Update(TableName,Id,numerator_params);
                bildirim.GuncellemeBasarili();
                Yukle();
            }
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            txtOnEk.Text = gridView1.GetFocusedRowCellValue("Prefix").ToString();
            txtNumara.Text = gridView1.GetFocusedRowCellValue("Number").ToString();
            txtIsim.Text = gridView1.GetFocusedRowCellValue("Name").ToString();
            cmbType.Text = gridView1.GetFocusedRowCellValue("InventoryType").ToString();
            chckIsActive.Checked = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("IsActive"));
        }
    }
}