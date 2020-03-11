using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarastoApi.Backend;

namespace VarastoApi.Controllers
{
    public class VarausController : Controller
    {
        public ActionResult Varaus()
        {

            List<Varaus> varaukset = new List<Varaus>();
            SqlConnection cnn; //tietokantayhteys-olio, jaetaan tämä muualle
            DatabaseManager dm = new DatabaseManager(); //tietokannanhallinta-olio, mm. yhdistys kantaan

            cnn = dm.OpenConnection(); //yhdistetään kantaan
            if (cnn != null)
            {
                DatabaseVaraus dmMat = new DatabaseVaraus(cnn); //luodaan olio, jolla käsitellään materiaaleja tietokannassa
                varaukset = dmMat.SelectAll(varaukset); //haetaan kaikki materiaalit kannasta listaan


            }

            return View(varaukset);
        }
    }
}
