﻿using ClonestoneMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

                    var gt = (from t in cont.tblpersons
                              join s in cont.tbllogins
                              on t.idperson equals s.idlogin
                              where s.email == email && s.passcode == password
                              select new { Id = t.idperson, Gamertag = t.gamertag }).FirstOrDefault();
                    
                    if (gt != null)
                    {
                        return gt.Id + "|" + gt.Gamertag;
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

        // GET: PasswordForgotten
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string passcode)
        {
            string hashpass = getHashSha512(passcode);

            try
            {
                using (ClonestoneEntities cont = new ClonestoneEntities())
                {

                    var gt = (from t in cont.tblpersons
                              join s in cont.tbllogins
                              on t.idperson equals s.idlogin
                              where s.email == email && s.passcode == hashpass
                              select new { Id = t.idperson, Gamertag = t.gamertag }).FirstOrDefault();

                    if (gt != null)
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
            return null;

        }

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
    }
}