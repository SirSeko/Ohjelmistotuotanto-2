using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {
    //Käyttäjä-olio, kertoo käyttäjänimen ja valtuustason
    public class Kayttaja {
        public string Kayttajanimi;
        public int Valtuus;

        public Kayttaja(string kayttajanimi, int valtuus) {
            if (checkKayttajanimi(kayttajanimi)) {
                this.Kayttajanimi = kayttajanimi;
            } else {
                //TODO Virheilmoitus
            }
            if (checkValtuus(valtuus)) {
                this.Valtuus = valtuus;
            } else {
                //TODO Virheilmoitus
            }
        }

        bool checkValtuus(int valtuus) {
            if (valtuus < 0 || valtuus > 2) return false;
            else return true;
        }

        bool checkKayttajanimi(string kayttajanimi) {
            if (kayttajanimi.Length < 4) return false;
            else return true;
        }
    }
}