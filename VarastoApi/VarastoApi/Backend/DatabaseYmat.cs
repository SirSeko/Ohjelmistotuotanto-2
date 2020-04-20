using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace VarastoApi.Backend {
    public class DatabaseYmat {
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

        public DatabaseYmat(SqlConnection cnn) {
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Ymat> SelectAll(List<Ymat> materiaalit) {
            sql = "Select * from mydb.Valineet;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read()) {
                int.TryParse(dataReader.GetValue(0).ToString(), out id);
                koko = dataReader.GetValue(1).ToString();
                int.TryParse(dataReader.GetValue(2).ToString(), out maara);
                float.TryParse(dataReader.GetValue(3).ToString(), out hinta);
                yksikko = dataReader.GetValue(4).ToString();
                int.TryParse(dataReader.GetValue(5).ToString(), out sijainti);
                kauppa = dataReader.GetValue(6).ToString();
                lisatiedot = dataReader.GetValue(7).ToString();
                Ymat m = Ymat.Create(id, koko, hinta, maara, yksikko, sijainti, kauppa, lisatiedot);
                materiaalit.Add(m);
            }
            dataReader.Close();
            return materiaalit;
        }

        //Lisää tilauksen tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Ymat m) {
            try {
                sql = "Insert into mydb.Valineet(Koko, Määrä, Hinta, Yksikkö, Sijainti, Kauppa, Lisätiedot) values ('" + m.Koko + "', '" + m.Maara+ "', '" + m.Hinta+ "', '" + m.Yksikko + "', '" + m.Sijainti + "', '" + m.Kauppa + "', '" + m.Lisatiedot + "');";
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
        public bool Update(Ymat m) {
            try {
                sql = "UPDATE mydb.Valineet SET Koko='" + m.Koko + "', Hinta='" + m.Hinta + "', Määrä='" + m.Maara + "', Yksikkö='" + m.Yksikko + "', Sijainti='" + m.Sijainti + "', Kauppa='" + m.Kauppa + "', Lisätiedot='" + m.Lisatiedot + "' WHERE ValineetID='" + m.Id + "';";
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
        public Ymat SelectId(int id) {
            sql = "Select * from mydb.Valineet Where ValineetID='" + id + "';";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            int.TryParse(dataReader.GetValue(0).ToString(), out id);
            koko = dataReader.GetValue(1).ToString();
            int.TryParse(dataReader.GetValue(2).ToString(), out maara);
            float.TryParse(dataReader.GetValue(3).ToString(), out hinta);

            yksikko = dataReader.GetValue(4).ToString();
            int.TryParse(dataReader.GetValue(5).ToString(), out sijainti);
            kauppa = dataReader.GetValue(6).ToString();
            lisatiedot = dataReader.GetValue(7).ToString();
            Ymat m = Ymat.Create(id, koko, hinta, maara, yksikko, sijainti, kauppa, lisatiedot);
            return m;
        }
        public bool Delete(int id) {
            try {
                sql = "Delete FROM mydb.Valineet WHERE ValineetID='" + id + "';";
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