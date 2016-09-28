using ClonestoneMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class AdministrationController : Controller
    {
        [HttpGet]
        public string verifyLogin()
        {
            return null;
        }

        [HttpPost]
        public string verifyLogin(string email, string password)
        {
            try
            {
                using(ClonestoneEntities cont = new ClonestoneEntities())
                {
                    var res = (from l in cont.tbllogins
                               where l.email == email && l.passcode == password
                               select l).FirstOrDefault();

                    if(res != null)
                    {
                        return "OK";
                    }
                }
            }
            catch(Exception e)
            {
              //TODO - Fehlerbehandlung
              //Fehlerbehandlung
            }
            finally
            {
              //TODO - Fehlerbehandlung
              //Aufräumarbeiten
            }

            //Ergebnis zurückschicken
            return "email or password WRONG!";
        }
    }
}