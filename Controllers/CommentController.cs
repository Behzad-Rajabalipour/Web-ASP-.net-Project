using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Service;
using WebApplication11.Models;
using WebApplication12.App_Start;
using WebApplication12.Models.ViewModels;

namespace WebApplication12.Controllers
{
    public class CommentController : Controller
    {
        DbNewsContextEntity db = new DbNewsContextEntity();     // db
        CommentService _commentService;                         // comment service

        public CommentController()
        {
            _commentService = new CommentService(db);         // be comment service, db midim
        }

        public ActionResult Index()
        {
            return View();
        }

        // template = List, Model = CommentViewModel, Database Context = khali, create Patial View
        // Partial view dakhele yek View dige estefade mishe
        // toye Partial View, style va js kar nemikone. bayad bezarish to View ta kar kone
        public ActionResult ShowComment(int id)         // Id hamun NewsId hast
        {
            var comments = _commentService.GetAll().Where(t => t.IsActive && t.NewsId == id).OrderByDescending(u =>  u.commentId).ToList();
            var commentViewModels = AutoMapperConfig.mapper.Map<IEnumerable<Comment>,List<CommentViewModel>>(comments);     // tabdil mikonim Comment Model ro be CommentViewModel Model
            return PartialView(commentViewModels);
        }

        // Create, Partial View, Model CommentViewModel, Database = khali 
        public ActionResult CreateComment(int id) {         // Id hamun NewsId hast
            CommentViewModel commentViewModel =  new CommentViewModel();
            commentViewModel.NewsId = id;
            commentViewModel.IsActive = true;
            return PartialView(commentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "NewsId, Name, Email, CommentText, Description, IsActive")] CommentViewModel commentViewModel) // age IsActive ro nemiferestadim, error midad inja. chon required hast dakhale Model
        {
            if (ModelState.IsValid)
            {
                Comment comment = AutoMapperConfig.mapper.Map<CommentViewModel,Comment>(commentViewModel);
                comment.RegisterDate = DateTime.Now;
                _commentService.Add(comment);           // add mikone be DB
                _commentService.Save();
                return RedirectToAction("ShowComment", new { id = comment.NewsId });
            }
            Response.StatusCode = 400;          // Status Error return kon
            return PartialView(commentViewModel);
        }

    }
}