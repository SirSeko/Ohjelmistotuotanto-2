using System;
using System.Data.SqlClient;
namespace varastosovellus.Backend
{

    //Yhteyden luonti tietokantaan ja yhteyden jako muille luokille
    public class DatabaseManager
    {

        string connectionString;

        //käytetään kaikissa SQL komennoissa
        SqlConnection cnn;


        //konstruktori
        public DatabaseManager(){
            cnn = new SqlConnection();
            cnn.ConnectionString = "Data Source=varastotyokalu.c2dphe33amuf.us-east-2.rds.amazonaws.com;Initial Catalog=mydb;User ID=admin;Password=varastotyokalu2020";
        }


        //luo yhteyden ja palauttaa yhteyden olion
        public SqlConnection OpenConnection(){

            try{
            cnn.Open();
            Console.WriteLine("SQL connection open");
            return cnn;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //sulkee yhteyden
        public void CloseConnection(){
            cnn.Close();
            Console.WriteLine("SQL connection closed");
        }
        
    }
}