using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicAlbumWeb;

namespace MusicAlbumWeb.Controllers
{
    public class FavoriteMusicsController : Controller
    {
        private Entities db = new Entities();

        // GET: FavoriteMusics
        public ActionResult Index()
        {
            var favoriteMusic = db.FavoriteMusic.Include(f => f.MusicAlbum);
            return View(favoriteMusic.ToList());
        }

        // GET: FavoriteMusics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteMusic favoriteMusic = db.FavoriteMusic.Find(id);
            if (favoriteMusic == null)
            {
                return HttpNotFound();
            }
            return View(favoriteMusic);
        }

        // GET: FavoriteMusics/Create
        public ActionResult Create()
        {
            ViewBag.MusicFavId = new SelectList(db.MusicAlbum, "Id", "Musicname");
            return View();
        }

        // POST: FavoriteMusics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MusicFavId,UserEmail")] FavoriteMusic favoriteMusic)
        {
            if (ModelState.IsValid)
            {
                db.FavoriteMusic.Add(favoriteMusic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MusicFavId = new SelectList(db.MusicAlbum, "Id", "Musicname", favoriteMusic.MusicFavId);
            return View(favoriteMusic);
        }

        // GET: FavoriteMusics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteMusic favoriteMusic = db.FavoriteMusic.Find(id);
            if (favoriteMusic == null)
            {
                return HttpNotFound();
            }
            ViewBag.MusicFavId = new SelectList(db.MusicAlbum, "Id", "Musicname", favoriteMusic.MusicFavId);
            return View(favoriteMusic);
        }

        // POST: FavoriteMusics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MusicFavId,UserEmail")] FavoriteMusic favoriteMusic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favoriteMusic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MusicFavId = new SelectList(db.MusicAlbum, "Id", "Musicname", favoriteMusic.MusicFavId);
            return View(favoriteMusic);
        }

        // GET: FavoriteMusics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteMusic favoriteMusic = db.FavoriteMusic.Find(id);
            if (favoriteMusic == null)
            {
                return HttpNotFound();
            }
            return View(favoriteMusic);
        }

        // POST: FavoriteMusics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavoriteMusic favoriteMusic = db.FavoriteMusic.Find(id);
            db.FavoriteMusic.Remove(favoriteMusic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult ToggleFavorite(int musicId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var db = new Entities();
                var userEmail = User.Identity.Name;
                var existingFavorite = db.FavoriteMusic.SingleOrDefault(f => f.MusicFavId == musicId && f.UserEmail == userEmail);

                var musicAlbum = db.MusicAlbum.Find(musicId);

                if (existingFavorite == null)
                {
                    var newFavorite = new FavoriteMusic
                    {
                        MusicFavId = musicId,
                        UserEmail = userEmail
                    };

                    db.FavoriteMusic.Add(newFavorite);
                    musicAlbum.Hit = (musicAlbum.Hit ?? 0) + 1;
                }
                else
                {
                    db.FavoriteMusic.Remove(existingFavorite);
                    musicAlbum.Hit = (musicAlbum.Hit ?? 0) - 1;
                }

                db.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "ผู้ใช้ไม่ได้ล็อกอิน" });
        }
    }
}
