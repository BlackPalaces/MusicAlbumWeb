using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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

    
        public ActionResult Manager()
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
        public ActionResult Create([Bind(Include = "Id,Musicname,Artist,Musiclink,Album,Musicpic,Hit")] MusicAlbum musicAlbum)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include = "Id,Musicname,Artist,Musiclink,Album,Musicpic,Hit")] MusicAlbum musicAlbum)
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
