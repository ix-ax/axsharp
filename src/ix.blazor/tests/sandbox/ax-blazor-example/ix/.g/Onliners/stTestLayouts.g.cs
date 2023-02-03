using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class stTestLayouts : Ix.Connector.ITwinObject
{
    [Container(Layout.Stack)]
    public stSimplePrimitive test_stack { get; }

    [Container(Layout.Wrap)]
    public stSimplePrimitive test_wrap { get; }

    [Container(Layout.Tabs)]
    public stSimplePrimitive test_tabs { get; }

    [Container(Layout.UniformGrid)]
    public stSimplePrimitive test_uniform { get; }

    [Container(Layout.Stack)]
    [Group(GroupLayout.GroupBox)]
    public stSimplePrimitive test_groupbox_stack { get; }

    [Container(Layout.Stack)]
    [Group(GroupLayout.Border)]
    public stSimplePrimitive test_border_stack { get; }

    [Container(Layout.Wrap)]
    [Group(GroupLayout.GroupBox)]
    public stSimplePrimitive test_groupbox_wrap { get; }

    [Container(Layout.Wrap)]
    [Group(GroupLayout.Border)]
    public stSimplePrimitive test_border_wrap { get; }

    [Container(Layout.Tabs)]
    [Group(GroupLayout.GroupBox)]
    public stSimplePrimitive test_groupbox_tabs { get; }

    [Container(Layout.Tabs)]
    [Group(GroupLayout.Border)]
    public stSimplePrimitive test_border_tabs { get; }

    [Container(Layout.UniformGrid)]
    [Group(GroupLayout.GroupBox)]
    public stSimplePrimitive test_groupbox_uniformGrid { get; }

    [Container(Layout.UniformGrid)]
    [Group(GroupLayout.Border)]
    public stSimplePrimitive test_border_uniformGrid { get; }

    public stTestLayouts(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        test_stack = new stSimplePrimitive(this, "test_stack", "test_stack");
        test_wrap = new stSimplePrimitive(this, "test_wrap", "test_wrap");
        test_tabs = new stSimplePrimitive(this, "test_tabs", "test_tabs");
        test_uniform = new stSimplePrimitive(this, "test_uniform", "test_uniform");
        test_groupbox_stack = new stSimplePrimitive(this, "test_groupbox_stack", "test_groupbox_stack");
        test_border_stack = new stSimplePrimitive(this, "test_border_stack", "test_border_stack");
        test_groupbox_wrap = new stSimplePrimitive(this, "test_groupbox_wrap", "test_groupbox_wrap");
        test_border_wrap = new stSimplePrimitive(this, "test_border_wrap", "test_border_wrap");
        test_groupbox_tabs = new stSimplePrimitive(this, "test_groupbox_tabs", "test_groupbox_tabs");
        test_border_tabs = new stSimplePrimitive(this, "test_border_tabs", "test_border_tabs");
        test_groupbox_uniformGrid = new stSimplePrimitive(this, "test_groupbox_uniformGrid", "test_groupbox_uniformGrid");
        test_border_uniformGrid = new stSimplePrimitive(this, "test_border_uniformGrid", "test_border_uniformGrid");
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