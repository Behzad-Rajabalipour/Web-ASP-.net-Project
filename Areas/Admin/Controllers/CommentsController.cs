using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;
using WebApplication11.Service;
using WebApplication12.App_Start;
using WebApplication12.Models.ViewModels;

namespace WebApplication12.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        private DbNewsContextEntity db = new DbNewsContextEntity();

        CommentService _commentService;
        public CommentsController()
        {
            _commentService = new CommentService(db);
        }


        // GET: Admin/Comments
        public ActionResult Index()
        {
            var comments = _commentService.GetAll();
            var commentViewModels= AutoMapperConfig.mapper.Map<IEnumerable<Comment>, List<CommentViewModel>>(comments);
            return View(commentViewModels);
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Comment comment = db.Comments.Find(id);          // mostaghim az DB, data nemigirim. az service migirim
            Comment comment = _commentService.GetEntity(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }

            // ViewBag.NewsId = new SelectList(db.News, "NewsId", "NewsTitle", comment.NewsId);     // nemikhaym bezarim edit kone
            var commentViewModel = AutoMapperConfig.mapper.Map<Comment, CommentViewModel>(comment);
            return View(commentViewModel);
        }

        // POST: Admin/Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentId,commentText,Name,Email,RegisterDate,IsActive,NewsId")] CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = AutoMapperConfig.mapper.Map<CommentViewModel,Comment>(commentViewModel);
                // db.Entry(comment).State = EntityState.Modified;              // az Db estefade nemikoni
                _commentService.Update(comment);                                // az service estefade mikonim
                // db.SaveChanges();
                _commentService.Save();
                return RedirectToAction("Index");
            }
            return View(commentViewModel);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
