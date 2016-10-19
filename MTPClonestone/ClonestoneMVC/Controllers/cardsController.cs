using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClonestoneMVC.Models;

namespace ClonestoneMVC.Controllers
{
    public class cardsController : Controller
    {
        private ClonestoneEntities db = new ClonestoneEntities();

        // GET: cards
        public ActionResult Index()
        {
            var tblcards = db.tblcards.Include(t => t.tblability).Include(t => t.tblclass).Include(t => t.tbltype);
            return View(tblcards.ToList());
        }

        // GET: cards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcard tblcard = db.tblcards.Find(id);
            if (tblcard == null)
            {
                return HttpNotFound();
            }
            return View(tblcard);
        }

        // GET: cards/Create
        public ActionResult Create()
        {
            ViewBag.fkability = new SelectList(db.tblabilities, "idability", "ability");
            ViewBag.fkclass = new SelectList(db.tblclasses, "idclass", "class");
            ViewBag.fktype = new SelectList(db.tbltypes, "idtype", "typename");
            return View();
        }

        // POST: cards/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idcard,cardname,mana,life,attack,flavor,fktype,fkclass,fkability,pic")] tblcard tblcard)
        {
            if (ModelState.IsValid)
            {
                db.tblcards.Add(tblcard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fkability = new SelectList(db.tblabilities, "idability", "ability", tblcard.fkability);
            ViewBag.fkclass = new SelectList(db.tblclasses, "idclass", "class", tblcard.fkclass);
            ViewBag.fktype = new SelectList(db.tbltypes, "idtype", "typename", tblcard.fktype);
            return View(tblcard);
        }

        // GET: cards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcard tblcard = db.tblcards.Find(id);
            if (tblcard == null)
            {
                return HttpNotFound();
            }
            ViewBag.fkability = new SelectList(db.tblabilities, "idability", "ability", tblcard.fkability);
            ViewBag.fkclass = new SelectList(db.tblclasses, "idclass", "class", tblcard.fkclass);
            ViewBag.fktype = new SelectList(db.tbltypes, "idtype", "typename", tblcard.fktype);
            return View(tblcard);
        }

        // POST: cards/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idcard,cardname,mana,life,attack,flavor,fktype,fkclass,fkability,pic")] tblcard tblcard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblcard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fkability = new SelectList(db.tblabilities, "idability", "ability", tblcard.fkability);
            ViewBag.fkclass = new SelectList(db.tblclasses, "idclass", "class", tblcard.fkclass);
            ViewBag.fktype = new SelectList(db.tbltypes, "idtype", "typename", tblcard.fktype);
            return View(tblcard);
        }

        // GET: cards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcard tblcard = db.tblcards.Find(id);
            if (tblcard == null)
            {
                return HttpNotFound();
            }
            return View(tblcard);
        }

        // POST: cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblcard tblcard = db.tblcards.Find(id);
            db.tblcards.Remove(tblcard);
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
