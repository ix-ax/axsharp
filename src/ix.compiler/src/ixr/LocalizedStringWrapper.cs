using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ix.ixr_doc
{
    public class LocalizedStringWrapper
    {

        public Dictionary<string, string> LocalizedStringsDictionary {get; private set; }
        public LocalizedStringWrapper()
        {
            LocalizedStringsDictionary = new Dictionary<string, string>();
        }

        public string CreateId(string rawText)
        { 
            StringBuilder sb = new StringBuilder();
            foreach (var c in rawText)
            {
                //try to find character in mappings
                var mapchar = SymbolMappings.TryToGetValueByChar(c);
                if(mapchar != null)
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
                    // TODO what to do here?
                    //unknown
                }
            }
            return sb.ToString();
    
          
        }

        public bool IsValidId(string id) 
        {
            return Microsoft.CodeAnalysis.CSharp.SyntaxFacts.IsValidIdentifier(id);
        }
        public string TryToGetLocalizedString(string text)
        { 
            //match only text within <# #>
            Regex rx = new Regex("<#(.*)#>",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var match = rx.Match(text);
            if(match.Success) 
            {
                return match.Value;
            }
        
            return null; 
        
        }
        public string GetRawTextFromLocalizedString(string text)
        {
           // deletes <# from beginning and #> from the end
           return text.Substring(2,text.Length-4);
    
        }
    }
}
