using System;

namespace Pocos
{
    public partial class testingProgram
    {
        public stTestPrimitive testPrimitive { get; set; } = new stTestPrimitive();
        public stTestComplex testComplex { get; set; } = new stTestComplex();
        public global::stTestEnum testEnum { get; set; }

        public stTestRenderIgnore testRenderIgnore { get; set; } = new stTestRenderIgnore();
        public TestEmpty testEmpty { get; set; } = new TestEmpty();
        public TestLayoutOverwriting testLayoutOverwrite { get; set; } = new TestLayoutOverwriting();
        public TestMixed testMixed { get; set; } = new TestMixed();
        public TestMultipleLayouts testMultipleLayouts { get; set; } = new TestMultipleLayouts();
        public TestSimple testSimple { get; set; } = new TestSimple();
        public TestWithoutLayouts testWithoutLayouts { get; set; } = new TestWithoutLayouts();
        public TestSimpleNested testSimpleNested { get; set; } = new TestSimpleNested();
        public stTestMultipleNested testMultipleNested { get; set; } = new stTestMultipleNested();
        public stTestLayouts testLayouts { get; set; } = new stTestLayouts();
    }
}