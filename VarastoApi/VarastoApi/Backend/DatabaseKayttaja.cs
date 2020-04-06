using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace VarastoApi.Backend {
    public class DatabaseKayttaja {
        string hash;
        string kayttajanimi;
        int valtuus;

        SqlConnection cnn; //yhteys kantaan
        SqlCommand command; //komento-olio
        SqlDataReader dataReader; //luenta-olio
        SqlDataAdapter adapter = new SqlDataAdapter(); //käskynantoon liittyvä olio
        string sql;

        public DatabaseKayttaja(SqlConnection cnn) {
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Kayttaja> SelectAll() {
            try {
                List<Kayttaja> k = new List<Kayttaja>();
                sql = "Select Kayttajanimi, Valtuudet from mydb.Kayttajat;";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read()) {
                    kayttajanimi = dataReader.GetValue(0).ToString();
                    int.TryParse(dataReader.GetValue(1).ToString(), out valtuus);
                    Kayttaja kk = Kayttaja.Create(kayttajanimi, valtuus);
                    k.Add(kk);
                }
                dataReader.Close();
                return k;
            } catch (Exception ex) {
                ExceptionController.WriteException(this, ex.Message);
                return null;
            }
        }

        public Kayttaja SelectKayttaja(string kayttajanimi, string hash) {
            try {
                this.kayttajanimi = "";
                this.valtuus = -1;
                sql = "Select Kayttajanimi, Valtuudet FROM mydb.Kayttajat WHERE Kayttajanimi = '" + kayttajanimi + "' AND Salasana ='" + hash + "';";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                dataReader.Read();
                this.kayttajanimi = dataReader.GetValue(0).ToString();
                int.TryParse(dataReader.GetValue(1).ToString(), out this.valtuus);
                dataReader.Close();
                Kayttaja k = Kayttaja.Create(this.kayttajanimi, this.valtuus);
                return k;
            } catch (Exception ex) {
                ExceptionController.WriteException(this, ex.Message);
                return null;
            }
        }

        //Lisää tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Kayttaja k, string hash) {
            try {
                sql = "Insert into mydb.Kayttajat (Kayttajanimi, Salasana, Valtuudet) values ('" + k.Kayttajanimi + "', '" + hash + "', '" + k.Valtuus + "');";
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

        //Muokkaa TilausID:n mukaista Columnia. Tarkennus: Vastaanottaa olion Tilattava ja korvaa tietokannasta TilausId:n mukaisen columnin tiedot.
        public bool Update(Kayttaja k, string hash) {
            try {
                sql = "UPDATE mydb.Kayttajat SET Kayttajanimi='" + k.Kayttajanimi + "', Salasana ='" + hash +"', Valtuudet = '" + k.Valtuus +"' WHERE Kayttajanimi='" + k.Kayttajanimi + "' AND Salasana= '" + hash + ";";
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

        //Poistaa taulusta kohdan, missä tilaus id on x ja materiaali id on y
        public bool Delete(string Kayttajanimi) {
            try {
                sql = "Delete FROM mydb.Kayttajat WHERE Kayttajanimi='" + Kayttajanimi + "';";
                command = new SqlCommand(sql, cnn); //en tiedä miksi on kaksi eri sql-komentoa, ohjeiden mukaan tein Owo
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