using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DemoWeb2.Models;
using Newtonsoft.Json;

namespace DemoWeb2.Controllers
{
    public class LoginController : Controller
    {
        private DBSportStore1Entities database = new DBSportStore1Entities();
        private readonly string clientId = "1098593658235-tt8iff6m9tg8l2cfdngofo6j3phtc5dn.apps.googleusercontent.com";
        private readonly string clientSecret = "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";
        private readonly string redirectUri = "https://localhost:44354/Login/GoogleLoginCallback";

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var state = Guid.NewGuid().ToString();
            var authorizeUrl = string.Format("https://accounts.google.com/o/oauth2/auth?client_id=1098593658235-tt8iff6m9tg8l2cfdngofo6j3phtc5dn.apps.googleusercontent.com&redirect_uri=https://localhost:44354/Login/GoogleLoginCallback&response_type=code&scope=email profile&state={2}", clientId, redirectUri, state);
            return Redirect(authorizeUrl);
        }

        public ActionResult GoogleLoginCallback(string code, string state)
        {
            var tokenUrl = "https://accounts.google.com/o/oauth2/token";
            var userInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

            var parameters = new Dictionary<string, string>
            {
                {"code", code},
                {"client_id", clientId},
                {"client_secret", clientSecret},
                {"redirect_uri", redirectUri},
                {"grant_type", "authorization_code"}
            };

            var tokenResponse = HttpPost(tokenUrl, parameters);
            var accessToken = tokenResponse["access_token"];

            var userInfoResponse = HttpGet(userInfoUrl, accessToken);
            var email = userInfoResponse["email"];
            var name = userInfoResponse["name"];
            var googleId = userInfoResponse["id"];

            // Kiểm tra xem người dùng đã tồn tại dựa trên email
            var existingCustomer = database.Customers.FirstOrDefault(c => c.EmailCus == email);

            if (existingCustomer != null)
            {
                var googleUser = database.Customers.FirstOrDefault(k => k.GoogleId == googleId);
                if (googleUser != null)
                {
                    Session["TaiKhoan"] = googleUser;
                    return RedirectToAction("Index", "CustomerProduct");
                }

                return RedirectToAction("Index", "CustomerProduct");
            }
            else
            {
                // Thêm người dùng mới vào cơ sở dữ liệu
                var customer = new Customer
                {
                    NameCus = name,
                    EmailCus = email,
                    GoogleId = googleId,
                    PassCus = "GoogleUser" + googleId // Tạm thời đặt mật khẩu

                    // Các thông tin khác tùy theo cấu trúc của bảng Customer
                };
                try
                {
                    database.Customers.Add(customer);
                    database.SaveChanges();
                    return RedirectToAction("Index", "CustomerProduct");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            // Log or handle the validation error messages
                            Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                    // Handle the exception or show an error message to the user
                    return View();

                }
            }
        }

        // ... Các phần mã khác không thay đổi

        private Dictionary<string, string> HttpPost(string url, Dictionary<string, string> parameters)
        {
            using (var client = new WebClient())
            {
                var parameterCollection = new NameValueCollection();
                foreach (var entry in parameters)
                {
                    parameterCollection.Add(entry.Key, entry.Value);
                }

                var response = client.UploadValues(url, "POST", parameterCollection);

                var result = Encoding.ASCII.GetString(response);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            }
        }

        private Dictionary<string, string> HttpGet(string url, string accessToken)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Authorization", "Bearer " + accessToken);
                var response = client.DownloadString(url);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            }
        }
    }
}
