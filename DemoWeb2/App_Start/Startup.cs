using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Configuration;
using System.Web.Mvc;

namespace DemoWeb2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Users/Login")
            });

            app.UseStageMarker(DefaultAuthenticationTypes.ExternalCookie);

            // Cấu hình xác thực bằng Google
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["GoogleClientId"],
                ClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"],
                CallbackPath = new PathString("/Login/GoogleLoginCallback")

            });

            // Cấu hình xác thực bằng Facebook
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "1756001991506584",
                AppSecret = "d4e852ca18e0643a41594e4045009c9",
                CallbackPath = new PathString("/Login/FacebookLoginCallback") // Đây là đường dẫn callback sau khi xác thực thành công
            });

            // Đảm bảo tất cả yêu cầu cần phải xác thực
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
        }
    }
}
