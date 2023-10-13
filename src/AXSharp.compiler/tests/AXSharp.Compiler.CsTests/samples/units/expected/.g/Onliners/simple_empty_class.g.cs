using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;

public partial class simple_class : AXSharp.Connector.ITwinObject
{
    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public simple_class(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

    public async virtual Task<T> OnlineToPlain<T>()
    {
        return await (dynamic)this.OnlineToPlainAsync();
    }

    public async Task<Pocos.simple_class> OnlineToPlainAsync()
    {
        Pocos.simple_class plain = new Pocos.simple_class();
        await this.ReadAsync<IgnoreOnPocoOperation>();
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<Pocos.simple_class> _OnlineToPlainNoacAsync()
    {
        Pocos.simple_class plain = new Pocos.simple_class();
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    protected async Task<Pocos.simple_class> _OnlineToPlainNoacAsync(Pocos.simple_class plain)
    {
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain)
    {
        await this.PlainToOnlineAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.simple_class plain)
    {
        return await this.WriteAsync<IgnoreOnPocoOperation>();
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task _PlainToOnlineNoacAsync(Pocos.simple_class plain)
    {
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<Pocos.simple_class> ShadowToPlainAsync()
    {
        Pocos.simple_class plain = new Pocos.simple_class();
        return plain;
    }

    protected async Task<Pocos.simple_class> ShadowToPlainAsync(Pocos.simple_class plain)
    {
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.simple_class plain)
    {
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.simple_class CreateEmptyPoco()
    {
        return new Pocos.simple_class();
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
    public System.String AttributeName { get => string.IsNullOrEmpty(_attributeName) ? SymbolTail : _attributeName.Interpolate(this).CleanUpLocalizationTokens(); set => _attributeName = value; }

    public System.String GetAttributeName(System.Globalization.CultureInfo culture)
    {
        return this.Translate(_attributeName, culture).Interpolate(this);
    }

    private string _humanReadable;
    public string HumanReadable { get => string.IsNullOrEmpty(_humanReadable) ? SymbolTail : _humanReadable.Interpolate(this).CleanUpLocalizationTokens(); set => _humanReadable = value; }

    public System.String GetHumanReadable(System.Globalization.CultureInfo culture)
    {
        return this.Translate(_humanReadable, culture);
    }

    protected System.String @SymbolTail { get; set; }

    protected AXSharp.Connector.ITwinObject @Parent { get; set; }

    public AXSharp.Connector.Localizations.Translator Interpreter => global::units.PlcTranslator.Instance;
}