using System;

namespace Pocos
{
    namespace ClassWithPragmasNamespace
    {
        public partial class ClassWithPragmas
        {
            public ClassWithPragmasNamespace.ComplexType1 myComplexType { get; set; } = new ClassWithPragmasNamespace.ComplexType1();
        }

        public partial class ComplexType1
        {
        }
    }
}