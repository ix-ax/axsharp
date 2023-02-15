using System;

namespace Pocos
{
    namespace misc
    {
        public partial class VariousMembers
        {
            public misc.SomeClass _SomeClass { get; set; } = new misc.SomeClass();
            public misc.Motor _Motor { get; set; } = new misc.Motor();
        }

        public partial class SomeClass
        {
            public string SomeClassVariable { get; set; } = string.Empty;
        }

        public partial class Motor
        {
            public Boolean isRunning { get; set; }
        }

        public partial class Vehicle
        {
            public misc.Motor m { get; set; } = new misc.Motor();
            public Int16 displacement { get; set; }
        }
    }
}