using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace Vehicle2
{
    public partial class example : Ix.Connector.ITwinObject
    {
        [Container(Layout.Stack)]
        public test_primitive primitives_stack { get; }

        [Container(Layout.Wrap)]
        public test_primitive primitives_wrap { get; }

        [Container(Layout.Tabs)]
        public test_primitive primitives_tabs { get; }

        [Container(Layout.UniformGrid)]
        public test_primitive primitives_uniform { get; }

        [Container(Layout.Stack)]
        [Group(GroupLayout.GroupBox)]
        public test_primitive test_groupbox { get; }

        [Container(Layout.Stack)]
        [Group(GroupLayout.Border)]
        public test_primitive test_border { get; }

        [Container(Layout.Tabs)]
        [Group(GroupLayout.GroupBox)]
        public groupbox testgroupbox { get; }

        public border testborder { get; }

        public ixcomponent ixcomponent_instance { get; }

        public example(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            primitives_stack = new test_primitive(this, "primitives_stack", "primitives_stack");
            primitives_wrap = new test_primitive(this, "primitives_wrap", "primitives_wrap");
            primitives_tabs = new test_primitive(this, "primitives_tabs", "primitives_tabs");
            primitives_uniform = new test_primitive(this, "primitives_uniform", "primitives_uniform");
            test_groupbox = new test_primitive(this, "test_groupbox", "test_groupbox");
            test_border = new test_primitive(this, "test_border", "test_border");
            testgroupbox = new groupbox(this, "testgroupbox", "testgroupbox");
            testborder = new border(this, "testborder", "testborder");
            ixcomponent_instance = new ixcomponent(this, "ixcomponent_instance", "ixcomponent_instance");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        private IList<Ix.Connector.ITwinObject> Children { get; } = new List<Ix.Connector.ITwinObject>();
        public IEnumerable<Ix.Connector.ITwinObject> GetChildren()
        {
            return Children;
        }

        private IList<Ix.Connector.ITwinElement> Kids { get; } = new List<Ix.Connector.ITwinElement>();
        public IEnumerable<Ix.Connector.ITwinElement> GetKids()
        {
            return Kids;
        }

        private IList<Ix.Connector.ITwinPrimitive> ValueTags { get; } = new List<Ix.Connector.ITwinPrimitive>();
        public IEnumerable<Ix.Connector.ITwinPrimitive> GetValueTags()
        {
            return ValueTags;
        }

        public void AddValueTag(Ix.Connector.ITwinPrimitive valueTag)
        {
            ValueTags.Add(valueTag);
        }

        public void AddKid(Ix.Connector.ITwinElement kid)
        {
            Kids.Add(kid);
        }

        public void AddChild(Ix.Connector.ITwinObject twinObject)
        {
            Children.Add(twinObject);
        }

        protected Ix.Connector.Connector @Connector { get; }

        public Ix.Connector.Connector GetConnector()
        {
            return this.@Connector;
        }

        public string GetSymbolTail()
        {
            return this.SymbolTail;
        }

        public Ix.Connector.ITwinObject GetParent()
        {
            return this.@Parent;
        }

        public string Symbol { get; protected set; }

        public System.String AttributeName { get; set; }

        public string HumanReadable { get; set; }

        protected System.String @SymbolTail { get; set; }

        protected Ix.Connector.ITwinObject @Parent { get; set; }
    }
}