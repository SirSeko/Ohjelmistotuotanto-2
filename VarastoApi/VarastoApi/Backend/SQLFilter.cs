using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {


    //Tarkistaa käyttäjän syötteen sisällön, ettei sieltä löydy mitään sisältöä, jolla voisi vahingoittaa tietokantaa. Olio kannattanee luoda kontrollerissa
    public class SQLFilter {
        string[] badInput = { ";", "'", "--", "/*", "*/", "xp_" };
        public SQLFilter(string input) {
        }

        //tarkistetaan KAIKKI käyttäjän input stringit joita käytetään tietokannassa, esim kaikki tallennukset ja muokkaukset
        //Palauttaa false, jos kiellettyjä merkintöjä syötteessä, true muuten.
        public bool checkInput(string input) {
            foreach (string s in badInput) {
                if (input.Contains(s)) {
                    return false;
                }
            }
            return true;
        }

    }
}