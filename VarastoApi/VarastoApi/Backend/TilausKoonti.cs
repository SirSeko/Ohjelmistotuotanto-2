using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace VarastoApi.Backend {
    public class TilausKoonti
    {
        public List<Tilaus>Tilaukset;
        private DatabaseManager dbMana;
        private DatabaseTilaus dbTila;
        SqlConnection cnn;

        public TilausKoonti()
        {
            Tilaukset = new List<Tilaus>();
            dbMana = new DatabaseManager();
        }

        public void Initiate()
        {
            cnn = dbMana.OpenConnection();
            dbTila = new DatabaseTilaus(cnn);
            Tilaukset = dbTila.SelectAll(Tilaukset);
            dbMana.CloseConnection();
        }
    }
    
}