using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;
using WebApplication11.Service;
using WebApplication12.App_Start;
using WebApplication12.Classes;
using WebApplication12.Models.ViewModels;

namespace WebApplication12.Controllers
{
    public class NewsController : Controller
    {
        DbNewsContextEntity db = new DbNewsContextEntity();
        NewsGroupService _newsGroupService;
        NewsService _newsService;

        /// <summary>
        /// Constructor hast
        /// </summary>
        public NewsController()
        {
            _newsGroupService = new NewsGroupService(db);
            _newsService = new NewsService(db);
        }

        // Add View => tike Create as a partial view ro bezan
        public ActionResult ShowNewsGroup(int LocId)
        {
            ViewBag.LocId = LocId;
            var newsGroups = _newsGroupService.GetAll();
            List<NewsGroupViewModel> newsGroupViewModels = ExtensionClass.IENewsGroupToLNewsGroupViewModel(newsGroups);   // foldere Classes => ExtensionClass.cs
            return PartialView(newsGroupViewModels);
        }
    
        /// <summary>
        ///  Partial View dare 
        /// </summary>
        /// <param name="count"></param>
        /// <returns> Listi az lastNews ha, ya, yek dune lastNews </returns>
        public ActionResult LastNews(int? count)
        {
            // /News/LastNews/?count=3
            if (count != null)
            {
                var lastNews = _newsService.getLastNewsList(count);      // to Service neveshtim in ro
                List<NewsViewModel> newsViewModels = ExtensionClass.LNewsToLNewsViewModel(lastNews);
                return PartialView(newsViewModels);         // inja List mifreste
            }
            // /News/LastNews
            else
            {
                var lastNews = _newsService.getlastNews();              // to Service neveshtim in ro
                NewsViewModel newsViewModels = ExtensionClass.NewsToNewsViewModel(lastNews);    // foldere Classes => ExtensionClass.cs
                return PartialView(newsViewModels);        // inja faghat yek entity mifreste
            }
        }

        // Partial View dare
        public ActionResult BestNews(int count)
        {
            var bestNews = _newsService.getBestNewsList(count);       // to Service neveshtim in ro
            List<NewsViewModel> newsViewModels = ExtensionClass.LNewsToLNewsViewModel(bestNews);
            return PartialView(newsViewModels);
        }

        // url => /News/NewsDetail/11
        // View kamel hast
        public ActionResult NewsDetail(int id)
        {
            var news = _newsService.GetEntity(id);
            if (news == null || !news.IsActive)             // age IsActive nabud
            {
                return HttpNotFound(); 
            }

            // har bar ke in Action run mishe(in page load mishe), see++ mishe
            news.See++;                 
            _newsService.Update(news);
            _newsService.Save();

            NewsViewModel newsViewModels = ExtensionClass.NewsToNewsViewModel(news);    // foldere Classes => ExtensionClass.cs
            return View(newsViewModels);
        }

        /// <summary>
        ///   Partial View dare
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        /// <param name="code"></param>
        /// <returns> Partial View az NewsLikeViewModel </returns>
        public ActionResult ShowLike(int newsId, bool state, int code)
        {
            var news = _newsService.GetEntity(newsId);
            // yek ModelView jadid sakhtim
            NewsLikeViewModel newsLikeViewModel = new NewsLikeViewModel()
            {
                NewsId = newsId,
                Like = news.Like,
                NewsState = state,
            };
            ViewBag.code = code;
            return PartialView(newsLikeViewModel);          // in PrtialView bar migarde be Ajax, success
        }
        
        public ActionResult changeLikeState(int newsId, bool state, int code)
        {
            var news = _newsService.GetEntity(newsId);
            news.Like =  (state) ? (news.Like - 1) : (news.Like + 1);
            _newsService.Update(news);
            _newsService.Save();
            return RedirectToAction("ShowLike", new {newsId, state, code});       // chon ham nam ast lazem nist injuri, new {newsId = newsId, state = state}
        }

        public ActionResult ShowNewsList(int? id)           // id inja NewsGroupId hast
        {
            IEnumerable<News> listNews = _newsService.getAllNewsList();    // to Service neveshtim in ro
            if (id != null)
            {
                listNews = listNews.Where(t => t.NewsGroupId == id);
            }
            List<NewsViewModel> newsViewModels = ExtensionClass.LNewsToLNewsViewModel(listNews);

            return View(newsViewModels);
        }
    }
}