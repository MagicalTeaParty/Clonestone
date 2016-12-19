using ClonestoneMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class EditController : Controller
    {

        private ClonestoneEntities db = new ClonestoneEntities();

        // GET: /Edit
        //public ActionResult Index()
        //{
        //    //var news = (b => b.tblpersons);
        //    return View(db.tbledits.ToList());
        //}

        // GET: /Detail/Id

        public ActionResult Detail(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            var news = db.tbledits.Find(id);
            if(news == null)
            {
                return RedirectToAction("Index");
            }
            return View(news);
        }

       
        public ActionResult Index()
        {
            var Edit = (from edit in db.tbledits.ToList()
                           where edit.isnews == true
                           select edit);
            return View(Edit);
        }

    }
}