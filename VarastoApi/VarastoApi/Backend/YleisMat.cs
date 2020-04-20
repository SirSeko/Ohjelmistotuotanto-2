using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {
    public class YleisMat {
        public int Id;
        public string Koko;
        public float Hinta;
        public int Maara;
        public string Yksikko;
        public int Sijainti;
        public string Kauppa;
        public string Lisatiedot;

        public static YleisMat MuunnaYleiseksi(Maali x) {
            YleisMat y = new YleisMat();
            y.Id = x.Id;
            y.Koko = x.Koko;
            y.Hinta = x.Hinta;
            y.Maara = x.Maara;
            y.Yksikko = x.Yksikko;
            y.Sijainti = x.Sijainti;
            y.Kauppa = x.Kauppa;
            y.Lisatiedot = x.Lisatiedot;
            return y;
        }

        public static YleisMat MuunnaYleiseksi(Vaneri x) {
            YleisMat y = new YleisMat();
            y.Id = x.Id;
            y.Koko = x.Koko;
            y.Hinta = x.Hinta;
            y.Maara = x.Maara;
            y.Yksikko = x.Yksikko;
            y.Sijainti = x.Sijainti;
            y.Kauppa = x.Kauppa;
            y.Lisatiedot = x.Lisatiedot;
            return y;
        }

        public static YleisMat MuunnaYleiseksi(Lauta x) {
            YleisMat y = new YleisMat();
            y.Id = x.Id;
            y.Koko = x.Koko;
            y.Hinta = x.Hinta;
            y.Maara = x.Maara;
            y.Yksikko = x.Yksikko;
            y.Sijainti = x.Sijainti;
            y.Kauppa = x.Kauppa;
            y.Lisatiedot = x.Lisatiedot;
            return y;
        }

    }
}