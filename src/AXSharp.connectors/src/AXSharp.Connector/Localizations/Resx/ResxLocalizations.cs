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

namespace AXSharp.Localizations.Abstractions;

internal class ResxLocalizations : ILocalizer
{
    private static readonly IList<ResxLocalizations> Localizers = new List<ResxLocalizations>();

    private readonly ResourceManager _resourceManager;

    public ResxLocalizations()
    {
    }

    public ResxLocalizations(Type resource)
    {
        Id = resource.FullName;
        _resourceManager = new ResourceManager(resource);
        _resourceManager.IgnoreCase = true;
        AddLocalizer(this);
    }

    internal string Id { get; set; }

    public string Localize(string str)
    {
        var localizables = LocalizationHelper.GetTranslatable(str);

        var translationStringBuilder = new StringBuilder(str);


        foreach (var localizable in localizables)
        {
            string translation = null;
            var validIdentifier = IdentifierValidator.MakeValidIdentifier(localizable.Key);
            // Search in first level resource
            if (_resourceManager != null) translation = _resourceManager?.GetString(validIdentifier);

            // Search in all other resources
            if (translation == null) translation = LocalizeTokenInAnyOther(validIdentifier);

            // Set key if not found anywhere
            translation = translation == null ? localizable.Key : translation;

            var trans = LocalizationHelper.CleanUpLocalizationTokens(translation);
            translationStringBuilder = translationStringBuilder.Replace(localizable.Key, trans);
        }

        return translationStringBuilder.ToString();
    }


    private void AddLocalizer(ResxLocalizations localizations)
    {
        if (!Localizers.Any(p => p.Id == localizations.Id)) Localizers.Add(localizations);
    }

    private string LocalizeTokenInAnyOther(string token)
    {
        var translation = token;
        foreach (var localizer in Localizers.Where(p => p.Id != Id))
        {
            translation = localizer._resourceManager.GetString(token);
            if (translation != null && translation != token) return translation;
        }

        return null;
    }

    private string LocalizeInParents(string token, ITwinObject rootObj, string translation = null)
    {
        var obj = rootObj?.GetParent();

        if (obj != null)
        {
            var asseblyName = obj?.GetType().Assembly.GetName().Name;
            var localizationType = obj?.GetType().Assembly.GetType($"{asseblyName}.Properties.Localizations");

            if (localizationType != null) translation = TranslatorBase.Get(localizationType).Translate(token);

            if (translation == null) LocalizeInParents(token, rootObj, translation);

            return translation;
        }

        return null;
    }

    public string Localize(string str, ITwinObject obj)
    {
        var localizables = LocalizationHelper.GetTranslatable(str);
        string translation = null;

        var translationStringBuilder = new StringBuilder(str);


        foreach (var localizable in localizables)
        {
            translation = null;
            var validIdentifier = IdentifierValidator.MakeValidIdentifier(localizable.Key);
            // Search in first level resource
            if (_resourceManager != null) translation = _resourceManager?.GetString(validIdentifier);

            // Search in all other resources
            if (translation == null)
                try
                {
                    return LocalizeInParents(str, obj);
                }
                catch (Exception)
                {
                    // Ignore to prevent runtime errors.
                }

            // Set key if not found anywhere
            translation = translation == null ? localizable.Key : translation;

            var trans = LocalizationHelper.CleanUpLocalizationTokens(translation);
            translationStringBuilder = translationStringBuilder.Replace(localizable.Key, trans);
        }

        return translationStringBuilder.ToString();
    }
}