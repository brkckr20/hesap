﻿using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace Hesap.Utils
{
    public class Listele
    {
        Baglanti _baglanti;
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        public Listele()
        {
            _baglanti = new Baglanti();
        }
        public void Liste(string sql, GridControl xtraGrid)
        {
            string content = ayarlar.VeritabaniTuru();
            string mssqlString = ayarlar.MssqlConnStr();
            string sqliteString = ayarlar.SqliteConnStr();

            using (var connection = new Baglanti().GetConnection())
            {
                DataTable dataTable = new DataTable();
                if (content == "mssql")
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, mssqlString))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                else
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, sqliteString))
                    {
                        adapter.Fill(dataTable); // kumaş kodu seçerken isnull hatası veriyor. kontrol edilecek
                    }
                }
                xtraGrid.DataSource = dataTable;
                UpdateColumnHeaders(xtraGrid);
            }
        }
        public void ListeWithParams(string sql, GridControl xtraGrid, string param1)
        {
            string content = ayarlar.VeritabaniTuru();
            string mssqlString = ayarlar.MssqlConnStr();
            string sqliteString = ayarlar.SqliteConnStr();

            using (var connection = new Baglanti().GetConnection())
            {
                DataTable dataTable = new DataTable();
                if (content == "mssql")
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, mssqlString))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@UrunKodu", param1);
                        adapter.Fill(dataTable);
                    }
                }
                else
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, sqliteString))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@UrunKodu", param1);
                        adapter.Fill(dataTable);
                    }
                }
                xtraGrid.DataSource = dataTable;
            }
        }
        public void UpdateColumnHeaders(GridControl xtraGrid)
        {
            GridView gridView = xtraGrid.MainView as GridView;

            if (gridView != null)
            {
                foreach (GridColumn column in gridView.Columns)
                {
                    if (ColumnHeaders.Headers.ContainsKey(column.FieldName))
                    {
                        column.Caption = ColumnHeaders.Headers[column.FieldName];
                    }
                }
            }
        }

    }

}
