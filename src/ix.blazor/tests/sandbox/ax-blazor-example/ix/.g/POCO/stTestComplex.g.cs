using System;

namespace Pocos
{
    public partial class stTestComplex
    {
        public Int16 testInteger { get; set; }

        public stComplex testComplexInstance { get; set; } = new stComplex();
        public string testString { get; set; } = string.Empty;
        public stComplexUnknown testComplexUnknownInstance { get; set; } = new stComplexUnknown();
    }
}