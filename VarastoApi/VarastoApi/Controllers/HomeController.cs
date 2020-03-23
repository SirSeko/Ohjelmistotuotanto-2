
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
        DatabaseManager dbMan;
        DatabaseVaneri dbVan;
        List<Vaneri> vanerit = new List<Vaneri>();
        SqlConnection cnn; //tietokantayhteys-olio, jaetaan tämä muualle
        MateriaaliKoonti matko = new MateriaaliKoonti();

        public ActionResult Index()
        {
            matko.Initiate();
            return View(matko);

        }
        //TÄMÄ ON TESTAILUA



        [HttpGet]
        public ActionResult Edit(int id)  //Edit napilla palauttaa vanerin
        {
            dbMan = new DatabaseManager();
            cnn = dbMan.OpenConnection();
            dbVan = new DatabaseVaneri(cnn);
            Vaneri v = dbVan.SelectId(id);
            dbMan.CloseConnection();
            return PartialView(v);
        }





        [System.Web.Mvc.HttpPost] // Muokataan tietuetta
        public ActionResult EditInfo(FormCollection form)
        {
            //luodaan uusi vaneri olio
            Vaneri van = new Vaneri();
            int i = 0;
            if (form["Tid"] != null) {

                //Haetaan formista tiedot olioon jos id ei ole null 

                if (!int.TryParse(form[String.Format("Tid")], out van.Id)) {
                    ExceptionController.WriteException(this, "VaneriID muunnossa integeriksi virhe.");
                    return RedirectToAction("Index");
                }
                van.Koko = form[String.Format("TKoko")];
                if (!SQLFilter.checkInput(van.Koko)) {
                    ExceptionController.WriteException(this, "VaneriKoko ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                if (!float.TryParse(form[String.Format("THinta")], out van.Hinta)) {
                    ExceptionController.WriteException(this, "VaneriHinta muunnossa floatiksi virhe.");
                    return RedirectToAction("Index");
                }
                van.Kauppa = form[String.Format("TYksikko")];
                if (!SQLFilter.checkInput(van.Kauppa)) {
                    ExceptionController.WriteException(this, "VaneriKauppa ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                van.Lisatiedot = form[String.Format("TLisatiedot")];
                if (!SQLFilter.checkInput(van.Lisatiedot)) {
                    ExceptionController.WriteException(this, "VaneriKauppa ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                if (!int.TryParse(form[String.Format("TSijainti")], out van.Sijainti)) {
                    ExceptionController.WriteException(this, "VaneriSijainti muunnossa integeriksi virhe.");
                    return RedirectToAction("Index");
                }
                string valittu = Request.Form["TYksikko"].ToString();
                if (!SQLFilter.checkInput(valittu)) {
                    ExceptionController.WriteException(this, "VaneriYksikko ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                van.Yksikko = valittu; //onko jokin syy, että on aiemmin string valittu ja nyt laitetaan vanerin attribuutiksi?

                if (!int.TryParse(form[String.Format("TMaara")], out van.Maara)) {
                    ExceptionController.WriteException(this, "VaneriMaara muunnossa integeriksi virhe.");
                    return RedirectToAction("Index");
                }


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


        [HttpGet] //Esimerkki modulaarisesta sivun rakennuksesta
        public ActionResult Vanerit() {
            Models.Vanerit v = new Models.Vanerit();
            return View(v);
        }


        [HttpGet]
        public ActionResult AddNew()
        {
            return PartialView();


        }

        public object GetValinta(string tyyppi)
        {
            
            switch (tyyppi) //Luodaan asianmukainen olio
            {
                case "1":
                    {
                        Vaneri obj = new Vaneri();
                        return obj;
                    }


                case "2":
                    {
                        Maali obj = new Maali();
                        return obj;
                    }


                case "3":
                    {
                        Lauta obj = new Lauta();
                        return obj;
                    }


            };
            return null;

           
        }
      
        [System.Web.Mvc.HttpPost] // Muokataan tietuetta
        public ActionResult AddNew(FormCollection form)
        {
            //Tarkistetaan mikä materiaali on kyseesssä
            string tyyppi = form[String.Format("tyyppi")];
            object obj = GetValinta(tyyppi);
           
            //Haetaan formista tiedot olioon jos id ei ole null 
            

            string Koko = form[String.Format("koko")];
                if (!SQLFilter.checkInput(Koko))
                {
                    ExceptionController.WriteException(this, "VaneriKoko ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                if (!float.TryParse(form[String.Format("hinta")], out float Hinta))
                {
                    ExceptionController.WriteException(this, "VaneriHinta muunnossa floatiksi virhe.");
                    return RedirectToAction("Index");
                }
                string Kauppa = form[String.Format("kauppa")];
                if (!SQLFilter.checkInput(Kauppa))
                {
                    ExceptionController.WriteException(this, "VaneriKauppa ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                string Lisatiedot = form[String.Format("komm")];
                if (!SQLFilter.checkInput(Lisatiedot))
                {
                    ExceptionController.WriteException(this, "VaneriLisatiedot ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                if (!int.TryParse(form[String.Format("sijainti")], out int Sijainti))
                {
                    ExceptionController.WriteException(this, "VaneriSijainti muunnossa integeriksi virhe.");
                    return RedirectToAction("Index");
                }
                string valittu = Request.Form["yksikko"].ToString();
                if (!SQLFilter.checkInput(valittu))
                {
                    ExceptionController.WriteException(this, "VaneriYksikko ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                //van.Yksikko = valittu; //onko jokin syy, että on aiemmin string valittu ja nyt laitetaan vanerin attribuutiksi?

                if (!int.TryParse(form[String.Format("maara")], out int Maara))
                {
                    ExceptionController.WriteException(this, "VaneriMaara muunnossa integeriksi virhe.");
                    return RedirectToAction("Index");
                }

            obj.Koko = Koko;
            
        

        
            
            DatabaseManager mm = new DatabaseManager();
            cnn = mm.OpenConnection();    //Avataan yhteys

          
            Vaneri van = new Vaneri();
            DatabaseVaneri dmVan = new DatabaseVaneri(cnn);
            dmVan.InsertInto(van);
            mm.CloseConnection(); //SUljetaan yhteys

         



            //Palataan Indexiin
            return RedirectToAction("Index");
            
        }

        }
}
