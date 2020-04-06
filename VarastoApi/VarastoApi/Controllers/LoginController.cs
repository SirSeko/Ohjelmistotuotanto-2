using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarastoApi.Backend;
using System.Data.SqlClient;

namespace VarastoApi.Controllers {
    public class LoginController : Controller {
        DatabaseManager dbMan;
        SqlConnection cnn;
        DatabaseKayttaja dbKay;
        // GET: Login
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection form) { //Kirjautuminen
            string kayttajanimi = form[string.Format("kayttajanimi")];
            string salasana = form[string.Format("salasana")];
            if (ModelState.IsValid) { //En ole varma mitä tekee
                if (SQLFilter.checkInput(kayttajanimi) && SQLFilter.checkInput(salasana)) { //Tarkistetaan käyttäjän syöte SQL varten
                    dbMan = new DatabaseManager();
                    cnn = dbMan.OpenConnection();
                    dbKay = new DatabaseKayttaja(cnn);
                    //string hash = PBKDF2Hash.HashPasswordUsingPBKDF2(salasana);
                    string hash = salasana;
                    Kayttaja k = dbKay.SelectKayttaja(kayttajanimi, hash);
                    dbMan.CloseConnection();
                    if (k != null) {
                        //Session["UserID"] = k.UserId.ToString();         --Tarvitseeko? ehkä, ehkä ei
                        Session["Kayttajanimi"] = k.Kayttajanimi;
                        Session["Valtuus"] = k.Valtuus.ToString();  //Tällä voinee lukita sisältöä
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Login");
        }
    }
}