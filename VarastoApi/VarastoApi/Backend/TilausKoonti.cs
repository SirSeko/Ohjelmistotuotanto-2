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

        //Hakee kannasta tilaukseen kuuluvat tilattavat ja lisää listaan(object), jonka palauttaa
        public List<object> TilattavatMateriaalit(int TilausId) {
            cnn = dbMana.OpenConnection();
            DatabaseTilattava dbT = new DatabaseTilattava(cnn);
            DatabaseLauta dbL = new DatabaseLauta(cnn);
            DatabaseVaneri dbV = new DatabaseVaneri(cnn);
            DatabaseMaali dbM = new DatabaseMaali(cnn);
            List<object> tilMat = new List<object>();
            List<Tilattava> tilattavat = dbT.SelectTilaus(TilausId);
            if (tilattavat != null) {
                foreach (Tilattava t in tilattavat) {
                    object o = new object();
                    if (o == null) {
                        o = dbL.SelectId(t.MateriaaliId);
                    }
                    if (o == null) {
                        o = dbV.SelectId(t.MateriaaliId);
                    }
                    if (o == null) {
                        o = dbM.SelectId(t.MateriaaliId);
                    }
                    if (o != null) {
                        tilMat.Add(o);
                    }
                }
            }
            dbMana.CloseConnection();
            return tilMat;
        }
    }
    
}