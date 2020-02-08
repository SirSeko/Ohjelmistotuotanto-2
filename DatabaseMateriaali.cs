using System.Data.SqlClient;
namespace varastosovellus
{
    public class DatabaseMateriaali
    {
        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        string sql, Output="";

        public DatabaseMateriaali(SqlConnection cnn){
            this.cnn = cnn;
        }

        public void test(){
            sql = "CREATE TABLE Testi(asd int, dew int);";
            command = new SqlCommand(sql, cnn);
            command.BeginExecuteNonQuery();
        }
    }
}