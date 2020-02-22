using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {
    //Käyttäjä-olio, kertoo käyttäjänimen ja valtuustason
    public class Kayttaja {
        public string Kayttajanimi;
        public int Valtuus;

        private Kayttaja(string kayttajanimi, int valtuus) {
            this.Kayttajanimi = kayttajanimi;
            this.Valtuus = valtuus;
        }
        
        /// <summary>
        /// Tarkistaa parametrit ja luo uuden käyttäjän
        /// </summary>
        /// <param name="kayttajanimi">pituus vähintään 4</param>
        /// <param name="valtuus">0...2</param>
        /// <returns>Kayttaja. Virheellisellä sytteellä null</returns>
        public static Kayttaja Create(string kayttajanimi, int valtuus) {
            Kayttaja k = new Kayttaja("", -1);
            if (!checkKayttajanimi(kayttajanimi)) {
                ExceptionController.WriteException(k, "Käyttäjää luodessa huono käyttäjänimi.");
                return null;
            }
            if (!checkValtuus(valtuus)) {
                ExceptionController.WriteException(k, "Käyttäjää luodessa huono valtuus.");
                return null;
            }
            return new Kayttaja(kayttajanimi, valtuus);
        }


        //tarkistukset
        static bool checkValtuus(int valtuus) {
            if (valtuus < 0 || valtuus > 2) return false;
            else return true;
        }
        static bool checkKayttajanimi(string kayttajanimi) {
            if (kayttajanimi.Length < 4) return false;
            else return true;
        }
    }
}