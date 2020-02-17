using System.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace varastosovellus.Backend
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
                int.TryParse(dataReader.GetValue(4).ToString(), out materiaaliId);
                int.TryParse(dataReader.GetValue(3).ToString(), out maara);
                Varaus v = new Varaus(id, varaajanNimi, materiaaliId, maara);
                varaukset.Add(v);
            }
            return varaukset;
        }
    }
}