
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
using Newtonsoft.Json;
using System.Linq;
using System.Collections;
using System.Web.Http;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;

namespace VarastoApi.Controllers

{


    public class HomeController : Controller
    {

        List<Vaneri> vanerit = new List<Vaneri>();
        SqlConnection cnn; //tietokantayhteys-olio, jaetaan tämä muualle
        MateriaaliKoonti matko = new MateriaaliKoonti();

        public ActionResult Index()
        {


            //List<MateriaaliKoonti> mk = new List<MateriaaliKoonti>(); //Lista materiaalikoonnista
            var mk = new MateriaaliKoonti(); //Luodaan koonti-olio. Listan tuominen aiheutti herjan.

            List<Maali> maali = new List<Maali>();
            

            DatabaseManager dm = new DatabaseManager(); //tietokannanhallinta-olio, mm. yhdistys kantaan

            cnn = dm.OpenConnection(); //yhdistetään kantaan
            if (cnn != null)
            {
                DatabaseVaneri dmVan = new DatabaseVaneri(cnn); //luodaan olio, jolla käsitellään materiaaleja tietokannassa
                vanerit = dmVan.SelectAll(vanerit); //haetaan kaikki materiaalit kannasta listaan
                DatabaseMaali dmMaa = new DatabaseMaali(cnn); // sama maaleille
                maali = dmMaa.SelectAll(maali);
                mk.Vanerit = vanerit; //Lisätään vanerit koontilistaan
                mk.Maalit = maali; //Lisätään maalit koontilistaan

                // SAMALLA PERIAATTEELLA VOI LISÄTÄ MUUT MATERIAALIT //






            }
            return View(mk);

        }
        //TÄMÄ ON TESTAILUA



        [HttpGet]
        public ActionResult Edit(int id)  //Edit napilla palauttaa vanerin
        {
            vanerit = matko.Vanerit;
            
            foreach (Vaneri v in vanerit) {
                if (v.Id == id) {
                    return PartialView(v);
                }
            }
            return PartialView(null); //nullll
        }





        [System.Web.Mvc.HttpPost] // Muokataan tietuetta
        public ActionResult EditInfo(FormCollection form)
        {
            //luodaan uusi vaneri olio
            Vaneri van = new Vaneri();
            int i = 0;
            if (form["Tid"] != null) {

                //Haetaan formista tiedot olioon jos id ei ole null 

                van.Id = Convert.ToInt32(form[String.Format("Tid")]);
                van.Koko = form[String.Format("TKoko")];
                van.Hinta = float.Parse(form[String.Format("THinta")]);
                van.Kauppa = form[String.Format("TYksikko")];
                van.Lisatiedot = form[String.Format("TLisatiedot")];
                van.Sijainti = Convert.ToInt32(form[String.Format("TSijainti")]);
                string valittu = Request.Form["TYksikko"].ToString();
                van.Yksikko = valittu;
                van.Maara = Convert.ToInt32(form[String.Format("TMaara")]);



            };
            //Avataan yhteys
            DatabaseManager mm = new DatabaseManager();
            cnn = mm.OpenConnection();
            DatabaseVaneri dmVan = new DatabaseVaneri(cnn);
            //Pusketaan tiedot
            dmVan.Update(van);
            mm.CloseConnection();
            
           
            //Palataan Indexiin
            return RedirectToAction("Index");
        }

        [HttpGet] // Poistetaan tietueet tällä Getillä
        public ActionResult Delete(int id) {

            // Otetaan yhteys tietokantaan
            DatabaseManager mm = new DatabaseManager();
            cnn = mm.OpenConnection();
            DatabaseVaneri dmVan = new DatabaseVaneri(cnn);
            //Poistetaan tietue
            dmVan.Delete(id);
            mm.CloseConnection();
           
           
          //Palataan indexiin
            return RedirectToAction("Index");
        }




    }
}
