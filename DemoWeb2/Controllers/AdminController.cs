using DemoWeb2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb2.Controllers
{
    public class AdminController : Controller
    {
        private DBSportStore1Entities database = new DBSportStore1Entities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminUser ad)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ad.Email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(ad.Password))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    var admin = database.AdminUsers.FirstOrDefault(k => k.Email == ad.Email && k.Password == ad.Password);
                    if (admin != null)
                    {
                        Session["TaiKhoan"] = admin;
                        return RedirectToAction("Index", "Categories");
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult ConfirmCate(AdminUser ad)
        {
            if (Session["TaiKhoan"] == null) //Chưa đăng nhập
                return RedirectToAction("Login", "Admin");
            var admin = database.Customers.FirstOrDefault(k => k.EmailCus == ad.Email && k.PassCus == ad.Password);
            if (admin != null)
            {
                Session["TaiKhoan"] = admin;
                return RedirectToAction("Index", "Categories");
            }
            //if (myCart == null || myCart.Count == 0) //Chưa có giỏ hàng hoặc chưa có sp
            //    return RedirectToAction("Index", "CustomerProduct");
            //ViewBag.TotalNumber = GetTotalNumber();
            //ViewBag.TotalPrice = GetTotalPrice();
            return View(); //Trả về View xác nhận đặt hàng
        }
        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Login", "Admin");
        }
    }
}