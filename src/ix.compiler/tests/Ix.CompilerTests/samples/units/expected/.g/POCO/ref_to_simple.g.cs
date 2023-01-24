using System;

namespace Pocos
{
    namespace RefToSimple
    {
        public partial class ref_to_simple
        {
            public Int16 a { get; set; } = new Int16();
            public RefToSimple.referenced b { get; set; } = new RefToSimple.referenced();
        }

        public partial class referenced
        {
            public Int16 b { get; set; }
        }
    }
}