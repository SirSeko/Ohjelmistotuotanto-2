using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using varastosovellus.Backend;

namespace varastosovellus
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Materiaali> materiaalit = new List<Materiaali>(); //Lista materiaaleista
            List<Tilaus> tilaukset = new List<Tilaus>(); //Lista tilauksista
            List<Varaus> varaukset = new List<Varaus>();
            SqlConnection cnn; //tietokantayhteys-olio, jaetaan tämä muualle
            DatabaseManager dm = new DatabaseManager(); //tietokannanhallinta-olio, mm. yhdistys kantaan

            cnn = dm.OpenConnection(); //yhdistetään kantaan
            if (cnn != null)
            {
                DatabaseMateriaali dmMat = new DatabaseMateriaali(cnn); //luodaan olio, jolla käsitellään materiaaleja tietokannassa
                materiaalit = dmMat.SelectAll(materiaalit); //haetaan kaikki materiaalit kannasta listaan

                DatabaseTilaus dmTil = new DatabaseTilaus(cnn); //luodaan olio, jolla käsitellään tilauksia tietokannassa
                tilaukset = dmTil.SelectAll(tilaukset); //haetaan kaikki tilaukset kannasta listaan

                DatabaseVaraus dmVar = new DatabaseVaraus(cnn);
                varaukset = dmVar.SelectAll(varaukset);

                //dmMat.test(); //testi
                dm.CloseConnection();

            }
        }
    }
}
