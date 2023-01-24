using System;

namespace Pocos
{
    namespace ArrayDeclarationSimpleNamespace
    {
        public partial class array_declaration_class
        {
            public Int16[] primitive { get; set; }

            public ArrayDeclarationSimpleNamespace.some_complex_type[] complex { get; set; }
        }

        public partial class some_complex_type
        {
        }
    }
}