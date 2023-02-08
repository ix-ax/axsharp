using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class TestStruct : Ix.Connector.ITwinObject
{
    public OnlinerInt e { get; }

    public OnlinerString r44 { get; }

    public OnlinerString k21 { get; }

    public stExample example { get; }

    public TestStruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        e = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "e", "e");
        r44 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "r44", "r44");
        k21 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "k21", "k21");
        example = new stExample(this, "example", "example");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.TestStruct> OnlineToPlainAsync()
    {
        Pocos.TestStruct plain = new Pocos.TestStruct();
        await this.ReadAsync();
        plain.e = e.LastValue;
        plain.r44 = r44.LastValue;
        plain.k21 = k21.LastValue;
        plain.example = await example.OnlineToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.TestStruct> OnlineToPlainAsync(Pocos.TestStruct plain)
    {
        plain.e = e.LastValue;
        plain.r44 = r44.LastValue;
        plain.k21 = k21.LastValue;
        plain.example = await example.OnlineToPlainAsync();
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.TestStruct plain)
    {
        e.Cyclic = plain.e;
        r44.Cyclic = plain.r44;
        k21.Cyclic = plain.k21;
        await this.example.PlainToOnlineAsync(plain.example);
        return await this.WriteAsync();
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