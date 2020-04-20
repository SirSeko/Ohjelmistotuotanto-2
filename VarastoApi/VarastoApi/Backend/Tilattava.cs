using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarastoApi.Backend {
    public class Tilattava {
        public int TilausId;
        public int MateriaaliId;

        public Tilattava(int TilausId, int MateriaaliId) {
            this.TilausId = TilausId;
            this.MateriaaliId = MateriaaliId;
        }
        public static Tilattava Create(int TilausId, int MateriaaliId)
        {
            Tilattava t = new Tilattava(-1,-1);
            return new Tilattava(TilausId, MateriaaliId);
        }
    }
}