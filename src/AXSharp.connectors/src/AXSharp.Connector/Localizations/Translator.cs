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
        public Translator(Connector connector)
        {
            SetLocalizationResource(connector.GetType(), "Properties.PlcStringResources");
        }

        private ResxLocalizations LocalizationResources { get; set; }
        
        public string Translate(string originalString, ITwinElement twin)
        {
            return LocalizationResources?.Localize(originalString, twin);
        }

        public void SetLocalizationResource(Type twinControllerType, string resourceName)
        {
            var assemblyName = twinControllerType.Assembly.GetName().Name;
            var localizationType = twinControllerType.Assembly.GetType($"{assemblyName}.{resourceName}");
            if (localizationType != null)
            {
                LocalizationResources = new ResxLocalizations(localizationType);
            }
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
