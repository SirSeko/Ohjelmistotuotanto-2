namespace VarastoApi.Backend
{
    public class Tilaus
    {
        public int Id;
        public string TilaajanNimi;
        public int Maara;
        public int MateriaaliId;



        //Uudet tilaukset luodaan vain konstruktorilla!! Näin toimii hyvä tarkistus
        public Tilaus(int Id, string TilaajanNimi, int Maara, int MateriaaliId)
        {
            if (checkId(Id)){
                this.Id = Id;
            } else {
                //TODO Virheilmoitus
            }
            if (checkTilaajanNimi(TilaajanNimi)){
                this.TilaajanNimi = TilaajanNimi;
            } else {
                //TODO Virheilmoitus
            }
            if (checkMaara(Maara)){
                this.Maara = Maara;
            } else {
                //TODO Virheilmoitus
            }
            if (checkMateriaaliId(MateriaaliId)){
                this.MateriaaliId = MateriaaliId;
            } else {
                //TODO Virheilmoitus
            }
        }


        //tarkistus ID:lle TODO
        bool checkId(int Id)
        {
            if (Id < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //tarkistus tilaajan nimelle, lähinnä ettei ole tyhjä ja on enemmän kuin vaikka "x"
        bool checkTilaajanNimi(string TilaajanNimi)
        {
            if (TilaajanNimi == null || TilaajanNimi == "" || TilaajanNimi.Length < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //tarkistus määrälle
        bool checkMaara(int Maara)
        {
            if (Maara < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //tarkistus Materiaalin ID:lle TODO
        bool checkMateriaaliId(int Id)
        {
            if (Id < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}