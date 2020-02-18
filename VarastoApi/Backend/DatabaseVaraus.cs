using System.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace VarastoApi.Backend
{
    public class DatabaseVaraus
    {
        SqlConnection cnn; //yhteys kantaan
        SqlCommand command; //komento-olio
        SqlDataReader dataReader; //luenta-olio
        SqlDataAdapter adapter = new SqlDataAdapter(); //käskynantoon liittyvä olio
        string sql;

        int id;
        string varaajanNimi;
        int maara;
        int materiaaliId;

        public DatabaseVaraus(SqlConnection cnn){
            this.cnn = cnn;
        }

        //Lukee kaiken datan taulusta
        public List<Varaus> SelectAll(List<Varaus> varaukset){
            sql = "Select * from mydb.Varaus;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while(dataReader.Read()){
                int.TryParse(dataReader.GetValue(0).ToString(), out id);
                varaajanNimi = dataReader.GetValue(1).ToString();
                int.TryParse(dataReader.GetValue(2).ToString(), out materiaaliId);
                int.TryParse(dataReader.GetValue(3).ToString(), out maara);
                Varaus v = new Varaus(id, varaajanNimi, materiaaliId, maara);
                varaukset.Add(v);
            }
            dataReader.Close();
            return varaukset;
        }


        //Lisää uuden varauksen tauluun, palauttaa true jos ei tule poikkeusta, muuten false
        public bool InsertInto(Varaus v) {
            try {
                sql = "Insert into mydb.Varaus (VaraajanNimi, MateriaaliID, Määrä) values ('" + v.VaraajanNimi + "', '" + v.MateriaaliId.ToString() + "', '" + v.Maara.ToString() + "');";
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

        //Muokkaa ID:n mukaista Columnia. tark Vastaanottaa olion Varaus ja korvaa tietokannasta MateriaaliID:n mukaisen columnin tiedot.
        public bool Update(Varaus v){
            try{
                sql = "UPDATE mydb.Varaus SET VaraajanNimi='" + v.VaraajanNimi + "', MateriaaliID='" + v.MateriaaliId + "', Määrä='" + v.Maara + "' WHERE VarausID ='" + v.Id + "';";
                command = new SqlCommand(sql, cnn); //en tiedä miksi on kaksi eri sql-komentoa, ohjeiden mukaan tein Owo
                adapter.UpdateCommand = new SqlCommand(sql, cnn); //tämä on se toinen, mutta tämä ilmeisesti on käytössä?
                adapter.UpdateCommand.ExecuteNonQuery();
                command.Dispose(); //poistetaan olio
                return true;
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}