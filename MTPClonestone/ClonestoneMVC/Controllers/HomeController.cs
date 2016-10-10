using ClonestoneMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Datenbankverbindung mittels EF
            using(ClonestoneEntities cont = new ClonestoneEntities())
            {
                //Abfrage mittels LINQ
                //var linqRes = (from c in cont.tblcards
                //               where c.mana == 5
                //               select c).ToList();

                //Abfrage mittels Lambda
                //List<tblcard> lambdaRes = cont.tblcards.Where(c=>c.mana==5).ToList();

                //Abfrage mittels SP
                //var spRes = cont.spGetMana5().ToList();
            }

                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}