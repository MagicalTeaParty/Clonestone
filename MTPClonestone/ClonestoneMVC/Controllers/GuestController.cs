using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Tutorial()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }
    }
}