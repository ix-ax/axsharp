using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Localizations;

namespace AXSharp.Connector.Localizations
{
    public class Translator
    {
        private ResxLocalizations LocalizationResources { get; set; }
        
        public string Translate(string originalString, ITwinElement twin)
        {
            if (LocalizationResources == null)
            {
                return originalString.CleanUpLocalizationTokens();
            }

            return LocalizationResources?.Localize(originalString, twin);
        }

        public void SetLocalizationResource(Type resourceType)
        {
            if (resourceType != null)
            {
                LocalizationResources = new ResxLocalizations(resourceType);
            }
        }
    }
}
