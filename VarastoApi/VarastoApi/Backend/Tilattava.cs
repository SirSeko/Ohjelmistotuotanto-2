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
    }
}