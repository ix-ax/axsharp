using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class Extended : Extendee
{
    public Extended(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
    }

    public async Task<Pocos.Extended> OnlineToPlainAsync()
    {
        Pocos.Extended plain = new Pocos.Extended();
        await this.ReadAsync();
        await base.OnlineToPlainAsync(plain);
        return plain;
    }

    protected async Task<Pocos.Extended> OnlineToPlainAsync(Pocos.Extended plain)
    {
        await base.OnlineToPlainAsync(plain);
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Extended plain)
    {
        await base.PlainToOnlineAsync(plain);
        return await this.WriteAsync();
    }

    public async Task<Pocos.Extended> ShadowToPlainAsync()
    {
        Pocos.Extended plain = new Pocos.Extended();
        await base.ShadowToPlainAsync(plain);
        return plain;
    }

    protected async Task<Pocos.Extended> ShadowToPlainAsync(Pocos.Extended plain)
    {
        await base.ShadowToPlainAsync(plain);
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Extended plain)
    {
        await base.PlainToShadowAsync(plain);
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }
}

public partial class Extendee : Ix.Connector.ITwinObject
{
    public Extendee(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.Extendee> OnlineToPlainAsync()
    {
        Pocos.Extendee plain = new Pocos.Extendee();
        await this.ReadAsync();
        return plain;
    }

    protected async Task<Pocos.Extendee> OnlineToPlainAsync(Pocos.Extendee plain)
    {
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Extendee plain)
    {
        return await this.WriteAsync();
    }

    public async Task<Pocos.Extendee> ShadowToPlainAsync()
    {
        Pocos.Extendee plain = new Pocos.Extendee();
        return plain;
    }

    protected async Task<Pocos.Extendee> ShadowToPlainAsync(Pocos.Extendee plain)
    {
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Extendee plain)
    {
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
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