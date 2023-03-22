using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ExtendsAndImplements : ExtendeeExtendsAndImplements, IImplementation1, IImplementation2
{
    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public ExtendsAndImplements(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
    {
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        PreConstruct(parent, readableTail, symbolTail);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public override T OnlineToPlain<T>()
    {
        return (dynamic)this.OnlineToPlainAsync().Result;
    }

    public async Task<Pocos.ExtendsAndImplements> OnlineToPlainAsync()
    {
        Pocos.ExtendsAndImplements plain = new Pocos.ExtendsAndImplements();
        await this.ReadAsync();
        await base.OnlineToPlainAsync(plain);
        return plain;
    }

    protected async Task<Pocos.ExtendsAndImplements> OnlineToPlainAsync(Pocos.ExtendsAndImplements plain)
    {
        await base.OnlineToPlainAsync(plain);
        return plain;
    }

    public override void PlainToOnline<T>(T plain)
    {
        this.PlainToOnlineAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ExtendsAndImplements plain)
    {
        await base.PlainToOnlineAsync(plain);
        return await this.WriteAsync();
    }

    public override T ShadowToPlain<T>()
    {
        return (dynamic)this.ShadowToPlainAsync().Result;
    }

    public async Task<Pocos.ExtendsAndImplements> ShadowToPlainAsync()
    {
        Pocos.ExtendsAndImplements plain = new Pocos.ExtendsAndImplements();
        await base.ShadowToPlainAsync(plain);
        return plain;
    }

    protected async Task<Pocos.ExtendsAndImplements> ShadowToPlainAsync(Pocos.ExtendsAndImplements plain)
    {
        await base.ShadowToPlainAsync(plain);
        return plain;
    }

    public override void PlainToShadow<T>(T plain)
    {
        this.PlainToShadowAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ExtendsAndImplements plain)
    {
        await base.PlainToShadowAsync(plain);
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.ExtendsAndImplements CreateEmptyPoco()
    {
        return new Pocos.ExtendsAndImplements();
    }
}

public partial class ExtendeeExtendsAndImplements : AXSharp.Connector.ITwinObject
{
    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public ExtendeeExtendsAndImplements(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

    public virtual T OnlineToPlain<T>()
    {
        return (dynamic)this.OnlineToPlainAsync().Result;
    }

    public async Task<Pocos.ExtendeeExtendsAndImplements> OnlineToPlainAsync()
    {
        Pocos.ExtendeeExtendsAndImplements plain = new Pocos.ExtendeeExtendsAndImplements();
        await this.ReadAsync();
        return plain;
    }

    protected async Task<Pocos.ExtendeeExtendsAndImplements> OnlineToPlainAsync(Pocos.ExtendeeExtendsAndImplements plain)
    {
        return plain;
    }

    public virtual void PlainToOnline<T>(T plain)
    {
        this.PlainToOnlineAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ExtendeeExtendsAndImplements plain)
    {
        return await this.WriteAsync();
    }

    public virtual T ShadowToPlain<T>()
    {
        return (dynamic)this.ShadowToPlainAsync().Result;
    }

    public async Task<Pocos.ExtendeeExtendsAndImplements> ShadowToPlainAsync()
    {
        Pocos.ExtendeeExtendsAndImplements plain = new Pocos.ExtendeeExtendsAndImplements();
        return plain;
    }

    protected async Task<Pocos.ExtendeeExtendsAndImplements> ShadowToPlainAsync(Pocos.ExtendeeExtendsAndImplements plain)
    {
        return plain;
    }

    public virtual void PlainToShadow<T>(T plain)
    {
        this.PlainToShadowAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ExtendeeExtendsAndImplements plain)
    {
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.ExtendeeExtendsAndImplements CreateEmptyPoco()
    {
        return new Pocos.ExtendeeExtendsAndImplements();
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

public partial interface IImplementation1
{
}

public partial interface IImplementation2
{
}