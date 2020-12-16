using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aspnet_WebAPI_MSAL_Client_Credential_Flow
{
    public partial class Startup
    {

        private string clientId = "07915ac2-4a91-4498-b4d0-cd07eb7b618f";

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {

                AccessTokenFormat = new JwtFormat(
                        new TokenValidationParameters()
                        {
                            ValidAudiences = new[] { "07915ac2-4a91-4498-b4d0-cd07eb7b618f", $"api://{clientId}" },
                            //true if token from single tenant only.
                            ValidateIssuer = false
                        },
                        new OpenIdConnectSecurityKeyProvider("https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration")
                    )
            });
        }


    }
}