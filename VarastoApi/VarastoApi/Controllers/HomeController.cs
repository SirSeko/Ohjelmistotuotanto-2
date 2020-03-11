
using System.Net;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json.Linq;

using System.Diagnostics;
using System.Collections.Generic;
using System.Data.SqlClient;
using VarastoApi.Backend;
using System;

namespace VarastoApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Materiaali> materiaalit = new List<Materiaali>(); //Lista materiaaleista
            List<MateriaaliKoonti> mk = new List<MateriaaliKoonti>(); //Lista materiaalikoonnista
           

            List<Tilaus> tilaukset = new List<Tilaus>(); //Lista tilauksista
            List<Varaus> varaukset = new List<Varaus>();
            SqlConnection cnn; //tietokantayhteys-olio, jaetaan tämä muualle
            DatabaseManager dm = new DatabaseManager(); //tietokannanhallinta-olio, mm. yhdistys kantaan

            cnn = dm.OpenConnection(); //yhdistetään kantaan
            if (cnn != null)
            {
                DatabaseMateriaali dmMat = new DatabaseMateriaali(cnn); //luodaan olio, jolla käsitellään materiaaleja tietokannassa
                materiaalit = dmMat.SelectAll(materiaalit); //haetaan kaikki materiaalit kannasta listaan

                // !! TÄYTYY SAADA LISTA TAKAISIN OBJEKTIKSI !! 

                /*
                 * @model List<VarastoApi.Backend.Materiaali>
                //Luodaan API kutsu
                WebRequest request = WebRequest.Create("https://pokeapi.co/api/v2/pokemon/1/");
                //Lähetä API kutsu
                WebResponse response = request.GetResponse();
                //Saa takaisin vastaus streami
                Stream stream = response.GetResponseStream();
                //Lukee streamin
                StreamReader reader = new StreamReader(stream);
                //Luetaan stringiin
                string responseFromServer = reader.ReadToEnd();

                //Muutaan Json objektiksi joka on paremmin luettavissa
                JObject parsedString = JObject.Parse(responseFromServer);
                Pokemon myPokemon = parsedString.ToObject<Pokemon>();

                Debug.WriteLine(myPokemon.moves[0].move.name);

                return View(myPokemon);
                */
            }
            return View(mk);
        }
    }
}
