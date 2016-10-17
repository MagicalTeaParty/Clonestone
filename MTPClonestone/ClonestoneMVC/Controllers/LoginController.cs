using ClonestoneMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class LoginController : Controller
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
                    //var res = (from l in cont.tbllogins
                    //           where l.email == email && l.passcode == password
                    //           select l).FirstOrDefault();

                    var gt = (from t in cont.tblpersons
                              join s in cont.tbllogins
                              on t.idperson equals s.idlogin
                              where s.email == email && s.passcode == password
                              select new { Id = t.idperson, Gamertag = t.gamertag }).FirstOrDefault();
                    
                    if (gt != null)
                    {
                        return gt.Id + "|" + gt.Gamertag;
                        //return Json(gt);
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
            //return "email or password WRONG!";
            return null;
        }
    }
}