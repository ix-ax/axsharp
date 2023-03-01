using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ix.ixr_doc
{
    public class LocalizedStringWrapper
    {
        // composite object with location and raw value
        public Dictionary<string, StringValueWrapper> LocalizedStringsDictionary {get; private set; }
        private Regex _localizedStringRegex;
        private Regex _attributeNameRegex;
        public LocalizedStringWrapper()
        {
            LocalizedStringsDictionary = new Dictionary<string, StringValueWrapper>();
            _localizedStringRegex = new Regex("<#(.*?)#>",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

             _attributeNameRegex = new Regex("#ix-set:AttributeName",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);


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
                     //unknown
                    sb.Append('_'); 
                }
            }
            return sb.ToString();
    
          
        }

        public bool IsValidId(string id) 
        {
            return Microsoft.CodeAnalysis.CSharp.SyntaxFacts.IsValidIdentifier(id);
        }
        public IEnumerable<string> TryToGetLocalizedStrings(string text)
        { 
            //match only text within <# #>
            var matches = _localizedStringRegex.Matches(text).ToList();
            if(matches.Count > 0) 
            {
                return matches.Select(m => m.Value);;
            }
        
            return null; 
        
        }
        public string GetRawTextFromLocalizedString(string text)
        {
           // deletes <# from beginning and #> from the end
           return text.Substring(2,text.Length-4);
    
        }

        public bool IsAttributeNamePragmaToken(string text)
        {
            return _attributeNameRegex.IsMatch(text);
        }
    }
    

    public class StringValueWrapper
    {
        public StringValueWrapper(string rawValue, string fileName, int line)
        {
            RawValue = rawValue;
            FileName = fileName;
            Line = line;
        }

        public string RawValue { get; set; }
        public string FileName { get; set; }
        public int Line {get; set;}
    }
}
