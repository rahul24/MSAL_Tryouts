using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace Aspnet_WebAPI_MSAL_Client_Credential_Flow
{
    public class OpenIdConnectSecurityKeyProvider : IIssuerSecurityKeyProvider
    {
        private readonly ReaderWriterLockSlim _synclock = new ReaderWriterLockSlim();
        public ConfigurationManager<OpenIdConnectConfiguration> ConfigManager;
        private string _issuer;
        private ICollection<SecurityKey> _keys;

        public OpenIdConnectSecurityKeyProvider(string metadataEndpoint)
        {
            //enable tls is required otherwise throw error.
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            ConfigManager = new ConfigurationManager<OpenIdConnectConfiguration>(metadataEndpoint, new OpenIdConnectConfigurationRetriever());
        }


        public string Issuer
        {
            get
            {
                RetrieveMetadata();
                _synclock.EnterReadLock();
                try
                {
                    return _issuer;
                }
                finally
                {
                    _synclock.ExitReadLock();
                }
            }
        }

        public IEnumerable<SecurityKey> SecurityKeys
        {
            get
            {
                RetrieveMetadata();
                _synclock.EnterReadLock();
                try
                {
                    return _keys;
                }
                finally
                {
                    _synclock.ExitReadLock();
                }

            }
        }

        private void RetrieveMetadata()
        {

            _synclock.EnterWriteLock();
            try
            {
                //Self host then get configs from DAPI.
                //On IIS hosted then get from machine config.
                OpenIdConnectConfiguration configuration = ConfigManager.GetConfigurationAsync().Result;
                _issuer = configuration.Issuer;
                _keys = configuration.SigningKeys;
            }
            finally
            {
                _synclock.ExitWriteLock();
            }
        }
    }
}