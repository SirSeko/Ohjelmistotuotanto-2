namespace VarastoApi.Backend {
    public class Tilaus {
        public int Id;
        public string TilaajanNimi;
        public int Maara;
        public int MateriaaliId;



        //Uudet tilaukset luodaan vain konstruktorilla!! Näin toimii hyvä tarkistus
        private Tilaus(int Id, string TilaajanNimi, int Maara, int MateriaaliId) {
            this.Id = Id;
            this.TilaajanNimi = TilaajanNimi;
            this.Maara = Maara;
            this.MateriaaliId = MateriaaliId;
            }

        /// <summary>
        /// Tarkistaa parametrit ja luo uuden tilauksen. Viheellisellä syötteellä palauttaa null.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="TilaajanNimi"></param>
        /// <param name="Maara"></param>
        /// <param name="MateriaaliId"></param>
        /// <returns>Tilaus || null</returns>
        public static Tilaus Create(int Id, string TilaajanNimi, int Maara, int MateriaaliId) {
            Tilaus t = new Tilaus(-1, "", -1, -1);
            if (!checkId(Id)) {
                ExceptionController.WriteException(t, "Tilasta luodessa huono ID.");
                return null;
            }
            if (!checkTilaajanNimi(TilaajanNimi)) {
                ExceptionController.WriteException(t, "Tilausta luodessa huono tilaajan nimi.");
                return null;
            }
            if (!checkMaara(Maara)) {
                ExceptionController.WriteException(t, "Tilausta luodessa huono tilaajan nimi.");
                return null;
            }
            if (!checkMateriaaliId(MateriaaliId)) {
                ExceptionController.WriteException(t, "Tilausta luodessa huono materiaalin id.");
                return null;
            }
            return new Tilaus(Id, TilaajanNimi, Maara, MateriaaliId);
        }


        //tarkistus ID:lle TODO
        static bool checkId(int Id) {
            if (Id < -1) {
                return false;
            } else {
                return true;
            }
        }

        //tarkistus tilaajan nimelle, lähinnä ettei ole tyhjä ja on enemmän kuin vaikka "x"
        static bool checkTilaajanNimi(string TilaajanNimi) {
            if (TilaajanNimi == null || TilaajanNimi == "" || TilaajanNimi.Length < 2) {
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