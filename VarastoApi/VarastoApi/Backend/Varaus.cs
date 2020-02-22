namespace VarastoApi.Backend {
    public class Varaus {
        public int Id; //VarausId
        public string VaraajanNimi;
        public int MateriaaliId;
        public int Maara;

        string sender() {
            return this.GetType().ToString();
        }

        private Varaus(int Id, string VaraajanNimi, int MateriaaliId, int Maara) {
            this.Id = Id;
            this.VaraajanNimi = VaraajanNimi;
            this.MateriaaliId = MateriaaliId;
            this.Maara = Maara;
        }

        /// <summary>
        /// Tarkistaa parametrit ja jos kaikki on OK, palauttaa varaus-olion. Jos jokin virhe, kirjoittaa lokiin ja palauttaa null
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="VaraajanNimi"></param>
        /// <param name="MateriaaliId"></param>
        /// <param name="Maara"></param>
        /// <returns>new Varaus || null, jos virhe</returns>
        public static Varaus CreateVaraus(int Id, string VaraajanNimi, int MateriaaliId, int Maara) {
            Varaus v = new Varaus(-1, "", -1, -1);
            if (!checkId(Id)) {
                ExceptionController.WriteException(v, "Varausta luodessa huono ID.");
                return null;
            }
            if (!checkVaraajanNimi(VaraajanNimi)) {
                ExceptionController.WriteException(v, "Varausta luodessa huono varaajan nimi.");
                return null;
            }
            if (!checkMateriaaliId(MateriaaliId)) {
                ExceptionController.WriteException(v, "Varausta luodessa huono materiaaliID.");
                return null;
            }
            if (!checkMaara(Maara)) {
                ExceptionController.WriteException(v, "Varausta luodessa huono määrä.");
                return null;
            }
            return new Varaus(Id, VaraajanNimi, MateriaaliId, Maara);
        }


        //tarkistus ID:lle TODO
        static bool checkId(int Id) {
            if (Id < -1) {
                return false;
            } else {
                return true;
            }
        }

        //tarkistus varaajan nimelle, lähinnä ettei ole tyhjä ja on enemmän kuin vaikka "x"
        static bool checkVaraajanNimi(string VaraajanNimi) {
            if (VaraajanNimi == null || VaraajanNimi == "" || VaraajanNimi.Length < 2) {
                return false;
            } else {
                return true;
            }
        }

        //tarkistus määrälle
        static bool checkMaara(int Maara) {
            if (Maara < 0) {
                return false;
            } else {
                return true;
            }
        }

        //tarkistus Materiaalin ID:lle TODO
        static bool checkMateriaaliId(int Id) {
            if (Id < 0) {
                return false;
            } else {
                return true;
            }
        }
    }
}