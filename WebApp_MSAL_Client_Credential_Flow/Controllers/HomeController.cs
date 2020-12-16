using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebApp_MSAL_Client_Credential_Flow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create("383e5fcf-269b-4bba-9a82-87df031e5f08")
                .WithRedirectUri("https://localhost:44373/")
                .WithAuthority(new Uri("https://login.microsoftonline.com/AzurerTest.onmicrosoft.com"))
                .WithClientSecret("uwW9_4b_I0p.T2z1~AqWFMut2o-1VBOjEB")
                .Build();

            string[] scopes = new string[] { "api://07915ac2-4a91-4498-b4d0-cd07eb7b618f/.default" };

            var r = app.AcquireTokenForClient(scopes).ExecuteAsync().GetAwaiter().GetResult();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",r.AccessToken);
            var response =client.GetAsync("https://localhost:44350/weatherforecast").GetAwaiter().GetResult();

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
    }
}