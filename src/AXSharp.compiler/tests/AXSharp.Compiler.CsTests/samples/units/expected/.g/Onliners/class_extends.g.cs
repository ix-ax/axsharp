using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

public partial class Extended : Extendee
{
    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public Extended(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
    {
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        PreConstruct(parent, readableTail, symbolTail);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public T OnlineToPlain<T>()
    {
        return (dynamic)this.OnlineToPlainAsync().Result;
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

    public void PlainToOnline<T>(T plain)
    {
        this.PlainToOnlineAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Extended plain)
    {
        await base.PlainToOnlineAsync(plain);
        return await this.WriteAsync();
    }

    public T ShadowToPlain<T>()
    {
        return (dynamic)this.ShadowToPlainAsync().Result;
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

    public void PlainToShadow<T>(T plain)
    {
        this.PlainToShadowAsync((dynamic)plain).Wait();
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

    public Pocos.Extended CreateEmptyPoco()
    {
        return new Pocos.Extended();
    }
}

public partial class Extendee : AXSharp.Connector.ITwinObject
{
    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public Extendee(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        PreConstruct(parent, readableTail, symbolTail);
        parent.AddChild(this);
        parent.AddKid(this);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public T OnlineToPlain<T>()
    {
        return (dynamic)this.OnlineToPlainAsync().Result;
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

    public void PlainToOnline<T>(T plain)
    {
        this.PlainToOnlineAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Extendee plain)
    {
        return await this.WriteAsync();
    }

    public T ShadowToPlain<T>()
    {
        return (dynamic)this.ShadowToPlainAsync().Result;
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

    public void PlainToShadow<T>(T plain)
    {
        this.PlainToShadowAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Extendee plain)
    {
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.Extendee CreateEmptyPoco()
    {
        return new Pocos.Extendee();
    }

    private IList<AXSharp.Connector.ITwinObject> Children { get; } = new List<AXSharp.Connector.ITwinObject>();
    public IEnumerable<AXSharp.Connector.ITwinObject> GetChildren()
    {
        return Children;
    }

    private IList<AXSharp.Connector.ITwinElement> Kids { get; } = new List<AXSharp.Connector.ITwinElement>();
    public IEnumerable<AXSharp.Connector.ITwinElement> GetKids()
    {
        return Kids;
    }

    private IList<AXSharp.Connector.ITwinPrimitive> ValueTags { get; } = new List<AXSharp.Connector.ITwinPrimitive>();
    public IEnumerable<AXSharp.Connector.ITwinPrimitive> GetValueTags()
    {
        return ValueTags;
    }

    public void AddValueTag(AXSharp.Connector.ITwinPrimitive valueTag)
    {
        ValueTags.Add(valueTag);
    }

    public void AddKid(AXSharp.Connector.ITwinElement kid)
    {
        Kids.Add(kid);
    }

    public void AddChild(AXSharp.Connector.ITwinObject twinObject)
    {
        Children.Add(twinObject);
    }

    protected AXSharp.Connector.Connector @Connector { get; }

    public AXSharp.Connector.Connector GetConnector()
    {
        return this.@Connector;
    }

    public string GetSymbolTail()
    {
        return this.SymbolTail;
    }

    public AXSharp.Connector.ITwinObject GetParent()
    {
        return this.@Parent;
    }

    public string Symbol { get; protected set; }

    private string _attributeName;
    public System.String AttributeName
    {
        get
        {
            return AXSharp.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_attributeName);
        }

        set
        {
            _attributeName = value;
        }
    }

    public string HumanReadable { get; set; }

    protected System.String @SymbolTail { get; set; }

    protected AXSharp.Connector.ITwinObject @Parent { get; set; }
}