
using System.Net;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json.Linq;

using System.Diagnostics;
using System.Collections.Generic;
using System.Data.SqlClient;
using VarastoApi.Backend;
using System;
using System.Net.Http;

namespace VarastoApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Materiaali> materiaalit = new List<Materiaali>(); //Lista materiaaleista

            //List<MateriaaliKoonti> mk = new List<MateriaaliKoonti>(); //Lista materiaalikoonnista
            var mk = new MateriaaliKoonti(); //Luodaan koonti-olio. Listan tuominen aiheutti herjan.

            List<Vaneri> vaneri = new List<Vaneri>();
            List<Maali> maali = new List<Maali>();

            List<Tilaus> tilaukset = new List<Tilaus>(); //Lista tilauksista
            List<Varaus> varaukset = new List<Varaus>();
            SqlConnection cnn; //tietokantayhteys-olio, jaetaan tämä muualle
            DatabaseManager dm = new DatabaseManager(); //tietokannanhallinta-olio, mm. yhdistys kantaan

            cnn = dm.OpenConnection(); //yhdistetään kantaan
            if (cnn != null)
            {
                DatabaseVaneri dmVan = new DatabaseVaneri(cnn); //luodaan olio, jolla käsitellään materiaaleja tietokannassa
                vaneri = dmVan.SelectAll(vaneri); //haetaan kaikki materiaalit kannasta listaan
                DatabaseMaali dmMaa = new DatabaseMaali(cnn); // sama maaleille
                maali = dmMaa.SelectAll(maali);
                mk.Vanerit = vaneri; //Lisätään vanerit koontilistaan
                mk.Maalit = maali; //Lisätään maalit koontilistaan

                // SAMALLA PERIAATTEELLA VOI LISÄTÄ MUUT MATERIAALIT //



                


            }
            return View(mk);

        }
        // TÄMÄ ON TESTAILUA 
        public ActionResult Vaneri()
        {
            IEnumerable<VarastoApi.Backend.Vaneri> vanerit = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54434/api/");
                //HTTP GET
                var responseTask = client.GetAsync("materiaali");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<IList<VarastoApi.Backend.Vaneri>>();
                    readTask.Wait();

                    vanerit = readTask.Result;
                }
            }
            
            return View(vanerit);
        }

    }
}
