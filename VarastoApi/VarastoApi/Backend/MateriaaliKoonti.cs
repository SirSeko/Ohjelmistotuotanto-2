using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {
    public class MateriaaliKoonti {
        public List<Materiaali> Materiaalit;
        public List<Vaneri> Vanerit;
        public List<Sijainti> Sijainnit;
        public List<Maali> Maalit;
        public List<Lauta> Laudat;

        public MateriaaliKoonti() {
            Materiaalit = new List<Materiaali>();
            Vanerit = new List<Vaneri>();
            Sijainnit = new List<Sijainti>();
            Maalit = new List<Maali>();
            Laudat = new List<Lauta>();
        }
    }
}