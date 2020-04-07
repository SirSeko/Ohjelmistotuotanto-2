using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VarastoApi.Backend;


namespace VarastoApi.Models
{
    public class Tilaukset 
    {
        public List<Tilaus> Lista;


        public Tilaukset()
        {
            Lista = new List<Tilaus>();
            DatabaseManager dbMang = new DatabaseManager();
            DatabaseTilaus dbTil = new DatabaseTilaus(dbMang.OpenConnection());
            Lista = dbTil.SelectAll(Lista);
            dbMang.CloseConnection();
        }

    }
}