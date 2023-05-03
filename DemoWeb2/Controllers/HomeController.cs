using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult DesktopComputer()
        {
            return View();
        }
        public ActionResult ThongTin()
        {
            return View();
        }
    }
}