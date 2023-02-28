using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixr_doc
{
    public static class SymbolMappings
    {

        public static Dictionary<char, string> Mappings = new Dictionary<char, string>()
        {
	        {' ', "_"},
            {',', "_COMMA_"},
            {'.', "_DOT_"},
            {':', "_COLON_"},
            {';', "_SEMICOLON_"},
            {'-', "_DASH_"},
            {'?', "_QMARK_"},
            {'!', "_EXCLAMATION_"},
	        {'1', "_ONE_"},
            {'2', "_TWO_"},
            {'3', "_THREE_"},
            {'4', "_FOUR_"},
            {'5', "_FIVE_"},
            {'6', "_SIX_"},
            {'7', "_SEVEN_"},
            {'8', "_EIGHT_"},
            {'9', "_NINE_"},
            {'0', "_ZERO_"},
	       
        };

        public static string TryToGetValueByChar(char key)
        { 
            Mappings.TryGetValue(key, out var value);
            return value;
        }
    }
}
