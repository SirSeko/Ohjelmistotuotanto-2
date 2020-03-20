using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarastoApi.Backend;

namespace VarastoApi.Models {
    /*Tällä hetkellä vain testikäyttöä varten tehty luokka
     Vanerit.cshtml käyttöä varten, suorittaa kantahaun vain vaneri-taululle*/
    public class Vanerit { 
        public List<Vaneri> Lista;
        public Vanerit() {
            Lista = new List<Vaneri>();
            DatabaseManager dbMan = new DatabaseManager();
            DatabaseVaneri dbVan = new DatabaseVaneri(dbMan.OpenConnection());
            Lista = dbVan.SelectAll(Lista);
            dbMan.CloseConnection();
        }
    }
}