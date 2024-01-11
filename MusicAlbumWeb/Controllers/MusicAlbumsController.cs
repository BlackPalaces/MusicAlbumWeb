using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;

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

        public ActionResult Chart()
        {
            return View();
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
            return View();
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
    }
}
