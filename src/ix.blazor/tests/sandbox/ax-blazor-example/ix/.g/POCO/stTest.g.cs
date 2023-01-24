using System;

namespace Pocos
{
    public partial class stTest
    {
        public Int16 p1 { get; set; }

        public Int16 p2 { get; set; }

        public stTest3 stTest3Struct { get; set; } = new stTest3();
        public DateOnly DateVar2 { get; set; } = default(DateOnly);
        public stComplex complexInstanceNested { get; set; } = new stComplex();
    }
}