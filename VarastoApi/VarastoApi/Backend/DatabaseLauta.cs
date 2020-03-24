using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace VarastoApi.Backend {
    public class DatabaseLauta {
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

        public DatabaseLauta(SqlConnection cnn) {
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Lauta> SelectAll(List<Lauta> laudat) {
            sql = "Select * from mydb.Lauta;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read()) {
                int.TryParse(dataReader.GetValue(0).ToString(), out id);
                koko = dataReader.GetValue(1).ToString();
                float.TryParse(dataReader.GetValue(2).ToString(), out hinta);
                int.TryParse(dataReader.GetValue(3).ToString(), out maara);
                lisatiedot = dataReader.GetValue(4).ToString();
                int.TryParse(dataReader.GetValue(5).ToString(), out sijainti);
                kauppa = dataReader.GetValue(6).ToString();
                yksikko = dataReader.GetValue(7).ToString();
                Lauta m= Lauta.Create(id, koko, hinta, maara, yksikko, sijainti, kauppa, lisatiedot);
                laudat.Add(m);
            }
            dataReader.Close();
            return laudat;
        }

        //Lisää tilauksen tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Lauta m) {
            try {
                sql = "Insert into mydb.Lauta(Koko, Hinta, Määrä, Yksikkö, Sijainti, Kauppa, Lisätiedot) values ('" + m.Koko + "', '" + m.Hinta + "', '" + m.Maara + "', '" + m.Yksikko + "', '" + m.Sijainti + "', '" + m.Kauppa + "', '" + m.Lisatiedot + "');";
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
        public bool Update(Lauta m) {
            try {
                sql = "UPDATE mydb.Lauta SET Koko='" + m.Koko + "', Hinta='" + m.Hinta + "', Määrä='" + m.Maara + "', Yksikkö='=" + m.Yksikko + "', Sijainti='" + m.Sijainti + "', Kauppa='" + m.Kauppa + "', Lisätiedot='" + m.Lisatiedot + "' WHERE LautaID='" + m.Id + "';";
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
        public Lauta SelectId(int id)
        {
            sql = "Select * from mydb.Lauta Where LautaId='" + id + "';";
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
            Lauta m = Lauta.Create(id, koko, hinta, maara, yksikko, sijainti, kauppa, lisatiedot);
            return m;
        }

    }
}