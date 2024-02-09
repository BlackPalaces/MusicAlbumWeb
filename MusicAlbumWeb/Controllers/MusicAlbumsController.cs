using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Data.Entity.SqlServer;

namespace MusicAlbumWeb
{
    public class MusicAlbumsController : Controller
    {
        private Entities db = new Entities();

        // GET: MusicAlbums
        public ActionResult Index()
        {
            return View(db.MusicAlbum.ToList());
        }
        public ActionResult AlbumList(string NameAlbum)
        {
            var db = new Entities();
            var MusicforAlbum = db.MusicAlbum.Where(m => m.Album == NameAlbum).ToList();

            return View(MusicforAlbum);
        }

        public ActionResult ArtistList(string NameArtist)
        {
            var db = new Entities();
            var ArtistforAlbum = db.MusicAlbum.Where(m => m.Artist == NameArtist).ToList();

            return View(ArtistforAlbum);
        }

        public ActionResult Chart()
        {
            return View();
        }
        public ActionResult MusicGenres()
        {
            var db = new Entities();

            // ดึงค่า parameter "type" จาก query string
            string type = Request.QueryString["type"];

        

            // ในที่นี้คุณสามารถใช้ค่า type ที่ดึงมาเพื่อกรองข้อมูลที่คุณต้องการจาก database
            var data = db.MusicAlbum.Where(album => album.Type == type).ToList();

            return View(data);
        }
        public JsonResult GetDataJson()
        {
            var db = new Entities();
            var data = db.MusicAlbum.ToList();

            return Json(new { JSONList = data }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Manager()
        {
            // Check if the user has admin status
            if (User.Identity.IsAuthenticated)
            {
                // Assuming you have a User model with a Status property
                var user = db.AspNetUsers.FirstOrDefault(u => u.UserName == User.Identity.Name);

                if (user != null && user.Status == "Admin")
                {
                    // User has admin status, allow access to Manager page
                    return View();
                }
            }

            // If the user is not authenticated or doesn't have admin status, redirect to login or handle accordingly
            return RedirectToAction("Login", "Account"); // Adjust the redirect action as needed
        }


        public ActionResult MyFavorite()
        {
            // ตรวจสอบสถานะการล็อกอินของผู้ใช้
            if (!User.Identity.IsAuthenticated)
            {
                // หากไม่ได้ล็อกอิน ให้ redirect ไปยังหน้าล็อกอิน
                return RedirectToAction("Login", "Account");
            }
            var userEmail = User.Identity.Name;

         
                using (var db = new Entities())
                {
                    // ดึงรายการ id เพลงที่ผู้ใช้ชอบจากฐานข้อมูล
                    var favoriteMusicIds = db.FavoriteMusic
                        .Where(f => f.UserEmail == userEmail)
                        .Select(f => f.MusicFavId)
                        .ToList();

                    // ดึงเพลงที่ผู้ใช้ชอบจากฐานข้อมูล MusicAlbum
                    var favoriteSongs = db.MusicAlbum
                        .Where(m => favoriteMusicIds.Contains(m.Id))
                        .ToList();

                    return View(favoriteSongs);
            }
        }

        public ActionResult MusicPlay(int id)
        {
           
            var db = new Entities();
            var userId = User.Identity.GetUserId();
            var MusicforP = db.MusicAlbum.Where(m => m.Id == id).ToList();
            ViewBag.UserId = userId;

            return View(MusicforP);
        }



        // GET: MusicAlbums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicAlbum musicAlbum = db.MusicAlbum.Find(id);
            if (musicAlbum == null)
            {
                return HttpNotFound();
            }
            return View(musicAlbum);
        }

        // GET: MusicAlbums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusicAlbums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Musicname,Artist,Musiclink,Album,Type,Musicpic,Hit")] MusicAlbum musicAlbum)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    file.SaveAs(path);
                    musicAlbum.Musicpic = fileName;
                }
                db.MusicAlbum.Add(musicAlbum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musicAlbum);
        }

        // GET: MusicAlbums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicAlbum musicAlbum = db.MusicAlbum.Find(id);
            if (musicAlbum == null)
            {
                return HttpNotFound();
            }
            return View(musicAlbum);
        }

        // POST: MusicAlbums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Musicname,Artist,Musiclink,Album,Type,Musicpic,Hit")] MusicAlbum musicAlbum)
        {
            if (ModelState.IsValid)
            {
                // Check if a file is uploaded
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                        file.SaveAs(path);
                        musicAlbum.Musicpic = fileName;
                    }
                }

                db.Entry(musicAlbum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musicAlbum);
        }

        // GET: MusicAlbums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicAlbum musicAlbum = db.MusicAlbum.Find(id);
            if (musicAlbum == null)
            {
                return HttpNotFound();
            }
            return View(musicAlbum);
        }

        // POST: MusicAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusicAlbum musicAlbum = db.MusicAlbum.Find(id);
            db.MusicAlbum.Remove(musicAlbum);
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

        [HttpGet]
        public JsonResult Search(string query)
        {
            var results = db.MusicAlbum
                .Where(m =>
                    SqlFunctions.PatIndex("%" + query + "%", m.Musicname) > 0 ||
                    SqlFunctions.PatIndex("%" + query + "%", m.Artist) > 0 ||
                    SqlFunctions.PatIndex("%" + query + "%", m.Album) > 0
                )
                .Select(m => new { m.Id, m.Musicname, m.Artist, m.Album })
                .ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

    }
}
