using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {
    public class Sijainti {
        public int Id;
        public string Nimi;
        public string Kaappi;
        public string Hylly;
        public string Lisatiedot;

        private Sijainti(int id, string nimi, string kaappi, string hylly, string lisatiedot) {
            Id = id;
            Nimi = nimi;
            Kaappi = kaappi;
            Hylly = hylly;
            Lisatiedot = lisatiedot;
        }

        public Sijainti Create(int id, string nimi, string kaappi, string hylly, string lisatiedot) {
            if (checkId(id) && checkNimi(nimi) && checkKaappi(kaappi) && checkHylly(hylly) && checkLisatiedot(lisatiedot)) {
                return new Sijainti(id, nimi, kaappi, hylly, lisatiedot)
            } else return null;
        }

        bool checkId(int id) {
            if (id < -1) {
                ExceptionController.WriteException(this, "Virhe id:ssä.");
                return false;
            } else return true;
        }

        bool checkNimi(string nimi) {
            if (nimi.Length > 50 || nimi == null || nimi == "") {
                ExceptionController.WriteException(this, "Virhe nimessä.");
                return false;
            } else return true;
        }

        bool checkKaappi(string kaappi) {
            if (kaappi.Length > 50 || kaappi == null || kaappi == "") {
                ExceptionController.WriteException(this, "Virhe kaapin nimessä.");
                return false;
            } else return true;
        }

        bool checkHylly(string hylly) {
            if (hylly.Length > 50 || hylly == null || hylly == "") {
                ExceptionController.WriteException(this, "Virhe hyllyn nimessä.");
                return false;
            } else return true;
        }

        bool checkLisatiedot(string lisatiedot) {
            if (lisatiedot.Length > 100) {
                ExceptionController.WriteException(this, "Lisätiedoissa liian monta merkkiä.");
                return false;
            } else return true;
        }
    }
}