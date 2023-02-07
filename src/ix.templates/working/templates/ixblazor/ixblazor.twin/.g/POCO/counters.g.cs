using System;

namespace Pocos
{
    public partial class Counters
    {
        public Counter wrap { get; set; } = new Counter();
        public Counter stack { get; set; } = new Counter();
        public Counter grid { get; set; } = new Counter();
        public Counter tabs { get; set; } = new Counter();
    }

    public partial class Counter
    {
        public UInt64 Counts { get; set; }

        public Boolean AllowCounter { get; set; }

        public Boolean ResetCounter { get; set; }
    }
}