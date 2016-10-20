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
        public ActionResult Create(UserRegistration user)
        {
            string hashpass = getHashSha512(user.Password);

            try
            {
                using (ClonestoneEntities cont = new ClonestoneEntities())
                {
                    tblperson insert = new tblperson();
                    tbllogin ins = new tbllogin();

                    insert.firstname = user.Firstname;
                    insert.lastname = user.Lastname;
                    insert.gamertag = user.Gamertag;
                    ins.email = user.Email;
                    ins.passcode = hashpass;

                    cont.tblpersons.Add(insert);
                    cont.tbllogins.Add(ins);
                    cont.SaveChanges();

                    var role = (from t in cont.tblpersons
                                where t.gamertag == user.Gamertag
                                select t.idperson).FirstOrDefault();

                    



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
