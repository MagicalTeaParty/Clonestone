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
    public class tbleditFilesController : Controller
    {
        private ClonestoneEntities db = new ClonestoneEntities();

        // GET: tbleditFiles
        public ActionResult Index()
        {
            var tbleditFiles = db.tbleditFiles.Include(t => t.tbledit);
            return View(tbleditFiles.ToList());
        }

        // GET: tbleditFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbleditFile tbleditFile = db.tbleditFiles.Find(id);
            if (tbleditFile == null)
            {
                return HttpNotFound();
            }
            return View(tbleditFile);
        }

        // GET: tbleditFiles/Create
        public ActionResult Create()
        {
            ViewBag.fkedit = new SelectList(db.tbledits, "idedit", "title");
            return View();
        }

        // POST: tbleditFiles/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ideditfile,fkedit,uidfile,editfile,mimetype")] tbleditFile tbleditFile, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload !=null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        tbleditFile.editfile = reader.ReadBytes(upload.ContentLength);
                        tbleditFile.mimetype = upload.ContentType;
                    }
                }
                db.tbleditFiles.Add(tbleditFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fkedit = new SelectList(db.tbledits, "idedit", "title", tbleditFile.fkedit);
            return View(tbleditFile);
        }

        // GET: tbleditFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbleditFile tbleditFile = db.tbleditFiles.Find(id);
            if (tbleditFile == null)
            {
                return HttpNotFound();
            }
            ViewBag.fkedit = new SelectList(db.tbledits, "idedit", "title", tbleditFile.fkedit);
            return View(tbleditFile);
        }

        // POST: tbleditFiles/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ideditfile,fkedit,uidfile,editfile,mimetype")] tbleditFile tbleditFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbleditFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fkedit = new SelectList(db.tbledits, "idedit", "title", tbleditFile.fkedit);
            return View(tbleditFile);
        }

        // GET: tbleditFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbleditFile tbleditFile = db.tbleditFiles.Find(id);
            if (tbleditFile == null)
            {
                return HttpNotFound();
            }
            return View(tbleditFile);
        }

        // POST: tbleditFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbleditFile tbleditFile = db.tbleditFiles.Find(id);
            db.tbleditFiles.Remove(tbleditFile);
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
