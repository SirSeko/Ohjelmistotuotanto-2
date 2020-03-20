using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace VarastoApi.Backend {
    public class DatabaseVaneri {

        SqlConnection cnn; //yhteys kantaan
        SqlCommand command; //komento-olio
        SqlDataReader dataReader; //luenta-olio
        SqlDataAdapter adapter = new SqlDataAdapter(); //käskynantoon liittyvä olio
        string sql;

        int id;
        string koko;
        float hinta;
        int maara;
        string yksikko;
        int sijainti;
        string kauppa;
        string lisatiedot;

        public DatabaseVaneri(SqlConnection cnn) {
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Vaneri> SelectAll(List<Vaneri> vanerit) {
            sql = "Select * from mydb.Vaneri;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read()) {
                int.TryParse(dataReader.GetValue(0).ToString(), out id);
                koko = dataReader.GetValue(1).ToString();
                float.TryParse(dataReader.GetValue(2).ToString(), out hinta);
                int.TryParse(dataReader.GetValue(3).ToString(), out maara);
                yksikko = dataReader.GetValue(4).ToString();
                int.TryParse(dataReader.GetValue(5).ToString(), out sijainti);
                kauppa = dataReader.GetValue(6).ToString();
                lisatiedot = dataReader.GetValue(7).ToString();
                Vaneri v = Vaneri.Create(id, koko, hinta, maara, yksikko, sijainti, kauppa, lisatiedot);
                vanerit.Add(v);
            }
            dataReader.Close();
            return vanerit;
        }
        //Hakee IDn perusteella datan
        public Vaneri SelectId(int id) {
            sql = "Select * from mydb.Vaneri Where VaneriId='" + id + "';";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            int.TryParse(dataReader.GetValue(0).ToString(), out id);
            koko = dataReader.GetValue(1).ToString();
            float.TryParse(dataReader.GetValue(2).ToString(), out hinta);
            int.TryParse(dataReader.GetValue(3).ToString(), out maara);
            yksikko = dataReader.GetValue(4).ToString();
            int.TryParse(dataReader.GetValue(5).ToString(), out sijainti);
            kauppa = dataReader.GetValue(6).ToString();
            lisatiedot = dataReader.GetValue(7).ToString();
            dataReader.Close();
            Vaneri v = Vaneri.Create(id, koko, hinta, maara, yksikko, sijainti, kauppa, lisatiedot);
            return v;
        }

        //Lisää tilauksen tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Vaneri v) {
            try {
                sql = "Insert into mydb.Vaneri(Koko, Hinta, Määrä, Yksikkö, Sijainti, Kauppa, Lisätiedot) values ('" + v.Koko + "', '" + v.Hinta + "', '" + v.Maara + "', '" + v.Yksikko + "', '" + v.Sijainti + "', '" + v.Sijainti + "', '" + v.Kauppa + "', '" + v.Lisatiedot + "');";
                command = new SqlCommand(sql, cnn); //en tiedä miksi on kaksi eri sql-komentoa, ohjeiden mukaan tein o.o
                adapter.InsertCommand = new SqlCommand(sql, cnn); //tämä on se toinen, mutta tämä ilmeisesti on käytössä?
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose(); //poistetaan olio
                return true;
            } catch (Exception ex) {
                ExceptionController.WriteException(this, ex.Message);
                return false;
            }
        }

        //Muokkaa ID:n mukaista Columnia. Tarkennus: Vastaanottaa olion Tilaus ja korvaa tietokannasta MateriaaliID:n mukaisen columnin tiedot.
        public bool Update(Vaneri v) {
            try {
                sql = "UPDATE mydb.Vaneri SET Koko='" + v.Koko + "', Hinta='" + v.Hinta + "', Määrä='" + v.Maara + "', Yksikkö='=" + v.Yksikko + "', Sijainti='" + v.Sijainti + "', Kauppa='" + v.Kauppa + "', Lisätiedot='" + v.Lisatiedot + "' WHERE VaneriID='" + v.Id + "';";
                command = new SqlCommand(sql, cnn); //en tiedä miksi on kaksi eri sql-komentoa, ohjeiden mukaan tein d:D
                adapter.UpdateCommand = new SqlCommand(sql, cnn); //tämä on se toinen, mutta tämä ilmeisesti on käytössä?
                adapter.UpdateCommand.ExecuteNonQuery();
                command.Dispose(); //poistetaan olio
                return true;
            } catch (Exception ex) {
                ExceptionController.WriteException(this, ex.Message);
                return false;
            }

        }
    }
}