using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class TestSimple : Ix.Connector.ITwinObject
{
    public OnlinerString Piston_A1 { get; }

    public OnlinerString Piston_A2 { get; }

    public OnlinerString Piston_A3 { get; }

    public OnlinerString Piston_A4 { get; }

    public TestSimple(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Piston_A1 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A1", "Piston_A1");
        Piston_A1.AttributeName = "A1";
        Piston_A2 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A2", "Piston_A2");
        Piston_A2.AttributeName = "A2";
        Piston_A3 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A3", "Piston_A3");
        Piston_A3.AttributeName = "A3";
        Piston_A4 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A4", "Piston_A4");
        Piston_A4.AttributeName = "A4";
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