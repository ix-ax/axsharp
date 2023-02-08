using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class stTest : Ix.Connector.ITwinObject
{
    public OnlinerInt p1 { get; }

    public OnlinerInt p2 { get; }

    public stTest3 stTest3Struct { get; }

    public OnlinerDate DateVar2 { get; }

    public stComplex complexInstanceNested { get; }

    public stTest(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        p1 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "p1", "p1");
        p2 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "p2", "p2");
        stTest3Struct = new stTest3(this, "stTest3Struct", "stTest3Struct");
        DateVar2 = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "DateVar2", "DateVar2");
        complexInstanceNested = new stComplex(this, "complexInstanceNested", "complexInstanceNested");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.stTest> OnlineToPlainAsync()
    {
        Pocos.stTest plain = new Pocos.stTest();
        await this.ReadAsync();
        plain.p1 = p1.LastValue;
        plain.p2 = p2.LastValue;
        plain.stTest3Struct = await stTest3Struct.OnlineToPlainAsync();
        plain.DateVar2 = DateVar2.LastValue;
        plain.complexInstanceNested = await complexInstanceNested.OnlineToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.stTest> OnlineToPlainAsync(Pocos.stTest plain)
    {
        plain.p1 = p1.LastValue;
        plain.p2 = p2.LastValue;
        plain.stTest3Struct = await stTest3Struct.OnlineToPlainAsync();
        plain.DateVar2 = DateVar2.LastValue;
        plain.complexInstanceNested = await complexInstanceNested.OnlineToPlainAsync();
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.stTest plain)
    {
        p1.Cyclic = plain.p1;
        p2.Cyclic = plain.p2;
        await this.stTest3Struct.PlainToOnlineAsync(plain.stTest3Struct);
        DateVar2.Cyclic = plain.DateVar2;
        await this.complexInstanceNested.PlainToOnlineAsync(plain.complexInstanceNested);
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