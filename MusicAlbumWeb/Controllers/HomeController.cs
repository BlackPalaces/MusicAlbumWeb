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
            var AlbumList = db.Album.ToList();

            return View(AlbumList);
        }
        public ActionResult Artist()
        {
            var ArtistList = db.Artist.ToList();

            return View(ArtistList);
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

        
    }
}