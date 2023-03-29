// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using AXSharp.Connector;
using AXSharp.Connector.Localizations;

namespace AXSharp.Localizations;

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

    private string LocalizeInParents(string token, ITwinElement rootObj, string translation = null)
    {
        ITwinObject obj = rootObj?.GetParent();

        if (obj != null)
        {
            translation = obj.Translate(token);
            if (translation == null) LocalizeInParents(token, rootObj, translation);

            return translation;
        }

        return null;
    }

    public string Localize(string str, ITwinElement twinElement)
    {
        string translation = null;

        var sb = new StringBuilder(str);


        foreach (var localizable in LocalizationHelper.GetTranslatable(str).ToArray())
        {
            var validIdentifier = LocalizationHelper.CreateId(localizable.Key);
         
            // Search in first level resource
            if (_resourceManager != null) translation = _resourceManager?.GetString(validIdentifier);

            // Search in parent resources
            if (translation == null)
                try
                {
                    return LocalizeInParents(str, twinElement);
                }
                catch (Exception)
                {
                    // Ignore to prevent runtime errors.
                }

            // Set key if not found anywhere
            translation ??= localizable.Key;

            sb = sb.Replace(localizable.Key, translation.CleanUpLocalizationTokens());
        }

        return sb.ToString();
    }
}