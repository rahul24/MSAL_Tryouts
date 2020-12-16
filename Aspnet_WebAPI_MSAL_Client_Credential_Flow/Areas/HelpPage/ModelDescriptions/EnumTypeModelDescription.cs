using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aspnet_WebAPI_MSAL_Client_Credential_Flow.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}