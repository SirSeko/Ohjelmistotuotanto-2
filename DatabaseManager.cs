using System;
using System.Data.SqlClient;
namespace varastosovellus
{
    public class DatabaseManager
    {
        string connectionString;
        SqlConnection cnn;

        public DatabaseManager(){
            //cnn.ConnectionString = "Data Source=varastotyokalu.c2dphe33amuf.us-east-2.rds.amazonaws.com;Initial Catalog=master;User ID=admin;Password=varastotyokalu2020";


            //connectionString = @"Data Source=database-2.c2dphe33amuf.us-east-2.rds.amazonaws.com,3306;Initial Catalog=mydb;User ID=admin;Password=varastosovellus2020";
            //connectionString = @"server=varastosovellus.c2dphe33amuf.us-east-2.rds.amazonaws.com;database=mydb;UID=admin;password=varastosovellus2020";
        }

        public SqlConnection OpenConnection(){
            cnn = new SqlConnection(connectionString);
            cnn.ConnectionString = "Data Source=varastotyokalu.c2dphe33amuf.us-east-2.rds.amazonaws.com;Initial Catalog=mydb;User ID=admin;Password=varastotyokalu2020";
            cnn.Open();
            Console.WriteLine("SQL connection open");
            return cnn;
        }

        public void CloseConnection(){
            cnn.Close();
            Console.WriteLine("SQL connection closed");
        }
        
    }
}