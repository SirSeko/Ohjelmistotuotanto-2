using System.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace VarastoApi.Backend {
    public class DatabaseTilaus {
        SqlConnection cnn; //yhteys kantaan
        SqlCommand command; //komento-olio
        SqlDataReader dataReader; //luenta-olio
        SqlDataAdapter adapter = new SqlDataAdapter(); //käskynantoon liittyvä olio
        string sql;

        int id;
        string tilaajanNimi;
        string tilaajanOsoite;

        public DatabaseTilaus(SqlConnection cnn) {
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Tilaus> SelectAll(List<Tilaus> tilaukset) {
            try {
                sql = "Select * from mydb.Tilaus;";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read()) {
                    int.TryParse(dataReader.GetValue(0).ToString(), out id);
                    tilaajanNimi = dataReader.GetValue(1).ToString();
                    tilaajanOsoite = dataReader.GetValue(2).ToString();
                    Tilaus t = Tilaus.Create(id, tilaajanNimi, tilaajanOsoite);
                    tilaukset.Add(t);
                }
                dataReader.Close();
                return tilaukset;
            } catch (Exception ex) {
                ExceptionController.WriteException(this, ex.Message);
                return null;
            }
        }

        //Lisää tilauksen tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Tilaus t) {
            try {
                sql = "Insert into mydb.Tilaus (TilaajanNimi, TilaajanOsoite) values ('" + t.TilaajanNimi + "', '" + t.TilaajanOsoite + "');";
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
        public bool Update(Tilaus t) {
            try {
                sql = "UPDATE mydb.Tilaus SET TilaajanNimi='" + t.TilaajanNimi + "', TilaajanOsoite='" + t.TilaajanOsoite + "' WHERE TilausID ='" + t.Id + "';";
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

        public bool Delete(int id) {
            try {
                sql = "Delete FROM mydb.Tilaus WHERE TilausID='" + id + "';";
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