using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication12.Controllers
{
    public class HomeController : Controller
    {
        // View ro bebin
        public ActionResult Index()
        {
            return View();
        }

        // Route
        // 1. App_Start => RouteConfig.cs => add mikonim  routes.MapMvcAttributeRoutes(); 
        // 2. [Route("")] ro payin ezafe mikonim
        [Route("About")]                 // Url => /About bejaye /Home/AboutUs shode
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}