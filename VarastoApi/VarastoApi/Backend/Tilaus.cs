namespace VarastoApi.Backend {
    public class Tilaus {
        public int Id;
        public string TilaajanNimi;
        public string TilaajanOsoite;



        //Uudet tilaukset luodaan vain konstruktorilla!! Näin toimii hyvä tarkistus
        private Tilaus(int Id, string TilaajanNimi, string TilaajanOsoite) {
            this.Id = Id;
            this.TilaajanNimi = TilaajanNimi;
            this.TilaajanOsoite = TilaajanOsoite;
            }

        /// <summary>
        /// Tarkistaa parametrit ja luo uuden tilauksen. Viheellisellä syötteellä palauttaa null.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="TilaajanNimi"></param>
        /// <param name="Maara"></param>
        /// <param name="MateriaaliId"></param>
        /// <returns>Tilaus || null</returns>
        public static Tilaus Create(int Id, string TilaajanNimi, string TilaajanOsoite) {
            Tilaus t = new Tilaus(-1, "", "");
            if (!checkId(Id)) {
                ExceptionController.WriteException(t, "Tilasta luodessa huono ID.");
                return null;
            }
            if (!checkTilaajanNimi(TilaajanNimi)) {
                ExceptionController.WriteException(t, "Tilausta luodessa huono tilaajan nimi.");
                return null;
            }
            if (!checkTilaajanOsoite(TilaajanOsoite)) {
                ExceptionController.WriteException(t, "Tilausta luodessa huono tilaajan osoite.");
                return null;
            }
            return new Tilaus(Id, TilaajanNimi, TilaajanOsoite);
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

        //tarkistus tilaajan osoitteelle
        static bool checkTilaajanOsoite(string TilaajanOsoite) {
            if (TilaajanOsoite == null || TilaajanOsoite == "" || TilaajanOsoite.Length < 2) {
                return false;
            } else {
                return true;
            }
        }

    }
}