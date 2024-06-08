using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using WebApplication11.Models;
using WebApplication11.Service;
using WebApplication12.App_Start;
using WebApplication12.Models.ViewModels;

namespace WebApplication12.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private DbNewsContextEntity db = new DbNewsContextEntity();
        UserService _userService;

        public UsersController()
        {
            _userService = new UserService(db);
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = _userService.GetAll();
            var usersViewModel = AutoMapperConfig.mapper.Map<IEnumerable<User>, List<UserViewModel>>(users);
            return View(usersViewModel);
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetEntity(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userVideModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);
            return View(userVideModel);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,MobileNumber,Password,RegisterDate,IsActive,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetEntity(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);
            return View(userViewModel);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,MobileNumber,Password,RegisterDate,IsActive,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
