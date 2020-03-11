using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace VarastoApi.Backend {
    public class DatabaseSijainti {
        SqlConnection cnn; //yhteys kantaan
        SqlCommand command; //komento-olio
        SqlDataReader dataReader; //luenta-olio
        SqlDataAdapter adapter = new SqlDataAdapter(); //käskynantoon liittyvä olio
        string sql;

        int id;
        string nimi;
        string kaappi;
        string hylly;
        string lisatiedot;

        public DatabaseSijainti(SqlConnection cnn) {
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Sijainti> SelectAll(List<Sijainti> sijainnit) {
            sql = "Select * from mydb.Sijainti;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read()) {
                int.TryParse(dataReader.GetValue(0).ToString(), out id);
                nimi = dataReader.GetValue(1).ToString();
                kaappi = dataReader.GetValue(2).ToString();
                hylly = dataReader.GetValue(3).ToString();
                lisatiedot = dataReader.GetValue(4).ToString();
                Sijainti s = Sijainti.Create(id, nimi, kaappi, hylly, lisatiedot);
                sijainnit.Add(s);
            }
            dataReader.Close();
            return sijainnit;
        }

        //Lisää tilauksen tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Sijainti m) {
            try {
                sql = "Insert into mydb.Sijainti(SijaintiNimi, Kaappi, Hylly, Lisätiedot) values ('" + m.Nimi + "', '" + m.Kaappi+ "', '" + m.Hylly+ "', '" + m.Lisatiedot+ "');";
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
        public bool Update(Sijainti m) {
            try {
                sql = "UPDATE mydb.Sijainti SET SijaintiNimi='" + m.Nimi + "', Kaappi='" + m.Kaappi + "', Hylly='" + m.Hylly+ "', Lisätiedot='=" + m.Lisatiedot+ "' WHERE SijaintiID='" + m.Id + "';";
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