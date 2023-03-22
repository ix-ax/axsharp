using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

public partial class Motor : AXSharp.Connector.ITwinObject
{
    public OnlinerBool isRunning { get; }

    public Motor(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        isRunning = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "isRunning", "isRunning");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public T OnlineToPlain<T>()
    {
        return (dynamic)this.OnlineToPlainAsync().Result;
    }

    public async Task<Pocos.Motor> OnlineToPlainAsync()
    {
        Pocos.Motor plain = new Pocos.Motor();
        await this.ReadAsync();
        plain.isRunning = isRunning.LastValue;
        return plain;
    }

    protected async Task<Pocos.Motor> OnlineToPlainAsync(Pocos.Motor plain)
    {
        plain.isRunning = isRunning.LastValue;
        return plain;
    }

    public void PlainToOnline<T>(T plain)
    {
        this.PlainToOnlineAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Motor plain)
    {
        isRunning.Cyclic = plain.isRunning;
        return await this.WriteAsync();
    }

    public T ShadowToPlain<T>()
    {
        return (dynamic)this.ShadowToPlainAsync().Result;
    }

    public async Task<Pocos.Motor> ShadowToPlainAsync()
    {
        Pocos.Motor plain = new Pocos.Motor();
        plain.isRunning = isRunning.Shadow;
        return plain;
    }

    protected async Task<Pocos.Motor> ShadowToPlainAsync(Pocos.Motor plain)
    {
        plain.isRunning = isRunning.Shadow;
        return plain;
    }

    public void PlainToShadow<T>(T plain)
    {
        this.PlainToShadowAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Motor plain)
    {
        isRunning.Shadow = plain.isRunning;
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.Motor CreateEmptyPoco()
    {
        return new Pocos.Motor();
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

public partial class Vehicle : AXSharp.Connector.ITwinObject
{
    public Motor m { get; }

    public OnlinerInt displacement { get; }

    public Vehicle(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public T OnlineToPlain<T>()
    {
        return (dynamic)this.OnlineToPlainAsync().Result;
    }

    public async Task<Pocos.Vehicle> OnlineToPlainAsync()
    {
        Pocos.Vehicle plain = new Pocos.Vehicle();
        await this.ReadAsync();
        plain.m = await m.OnlineToPlainAsync();
        plain.displacement = displacement.LastValue;
        return plain;
    }

    protected async Task<Pocos.Vehicle> OnlineToPlainAsync(Pocos.Vehicle plain)
    {
        plain.m = await m.OnlineToPlainAsync();
        plain.displacement = displacement.LastValue;
        return plain;
    }

    public void PlainToOnline<T>(T plain)
    {
        this.PlainToOnlineAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Vehicle plain)
    {
        await this.m.PlainToOnlineAsync(plain.m);
        displacement.Cyclic = plain.displacement;
        return await this.WriteAsync();
    }

    public T ShadowToPlain<T>()
    {
        return (dynamic)this.ShadowToPlainAsync().Result;
    }

    public async Task<Pocos.Vehicle> ShadowToPlainAsync()
    {
        Pocos.Vehicle plain = new Pocos.Vehicle();
        plain.m = await m.ShadowToPlainAsync();
        plain.displacement = displacement.Shadow;
        return plain;
    }

    protected async Task<Pocos.Vehicle> ShadowToPlainAsync(Pocos.Vehicle plain)
    {
        plain.m = await m.ShadowToPlainAsync();
        plain.displacement = displacement.Shadow;
        return plain;
    }

    public void PlainToShadow<T>(T plain)
    {
        this.PlainToShadowAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Vehicle plain)
    {
        await this.m.PlainToShadowAsync(plain.m);
        displacement.Shadow = plain.displacement;
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.Vehicle CreateEmptyPoco()
    {
        return new Pocos.Vehicle();
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