namespace VarastoApi.Backend

{
    public class Materiaali
    {
        public int Id; //MateriaaliID
        public string Nimi;
        public string Koko; //Varmaan esim 6mm x 200mm x 200mm
        public float Hinta;
        public int Maara;



        //Uusi materiaali luodaan vain konstruktorilla! Näin pitää päästä kaikkien tarkistusten läpi ja saadaan virheilmoitukset
        private Materiaali(int Id, string Nimi, string Koko, float Hinta, int Maara){
                this.Id = Id;
            this.Nimi = Nimi;
            this.Koko = Koko;
                this.Hinta = Hinta;
            this.Maara = Maara;
        }

        /// <summary>
        /// Tarkistaa parametrit ja palauttaa uuden materiaalin jos kaikki ok.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Nimi"></param>
        /// <param name="Koko"></param>
        /// <param name="Hinta"></param>
        /// <param name="Maara"></param>
        /// <returns>Materiaali || null</returns>
        public static Materiaali Create(int Id, string Nimi, string Koko, float Hinta, int Maara) {
            Materiaali m = new Materiaali(-1, "", "", -1, -1);
            if (!checkId(Id)) {
                ExceptionController.WriteException(m, "Materiaalia luodessa huono ID.");
                return null;
            }
            if (!checkNimi(Nimi)) {
                ExceptionController.WriteException(m, "Materiaalia luodessa huono nimi.");
                return null;
            }
            if (!checkKoko(Koko)) {
                ExceptionController.WriteException(m, "Materiaalia luodessa huono koko.");
                return null;
            }
            if (!checkHinta(Hinta)){
                ExceptionController.WriteException(m, "Materiaalia luodessa huono hinta.");
                return null;
            }
            return new Materiaali(Id, Nimi, Koko, Hinta, Maara);
        }


        //tarkistus ID:lle, jos ID on -1, materiaali on uusi
        static bool checkId(int Id){
            if (Id < -1){
                return false;
            } else {
                return true;
            }
        }


        //tarkistus nimelle
        static bool checkNimi(string Nimi){
            if (Nimi == null || Nimi == "" || Nimi.Length < 2){
                return false;
            } else {
                return true;
            }
        }


        //tarkistus koolle
        static bool checkKoko(string Koko){
            if (Koko == null || Koko == ""){
                return false;
            } else {
                return true;
            }
        }


        //tarkistus hinnalle
        static bool checkHinta(float Hinta){
            if (Hinta < 0){
                return false;
            } else {
                return true;
            }
        }
    }
}