using System.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace varastosovellus.Backend
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
        string nimi;
        string koko; //Varmaan esim 6mm x 200mm x 200mm
        float hinta;
        int maara;

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
                nimi = dataReader.GetValue(1).ToString();
                koko = dataReader.GetValue(2).ToString();
                float.TryParse(dataReader.GetValue(3).ToString(), out hinta);
                int.TryParse(dataReader.GetValue(4).ToString(), out maara);
                Materiaali m = new Materiaali(id, nimi, koko, hinta, maara);
                materiaalit.Add(m);
            }
            return materiaalit;
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