using System.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace VarastoApi.Backend
{

    //tällä luokalla hallitaan materiaaleja tietokannassa
    public class DatabaseMateriaali
    {
        SqlConnection cnn; //yhteys kantaan
        SqlCommand command; //komento-olio
        SqlDataReader dataReader; //luenta-olio
        SqlDataAdapter adapter = new SqlDataAdapter(); //käskynantoon liittyvä olio
        string sql, Output=""; 
        int id; //MateriaaliID
        int maaliId; 
        int vaneriId;
        int lautaId; 

        public DatabaseMateriaali(SqlConnection cnn){
            this.cnn = cnn;
        }



        //Lukee kaiken datan taulusta
        public List<Materiaali> SelectAll(List<Materiaali> materiaalit){
            sql = "Select * from mydb.Materiaali;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while(dataReader.Read()){
                int.TryParse(dataReader.GetValue(0).ToString(), out id);
                int.TryParse(dataReader.GetValue(1).ToString(), out maaliId);
                int.TryParse(dataReader.GetValue(2).ToString(), out vaneriId);
                int.TryParse(dataReader.GetValue(3).ToString(), out lautaId);
                Materiaali m = Materiaali.Create(id, maaliId, vaneriId, lautaId);
                materiaalit.Add(m);
            }
            dataReader.Close();
            return materiaalit;
        }

        //Lisää materiaalin tauluun, palauttaa true jos onnistui ilman poikkeuksia, false jos tuli poikkeus
        public bool InsertInto(Materiaali m) {
            try {
                sql = "Insert into mydb.Materiaali (MateriaaliID, MaaliID, VaneriID, LautaID) values ('" + m.Id+ "', '" + m.MaaliId+ "', '" + m.VaneriId + "', '" + m.LautaId + "');";
                command = new SqlCommand(sql, cnn); //en tiedä miksi on kaksi eri sql-komentoa, ohjeiden mukaan tein o.o
                adapter.InsertCommand = new SqlCommand(sql, cnn); //tämä on se toinen, mutta tämä ilmeisesti on käytössä?
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose(); //poistetaan olio
                return true;
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Muokkaa ID:n mukaista Columnia. tark Vastaanottaa olion Materiaali ja korvaa tietokannasta MateriaaliID:n mukaisen columnin tiedot.
        public bool Update(Materiaali m){
            try {
                sql = "UPDATE mydb.Materiaali SET MateriaaliID='" + m.Id + "', MaaliID='" + m.MaaliId + "', VaneriID='" + m.VaneriId+ "', LautaID='" + m.LautaId+ "' WHERE MateriaaliID ='" + m.Id + "';";
                command = new SqlCommand(sql, cnn); //en tiedä miksi on kaksi eri sql-komentoa, ohjeiden mukaan tein UwU
                adapter.UpdateCommand = new SqlCommand(sql, cnn); //tämä on se toinen, mutta tämä ilmeisesti on käytössä?
                adapter.UpdateCommand.ExecuteNonQuery();
                command.Dispose(); //poistetaan olio
                return true;
            }catch (Exception ex){
                Console.WriteLine(ex.Message);
                return false;
            }
        }


            //testijuttuja
            public void test(){
            //sql = "CREATE TABLE Testi(asd int, dew int);";
            sql = "Insert into Testi (asd,dew) values(1, 2);";
            command = new SqlCommand(sql, cnn);
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            sql = "Select * from Testi;";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read()){
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + "\n";
            }
            Console.WriteLine(Output);
            //command.BeginExecuteNonQuery();
        }
    }
}