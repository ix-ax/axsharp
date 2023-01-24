using System;

namespace Pocos
{
    public partial class MAIN
    {
        public string Hello_World { get; set; } = string.Empty;
        public Int16 cislo { get; set; }

        public Boolean boolValue { get; set; }

        public Double Position { get; set; }

        public stTest instanceOfstTest { get; set; } = new stTest();
        public stTest2 instanceOfstTest2 { get; set; } = new stTest2();
        public stTest3 instanceOfstTest3 { get; set; } = new stTest3();
        public stBlazor instanceOfstBlazor { get; set; } = new stBlazor();
        public stTest[] arr1 { get; set; }

        public Int16[] arr2 { get; set; }

        public DateOnly dateVar { get; set; } = default(DateOnly);
        public stComplex instanceOfstComplex { get; set; } = new stComplex();
        public stTestPrimitive instanceOfstPrimitive { get; set; } = new stTestPrimitive();
        public stMultipleLayouts instanceOfstMultipleLayouts { get; set; } = new stMultipleLayouts();
        public stLayouts instanceOfstMultipleLayouts2 { get; set; } = new stLayouts();
        public IxComponent instanceOfIxComponent { get; set; } = new IxComponent();
        public GroupBox_other groupBox_test { get; set; } = new GroupBox_other();
        public CU00x cu00 { get; set; } = new CU00x();
        public CUBase cuBase { get; set; } = new CUBase();
    }
}