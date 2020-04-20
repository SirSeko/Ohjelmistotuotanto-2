using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {
    public class Ymat {
        public int Id;
        public string Koko;
        public float Hinta;
        public int Maara;
        public string Yksikko;
        public int Sijainti;
        public string Kauppa;
        public string Lisatiedot;

        private Ymat(int id, string koko, float hinta, int maara, string yksikko, int sijainti, string kauppa, string lisatiedot) {
            Id = id;
            Koko = koko;
            Hinta = hinta;
            Maara = maara;
            Yksikko = yksikko;
            Sijainti = sijainti;
            Kauppa = kauppa;
            Lisatiedot = lisatiedot;
        }

        public Ymat() {
        }

        public static Ymat Create(int id, string koko, float hinta, int maara, string yksikko, int sijainti, string kauppa, string lisatiedot) {
            if (checkHinta(hinta) && checkId(id) && checkKauppa(kauppa) && checkKoko(koko) && checkLisatiedot(lisatiedot) && checkMaara(maara) && checkSijainti(sijainti) && checkYksikko(yksikko)) {
                return new Ymat(id, koko, hinta, maara, yksikko, sijainti, kauppa, lisatiedot);
            } else return null;
        }

        static bool checkId(int id) {
            if (id < -1) {
                // ExceptionController.WriteException(this, "Virhe ID:ssä.");
                return false;
            } else return true;
        }

        static bool checkKoko(string koko) {
            if (koko.Length > 45 || koko == null || koko == "") {
                // ExceptionController.WriteException(this, "Virhe koossa.");
                return false;
            } else return true;
        }

        static bool checkHinta(float hinta) {
            if (hinta < 0.0f) {
                // ExceptionController.WriteException(this, "Virhe hinnassa.");
                return false;
            } else return true;
        }

        static bool checkMaara(int maara) {
            if (maara < -1) {
                // ExceptionController.WriteException(this, "Virhe määrässä.");
                return false;
            } else return true;
        }

        static bool checkYksikko(string yksikko) {
            if (yksikko.Length > 45 || yksikko == null || yksikko == "") {
                // ExceptionController.WriteException(this, "Virhe yksikössä.");
                return false;
            } else return true;
        }

        static bool checkSijainti(int sijainti) {
            if (sijainti < -1) {
                // ExceptionController.WriteException(this, "Virhe sijainnissa.");
                return false;
            } else return true;
        }

        static bool checkKauppa(string kauppa) {
            if (kauppa.Length > 50) {
                // ExceptionController.WriteException(this, "Kaupan nimi liian pitkä.");
                return false;
            } else return true;
        }

        static bool checkLisatiedot(string lisatiedot) {
            if (lisatiedot.Length > 100) {
                // ExceptionController.WriteException(this, "Lisatiedoissa liikaa merkkejä.");
                return false;
            } else return true;
        }
    }
}