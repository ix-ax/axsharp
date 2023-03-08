using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ExtendsAndImplements : ExtendeeExtendsAndImplements, IImplementation1, IImplementation2
{
    partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public ExtendsAndImplements(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        PreConstruct(parent, readableTail, symbolTail);
        PostConstruct(parent, readableTail, symbolTail);
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

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ExtendsAndImplements plain)
    {
        await base.PlainToOnlineAsync(plain);
        return await this.WriteAsync();
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

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ExtendsAndImplements plain)
    {
        await base.PlainToShadowAsync(plain);
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }
}

public partial class ExtendeeExtendsAndImplements : Ix.Connector.ITwinObject
{
    partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public ExtendeeExtendsAndImplements(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        PreConstruct(parent, readableTail, symbolTail);
        parent.AddChild(this);
        parent.AddKid(this);
        PostConstruct(parent, readableTail, symbolTail);
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

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ExtendeeExtendsAndImplements plain)
    {
        return await this.WriteAsync();
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

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ExtendeeExtendsAndImplements plain)
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

    private string _attributeName;
    public System.String AttributeName
    {
        get
        {
            return Ix.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_attributeName);
        }

        set
        {
            _attributeName = value;
        }
    }

    public string HumanReadable { get; set; }

    protected System.String @SymbolTail { get; set; }

    protected Ix.Connector.ITwinObject @Parent { get; set; }
}

public interface IImplementation1
{
}

public interface IImplementation2
{
}