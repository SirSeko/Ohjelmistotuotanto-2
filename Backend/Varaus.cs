namespace varastosovellus.Backend
{
    public class Varaus
    {
        public int Id; //VarausId
        public string VaraajanNimi;
        public int MateriaaliId;
        public int Maara;

        public Varaus(int Id, string VaraajanNimi, int MateriaaliId, int Maara)
        {
            if (checkId(Id)){
                this.Id = Id;
            } else {
                //TODO Virheilmoitus
        }
            if (checkVaraajanNimi(VaraajanNimi)){
                this.VaraajanNimi = VaraajanNimi;
            } else {
                //TODO Virheilmoitus
            }
            if (checkMateriaaliId(MateriaaliId)){
                this.MateriaaliId = MateriaaliId;
            } else {
                //TODO Virheilmoitus
            }
            if (checkMaara(Maara)){
                this.Maara = Maara;
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

        //tarkistus varaajan nimelle, lähinnä ettei ole tyhjä ja on enemmän kuin vaikka "x"
        bool checkVaraajanNimi(string VaraajanNimi)
        {
            if (VaraajanNimi == null || VaraajanNimi == "" || VaraajanNimi.Length < 2)
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