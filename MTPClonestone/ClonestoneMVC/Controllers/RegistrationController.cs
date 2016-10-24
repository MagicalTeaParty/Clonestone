using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClonestoneMVC.Models;
using System.Text;
using System.Security.Cryptography;

namespace ClonestoneMVC.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserData user)
        {

            string hashpass = getHashSha512(user.Password);

            try
            {
                using (ClonestoneEntities cont = new ClonestoneEntities())
                {
                    // Variante via Stored Procedure

                    int manipulierteDatensaetze = cont.pInsertUser(user.Firstname, user.Lastname, user.Gamertag, user.Email, hashpass);
                    
                    // Variante entity Framework

                    //tbllogin login = new tbllogin();
                    //tblrole rolle;
                    //tblperson person = new tblperson();

                    //login.email = user.Email;
                    //login.passcode = hashpass;

                    //person.firstname = user.Firstname;
                    //person.lastname = user.Lastname;
                    //person.gamertag = user.Gamertag;

                    //rolle = (from x in cont.tblroles
                    //         where x.idrole == 1
                    //         select x).FirstOrDefault();

                    //person.tbllogin = login;

                    //person.tblroles.Add(rolle);

                    //cont.tblpersons.Add(person);
                    //cont.tbllogins.Add(login);

                    //cont.SaveChanges();

                    if (manipulierteDatensaetze>0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception e)
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
            //FEHLER
            return null;

        }

        /// <summary>
        /// Hashmethode die auch im Clienten verwendet wird.
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string getHashSha512(string pass)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            SHA512Managed hashstring = new SHA512Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }



        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
