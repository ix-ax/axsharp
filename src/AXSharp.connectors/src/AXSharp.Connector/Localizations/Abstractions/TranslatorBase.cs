// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using AXSharp.Connector;
using AXSharp.Localizations.Resx;

namespace AXSharp.Localizations.Abstractions;

/// <summary>
///     Abstract class defines string translator.
/// </summary>
public abstract class TranslatorBase
{
    private static readonly IList<TranslatorBase> Translators = new List<TranslatorBase>();

    /// <summary>
    ///     Creates new instance of <see cref="TranslatorBase" />
    /// </summary>
    protected TranslatorBase()
    {
    }


    /// <summary>
    ///     Gets the id of this translator.
    /// </summary>
    public abstract string TranslatorsId { get; internal set; }


    /// <summary>
    ///     Translates the string.
    /// </summary>
    /// <param name="input">String to translate</param>
    /// <returns>Translated string.</returns>
    public abstract string Translate(string input);

    /// <summary>
    ///     Translates the string that is associated with given see <see cref="ITwinObject" />.
    /// </summary>
    /// <param name="input">String to translate.</param>
    /// <param name="obj">Object associated with the string to be translated.</param>
    /// <returns>Translated string.</returns>
    public abstract string Translate(string input, ITwinObject obj);

    /// <summary>
    ///     Gets translator with no association with specific library.
    /// </summary>
    /// <returns>Non associated translator.</returns>
    public static TranslatorBase Get()
    {
        var translator = Translators.AsQueryable().FirstOrDefault(p => p.TranslatorsId == null);
        return translator ?? new Translator();
    }


    /// <summary>
    ///     Gets translator associated with a specific assembly.
    /// </summary>
    /// <param name="type">Type for which the translator will be retrieved.</param>
    /// <returns>Translator associated with given type.</returns>
    public static TranslatorBase Get(Type type)
    {
        var translator = Translators.AsQueryable().FirstOrDefault(p => p.TranslatorsId == type.AssemblyQualifiedName);
        return translator ?? new Translator(type);
    }

    /// <summary>
    ///     Sets applications culture.
    /// </summary>
    /// <param name="culture">Culture to used by the application.</param>
    public static void LoadDictionaries(CultureInfo culture)
    {
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}