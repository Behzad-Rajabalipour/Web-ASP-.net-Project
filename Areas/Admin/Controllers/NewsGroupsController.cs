using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;                  //
using WebApplication12.Models.ViewModels;       //
using WebApplication11.Service;
using WebApplication12.App_Start;                 //


// Add Refernce mikonim WebApplication11
// Add Controller => MVC5 controller with views, using EntityFramework => Model = NewsGroup, Data Context class = DbNewsContextEntity.cs
namespace WebApplication12.Areas.Admin.Controllers
{
    public class NewsGroupsController : Controller
    {
        private DbNewsContextEntity db = new DbNewsContextEntity();
        NewsGroupService _newsGroupService;
        public NewsGroupsController()
        {
            _newsGroupService = new NewsGroupService(db);
        }

        public ActionResult Index()
        {
            IEnumerable<NewsGroup> newsGroups = _newsGroupService.GetAll();      //
            List<NewsGroupViewModel> newsGroupViewModels = AutoMapperConfig.mapper.Map<IEnumerable<NewsGroup>, List<NewsGroupViewModel>>(newsGroups);      // IeNumberable NewsGroup ro rikht toye ro List<NewsGroupViewModel>
            return View(newsGroupViewModels);                // list mifreste front, vali front IEnumerable migire
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsGroup newsGroup = _newsGroupService.GetEntity(id.Value);            // id.value yani hatman null nist
            if (newsGroup == null)
            {
                return HttpNotFound();
            }

            NewsGroupViewModel newsGroupViewModel = AutoMapperConfig.mapper.Map<NewsGroup, NewsGroupViewModel>(newsGroup);      //
            return View(newsGroupViewModel);
        }

        // create view  => template: Cretae, Model: NewsGroupViewModel, Data Context = khali chon be db vasl nist
        public ActionResult Create()
        {
            return View();
        }

        // create view  => template: Cretae, Model: NewsGroupViewModel, Data Context = khali chon be db vasl nist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsGroupTitle,Description")] NewsGroupViewModel newsGroupViewModel, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)             // age az front validation dorost nabashe, ya NewsViewModel required bashe ke nayomade back, inja valid nemishe
            {
                #region Save Image in Storage
                string imageName = "nophoto.jpg";
                if (imgUpload != null)
                {
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("/Images/News-Groups/") + imageName);
                }
                #endregion

                newsGroupViewModel.ImageName = imageName;
                // newsGroup chon be DB vasl hast, pas bayad NewsGroupId dashte bashe.
                newsGroupViewModel.NewsGroupId = _newsGroupService.NextNewsGroupId();           // Auto Increment ro baraye NewsGroup off kardim, pas bayad inja behesh NewsGroupId bedim
                NewsGroup newsGroup = AutoMapperConfig.mapper.Map<NewsGroupViewModel, NewsGroup>(newsGroupViewModel);   // change mikonim be newsGroup
                _newsGroupService.Add(newsGroup);           // Add
                _newsGroupService.Save();
                return RedirectToAction("Index");
            }

            return View(newsGroupViewModel);
        }

        // GET: Admin/NewsGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsGroup newsGroup = _newsGroupService.GetEntity(id.Value);            //
            if (newsGroup == null)
            {
                return HttpNotFound();
            }
            NewsGroupViewModel newsGroupViewModels = AutoMapperConfig.mapper.Map<NewsGroup, NewsGroupViewModel>(newsGroup);      // tabdil kon az un model be in model   
            return View(newsGroupViewModels);
        }

        // POST: Admin/NewsGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsGroupId,NewsGroupTitle,ImageName,Description")] NewsGroupViewModel newsGroupViewModel, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)         // age az front validation dorost nabashe, ya NewsViewModel required bashe ke nayomade back, inja valid nemishe
            {
                if (imgUpload != null)
                {
                    if (newsGroupViewModel.ImageName != "nophoto.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/News-Groups/") + newsGroupViewModel.ImageName);       // aks ro az storage pak mikone
                    }
                    else        // age nophoto.jpg nabud
                    {
                        newsGroupViewModel.ImageName = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(imgUpload.FileName);
                    }
                    imgUpload.SaveAs(Server.MapPath("/Images/News-Groups/") + newsGroupViewModel.ImageName);            // ba hamun esme khodesh save mikonim
                }

                NewsGroup newsGroup = AutoMapperConfig.mapper.Map<NewsGroupViewModel, NewsGroup>(newsGroupViewModel);   // change mikonim be newsGroup
                _newsGroupService.Update(newsGroup);           // Add
                _newsGroupService.Save();
                return RedirectToAction("Index");

            }
            return View(newsGroupViewModel);
        }

        // GET: Admin/NewsGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsGroup newsGroup = _newsGroupService.GetEntity(id.Value);        //
            if (newsGroup == null)
            {
                return HttpNotFound();
            }
            NewsGroupViewModel newsGroupViewModel = AutoMapperConfig.mapper.Map<NewsGroup, NewsGroupViewModel>(newsGroup);      //
            return View(newsGroupViewModel);
        }

        // POST: Admin/NewsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var newsGroup = _newsGroupService.GetEntity(id);
            _newsGroupService.Delete(id);           //
            _newsGroupService.Save();               //

            if (newsGroup.ImageName != "nophoto.jpg")
            {
                System.IO.File.Delete(Server.MapPath("/Images/News-Groups/") + newsGroup.ImageName);       // aks ro az storage pak mikone
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            // Dispose mikonim. mesle Destructor hast
            _newsGroupService.Dispose();
        }
    }
}
