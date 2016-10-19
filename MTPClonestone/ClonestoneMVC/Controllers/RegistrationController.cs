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
        //private ClonestoneEntities db = new ClonestoneEntities();


        //// GET: Registration/Create
        //public ActionResult Create()
        //{
        //    ViewBag.idperson = new SelectList(db.tbllogins, "idlogin", "email");
        //    return View();
        //}

        //// POST: Registration/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idperson,firstname,lastname,gamertag")] tblperson tblperson)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tblpersons.Add(tblperson);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.idperson = new SelectList(db.tbllogins, "idlogin", "email", tblperson.idperson);
        //    return View(tblperson);
        //}


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string firstname, string lastname, string gamertag, string email, string passcode)
        {
            string hashpass = getHashSha512(passcode);

            try
            {
                using (ClonestoneEntities cont = new ClonestoneEntities())
                {
                    tblperson insert = new tblperson();
                    tbllogin ins = new tbllogin();

                    insert.firstname = firstname;
                    insert.lastname = lastname;
                    insert.gamertag = gamertag;
                    ins.email = email;
                    ins.passcode = hashpass;

                    cont.tblpersons.Add(insert);
                    cont.tbllogins.Add(ins);
                    cont.SaveChanges();



                    if (true)
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
