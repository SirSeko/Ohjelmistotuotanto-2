using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;

namespace VarastoApi.Backend {
    public class Test {

        public void Run() {
            string path = "C:\\temp\\test.txt";

            List<Materiaali> materiaalit = new List<Materiaali>(); //Lista materiaaleista
            List<Tilaus> tilaukset = new List<Tilaus>(); //Lista tilauksista
            List<Varaus> varaukset = new List<Varaus>();
            SqlConnection cnn; //tietokantayhteys-olio, jaetaan tämä muualle
            DatabaseManager dm = new DatabaseManager(); //tietokannanhallinta-olio, mm. yhdistys kantaan

            cnn = dm.OpenConnection(); //yhdistetään kantaan
            if (cnn != null) {
                DatabaseMateriaali dmMat = new DatabaseMateriaali(cnn); //luodaan olio, jolla käsitellään materiaaleja tietokannassa
                materiaalit = dmMat.SelectAll(materiaalit); //haetaan kaikki materiaalit kannasta listaan

                DatabaseTilaus dmTil = new DatabaseTilaus(cnn); //luodaan olio, jolla käsitellään tilauksia tietokannassa
                tilaukset = dmTil.SelectAll(tilaukset); //haetaan kaikki tilaukset kannasta listaan

                DatabaseVaraus dmVar = new DatabaseVaraus(cnn);
                varaukset = dmVar.SelectAll(varaukset);

                //Kokeillaan nyt lisätä kantaan ja päivittää listaa
                Materiaali m = new Materiaali(-1, "Testi1", "Iso", 14.50f, 3);
                dmMat.InsertInto(m);
                materiaalit = new List<Materiaali>();
                materiaalit = dmMat.SelectAll(materiaalit); //päivitetään lista

                Tilaus t = new Tilaus(-1, "Tilaaja1", 666, materiaalit[materiaalit.Count - 1].Id);
                dmTil.InsertInto(t);
                tilaukset = new List<Tilaus>();
                tilaukset = dmTil.SelectAll(tilaukset);

                Varaus v = new Varaus(-1, "Varaaja1", materiaalit[materiaalit.Count - 1].Id, 69);
                dmVar.InsertInto(v);
                varaukset = new List<Varaus>();
                varaukset = dmVar.SelectAll(varaukset);

                write(path, materiaalit, tilaukset, varaukset);//lopputilanne
                dm.CloseConnection();
            }
        }


        //kirjoittaa tiedostoon testituloksia
        void write(string path, List<Materiaali> mat, List<Tilaus> til, List<Varaus> var) {
            try {
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine("Materiaalit:");
                foreach (Materiaali m in mat) {
                    sw.WriteLine(m.Hinta + " - " + m.Id + " - " + m.Koko + " - " + m.Maara + " - " + m.Nimi);
                }
                sw.WriteLine("Tilaukset:");
                foreach (Tilaus t in til) {
                    sw.WriteLine(t.Id + " - " + t.Maara + " - " + t.MateriaaliId + " - " + t.TilaajanNimi);
                }
                sw.WriteLine("Varaukset:");
                foreach (Varaus v in var) {
                    sw.WriteLine(v.Id + " - " + v.Maara + " - " + v.MateriaaliId + " - " + v.VaraajanNimi);
                }
                sw.Close();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

    }
}