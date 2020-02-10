namespace varastosovellus.Backend
{
    public class Materiaali
    {
        public int Id; //MateriaaliID
        public string Nimi;
        public string Koko; //Varmaan esim 6mm x 200mm x 200mm
        public float Hinta;
        public int Maara;



        //Uusi materiaali luodaan vain konstruktorilla! Näin pitää päästä kaikkien tarkistusten läpi ja saadaan virheilmoitukset
        public Materiaali(int Id, string Nimi, string Koko, float Hinta, int Maara){
            if (checkId(Id)){
                this.Id = Id;
            } else {
                //TODO Virheilmoitus
            }
            if (checkNimi(Nimi)){
                this.Nimi = Nimi;
            } else {
                //TODO Virheilmoitus
            }
            this.Koko = checkKoko(Koko);
            if (checkHinta(Hinta)){
                this.Hinta = Hinta;
            } else {
                //TODO Virheilmoitus
            }
            this.Maara = Maara;
        }


        //tarkistus ID:lle TODO
        bool checkId(int Id){
            if (Id < 0){
                return false;
            } else {
                return true;
            }
        }


        //tarkistus nimelle
        bool checkNimi(string Nimi){
            if (Nimi == null || Nimi == "" || Nimi.Length < 2){
                return false;
            } else {
                return true;
            }
        }


        //tarkistus koolle
        string checkKoko(string Koko){
            if (Koko == null || Koko == ""){
                return "";
            } else {
                return Koko;
            }
        }


        //tarkistus hinnalle
        bool checkHinta(float Hinta){
            if (Hinta < 0){
                return false;
            } else {
                return true;
            }
        }
    }
}