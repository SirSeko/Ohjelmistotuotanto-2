using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {
    public class Tilattava {
        public int TilausId;
        public int MateriaaliId;
        public int Maara;

        public Tilattava(int TilausId, int MateriaaliId, int Maara) {
            this.TilausId = TilausId;
            this.MateriaaliId = MateriaaliId;
            this.Maara = Maara;
        }
        public static Tilattava Create(int TilausId, int MateriaaliId, int Maara)
        {
            Tilattava t = new Tilattava(-1,-1,-1);
            return new Tilattava(TilausId, MateriaaliId, Maara);
        }
    }
}