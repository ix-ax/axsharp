using System;

namespace Pocos
{
    public partial class stComplexUnknown
    {
        public string stComplexUnknownString { get; set; } = string.Empty;
        public Boolean testBool { get; set; }

        public Int16 stComplexUnknowInteger { get; set; }

        public DateOnly TestDate { get; set; } = default(DateOnly);
    }
}