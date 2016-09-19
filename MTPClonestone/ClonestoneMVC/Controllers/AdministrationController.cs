using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class AdministrationController : Controller
    {
        [HttpPost]
        public string Save(string email, string password)
        {

            ///TODO Vergleich email pw mit DB

            //Ergebnis zurückschicken
            return "OK";
        }
    }
}