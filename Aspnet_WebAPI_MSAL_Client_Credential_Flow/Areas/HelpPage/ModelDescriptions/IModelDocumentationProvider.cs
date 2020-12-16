using System;
using System.Reflection;

namespace Aspnet_WebAPI_MSAL_Client_Credential_Flow.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}