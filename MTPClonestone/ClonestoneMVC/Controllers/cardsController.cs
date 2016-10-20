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
            //.Include(t => t.tblability).Include(t => t.tblclass).Include(t => t.tbltype)
            var tblcards = db.tblcards.Take(15);
            return View(tblcards.ToList());
        }

    }
}
