using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicAlbumWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Albams()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Playlist()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Artist()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}