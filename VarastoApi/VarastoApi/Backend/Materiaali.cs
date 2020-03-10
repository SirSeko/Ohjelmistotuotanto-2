namespace VarastoApi.Backend

{
    public class Materiaali
    {
        public int Id;
        public int MaaliId;
        public int VaneriId;
        public int LautaId;



        //Uusi materiaali luodaan vain konstruktorilla! Näin pitää päästä kaikkien tarkistusten läpi ja saadaan virheilmoitukset
        private Materiaali(int Id, int MaaliId, int VaneriId, int LautaId){
            this.Id = Id;
            this.MaaliId = MaaliId;
            this.VaneriId = VaneriId;
            this.LautaId = LautaId;
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
        public static Materiaali Create(int Id, int MaaliId, int VaneriId, int LautaId) {
            return new Materiaali(Id, MaaliId, VaneriId, LautaId);
        }
    }
}