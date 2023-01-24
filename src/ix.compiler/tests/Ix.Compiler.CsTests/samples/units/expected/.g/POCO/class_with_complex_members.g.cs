using System;

namespace Pocos
{
    namespace ClassWithComplexTypesNamespace
    {
        public partial class ClassWithComplexTypes
        {
            public ClassWithComplexTypesNamespace.ComplexType1 myComplexType { get; set; } = new ClassWithComplexTypesNamespace.ComplexType1();
        }

        public partial class ComplexType1
        {
        }
    }
}