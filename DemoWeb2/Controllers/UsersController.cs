﻿using DemoWeb2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2.Mvc;
using System.Web.Mvc;
using System.Threading;
using Google.Apis.Services;
using Google.Apis.Plus.v1;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DemoWeb2.Controllers
{
    public class UsersController : Controller
    {
        private DBSportStore1Entities database = new DBSportStore1Entities();

        // GET: Users
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (string.IsNullOrEmpty(cust.EmailCus))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(cust.PhoneCus))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được để trống");
                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa

                var khachhang = database.Customers.FirstOrDefault(k => k.NameCus == cust.NameCus);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tên này");
                if (ModelState.IsValid)
                {
                    database.Customers.Add(cust);
                    database.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer cust)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    var khachhang = database.Customers.FirstOrDefault(k => k.NameCus == cust.NameCus && k.PassCus == cust.PassCus);
                    if (khachhang != null)
                    {
                        Session["TaiKhoan"] = khachhang;
                        return RedirectToAction("Index", "CustomerProduct");
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        //[AllowAnonymous]
        //public ActionResult GoogleLogin()
        //{
        //    var cancellationToken = CancellationToken.None; // Tạo CancellationToken

        //    var result = new AuthorizationCodeMvcApp(this, new AppFlowMetadata())
        //        .AuthorizeAsync(cancellationToken) // Truyền CancellationToken vào đây
        //        .Result;

        //    if (result.Credential != null)
        //    {
        //        var service = new PlusService(new BaseClientService.Initializer
        //        {
        //            HttpClientInitializer = result.Credential,
        //            ApplicationName = "TestApp"
        //        });

        //        var me = service.People.Get("me").Execute();

        //        // Đăng ký người dùng nếu chưa đăng ký
        //        // Đặt phiên của người dùng, đăng nhập họ và chuyển hướng
        //        Session["user"] = me.Id;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        // Đăng nhập thất bại hoặc bị hủy
        //        return RedirectToAction("Login", "Account");
        //    }
        //}

        public ActionResult GoogleLoginCallback(string code)
        {
            if (code != null)
            {
                var client = new RestClient("https://www.googleapis.com/oauth2/v4/token");
                var request = new RestRequest();
                request.Method = Method.Post;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", code);
                request.AddParameter("redirect_uri", "https://localhost:59750/Login/GoogleLoginCallback");

                request.AddParameter("client_id", "1098593658235-tt8iff6m9tg8l2cfdngofo6j3phtc5dn.apps.googleusercontent.com");
                request.AddParameter("client_secret", "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");

                RestResponse response = client.Execute(request);
                var content = response.Content;
                var res = (JObject)JsonConvert.DeserializeObject(content);
                var client2 = new RestClient("https://www.googleapis.com/oauth2/v1/userinfo");
                client2.AddDefaultHeader("Authorization", "Bearer " + res["access_token"]);

                request.Method = Method.Get;

                var response2 = client2.Execute(request);

                var content2 = response2.Content;

                var user = (JObject)JsonConvert.DeserializeObject(content2);
                return RedirectToAction("Index", "CustomerProduct");

            }
            else
            {
                ViewBag.ReturnData = "";
            }


            return View();
        }
        public ActionResult Logout()
        {
            if (Session != null)
            {
                Session.Clear();
            }

            if (Response != null)
            {
                // Xóa cookie
                var authCookie = new HttpCookie("YourAuthCookieName");
                authCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(authCookie);
            }

            return RedirectToAction("Login");
        }
    }

}