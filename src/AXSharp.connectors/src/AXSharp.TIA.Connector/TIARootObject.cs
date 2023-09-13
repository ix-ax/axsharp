using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.TIA2AXSharp
{
    public class TIARootObject
    {
        public List<TIABrowseElement> TIATwinObjects { get; set; } = new List<TIABrowseElement>();

    }
}
