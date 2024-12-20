﻿using System;
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
    public class CommentsController : Controller
    {
        private Entities db = new Entities();

        // GET: Comments
        public ActionResult Index()
        {
            return View(db.Comment.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comment1,Musicid,UserEmail")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment1,Musicid,UserEmail")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comment.Find(id);
            db.Comment.Remove(comment);
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
        public ActionResult PostComment(string comment, int musicId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var db = new Entities();
                    var userEmail = User.Identity.Name;

                    // แสดงค่า comment ใน Output ของ Visual Studio
                    System.Diagnostics.Debug.WriteLine("comment: " + comment);

                    if (!string.IsNullOrWhiteSpace(comment))
                    {
                        var musicAlbum = db.MusicAlbum.Find(musicId);
                        if (musicAlbum == null)
                        {
                            return Json(new { success = false, message = "ไม่พบเพลงที่ระบุ" });
                        }

                        var newComment = new Comment
                        {
                            Comment1 = comment,
                            Musicid = musicId,
                            UserEmail = userEmail
                        };

                        db.Comment.Add(newComment);
                        db.SaveChanges();

                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Comment is empty or null" });
                    }
                }

                return Json(new { success = false, message = "ผู้ใช้ไม่ได้ล็อกอิน" });
                // หากไม่ได้ล็อกอิน ให้ redirect ไปยังหน้าล็อกอิน
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาดบางอย่าง", error = ex.Message });
            }
        }

      
        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            try
            {
                var db = new Entities();
                var comment = db.Comment.Find(commentId);

                if (comment == null)
                {
                    return Json(new { success = false, message = "ไม่พบคอมเมนต์ที่ต้องการลบ" });
                }

                db.Comment.Remove(comment);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาดบางอย่าง", error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateComment(int commentId, string editedComment)
        {
            try
            {
                var db = new Entities();
                var comment = db.Comment.Find(commentId);

                if (comment == null)
                {
                    return Json(new { success = false, message = "ไม่พบคอมเมนต์ที่ต้องการแก้ไข" });
                }

                comment.Comment1 = editedComment;
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาดบางอย่าง", error = ex.Message });
            }
        }


    }
}
