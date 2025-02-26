﻿using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Utils
{
    public class CRUD_Operations
    {
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        private int ExecuteSql(string sql, object parameters)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                return connection.ExecuteScalar<int>(sql, parameters);
            }
        }
        private void ExecuteNonQuery(string sql, object parameters)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                connection.Execute(sql, parameters);
            }
        }
        public void KartSil(int Id, string TabloAdi, bool ResimVarMi = false, PictureEdit pe = null)
        {
            if (Id != 0)
            {
                string sql = $"DELETE FROM {TabloAdi} WHERE Id = @Id";
                using (var connection = new Baglanti().GetConnection())
                {
                    if (bildirim.SilmeOnayı())
                    {
                        connection.Execute(sql, new { Id = Id });
                        bildirim.SilmeBasarili();
                    }
                }
            }
            else
            {
                bildirim.Uyari("Kayıt silebilmek için öncelikle listeden bir kayıt seçmelisiniz!");
            }
        }
        public void FisVeHavuzSil(string tableOne, string tableTwo, int Id)
        {
            if (bildirim.SilmeOnayı())
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string d1 = $"delete from {tableOne} where Id = @Id";
                    string d2 = $"delete from {tableTwo} where RefNo = @Id";
                    connection.Execute(d1, new { Id = Id });
                    connection.Execute(d2, new { Id = Id });
                    bildirim.SilmeBasarili();
                }
            }
        }
        public int InsertRecord(string tableName, IDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey("Id"))
            {
                parameters.Remove("Id"); // Id parametresini kaldır
            }
            var columns = string.Join(", ", parameters.Keys);
            var values = string.Join(", ", parameters.Keys.Select(k => "@" + k));
            var sql = $"INSERT INTO {tableName} ({columns}) OUTPUT INSERTED.Id VALUES ({values})";

            return ExecuteSql(sql, parameters);
        }
        public void UpdateRecord(string tableName, IDictionary<string, object> parameters, int id)
        {
            var setClause = string.Join(", ", parameters.Keys.Select(k => $"{k} = @{k}"));
            var sql = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

            // Id'yi parametre olarak ekleyelim
            parameters.Add("Id", id);

            ExecuteNonQuery(sql, parameters);
        }
        public void GetImageFromDB(byte[] resimBytes, PictureEdit pictureEdit)
        {
            try
            {
                if (resimBytes != null && resimBytes.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(resimBytes))
                    {
                        try
                        {
                            pictureEdit.Image = Image.FromStream(ms);
                        }
                        catch (ArgumentException ex)
                        {
                            MessageBox.Show($"Geçersiz resim formatı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    pictureEdit.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Resim yükleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ImageSelectPathAndSaveDB(Image image, string uniqueId, ButtonEdit be)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Resim", "Talepler");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, $"{uniqueId}.png");
                image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                be.Text = filePath;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Resim kaydetme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SatirSil(GridView gridView, string TabloAdi)
        {
                int rowHandle = gridView.FocusedRowHandle;
                int Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("D2Id"));
                if (rowHandle != -1)
                {
                string sql = $"DELETE FROM {TabloAdi} WHERE Id = @Id";
                using (var connection = new Baglanti().GetConnection())
                {
                    if (bildirim.SilmeOnayı())
                    {
                        connection.Execute(sql, new { Id = Id });
                        gridView.DeleteRow(rowHandle);
                        bildirim.SilmeBasarili();
                    }
                }
            }            
        }
        #region düzenlenecek
        /*
        public void KayitlariGetir(
                string OncemiSonrami,
                ref int _id,
                GridControl gridControl,
                string dp1,
                string dp2,
                string fisSorgusu,
                string kalemSorgusu,
                DateEdit tarih = null,
                TextEdit talimatNo = null,
                //ref int firmaId, // ref parametre
                TextEdit firmakodu = null,
                TextEdit firmaunvan = null,
                TextEdit aciklama = null
        )
        {
            int id = _id;
            int? istenenId = null;
            try
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql;
                    if (OncemiSonrami == "Önceki")
                    {
                        sql = $"SELECT MAX({dp1}.Id) FROM {dp1} INNER JOIN {dp2} ON {dp1}.Id = {dp2}.RefNo WHERE {dp1}.Id < @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = $"SELECT MIN({dp1}.Id) FROM {dp1} INNER JOIN {dp2} ON {dp1}.Id = {dp2}.RefNo WHERE {dp1}.Id > @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    var fis = connection.QueryFirstOrDefault(fisSorgusu, new { Id = istenenId });
                    var kalemler = connection.Query(kalemSorgusu, new { Id = istenenId });
                    if (fis != null && kalemler != null)
                    {
                        gridControl.DataSource = null;
                        _id = Convert.ToInt32(fis.Id);
                        tarih.EditValue = (DateTime)fis.Tarih;
                        talimatNo.Text = fis.TalimatNo.ToString();
                        //firmaId = Convert.ToInt32(fis.FirmaId);
                        firmakodu.Text = fis.FirmaKodu.ToString();
                        firmaunvan.Text = fis.FirmaUnvan.ToString();
                        //rchAciklama.Text = fis.Aciklama.ToString();
                        //txtYetkili.Text = fis.Yetkili.ToString();
                        //txtVade.Text = fis.Vade.ToString();
                        //comboBoxEdit1.Text = fis.OdemeSekli.ToString();
                        gridControl.DataSource = kalemler.ToList();
                    }
                    else
                    {
                        bildirim.Uyari("Gösterilecek başka kayıt bulunamadı!!");
                    }
                }
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
        }
        */
        #endregion

    }
}
