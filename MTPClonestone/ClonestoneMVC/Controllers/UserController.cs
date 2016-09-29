using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Profil()
        {
            return View();
        }

        public ActionResult Deckbuilder()
        {
            return View();
        }
    }
}