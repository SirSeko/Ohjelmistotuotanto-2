using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace VarastoApi.Backend {

    //Käsittelee KAIKKI virheet, jos tarvitsee muokata, muokkaa
    public static class ExceptionController {
        static string path = "C:\\temp\\VarastoApi\\ExceptionLog.txt"; //Virhelokin tiedostosijainti

        /// <summary>
        /// Kirjoittaa virheet tiedoston
        /// </summary>
        /// <param name="caller">Kutsujaluokka(this)</param>
        /// <param name="message">Virheviesti</param>
        public static void WriteException(object caller, string message) {
            try {
                Directory.CreateDirectory("C:\\temp\\VarastoApi");
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(DateTime.Now + " - " + caller.GetType().ToString() + " - " + message);
                sw.Close();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}