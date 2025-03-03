using Dapper;
using DevExpress.XtraEditors;
using Hesap.Utils;
using System;

namespace Hesap.Forms.Liste
{
    public partial class FrmKayitNoGoster : XtraForm
    {
        int _id;
        CRUD_Operations cRUD = new CRUD_Operations();
        string _kaydeden, _guncelleyen, _tableName;
        string _kayitTarihi, _guncellemeTarihi;
        public FrmKayitNoGoster(int id, string TableName)
        {
            InitializeComponent();
            _id = id;
            _tableName = TableName;
            using (var conn = new Baglanti().GetConnection())
            {
                string query = $@"
                                select 
	                            U.Name + ' ' +U.Surname [NameSurname],
	                            C.InsertedDate InsertedDate ,
	                            UU.Name + ' ' +UU.Surname [UpdateNameSurname],
	                            C.UpdatedDate UpdatedDate
	                            from 
	                            {TableName} C left join Users U on U.Id = C.InsertedBy
	                            left join Users UU on UU.Id = C.UpdatedBy
	                            where C.Id = {id}";
                var liste = conn.QueryFirstOrDefault(query);
                this._kaydeden = liste.NameSurname.ToString();
                this._kayitTarihi = liste.InsertedDate.ToString();
                if (liste.UpdateNameSurname != null && liste.UpdatedDate != null)
                {
                    this._guncelleyen = liste.UpdateNameSurname.ToString();
                    this._guncellemeTarihi = ((DateTime)liste.UpdatedDate).ToString();
                }
                else
                {
                    this._guncelleyen = "";
                    this._guncellemeTarihi = "";
                }

            }
        }

        private void FrmKayitNoGoster_Load(object sender, EventArgs e)
        {
            lblKayitNo.Text = this._id.ToString();
            lblKayitTarihi.Text = this._kayitTarihi.ToString();
            lblKayıtEden.Text = this._kaydeden.ToString();
            lblGuncelleyen.Text = this._guncelleyen.ToString();
            lblGuncellemeTarihi.Text = this._guncellemeTarihi.ToString();
        }
    }
}