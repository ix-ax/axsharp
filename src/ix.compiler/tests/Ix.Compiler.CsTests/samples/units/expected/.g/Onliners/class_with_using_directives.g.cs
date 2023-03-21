using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;
using SimpleFirstLevelNamespace;
using SimpleQualifiedNamespace.Qualified;
using HelloLevelOne.HelloLevelTwo;

internal partial class ClassWithUsingDirectives : Ix.Connector.ITwinObject
{
    partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public ClassWithUsingDirectives(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

    public T OnlineToPlain<T>()
    {
        return (dynamic)this.OnlineToPlainAsync().Result;
    }

    public async Task<Pocos.ClassWithUsingDirectives> OnlineToPlainAsync()
    {
        Pocos.ClassWithUsingDirectives plain = new Pocos.ClassWithUsingDirectives();
        await this.ReadAsync();
        return plain;
    }

    protected async Task<Pocos.ClassWithUsingDirectives> OnlineToPlainAsync(Pocos.ClassWithUsingDirectives plain)
    {
        return plain;
    }

    public void PlainToOnline<T>(T plain)
    {
        this.PlainToOnlineAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ClassWithUsingDirectives plain)
    {
        return await this.WriteAsync();
    }

    public T ShadowToPlain<T>()
    {
        return (dynamic)this.ShadowToPlainAsync().Result;
    }

    public async Task<Pocos.ClassWithUsingDirectives> ShadowToPlainAsync()
    {
        Pocos.ClassWithUsingDirectives plain = new Pocos.ClassWithUsingDirectives();
        return plain;
    }

    protected async Task<Pocos.ClassWithUsingDirectives> ShadowToPlainAsync(Pocos.ClassWithUsingDirectives plain)
    {
        return plain;
    }

    public void PlainToShadow<T>(T plain)
    {
        this.PlainToShadowAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ClassWithUsingDirectives plain)
    {
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.ClassWithUsingDirectives CreateEmptyPoco()
    {
        return new Pocos.ClassWithUsingDirectives();
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

namespace SimpleFirstLevelNamespace
{
}

namespace SimpleQualifiedNamespace.Qualified
{
}

namespace HelloLevelOne
{
    namespace HelloLevelTwo
    {
    }
}