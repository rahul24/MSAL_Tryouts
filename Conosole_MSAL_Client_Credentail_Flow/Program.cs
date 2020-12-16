using Conosole_MSAL_Client_Credentail_Flow.Util;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Conosole_MSAL_Client_Credentail_Flow
{
    class Program
    {
        static ConcurrentDictionary<string, byte[]> _cache = new ConcurrentDictionary<string, byte[]>();
        static void Main(string[] args)
        {
            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create("383e5fcf-269b-4bba-9a82-87df031e5f08")
                    .WithClientSecret("uwW9_4b_I0p.T2z1~AqWFMut2o-1VBOjEB")
                    .WithAuthority(new Uri("https://login.microsoftonline.com/AzurerTest.onmicrosoft.com"))
                    .Build();

            //caching
            //app.AppTokenCache.SetBeforeAccess((e)=> 
            //{
            //    if (_cache.ContainsKey(e.SuggestedCacheKey))
            //    {                 
            //        e.TokenCache.DeserializeMsalV3(_cache[e.SuggestedCacheKey]);
            //    }

            //});
            //app.AppTokenCache.SetAfterAccess((e)=>
            //{
            //    if (!_cache.ContainsKey(e.SuggestedCacheKey))
            //        _cache.GetOrAdd(e.SuggestedCacheKey, e.TokenCache.SerializeMsalV3());
                
            //});

            string[] scopes = new string[] { "api://07915ac2-4a91-4498-b4d0-cd07eb7b618f/.default" };

            var r  = app.AcquireTokenForClient(scopes).ExecuteAsync().GetAwaiter().GetResult();

            string token = r.AccessToken;

            
            HttpClient client = new HttpClient();

            var apiCaller = new ProtectedApiCallHelper(client);

            apiCaller.CallWebApiAndProcessResultASync($"https://localhost:44388/api/values", token, Display).GetAwaiter().GetResult();
        }

        private static void Display(IEnumerable<JObject> result)
        {
            Console.WriteLine("Web Api result: \n");

            foreach (var item in result)
            {
                foreach (JProperty child in item.Properties().Where(p => !p.Name.StartsWith("@")))
                {
                    Console.WriteLine($"{child.Name} = {child.Value}");
                }

                Console.WriteLine("");
            }
        }
    }
}
