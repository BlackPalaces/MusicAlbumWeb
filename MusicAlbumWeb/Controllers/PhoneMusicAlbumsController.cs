using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MusicAlbumWeb;

namespace MusicAlbumWeb.Controllers
{
    public class PhoneMusicAlbumsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/PhoneMusicAlbums
        public IQueryable<MusicAlbum> GetMusicAlbum()
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);

            var lmusic = db.MusicAlbum.ToList();
            foreach (var item in lmusic)
            {
                item.Musicpic = url + "/Content/Images/" + item.Musicpic;
            }
            return db.MusicAlbum;
        }

        // GET: api/PhoneMusicAlbums/5
        [ResponseType(typeof(MusicAlbum))]
        public IHttpActionResult GetMusicAlbum(int id)
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);

            MusicAlbum musicAlbum = db.MusicAlbum.Find(id);
            if (musicAlbum == null)
            {
                return NotFound();
            }
            musicAlbum.Musicpic = url + "/Content/Images/" + musicAlbum.Musicpic;
            return Ok(musicAlbum);
        }

        // PUT: api/PhoneMusicAlbums/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMusicAlbum(int id, MusicAlbum musicAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != musicAlbum.Id)
            {
                return BadRequest();
            }

            db.Entry(musicAlbum).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicAlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PhoneMusicAlbums
        [ResponseType(typeof(MusicAlbum))]
        public IHttpActionResult PostMusicAlbum(MusicAlbum musicAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MusicAlbum.Add(musicAlbum);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = musicAlbum.Id }, musicAlbum);
        }

        // DELETE: api/PhoneMusicAlbums/5
        [ResponseType(typeof(MusicAlbum))]
        public IHttpActionResult DeleteMusicAlbum(int id)
        {
            MusicAlbum musicAlbum = db.MusicAlbum.Find(id);
            if (musicAlbum == null)
            {
                return NotFound();
            }

            db.MusicAlbum.Remove(musicAlbum);
            db.SaveChanges();

            return Ok(musicAlbum);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MusicAlbumExists(int id)
        {
            return db.MusicAlbum.Count(e => e.Id == id) > 0;
        }
    }
}