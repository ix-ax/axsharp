// Ix.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ix.Localizations;

/// <summary>
///     Provides series of helper methods for working with localizations.
/// </summary>
public static class LocalizationHelper
{
    private static readonly string pattern = @"<#(.*)#>";
    private static readonly Regex rgx = new(pattern, RegexOptions.Compiled);

    /// <summary>
    ///     Retrieves localizables from a string.
    /// </summary>
    /// <param name="input">String from which the localizables should be retrieved</param>
    /// <param name="localizables">Pre-existing localizables.</param>
    /// <returns>List of localizables.</returns>
    public static List<LocalizableItem> GetLocalizables(string input, List<LocalizableItem> localizables = null)
    {
        return GetTranslatable(input, localizables).ToList();
    }

    internal static IEnumerable<LocalizableItem> GetTranslatable(string input,
        List<LocalizableItem> localizables = null)
    {
        localizables ??= new List<LocalizableItem>();

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

                        var localizableItem = new LocalizableItem { Key = input.Substring(start, end - start + 2) };

                        if (!localizables.Exists(p => p.Key == localizableItem.Key)) localizables.Add(localizableItem);
                    }
                    else
                    {
                        position = recoveryPosition + 2;
                        Console.WriteLine(
                            $"Missing localization end tag {input.Substring(start, input.Length - start)}");
                    }
                }
            }
            catch (Exception)
            {
                // Ignore to prevent runtime errors.
            }


            if (position == -1) break;
        }

        return localizables;
    }

    public static string CleanUpLocalizationTokens(this string localized)
    {
        if (localized != null) return localized.Replace("<#", string.Empty).Replace("#>", string.Empty);

        return string.Empty;
    }
}