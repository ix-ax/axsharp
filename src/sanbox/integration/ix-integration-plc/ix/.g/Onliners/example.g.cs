using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

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

    public MySecondNamespace.ixcomponent ixcomponent_instance2 { get; }

    public ThirdNamespace.ixcomponent ixcomponent_instance3 { get; }

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
        ixcomponent_instance2 = new MySecondNamespace.ixcomponent(this, "ixcomponent_instance2", "ixcomponent_instance2");
        ixcomponent_instance3 = new ThirdNamespace.ixcomponent(this, "ixcomponent_instance3", "ixcomponent_instance3");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public Pocos.example OnlineToPlain()
    {
        Pocos.example plain = new Pocos.example();
        plain.primitives_stack = primitives_stack.OnlineToPlain();
        plain.primitives_wrap = primitives_wrap.OnlineToPlain();
        plain.primitives_tabs = primitives_tabs.OnlineToPlain();
        plain.primitives_uniform = primitives_uniform.OnlineToPlain();
        plain.test_groupbox = test_groupbox.OnlineToPlain();
        plain.test_border = test_border.OnlineToPlain();
        plain.testgroupbox = testgroupbox.OnlineToPlain();
        plain.testborder = testborder.OnlineToPlain();
        plain.ixcomponent_instance = ixcomponent_instance.OnlineToPlain();
        plain.ixcomponent_instance2 = ixcomponent_instance2.OnlineToPlain();
        plain.ixcomponent_instance3 = ixcomponent_instance3.OnlineToPlain();
        return plain;
    }

    public void PlainToOnline(Pocos.example plain)
    {
        this.primitives_stack.PlainToOnline(plain.primitives_stack);
        this.primitives_wrap.PlainToOnline(plain.primitives_wrap);
        this.primitives_tabs.PlainToOnline(plain.primitives_tabs);
        this.primitives_uniform.PlainToOnline(plain.primitives_uniform);
        this.test_groupbox.PlainToOnline(plain.test_groupbox);
        this.test_border.PlainToOnline(plain.test_border);
        this.testgroupbox.PlainToOnline(plain.testgroupbox);
        this.testborder.PlainToOnline(plain.testborder);
        this.ixcomponent_instance.PlainToOnline(plain.ixcomponent_instance);
        this.ixcomponent_instance2.PlainToOnline(plain.ixcomponent_instance2);
        this.ixcomponent_instance3.PlainToOnline(plain.ixcomponent_instance3);
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