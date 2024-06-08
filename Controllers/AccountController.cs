using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication12.Models.ViewModels;   // add
using WebApplication11.Models;
using System.Web.Security;
using WebApplication11.Service;              // add

namespace WebApplication10.Controllers
{
    public class AccountController : Controller
    {
        DbNewsContextEntity db = new DbNewsContextEntity();         // be model ke be db vasl hast
        private UserService _userService;                           // use ha az service, null hast

        public AccountController()
        {
            _userService = new UserService(db);                     // db ro midim be userService
        }

        // Add View => Template = Create, Model Class = LoginViewModel, Data context class = khali chon be db vasl nist
        public ActionResult Login(string ReturnUrl="/")         // returnUrl az addressi ke redirect shode be inja hast. address ro az url 'GET' migire.  default ="/"
        {
            LoginViewModel loginInfo = new LoginViewModel()
            {
                returnUrl = ReturnUrl,
            };
            return View(loginInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "MobileNumber,Password,RememberMe,returnUrl")] LoginViewModel loginUser) { 
            if (ModelState.IsValid)
            {
                // inja mishod mostaghim az Db ham User haro gereft, vali az service migirim
                User user = _userService.GetAll().FirstOrDefault(t => t.MobileNumber == loginUser.MobileNumber && t.Password == loginUser.Password);
                if (user != null)
                {
                    if (user.IsActive == "true")
                    {
                        // yek token to cookie behehs mide. (username, remember me false ya true hast)
                        FormsAuthentication.SetAuthCookie(loginUser.MobileNumber, loginUser.RememberMe);       // in token baraye inke bedunim kodom User create karde. Controller => News => #ref3
                        // bade token, redirect kon be urli ke bude. /Admin/Default/Index  dige error nemide va login mishe
                        return Redirect(loginUser.returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("MobileNumber", "Your account is not active");
                    }
                }
                ModelState.AddModelError("MobileNumber", "User name ro password is wrong");
                return View();
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        // Partial View dare
        public ActionResult LoginState()
        {
            ViewBag.LoginState = false;
            if (User.Identity.IsAuthenticated)              // age user login hast. age authenticated hast
            {
                ViewBag.LoginState = true;
            }
            return PartialView();          // return mikone PartialView
        }
    }
}