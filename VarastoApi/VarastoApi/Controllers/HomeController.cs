
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
        public ActionResult Edit(int id)  //Edit napilla palauttaa LISTAN???????????? edit.cshtml:ään
        {
            vanerit = matko.Vanerit;
            
            foreach (Vaneri v in vanerit) {
                if (v.Id == id) {
                    List<Vaneri> PerkeleenPaskaListaHelvettiin = new List<Vaneri>();
                    PerkeleenPaskaListaHelvettiin.Add(v);
                    return PartialView(PerkeleenPaskaListaHelvettiin);
                }
            }
            return PartialView(null); //nullll
        }





        [System.Web.Mvc.HttpPost]
        public ActionResult EditInfo(FormCollection form)
        {
            Vaneri van = new Vaneri();
            int i = 0;
            while (form["Tid"] != null) {



                van.Id = Convert.ToInt32(form[String.Format("Tid", i)]);
                van.Koko = form[String.Format("TKoko", i)];
                van.Hinta = 500;
                van.Kauppa = "Paska";
                van.Lisatiedot = "Tämä on Posti";
                van.Sijainti = 1;
                van.Yksikko = "cm";
                van.Maara = 666;
                break;
               
            
            };
            DatabaseVaneri dmVan = new DatabaseVaneri(cnn);
            
            dmVan.Update(van);
            /*
           
            //https://www.c-sharpcorner.com/UploadFile/3d39b4/getting-data-from-view-to-controller-in-mvc/ */
            return RedirectToAction("Index");
        }
    }
}
