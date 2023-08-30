//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Google.Apis.Auth.OAuth2.Mvc;
//using System.Threading;
//using System.Web.Mvc;
//using Google.Apis.Auth.OAuth2.Flows;
//using Google.Apis.Auth.OAuth2;
//using System.Configuration;
//using Google.Apis.Util.Store;

//namespace DemoWeb2.Models
//{
//    public class AppFlowMetadata : FlowMetadata
//    {
//        private static readonly IAuthorizationCodeFlow flow =
//            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
//            {
//                ClientSecrets = new ClientSecrets
//                {
//                    ClientId = ConfigurationManager.AppSettings["GoogleClientId"],
//                    ClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"]
//                },
//                Scopes = new[] { "openid", "email", "profile" },
//                DataStore = new FileDataStore("Store")
//            });

//        public override string GetUserId(Controller controller)
//        {
//            var user = controller.Session["user"];
//            return user?.ToString();
//        }

//        public override IAuthorizationCodeFlow Flow => flow;
//    }
//}