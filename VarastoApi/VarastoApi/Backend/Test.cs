using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;

namespace VarastoApi.Backend {
    public class Test {
        
        public Test() {
            Directory.CreateDirectory("C:\\temp\\VarastoApi");
        }
        string path = "C:\\temp\\VarastoApi\\test.txt";
        public void Run() {
            



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

                //Kokeillaan nyt lisätä kantaan, muokata kantaa ja päivittää listaa 
                Materiaali m = new Materiaali(-1, "Testi1", "Iso", 14.50f, 3);
                Materiaali mU = new Materiaali(4, "Moottorisahat", "Kookas", 500f, 1);
                dmMat.InsertInto(m);
                dmMat.Update(mU);
                materiaalit = new List<Materiaali>();
                materiaalit = dmMat.SelectAll(materiaalit); //päivitetään lista

                Tilaus t = new Tilaus(-1, "Tilaaja1", 666, materiaalit[materiaalit.Count - 1].Id);
                Tilaus tU = new Tilaus(5 ,"Tilaaja4299", 1337, materiaalit[3].Id);
                dmTil.InsertInto(t);
                dmTil.Update(tU);
                tilaukset = new List<Tilaus>();
                tilaukset = dmTil.SelectAll(tilaukset);

                Varaus v = Varaus.Create(-1, "Varaaja1", materiaalit[materiaalit.Count - 1].Id, 69);
                Varaus vU = Varaus.Create(6, "Varaaja6069", materiaalit[4].Id, 21);
                dmVar.InsertInto(v);
                dmVar.Update(vU);
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

        public void CallMe(object caller) {
            StreamWriter sr = new StreamWriter("C:\\temp\\call.txt", true);
            sr.WriteLine(caller.GetType().ToString());
            sr.Close();
        }

        public void CallMeTwice() {
            CallMe(this);
        }

        public void CreateException() {
            ExceptionController.WriteException(this, "Suuri virhe, projekti tuhoutui. Koodi 69");
        }

        public void TestFactoryPattern() {
            Varaus v1 = Varaus.Create(-2, "", -1, -1);
            if (v1 == null) write("v1=null");
            else write(v1.ToString());
            Varaus v2 = Varaus.Create(69, "", -1, -1);
            if (v2 == null) write("v2=null");
            else write(v2.ToString());
            Varaus v3 = Varaus.Create(69, "nimi1", -1, -1);
            if (v3 == null) write("v3=null");
            else write(v3.ToString());
            Varaus v4 = Varaus.Create(69, "nimi2", 420, -1);
            if (v4 == null) write("v4=null");
            else write(v4.ToString());
            Varaus v5 = Varaus.Create(69, "toimii", 420, 366);
            if (v5 == null) write("v5=null");
            else write(v5.ToString());
            
        }

        void write(string text) {
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(DateTime.Now + " - " + text);
            sw.Close();
        }
    }
}