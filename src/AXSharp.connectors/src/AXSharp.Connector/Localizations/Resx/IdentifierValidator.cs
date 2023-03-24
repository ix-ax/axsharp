// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Text;
using System.Text.RegularExpressions;

namespace AXSharp.Localizations;

/// <summary>
///     Helper methods for creating localizations.
/// </summary>
public static class IdentifierValidator
{
    private const string start = @"(\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl}|\p{Nd})";
    private const string extend = @"(\p{Mn}|\p{Mc}|\p{Nd}|\p{Pc}|\p{Cf})";
    private static readonly Regex ident = new(string.Format("{0}({0}|{1})*", start, extend));

    internal static string MakeValidIdentifier(string name)
    {
        name = $"_{name}";

        var retVal = new StringBuilder();

        for (var index = 0; index < name.Length; index++)
        {
            var @char = name[index].ToString();

            retVal = retVal.Append(IsValidIdentifierCharacter(@char) ? @char : "_");
        }

        return retVal.ToString().Trim();
    }

    private static bool IsValidIdentifierCharacter(string @string)
    {
        @string = @string.Normalize();
        return ident.IsMatch(@string);
    }
}