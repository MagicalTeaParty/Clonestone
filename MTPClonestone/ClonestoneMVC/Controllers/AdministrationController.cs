using ClonestoneMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class AdministrationController : Controller
    {
        [HttpGet]
        public string Save()
        {
            return null;
        }

        [HttpPost]
        public string Save(string email, string password)
        {
            try
            {

                ///TODO Vergleich email pw mit DB
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
              //Fehlerbehandlung
            }
            finally
            {
              //Aufräumarbeiten
            }

            //Ergebnis zurückschicken
            return "email or password WRONG!";
        }
    }
}