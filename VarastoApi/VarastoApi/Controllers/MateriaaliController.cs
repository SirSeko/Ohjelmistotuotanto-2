using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VarastoApi.Backend;
using System.Data.SqlClient;

// Tämä on Testausvaiheessa

namespace VarastoApi.Controllers
{
    public class MateriaaliController : ApiController
    {
        List<Vaneri> vaneri = new List<Vaneri>(); //Lista vanereista
        SqlConnection cnn;
        DatabaseManager dm = new DatabaseManager(); //tietokannanhallinta-olio, mm. yhdistys kantaan

        public MateriaaliController() { 
            cnn = dm.OpenConnection();    
            if (cnn != null)
            {
                DatabaseVaneri dmVan = new DatabaseVaneri(cnn); //luodaan olio, jolla käsitellään materiaaleja tietokannassa
                vaneri = dmVan.SelectAll(vaneri); // Valitaan kaikki tiedot vaneritaulusta listaan
            }
        }

        // GET: api/Materiaali esimerkki, palauttaa kaikki vanerit
        public List<Vaneri> Get()
        {
                return vaneri;
        }

        // GET: api/Materiaali/5 esimerkki
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Materiaali esimerkki
        public void Post([FromBody]string value)
        {
        }

        // DELETE: api/Materiaali/5 esimerkki
        public void Delete(int id)
        {
        }

        //Voidaan määrittää route (osoite) eri komennoille esim. jos halutaan vain lisätiedot
        [Route("api/Materiaali/VaneriTiedot")] //Kun käyttäjä painaa linkkiä johon tämä osoite on määritelty, osaa sivu automaagisesti ohjata tähän.
        [HttpGet] //Määritetään mikä komento on kyseessä. tässä tapauksessa GET
        public List<string> HaeVaneriTiedot()
        {
            List<string> output = new List<string>(); //Luodaan tulostettava lista

            foreach (var tiedot in vaneri)
            {

                output.Add(tiedot.Lisatiedot);
            }
            return output;

        }
        // Esimerkki
        [Route("api/Materiaali/VaneriID/{VaneriID:int}")] //  Routeen voidaan myös määrittää joku arvo esim. VaneriID ja määritetään että se on int.
        [HttpGet] //Määritetään mikä komento on kyseessä. tässä tapauksessa GET
        public List<string> HaeVaneriID(int VaneriID) // Silloin myös tähän pitää hakea VaneriID
        {
            List<string> output = new List<string>(); //Luodaan tulostettava lista
            output.Add("Koko: ");
            output.Add(vaneri[0].Koko);

            return output;

        }

    }
}
