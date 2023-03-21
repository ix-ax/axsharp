// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using AXSharp.Connector;
using AXSharp.Localizations.Abstractions;

namespace AXSharp.Localizations.Resx;

/// <summary>
///     Provides translation mechanism for Resx files.
/// </summary>
public class Translator : TranslatorBase
{
    private readonly ResxLocalizations _localizations;

    /// <summary>
    ///     Create new instance of <see cref="TranslatorBase" />
    /// </summary>
    public Translator()
    {
        _localizations = new ResxLocalizations();
    }

    /// <summary>
    ///     Creates new instance of translator.
    /// </summary>
    /// <param name="type">Type for which the localizations will be created.</param>
    public Translator(Type type)
    {
        _localizations = new ResxLocalizations(type);
    }

    /// <summary>
    ///     Get localization Id
    /// </summary>
    public override string TranslatorsId
    {
        get => _localizations?.Id;

        internal set => _localizations.Id = value;
    }

    /// <summary>
    ///     Translates
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override string Translate(string input)
    {
        return _localizations.Localize(input);
    }

    /// <summary>
    ///     Translates the input string using the resources of given object.
    /// </summary>
    /// <param name="input">String to translate</param>
    /// <param name="obj">Object associated with the localization.</param>
    /// <returns>Translated string.</returns>
    public override string Translate(string input, ITwinObject obj)
    {
        return _localizations.Localize(input, obj);
    }
}