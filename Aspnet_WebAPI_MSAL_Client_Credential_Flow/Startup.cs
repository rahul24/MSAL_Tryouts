using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Aspnet_WebAPI_MSAL_Client_Credential_Flow.Startup))]
namespace Aspnet_WebAPI_MSAL_Client_Credential_Flow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}