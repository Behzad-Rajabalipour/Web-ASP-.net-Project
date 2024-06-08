using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class NewsController : Controller
    {
        private DbNewsContextEntity db = new DbNewsContextEntity();
        private NewsService _newsService;
        private NewsGroupService _newsGroupService;             //
        private UserService _userService;


        public NewsController()
        {
            // har kodom az ina DbbSet khdeshun ro migiran
            _newsService = new NewsService(db);                 //
            _newsGroupService = new NewsGroupService(db);       //      contextesh ro moshakhas mikonim
            _userService = new UserService(db);                 //
        }

        public ActionResult Index()
        {
            IEnumerable<News> news = _newsService.GetAll();
            var newsViewModels = AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(news);
            return View(newsViewModels);
        }

        
        // bayad esme input hatman id bashe ke /Admin/News/Details/3  beshe
        // age id nabaseh injuri bayad bedim /Admin/News/Details?newsId=3
        public ActionResult Details(int? id)                
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);           // id.value, chon null(?) darim
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(newsViewModel);
        }


        public ActionResult Create()
        {
            // DropDown toye front
            // ViewBag.NewsGroupId = new SelectList(db.NewsGroups, "NewsGroupId", "NewsGroupTitle");         // ma az db mostaghtim nemigirim. az service migirim
            // ViewBag.NewsGroupId = [["NewsGroupId1","NewsGroupTitle1"],["NewsGroupId2","NewsGroupTitle2"],..]
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle");  // NewsGroupId va NewsGroupTitle ro bebar front

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,NewsTitle,Description,NewsGroupId")] NewsViewModel newsViewModel, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)     // age az front validation dorost nabashe, ya NewsViewModel required bashe ke nayomade back, inja valid nemishe
            {
                #region Save Image in Storage
                string imageName = "nophoto.jpg";
                if (imgUpload != null)
                {
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("/Images/News/") + imageName);
                }
                #endregion

                newsViewModel.ImageName = imageName;
                var news = AutoMapperConfig.mapper.Map<NewsViewModel, News>(newsViewModel);
                news.IsActive = true;
                news.Like = 0;
                news.See = 0;
                news.RegisterDate = DateTime.Now;
                // (User.Identity.Name) hamun tokeni(MobileNumber) hast ke moghe login to cookie save shod. Controller => Account => #ref3
                news.UserId = _userService.GetUserId(User.Identity.Name);       // (MobileNumber)    

                // injuri ham mishod nevesht, vali ma mostaghim az DB nemigirim, az service migirim 
                // news.UserId = db.Users.FirstOrDefault(u => u.MobileNumber == User.Identity.Name).UserId;

                // db.News.Add(news);       // mostaghim to db save nemikonim. az service estefade mikonim
                // db.SaveChanges();
                _newsService.Add(news);
                _newsService.Save();
                return RedirectToAction("Index");
            }
            //DropDown
            // ina ViewBag hastan samte front darimeshun. (hamey ietm haye _newsGroupService, in ro biyar back, in ro be karbar neshun bede, ro in select bashe(inja nadarim))
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle");

            return View(newsViewModel);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);       //
            var newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            
            if (news == null)
            {
                return HttpNotFound();
            }
            // DropDown
            // ina ViewBag hastan samte front darimeshun. (hamey ietm haye _newsGroupService, in ro biyar back, in ro be karbar neshun bede, ro in select bashe)
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle", newsViewModel.NewsGroupId);    
            return View(newsViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,NewsTitle,Description,ImageName,RegisterDate,IsActive,See,Like,NewsGroupId,UserId")] NewsViewModel newsViewModel, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)         // age az front validation dorost nabashe, ya NewsViewModel required bashe ke nayomade back, inja valid nemishe
            {
                if (imgUpload != null)
                {
                    if (newsViewModel.ImageName != "nophoto.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/News/") + newsViewModel.ImageName);       // aks ro az storage pak mikone
                    }
                    else        // age nophoto.jpg nabud
                    {
                        newsViewModel.ImageName = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(imgUpload.FileName);
                    }
                    imgUpload.SaveAs(Server.MapPath("/Images/News/") + newsViewModel.ImageName);            // ba hamun esme khodesh save mikonim
                }

                News news = AutoMapperConfig.mapper.Map<NewsViewModel, News>(newsViewModel);   // change mikonim be newsGroup
                _newsService.Update(news);           // Add
                _newsService.Save();
                return RedirectToAction("Index");
            }
            // ina ViewBag hastan samte front darimeshun. (hamey ietm haye _newsGroupService, in ro biyar back, in ro be karbar neshun bede, ro in select bashe)
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle", newsViewModel.NewsGroupId);
            return View(newsViewModel);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);           //
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(newsViewModel);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]            // esmesho avaz kon bezar "Delete"
        [ValidateAntiForgeryToken]                  // baraye security hast
        public ActionResult DeleteConfirmed(int id)
        {
            var news = _newsService.GetEntity(id);
            _newsService.Delete(id);
            _newsService.Save();
            
            if (news.ImageName != "nophoto.jpg")
            {
                System.IO.File.Delete(Server.MapPath("/Images/News/") + news.ImageName);       // aks ro az storage pak mikone
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            // har 3ta dispose mishan. mesle destructor hast
            _newsService.Dispose();
            _newsGroupService.Dispose();
            _userService.Dispose();
        }
    }
}
