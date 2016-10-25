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
                ViewBag.position = "Home";
                return View();
        }

    }
}