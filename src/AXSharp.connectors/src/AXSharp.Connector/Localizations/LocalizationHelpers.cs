using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Localizations
{
    public static class LocalizationHelper
    {
        public static string CreateId(string rawText)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in rawText)
            {
                //try to find character in mappings
                var mapchar = TryToGetValueByChar(c);
                if (mapchar != null)
                {
                    sb.Append(mapchar);
                }
                //add only if is letter
                else if (Char.IsLetter(c))
                {
                    sb.Append(c);
                }
                else
                {
                    //unknown
                    sb.Append('_');
                }
            }
            return sb.ToString();


        }

        public static Dictionary<char, string> Mappings = new Dictionary<char, string>()
        {
            {' ', "_"},
            {'!', "_EXCLAMATIONMARK_"},
            {'"', "_QUOTATIONMARKS_"},
            {'#', "_HASHTAG_"},
            {'$', "_DOLLAR_"},
            {'%', "_PERCENT_"},
            {'&', "_AMPERSAND_"},
            {'\'', "_APOSTROPHE_"},
            {'(', "_BRACKET_"},
            {')', "_ENDBRACKET_"},
            {'*', "_ASTERISK_"},
            {'+', "_PLUS_"},
            {',', "_COMMA_"},
            {'-', "_DASH_"},
            {'.', "_DOT_"},
            {'/', "_SLASH_"},
            {'0', "_ZERO_"},
            {'1', "_ONE_"},
            {'2', "_TWO_"},
            {'3', "_THREE_"},
            {'4', "_FOUR_"},
            {'5', "_FIVE_"},
            {'6', "_SIX_"},
            {'7', "_SEVEN_"},
            {'8', "_EIGHT_"},
            {'9', "_NINE_"},
            {':', "_COLON_"},
            {';', "_SEMICOLON_"},
            {'<', "_LESSTHAN_"},
            {'=', "_EQUALS_"},
            {'>', "_GREATERTHAN_"},
            {'?', "_QUESTIONMARK_"},
            {'@', "_ATSIGN_"},
            {'[', "_SQUAREBRACKET_"},
            {'\\', "_BACKSLASH_"},
            {']', "_ENDSQUAREBRACKET_"},
            {'^', "_CARET_"},
            {'_', "_UNDERSCORE_"},
            {'`', "_BACKTICK_"},
            {'{', "_CURLYBRACKET_"},
            {'|', "_PIPE_"},
            {'}', "_ENDCURLYBRACKET_"},
            {'~', "_TILDE_"},
            {'€', "_EURO_"},
        };
        public static string TryToGetValueByChar(char key)
        {
            Mappings.TryGetValue(key, out var value);
            return value;
        }

        public static string CleanUpLocalizationTokens(this string localized)
        {
            if (localized != null) return localized.Replace("<#", string.Empty).Replace("#>", string.Empty);

            return string.Empty;
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
    }
}
