using System;

namespace Pocos
{
    namespace Generics
    {
        public partial class Extender : AXSharp.Connector.IPlain
        {
        }

        public partial class Extendee : Generics.Extender, AXSharp.Connector.IPlain
        {
            public Generics.SomeType SomeType { get; set; } = new Generics.SomeType();
            public Generics.SomeType SomeTypeAsPoco { get; set; } = new Generics.SomeType();
        }

        public partial class Extendee2 : Generics.Extender, AXSharp.Connector.IPlain
        {
            public Generics.SomeType SomeType { get; set; } = new Generics.SomeType();
        }

        public partial class SomeType : AXSharp.Connector.IPlain
        {
        }
    }
}