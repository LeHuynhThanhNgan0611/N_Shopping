//using Microsoft.AspNet.Identity;
//using Microsoft.Owin;
//using Microsoft.Owin.Extensions;
//using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin.Security.Google;
//using Owin;
//using System.Configuration;

//namespace DemoWeb2
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            app.UseCookieAuthentication(new CookieAuthenticationOptions
//            {
//                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
//                LoginPath = new PathString("/Account/Login")
//            });

//            app.UseStageMarker(DefaultAuthenticationTypes.ExternalCookie);

//            // Cấu hình xác thực bằng Google
//            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
//            {
//                ClientId = ConfigurationManager.AppSettings["GoogleClientId"],
//                ClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"],
//                CallbackPath = new PathString("/signin-google")
//            });


//        }
//    }
//}
