using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VarastoApi.Backend;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

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

        
        

      

        // POST: api/Materiaali esimerkki
        public void Post([FromBody]string value)
        {

        }

        // DELETE: api/Materiaali/5 esimerkki
        public void Delete(int id)
        {
        }

        //Voidaan määrittää route (osoite) eri komennoille esim. jos halutaan vain lisätiedot
        //[Route("api/Materiaali/VaneriTiedot")] //Kun käyttäjä painaa linkkiä johon tämä osoite on määritelty, osaa sivu automaagisesti ohjata tähän.
        //[HttpGet] //Määritetään mikä komento on kyseessä. tässä tapauksessa GET
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
        //[Route("Home/MateriaaliMuokkaus/{VaneriID:int}")] //  Routeen voidaan myös määrittää joku arvo esim. VaneriID ja määritetään että se on int.
        //[HttpGet] //Määritetään mikä komento on kyseessä. tässä tapauksessa GET
        public object HaeVaneriID(int VaneriID) // Silloin myös tähän pitää hakea VaneriID
        {
            //IEnumerable<VarastoApi.Backend.Vaneri> output = null;  //Luodaan tulostettava lista
            List<Vaneri> seppo = new List<Vaneri>().ToList();

            vaneri.ToList();
            var result = ((IEnumerable)vaneri).Cast<object>().ToList();
            result.Clear();
            int van = vaneri.FindIndex(x => x.Id == VaneriID);
            //pertti = vaneri.Where(x => x.Id == van);
            result.Add(vaneri.Where(p => p.Id == VaneriID).Select(p => p.Id));
            //result.Add(vaneri.Where(x => x.Id == VaneriID));
            //seppo.Add(seppo);
           

            return result; //("_PartialMateriaali", output);
            
        }
        
       

    }
}
