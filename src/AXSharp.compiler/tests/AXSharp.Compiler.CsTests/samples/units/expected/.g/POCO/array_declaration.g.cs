using System;

namespace Pocos
{
    namespace ArrayDeclarationSimpleNamespace
    {
        public partial class array_declaration_class : AXSharp.Connector.IPlain
        {
            public Int16[] primitive { get; set; } = new Int16[100];
            public ArrayDeclarationSimpleNamespace.some_complex_type[] complex { get; set; } = new ArrayDeclarationSimpleNamespace.some_complex_type[100];
        }

        public partial class some_complex_type : AXSharp.Connector.IPlain
        {
        }
    }
}