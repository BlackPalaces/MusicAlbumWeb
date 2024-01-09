using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicAlbumWeb.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult HomePage()
        {
            var musicList = db.MusicAlbum.ToList();

            // เรียงลำดับตามค่า Hit จากมากไปน้อย
            var sortedMusicList = musicList.OrderByDescending(m => m.Hit).ToList();
            return View(db.MusicAlbum.ToList());
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