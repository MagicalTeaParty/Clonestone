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
    public class CardsController : Controller
    {
        private ClonestoneEntities db = new ClonestoneEntities();

        // GET: cards
        public ActionResult Index(int? start)
        {
            if (start == null)
            {
                start = 0;
            }
          


            //.Include(t => t.tblability).Include(t => t.tblclass).Include(t => t.tbltype)
            var cards = db.tblcards.OrderBy(tblcard => tblcard.idcard).Skip((int)start).Take(9);
            var anz = db.tblcards.Count();

            ViewBag.start = start;
            ViewBag.steps = 9;
            ViewBag.len = anz;
            

            return View(cards.ToList());
        }

    }
}
