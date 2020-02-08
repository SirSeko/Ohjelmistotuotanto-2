using System;
using System.Data.SqlClient;

namespace varastosovellus
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cnn;
            DatabaseManager dm = new DatabaseManager();

            cnn = dm.OpenConnection();
            //System.Threading.Thread.Sleep(1000*10);
            DatabaseMateriaali dmMat = new DatabaseMateriaali(cnn);
            dmMat.test();
            dm.CloseConnection();
        }
    }
}
