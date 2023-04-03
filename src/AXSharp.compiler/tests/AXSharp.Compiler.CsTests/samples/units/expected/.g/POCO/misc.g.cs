using System;

namespace Pocos
{
    namespace Enums
    {
        public partial class ClassWithEnums : AXSharp.Connector.IPlain
        {
            public global::Enums.Colors colors { get; set; }

            public String NamedValuesColors { get; set; }
        }
    }

    namespace misc
    {
        public partial class VariousMembers : AXSharp.Connector.IPlain
        {
            public misc.SomeClass _SomeClass { get; set; } = new misc.SomeClass();
            public misc.Motor _Motor { get; set; } = new misc.Motor();
        }

        public partial class SomeClass : AXSharp.Connector.IPlain
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

    namespace UnknownArraysShouldNotBeTraspiled
    {
        public partial class ClassWithArrays : AXSharp.Connector.IPlain
        {
            public UnknownArraysShouldNotBeTraspiled.Complex[] _complexKnown { get; set; } = new UnknownArraysShouldNotBeTraspiled.Complex[11];
            public Byte[] _primitive { get; set; } = new Byte[11];
        }

        public partial class Complex : AXSharp.Connector.IPlain
        {
            public string HelloString { get; set; } = string.Empty;
            public UInt64 Id { get; set; }
        }
    }
}