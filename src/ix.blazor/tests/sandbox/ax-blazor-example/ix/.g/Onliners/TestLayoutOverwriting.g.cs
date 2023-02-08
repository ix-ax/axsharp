using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

[Container(Layout.UniformGrid)]
public partial class TestLayoutOverwriting : Ix.Connector.ITwinObject
{
    public OnlinerBool ix_bool { get; }

    public OnlinerInt ix_int { get; }

    public OnlinerString ix_string { get; }

    [Container(Layout.Wrap)]
    public TestSimple simple { get; }

    [Container(Layout.Wrap)]
    public prgWeatherStations weather { get; }

    public TestLayoutOverwriting(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        ix_bool = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "ix_bool", "ix_bool");
        ix_int = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "ix_int", "ix_int");
        ix_string = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "ix_string", "ix_string");
        simple = new TestSimple(this, "simple", "simple");
        weather = new prgWeatherStations(this, "weather", "weather");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.TestLayoutOverwriting> OnlineToPlainAsync()
    {
        Pocos.TestLayoutOverwriting plain = new Pocos.TestLayoutOverwriting();
        await this.ReadAsync();
        plain.ix_bool = ix_bool.LastValue;
        plain.ix_int = ix_int.LastValue;
        plain.ix_string = ix_string.LastValue;
        plain.simple = await simple.OnlineToPlainAsync();
        plain.weather = await weather.OnlineToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.TestLayoutOverwriting> OnlineToPlainAsync(Pocos.TestLayoutOverwriting plain)
    {
        plain.ix_bool = ix_bool.LastValue;
        plain.ix_int = ix_int.LastValue;
        plain.ix_string = ix_string.LastValue;
        plain.simple = await simple.OnlineToPlainAsync();
        plain.weather = await weather.OnlineToPlainAsync();
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.TestLayoutOverwriting plain)
    {
        ix_bool.Cyclic = plain.ix_bool;
        ix_int.Cyclic = plain.ix_int;
        ix_string.Cyclic = plain.ix_string;
        await this.simple.PlainToOnlineAsync(plain.simple);
        await this.weather.PlainToOnlineAsync(plain.weather);
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