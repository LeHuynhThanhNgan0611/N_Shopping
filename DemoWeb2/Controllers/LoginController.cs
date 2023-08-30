using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GoogleAuthentication.Services;

namespace DemoWeb2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GoogleLoginCallback(string code)
        {
            var clientId = "1098593658235-tt8iff6m9tg8l2cfdngofo6j3phtc5dn.apps.googleusercontent.com";
            var url = "https://localhost:59750/Login/GoogleLoginCallback";
            var clientsecret = "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";
            var token = await GoogleAuth.GetAuthAccessToken(code, clientId, clientsecret, url);
            var userProfile = await GoogleAuth.GetProfileResponseAsync(token.AccessToken.ToString());

            return View();
        }
    }
}