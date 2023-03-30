// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/dev/notices.md

using AXSharp.Localizations;

namespace AXSharp.Connector.Localizations;

public static class TranslatorExtension
{
    public static string Translate(this ITwinElement twin, string originalString)
    {
        originalString ??= string.Empty;
        if (!originalString.Contains("<#"))
        {
            return originalString;
        }

        var translated = twin?.Interpreter?.Translate(originalString, twin) ?? originalString;
        return translated.CleanUpLocalizationTokens();
    }
}