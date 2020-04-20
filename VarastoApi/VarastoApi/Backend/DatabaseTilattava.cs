using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace VarastoApi.Backend {
    public class DatabaseTilattava {
        SqlConnection cnn; //yhteys kantaan
        SqlCommand command; //komento-olio
        SqlDataReader dataReader; //luenta-olio
        SqlDataAdapter adapter = new SqlDataAdapter(); //käskynantoon liittyvä olio
        string sql;

        public int tilausId;
        public int materiaaliId;
        public int maara;

        public DatabaseTilattava(SqlConnection cnn) {
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Tilattava> SelectAll() {
            try {
                List<Tilattava> tilattavat = new List<Tilattava>();
                sql = "Select * from mydb.Tilattava;";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read()) {
                    int.TryParse(dataReader.GetValue(0).ToString(), out tilausId);
                    int.TryParse(dataReader.GetValue(1).ToString(), out materiaaliId);
                    int.TryParse(dataReader.GetValue(2).ToString(), out maara);
                    Tilattava t = new Tilattava(tilausId, materiaaliId, maara);
                    tilattavat.Add(t);
                }
                dataReader.Close();
                return tilattavat;
            } catch (Exception ex) {
                ExceptionController.WriteException(this, ex.Message);
                return null;
            }
        }

        //Hakee kaikki tilattavat annetulla TilausID:llä
        public List<Tilattava> SelectTilaus(int TilausID) {
            try {
                List<Tilattava> tilattavat = new List<Tilattava>();
                sql = "Select * from mydb.Tilattava where TilausID = '" + TilausID + "';";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read()) {
                    int.TryParse(dataReader.GetValue(0).ToString(), out tilausId);
                    int.TryParse(dataReader.GetValue(1).ToString(), out materiaaliId);
                    int.TryParse(dataReader.GetValue(2).ToString(), out maara);
                    Tilattava t = new Tilattava(tilausId, materiaaliId, maara);
                    tilattavat.Add(t);
                }
                dataReader.Close();
                return tilattavat;
            } catch (Exception ex) {
                ExceptionController.WriteException(this, ex.Message);
                return null;
            }
        }

        //Lisää tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Tilattava t) {
            try {
                sql = "Insert into mydb.Tilattava (TilausID, MateriaaliID, Määrä) values ('" + t.TilausId + "', '" + t.MateriaaliId+ "', '" + t.Maara+ "');";
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
        public bool Update(Tilattava t) {
            try {
                sql = "UPDATE mydb.Tilattava SET MateriaaliID='" + t.MateriaaliId + "', Määrä='" + t.Maara +"' WHERE TilausID ='" + t.TilausId + "' AND MateriaaliID = '" + t.MateriaaliId + ";";
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
        public bool Delete(Tilattava t) {
            try {
                sql = "Delete FROM mydb.Tilattava WHERE TilausID='" + t.TilausId + "' AND MateriaaliID = '" + t.MateriaaliId + "';";
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