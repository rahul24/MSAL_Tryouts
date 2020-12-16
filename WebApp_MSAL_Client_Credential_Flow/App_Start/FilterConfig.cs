using System.Web;
using System.Web.Mvc;

namespace WebApp_MSAL_Client_Credential_Flow
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
