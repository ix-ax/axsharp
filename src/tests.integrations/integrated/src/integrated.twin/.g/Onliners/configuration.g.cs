using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class integratedTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public MonsterData.Monster Monster { get; }

    public Pokus Pokus { get; }

    public integratedTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Monster = new MonsterData.Monster(this.Connector, "", "Monster");
        Pokus = new Pokus(this.Connector, "", "Pokus");
    }

    public integratedTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Monster = new MonsterData.Monster(this.Connector, "", "Monster");
        Pokus = new Pokus(this.Connector, "", "Pokus");
    }
}

public partial class Pokus : Ix.Connector.ITwinObject
{
    public Nested Nested { get; }

    public Pokus(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Nested = new Nested(this, "Nested", "Nested");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.Pokus> OnlineToPlain()
    {
        Pocos.Pokus plain = new Pocos.Pokus();
        await this.ReadAsync();
        plain.Nested = await Nested.OnlineToPlain();
        plain.Nested = await Nested.OnlineToPlain();
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.Pokus plain)
    {
        await this.Nested.PlainToOnline(plain.Nested);
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

public partial class Nested : Ix.Connector.ITwinObject
{
    public OnlinerString SomeString { get; }

    public OnlinerInt SomeInt { get; }

    public OnlinerByte SomeByte { get; }

    public Nested(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        SomeString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "SomeString", "SomeString");
        SomeInt = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "SomeInt", "SomeInt");
        SomeByte = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this, "SomeByte", "SomeByte");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.Nested> OnlineToPlain()
    {
        Pocos.Nested plain = new Pocos.Nested();
        await this.ReadAsync();
        plain.SomeString = SomeString.LastValue;
        plain.SomeInt = SomeInt.LastValue;
        plain.SomeByte = SomeByte.LastValue;
        plain.SomeString = SomeString.LastValue;
        plain.SomeInt = SomeInt.LastValue;
        plain.SomeByte = SomeByte.LastValue;
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.Nested plain)
    {
        SomeString.Cyclic = plain.SomeString;
        SomeInt.Cyclic = plain.SomeInt;
        SomeByte.Cyclic = plain.SomeByte;
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