
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using VarastoApi.Backend;
using System;

using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using System.Text;

namespace VarastoApi.Controllers {


    public class HomeController : Controller {
        DatabaseManager dbMan = new DatabaseManager();
        DatabaseVaneri dbVan;
        DatabaseMaali dbMaa;
        DatabaseLauta dbLau;

        SqlConnection cnn; //tietokantayhteys-olio, jaetaan tämä muualle
        MateriaaliKoonti matko = new MateriaaliKoonti();

        public ActionResult Index() {
            if (Session["Kayttajanimi"] != null) {
                matko.Initiate();
                return View(matko);
            } else {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult Tilaukset() {
            return View();
        }
        public ActionResult Asetukset() {
            return View();
        }
        public ActionResult Ohjeet() {
            return View();
        }



        [HttpGet]
        public ActionResult Edit(int id)  //Edit napilla palauttaa materiaalin
        {
            cnn = dbMan.OpenConnection(); //avataan yhteys
            string sid = id.ToString(); //int to string
            string eka = sid.Substring(0, 1); //id:n eka numero määrittää mikä tuote on kyseessä

            switch (eka) //Tarkistetaan mikä se on.
            {
                case "1":
                    //Haetaan tiedot tietokannasta
                    dbVan = new DatabaseVaneri(cnn);
                    Vaneri v = dbVan.SelectId(id);
                    dbMan.CloseConnection();
                    return PartialView(v);

                case "4":
                    dbMaa = new DatabaseMaali(cnn);
                    Maali maa = dbMaa.SelectId(id);
                    dbMan.CloseConnection();
                    return PartialView(maa);

                case "2":
                    dbLau = new DatabaseLauta(cnn);
                    Lauta lau = dbLau.SelectId(id); //Select id puuttuu lautadatabasesta
                    dbMan.CloseConnection();
                    return PartialView(lau);

            };
            return RedirectToAction("Index");

        }


        [HttpPost] // Muokataan tietuetta
        public ActionResult EditInfo(FormCollection form) //Tuodaan formin tiedot
        {

            //Haetaan id formista
            string sid = form[String.Format("Tid")];
            if (form["Tid"] != null) { //Mikäli ei null niin jatketaan

                if (!int.TryParse(form[String.Format("Tid")], out int id)) {
                    ExceptionController.WriteException(this, "VaneriID muunnossa integeriksi virhe.");
                    return RedirectToAction("Index");
                }
                string Koko = form[String.Format("TKoko")];
                if (!SQLFilter.checkInput(Koko)) {
                    ExceptionController.WriteException(this, "VaneriKoko ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                if (!float.TryParse(form[String.Format("THinta")], out float Hinta)) {
                    ExceptionController.WriteException(this, "VaneriHinta muunnossa floatiksi virhe.");
                    return RedirectToAction("Index");
                }
                string Kauppa = form[String.Format("TKauppa")];
                if (!SQLFilter.checkInput(Kauppa)) {
                    ExceptionController.WriteException(this, "VaneriKauppa ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                string Lisatiedot = form[String.Format("TLisatiedot")];
                if (!SQLFilter.checkInput(Lisatiedot)) {
                    ExceptionController.WriteException(this, "VaneriKauppa ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                if (!int.TryParse(form[String.Format("TSijainti")], out int sijainti)) {
                    ExceptionController.WriteException(this, "VaneriSijainti muunnossa integeriksi virhe.");
                    return RedirectToAction("Index");
                }
                string valittu = Request.Form["TYksikko"].ToString();
                if (!SQLFilter.checkInput(valittu)) {
                    ExceptionController.WriteException(this, "VaneriYksikko ei läpäissyt SQLFilteriä.");
                    return RedirectToAction("Index");
                }
                if (!int.TryParse(form[String.Format("TMaara")], out int Maara)) {
                    ExceptionController.WriteException(this, "VaneriMaara muunnossa integeriksi virhe.");
                    return RedirectToAction("Index");
                }

                string kutsu = "muokkaus"; //Muokkaus kutsu switchille
                switchi(kutsu, id, Koko, Hinta, Maara, valittu, sijainti, Kauppa, Lisatiedot); //Kutsutaan switchia ja viedään tiedot sinne.
                dbMan.CloseConnection();

            };

            //Palataan Indexiin
            return RedirectToAction("Index");
        }


        [HttpGet] // Poistetaan tietueet tällä Getillä  
        public ActionResult Delete(int id) { //Tuodaan tietueen id 
            string kutsu = "poisto";
            // Annetaan switchille tietoja koska se haluaa. Ainoa tärkeä tieto on kutsu ja id.
            switchi(kutsu, id, "a", 1, 1, "a", 1, "a", "a");
            dbMan.CloseConnection();
            return RedirectToAction("Index");

        }


        [HttpGet] //Esimerkki modulaarisesta sivun rakennuksesta
        public ActionResult Vanerit() {
            Models.Vanerit v = new Models.Vanerit();
            return View(v);
        }


        [HttpPost] // Muokataan tietuetta
        public ActionResult AddNew(FormCollection form) {

            //Haetaan tyyppi id. eli ensimmäinen numero joka määrittää mikä materiaali kyseessä
            int.TryParse(form[String.Format("tyyppi")], out int id);
            //Käydään läpi kaikki tiedot ja kirjoitetaan exception jos ongelmia
            string Koko = form[String.Format("koko")];
            if (!SQLFilter.checkInput(Koko)) {
                ExceptionController.WriteException(this, "VaneriKoko ei läpäissyt SQLFilteriä.");
                return RedirectToAction("Index");
            }
            if (!float.TryParse(form[String.Format("hinta")], out float Hinta)) {
                ExceptionController.WriteException(this, "VaneriHinta muunnossa floatiksi virhe.");
                return RedirectToAction("Index");
            }
            string Kauppa = form[String.Format("kauppa")];
            if (!SQLFilter.checkInput(Kauppa)) {
                ExceptionController.WriteException(this, "VaneriKauppa ei läpäissyt SQLFilteriä.");
                return RedirectToAction("Index");
            }
            string Lisatiedot = form[String.Format("komm")];
            if (!SQLFilter.checkInput(Lisatiedot)) {
                ExceptionController.WriteException(this, "VaneriLisatiedot ei läpäissyt SQLFilteriä.");
                return RedirectToAction("Index");
            }
            if (!int.TryParse(form[String.Format("sijainti")], out int Sijainti)) {
                ExceptionController.WriteException(this, "VaneriSijainti muunnossa integeriksi virhe.");
                return RedirectToAction("Index");
            }
            string Yksikko = Request.Form["yksikko"].ToString();
            if (!SQLFilter.checkInput(Yksikko)) {
                ExceptionController.WriteException(this, "VaneriYksikko ei läpäissyt SQLFilteriä.");
                return RedirectToAction("Index");
            }

            if (!int.TryParse(form[String.Format("maara")], out int Maara)) {
                ExceptionController.WriteException(this, "VaneriMaara muunnossa integeriksi virhe.");
                return RedirectToAction("Index");
            }
            //Kertoo switchille mikä kutsu on kyseessä
            string kutsu = "uusi";
            switchi(kutsu, id, Koko, Hinta, Maara, Yksikko, Sijainti, Kauppa, Lisatiedot); //Kutsutaan switchiä joka tekee loput hommasta
            dbMan.CloseConnection(); //suljetaan yhteys
            return RedirectToAction("Index"); // palataan


        }

        // Tämä tutkii tiedot ja päättää onko kyseessä lisäys, muokkaus vai poisto // Private huvin vuoksi..
        private bool switchi(string kutsu, int id, string Koko, float Hinta, int Maara, string Yksikko, int Sijainti, string Kauppa, string Lisatiedot) {
            string sid = id.ToString(); // Muutetaan id stringiksi koska haluamme vain ensimmäisen numeron joka kertoo materiaalin tyypin
            string tyyppi = sid.Substring(0, 1);

            cnn = dbMan.OpenConnection(); //Avataan yhteys
            //Tarkistetaan mikä materiaali on kyseesssä
            switch (tyyppi) //Tässä tarkastellaan mikä materiaali
            {
                case "1":
                    DatabaseVaneri dmVan = new DatabaseVaneri(cnn);

                    if (kutsu == "uusi") {  //Jos uusi niin luodaan uusi
                        dmVan.InsertInto(Vaneri.Create(id, Koko, Hinta, Maara, Yksikko, Sijainti, Kauppa, Lisatiedot));
                    } else if (kutsu == "muokkaus") //Jos muokkaus niin muokataan
                      {
                        dmVan.Update(Vaneri.Create(id, Koko, Hinta, Maara, Yksikko, Sijainti, Kauppa, Lisatiedot));
                    } else if (kutsu == "poista") //Jos poisto niin poistetaan
                      {
                        dmVan.Delete(id);
                    }
                    return true; //Palauteaan true jos homma ok.

                case "4":
                    DatabaseMaali dmMaa = new DatabaseMaali(cnn);
                    if (kutsu == "uusi") {
                        dmMaa.InsertInto(Maali.Create(id, Koko, Hinta, Maara, Yksikko, Sijainti, Kauppa, Lisatiedot));
                    } else if (kutsu == "muokkaus") {
                        dmMaa.Update(Maali.Create(id, Koko, Hinta, Maara, Yksikko, Sijainti, Kauppa, Lisatiedot));
                    } else if (kutsu == "poisto") {
                        dmMaa.Delete(id);
                    }
                    return true;
                case "2":
                    DatabaseLauta dmLau = new DatabaseLauta(cnn);
                    if (kutsu == "uusi") {
                        dmLau.InsertInto(Lauta.Create(id, Koko, Hinta, Maara, Yksikko, Sijainti, Kauppa, Lisatiedot));
                    } else if (kutsu == "muokkaus") {
                        dmLau.Update(Lauta.Create(id, Koko, Hinta, Maara, Yksikko, Sijainti, Kauppa, Lisatiedot));
                    } else if (kutsu == "poisto") {
                        dmLau.Delete(id);
                    }
                    return true;

            };
            return false; //Jos tyyppi ei täsmää palautetaan false
        }


        //Rekisteröinti
        DatabaseKayttaja dbKay;
        public ActionResult Register(FormCollection form) { //Kirjautuminen
            string kayttajanimi = form[string.Format("kayttajanimi")];
            string salasana = form[string.Format("salasana")];
            int valtuus;
            if (!int.TryParse(form[string.Format("valtuus")], out valtuus)) {
                return View("Kayttajat");
            }
            if (SQLFilter.checkInput(kayttajanimi) && SQLFilter.checkInput(salasana)) { //Tarkistetaan käyttäjän syöte SQL varten
                dbMan = new DatabaseManager();
                cnn = dbMan.OpenConnection();
                dbKay = new DatabaseKayttaja(cnn);
                //string hash = PBKDF2Hash.HashPasswordUsingPBKDF2(salasana);
                string hash = salasana;
                Kayttaja k = Kayttaja.Create(kayttajanimi, valtuus);
                dbKay.InsertInto(k, hash);
            }
            return RedirectToAction("Kayttajat");
        }

        public ActionResult Kayttajat() {
            Models.Kayttajat k = new Models.Kayttajat();
            return View(k);
        }
    }

}  
