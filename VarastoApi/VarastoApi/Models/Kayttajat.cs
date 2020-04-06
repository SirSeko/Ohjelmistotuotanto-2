using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VarastoApi.Backend;

namespace VarastoApi.Models {
    public class Kayttajat {
        public List<Kayttaja> Lista;

        public Kayttajat() {
            Lista = new List<Kayttaja>();
            DatabaseManager dbman = new DatabaseManager();
            SqlConnection cnn = dbman.OpenConnection();
            DatabaseKayttaja dbkay = new DatabaseKayttaja(cnn);
            Lista = dbkay.SelectAll();
            dbman.CloseConnection();
        }
    }
}