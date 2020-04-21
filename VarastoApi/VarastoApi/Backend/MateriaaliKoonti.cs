using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace VarastoApi.Backend {
    public class MateriaaliKoonti {
        public List<Materiaali> Materiaalit;
        public List<Vaneri> Vanerit;
        public List<Sijainti> Sijainnit;
        public List<Maali> Maalit;
        public List<Lauta> Laudat;
        public List<Ymat> YleisetMateriaalit;

        private DatabaseManager dbMan;
        private DatabaseMateriaali dbMat;
        private DatabaseLauta dbLau;
        private DatabaseMaali dbMaa;
        private DatabaseVaneri dbVan;
        private DatabaseSijainti dbSij;
        private DatabaseYmat dbYmat;

        SqlConnection cnn;

        public MateriaaliKoonti() {
            Materiaalit = new List<Materiaali>();
            Vanerit = new List<Vaneri>();
            Sijainnit = new List<Sijainti>();
            Maalit = new List<Maali>();
            Laudat = new List<Lauta>();
            YleisetMateriaalit = new List<Ymat>();

            dbMan = new DatabaseManager();

            
        }

        public void Initiate() {
            cnn = dbMan.OpenConnection();

            dbMat = new DatabaseMateriaali(cnn);
            dbLau = new DatabaseLauta(cnn);
            dbMaa = new DatabaseMaali(cnn);
            dbVan = new DatabaseVaneri(cnn);
            dbSij = new DatabaseSijainti(cnn);
            dbYmat = new DatabaseYmat(cnn);
            Materiaalit = dbMat.SelectAll(Materiaalit);
            Vanerit = dbVan.SelectAll(Vanerit);
            Sijainnit = dbSij.SelectAll(Sijainnit);
            Maalit = dbMaa.SelectAll(Maalit);
            Laudat = dbLau.SelectAll(Laudat);
            YleisetMateriaalit = dbYmat.SelectAll(YleisetMateriaalit);
            dbMan.CloseConnection();
        }
    }
}