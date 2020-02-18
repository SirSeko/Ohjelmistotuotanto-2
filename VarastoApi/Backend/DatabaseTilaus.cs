using System.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace VarastoApi.Backend
{
    public class DatabaseTilaus
    {
        SqlConnection cnn; //yhteys kantaan
        SqlCommand command; //komento-olio
        SqlDataReader dataReader; //luenta-olio
        SqlDataAdapter adapter = new SqlDataAdapter(); //käskynantoon liittyvä olio
        string sql;

        int id;
        string tilaajanNimi;
        int maara;
        int materiaaliId;

        public DatabaseTilaus(SqlConnection cnn){
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Tilaus> SelectAll(List<Tilaus> tilaukset){
            sql = "Select * from mydb.Tilaus;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while(dataReader.Read()){
                int.TryParse(dataReader.GetValue(0).ToString(), out id);
                tilaajanNimi = dataReader.GetValue(1).ToString();
                int.TryParse(dataReader.GetValue(2).ToString(), out maara);
                int.TryParse(dataReader.GetValue(3).ToString(), out materiaaliId);
                Tilaus t = new Tilaus(id, tilaajanNimi, maara, materiaaliId);
                tilaukset.Add(t);
            }
            dataReader.Close();
            return tilaukset;
        }

        //Lisää tilauksen tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Tilaus t) {
            try {
                sql = "Insert into mydb.Tilaus (TilaajanNimi, Määrä, MateriaaliID) values ('" + t.TilaajanNimi + "', '" + t.Maara.ToString() + "', '" + t.MateriaaliId.ToString() + "');";
                command = new SqlCommand(sql, cnn); //en tiedä miksi on kaksi eri sql-komentoa, ohjeiden mukaan tein o.o
                adapter.InsertCommand = new SqlCommand(sql, cnn); //tämä on se toinen, mutta tämä ilmeisesti on käytössä?
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose(); //poistetaan olio
                return true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}