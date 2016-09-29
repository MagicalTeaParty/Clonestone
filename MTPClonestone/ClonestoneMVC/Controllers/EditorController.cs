using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Controllers
{
    public class EditorController : Controller
    {
        // GET: Editor
        public ActionResult EditNews()
        {
            return View();
        }

        public ActionResult EditTutorial()
        {
            return View();
        }

        public ActionResult EditGallery()
        {
            return View();
        }
    }
}