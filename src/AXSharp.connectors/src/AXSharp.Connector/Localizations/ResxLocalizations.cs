using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using AXSharp.Connector;
using AXSharp.Connector.Localizations;
using AXSharp.Localizations;

namespace AXSharp.Connector.Localizations
{
    internal class ResxLocalizations
    {
        private readonly ResourceManager _resourceManager;

        public ResxLocalizations(Type resource)
        {
            _resourceManager = new ResourceManager(resource)
            {
                IgnoreCase = true
            };
        }

        internal static IEnumerable<string> GetTranslatable(string input,
            List<string> localizables = null)
        {
            localizables ??= new List<string>();

            if (string.IsNullOrEmpty(input)) return localizables;

            var position = 0;
            var recoveryPosition = 0;
            while (position < input.Length)
            {
                try
                {
                    position = input.IndexOf("<#", position);
                    var start = position;

                    if (position >= 0) recoveryPosition = position;

                    if (start >= 0)
                    {
                        position = input.IndexOf("#>", position);
                        if (position >= 0)
                        {
                            var end = position;

                            var localizableItem = input.Substring(start, end - start + 2);

                            if (!localizables.Contains(localizableItem)) localizables.Add(localizableItem);
                        }
                        else
                        {
                            position = recoveryPosition + 2;
                        }
                    }
                }
                catch
                {
                    // Ignore to prevent runtime errors.
                }

                if (position == -1) break;
            }

            return localizables;
        }

        private string LocalizeInParents(string token, ITwinElement rootObj, string translation = null)
        {
            var obj = rootObj?.GetParent();

            while (obj != null)
            {
                translation = obj.Translate(token);
                if (translation != null) return translation;

                obj = obj.GetParent();
            }

            return null;
        }

        public string Localize(string str, ITwinElement twinElement)
        {
            foreach (var localizable in GetTranslatable(str))
            {
                var validIdentifier = LocalizationHelper.CreateId(localizable.CleanUpLocalizationTokens());

                // Search in first level resource
                var translation = _resourceManager.GetString(validIdentifier);

                // Search in parent resources
                if (translation == null)
                {
                    try
                    {
                        return LocalizeInParents(str, twinElement);
                    }
                    catch
                    {
                        // Ignore to prevent runtime errors.
                    }
                }

                // Set key if not found anywhere
                translation ??= localizable;

                str = str.Replace(localizable, translation.CleanUpLocalizationTokens());
            }

            return str;
        }
    }
}
