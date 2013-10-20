using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NerdDinner.Learning.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Playing with Nerd Dinner tutorial project and PCL";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is my Nerd Dinner PCL testing project.  Hoping this works";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Right now it's just little old me - Tara";

            return View();
        }
    }
}
