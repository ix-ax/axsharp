using System;

namespace Pocos
{
    public partial class example
    {
        public test_primitive primitives_stack { get; set; } = new test_primitive();
        public test_primitive primitives_wrap { get; set; } = new test_primitive();
        public test_primitive primitives_tabs { get; set; } = new test_primitive();
        public test_primitive primitives_uniform { get; set; } = new test_primitive();
        public test_primitive test_groupbox { get; set; } = new test_primitive();
        public test_primitive test_border { get; set; } = new test_primitive();
        public groupbox testgroupbox { get; set; } = new groupbox();
        public border testborder { get; set; } = new border();
    }
}