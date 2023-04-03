using System;

namespace Pocos
{
    namespace RefToSimple
    {
        public partial class ref_to_simple : AXSharp.Connector.IPlain
        {
        }

        public partial class referenced : AXSharp.Connector.IPlain
        {
            public Int16 b { get; set; }
        }
    }
}