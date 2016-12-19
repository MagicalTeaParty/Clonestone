using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClonestoneMVC.Models;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClonestoneMVC.Controllers
{
    public class ProfileController : Controller
    {
        private ClonestoneEntities db = new ClonestoneEntities();

        // GET: Profile
        public ActionResult Index()
        {
            var tbllogins = db.tbllogins.Include(t => t.tblperson);
            return View(tbllogins.ToList());
        }

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbllogin tbllogin = db.tbllogins.Find(id);
            if (tbllogin == null)
            {
                return HttpNotFound();
            }
            return View(tbllogin);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            ViewBag.idlogin = new SelectList(db.tblpersons, "idperson", "firstname");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idlogin,email,passcode")] tbllogin tbllogin)
        {
            if (ModelState.IsValid)
            {
                db.tbllogins.Add(tbllogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idlogin = new SelectList(db.tblpersons, "idperson", "firstname", tbllogin.idlogin);
            return View(tbllogin);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbllogin tbllogin = db.tbllogins.Find(id);
            if (tbllogin == null)
            {
                return HttpNotFound();
            }
            ViewBag.idlogin = new SelectList(db.tblpersons, "idperson", "firstname", tbllogin.idlogin);
            ViewBag.Firstname = tbllogin.tblperson.firstname;
            ViewBag.Lastname = tbllogin.tblperson.lastname;
            ViewBag.Gamertag = tbllogin.tblperson.gamertag;
            return View(tbllogin);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idlogin,email,passcode")] tbllogin tbllogin, string newemail1, string newemail2 ) //2 pass felder!!!
        {

            if (newemail1 != null && newemail2 != null && newemail1 == newemail2)
            {
                tbllogin.email = newemail1;
            }

            if (ModelState.IsValid)
            {
                db.Entry(tbllogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idlogin = new SelectList(db.tblpersons, "idperson", "firstname", tbllogin.idlogin);
            return View(tbllogin);
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbllogin tbllogin = db.tbllogins.Find(id);
            if (tbllogin == null)
            {
                return HttpNotFound();
            }
            return View(tbllogin);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbllogin tbllogin = db.tbllogins.Find(id);
            db.tbllogins.Remove(tbllogin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
