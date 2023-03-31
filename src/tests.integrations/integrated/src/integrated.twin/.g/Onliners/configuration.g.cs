using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using RealMonsterData;

public partial class integratedTwinController : ITwinController
{
    public AXSharp.Connector.Connector Connector { get; }

    public MonsterData.Monster Monster { get; }

    public MonsterData.Monster OnlineToPlain_should_copy_entire_structure { get; }

    public MonsterData.Monster PlainToOnline_should_copy_entire_structure { get; }

    public MonsterData.Monster OnlineToShadowAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster ShadowToOnlineAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster ShadowToPlainAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster PlainToShadowAsync_should_copy_entire_structure { get; }

    public Pokus Pokus { get; }

    public RealMonsterData.RealMonster RealMonster { get; }

    public RealMonsterData.RealMonster OnlineToShadow_should_copy { get; }

    public RealMonsterData.RealMonster ShadowToOnline_should_copy { get; }

    public RealMonsterData.RealMonster OnlineToPlain_should_copy { get; }

    public RealMonsterData.RealMonster PlainToOnline_should_copy { get; }

    public all_primitives p_online_shadow { get; }

    public all_primitives p_shadow_online { get; }

    public all_primitives p_online_plain { get; }

    public all_primitives p_plain_online { get; }

    public all_primitives p_shadow_plain { get; }

    public all_primitives p_plain_shadow { get; }

    public RealMonsterData.RealMonster StartPolling_should_update_cyclic_property { get; }

    public integratedTwinController(AXSharp.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Monster = new MonsterData.Monster(this.Connector, "", "Monster");
        OnlineToPlain_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "OnlineToPlain_should_copy_entire_structure");
        PlainToOnline_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "PlainToOnline_should_copy_entire_structure");
        OnlineToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "OnlineToShadowAsync_should_copy_entire_structure");
        ShadowToOnlineAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ShadowToOnlineAsync_should_copy_entire_structure");
        ShadowToPlainAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ShadowToPlainAsync_should_copy_entire_structure");
        PlainToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "PlainToShadowAsync_should_copy_entire_structure");
        Pokus = new Pokus(this.Connector, "", "Pokus");
        RealMonster = new RealMonsterData.RealMonster(this.Connector, "", "RealMonster");
        OnlineToShadow_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "OnlineToShadow_should_copy");
        ShadowToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ShadowToOnline_should_copy");
        OnlineToPlain_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "OnlineToPlain_should_copy");
        PlainToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "PlainToOnline_should_copy");
        p_online_shadow = new all_primitives(this.Connector, "", "p_online_shadow");
        p_shadow_online = new all_primitives(this.Connector, "", "p_shadow_online");
        p_online_plain = new all_primitives(this.Connector, "", "p_online_plain");
        p_plain_online = new all_primitives(this.Connector, "", "p_plain_online");
        p_shadow_plain = new all_primitives(this.Connector, "", "p_shadow_plain");
        p_plain_shadow = new all_primitives(this.Connector, "", "p_plain_shadow");
        StartPolling_should_update_cyclic_property = new RealMonsterData.RealMonster(this.Connector, "", "StartPolling_should_update_cyclic_property");
    }

    public integratedTwinController(AXSharp.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Monster = new MonsterData.Monster(this.Connector, "", "Monster");
        OnlineToPlain_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "OnlineToPlain_should_copy_entire_structure");
        PlainToOnline_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "PlainToOnline_should_copy_entire_structure");
        OnlineToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "OnlineToShadowAsync_should_copy_entire_structure");
        ShadowToOnlineAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ShadowToOnlineAsync_should_copy_entire_structure");
        ShadowToPlainAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ShadowToPlainAsync_should_copy_entire_structure");
        PlainToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "PlainToShadowAsync_should_copy_entire_structure");
        Pokus = new Pokus(this.Connector, "", "Pokus");
        RealMonster = new RealMonsterData.RealMonster(this.Connector, "", "RealMonster");
        OnlineToShadow_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "OnlineToShadow_should_copy");
        ShadowToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ShadowToOnline_should_copy");
        OnlineToPlain_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "OnlineToPlain_should_copy");
        PlainToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "PlainToOnline_should_copy");
        p_online_shadow = new all_primitives(this.Connector, "", "p_online_shadow");
        p_shadow_online = new all_primitives(this.Connector, "", "p_shadow_online");
        p_online_plain = new all_primitives(this.Connector, "", "p_online_plain");
        p_plain_online = new all_primitives(this.Connector, "", "p_plain_online");
        p_shadow_plain = new all_primitives(this.Connector, "", "p_shadow_plain");
        p_plain_shadow = new all_primitives(this.Connector, "", "p_plain_shadow");
        StartPolling_should_update_cyclic_property = new RealMonsterData.RealMonster(this.Connector, "", "StartPolling_should_update_cyclic_property");
    }
}

public partial class Pokus : AXSharp.Connector.ITwinObject
{
    public Nested Nested { get; }

    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public Pokus(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        PreConstruct(parent, readableTail, symbolTail);
        Nested = new Nested(this, "Nested", "Nested");
        parent.AddChild(this);
        parent.AddKid(this);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public async virtual Task<T> OnlineToPlain<T>()
    {
        return await (dynamic)this.OnlineToPlainAsync();
    }

    public async Task<Pocos.Pokus> OnlineToPlainAsync()
    {
        Pocos.Pokus plain = new Pocos.Pokus();
        await this.ReadAsync();
        plain.Nested = await Nested.OnlineToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.Pokus> OnlineToPlainAsync(Pocos.Pokus plain)
    {
        plain.Nested = await Nested.OnlineToPlainAsync();
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain)
    {
        await this.PlainToOnlineAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Pokus plain)
    {
        await this.Nested.PlainToOnlineAsync(plain.Nested);
        return await this.WriteAsync();
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<Pocos.Pokus> ShadowToPlainAsync()
    {
        Pocos.Pokus plain = new Pocos.Pokus();
        plain.Nested = await Nested.ShadowToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.Pokus> ShadowToPlainAsync(Pocos.Pokus plain)
    {
        plain.Nested = await Nested.ShadowToPlainAsync();
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Pokus plain)
    {
        await this.Nested.PlainToShadowAsync(plain.Nested);
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.Pokus CreateEmptyPoco()
    {
        return new Pocos.Pokus();
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
    public System.String AttributeName { get => string.IsNullOrEmpty(_attributeName) ? SymbolTail : this.Translate(_attributeName).Interpolate(this); set => _attributeName = value; }

    public string HumanReadable { get; set; }

    protected System.String @SymbolTail { get; set; }

    protected AXSharp.Connector.ITwinObject @Parent { get; set; }

    public AXSharp.Connector.Localizations.Translator Interpreter => integrated.PlcTranslator.Instance;
}

public partial class Nested : AXSharp.Connector.ITwinObject
{
    public OnlinerString SomeString { get; }

    public OnlinerInt SomeInt { get; }

    public OnlinerByte SomeByte { get; }

    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public Nested(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        PreConstruct(parent, readableTail, symbolTail);
        SomeString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "SomeString", "SomeString");
        SomeInt = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "SomeInt", "SomeInt");
        SomeByte = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this, "SomeByte", "SomeByte");
        parent.AddChild(this);
        parent.AddKid(this);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public async virtual Task<T> OnlineToPlain<T>()
    {
        return await (dynamic)this.OnlineToPlainAsync();
    }

    public async Task<Pocos.Nested> OnlineToPlainAsync()
    {
        Pocos.Nested plain = new Pocos.Nested();
        await this.ReadAsync();
        plain.SomeString = SomeString.LastValue;
        plain.SomeInt = SomeInt.LastValue;
        plain.SomeByte = SomeByte.LastValue;
        return plain;
    }

    protected async Task<Pocos.Nested> OnlineToPlainAsync(Pocos.Nested plain)
    {
        plain.SomeString = SomeString.LastValue;
        plain.SomeInt = SomeInt.LastValue;
        plain.SomeByte = SomeByte.LastValue;
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain)
    {
        await this.PlainToOnlineAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Nested plain)
    {
        SomeString.Cyclic = plain.SomeString;
        SomeInt.Cyclic = plain.SomeInt;
        SomeByte.Cyclic = plain.SomeByte;
        return await this.WriteAsync();
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<Pocos.Nested> ShadowToPlainAsync()
    {
        Pocos.Nested plain = new Pocos.Nested();
        plain.SomeString = SomeString.Shadow;
        plain.SomeInt = SomeInt.Shadow;
        plain.SomeByte = SomeByte.Shadow;
        return plain;
    }

    protected async Task<Pocos.Nested> ShadowToPlainAsync(Pocos.Nested plain)
    {
        plain.SomeString = SomeString.Shadow;
        plain.SomeInt = SomeInt.Shadow;
        plain.SomeByte = SomeByte.Shadow;
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Nested plain)
    {
        SomeString.Shadow = plain.SomeString;
        SomeInt.Shadow = plain.SomeInt;
        SomeByte.Shadow = plain.SomeByte;
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.Nested CreateEmptyPoco()
    {
        return new Pocos.Nested();
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
    public System.String AttributeName { get => string.IsNullOrEmpty(_attributeName) ? SymbolTail : this.Translate(_attributeName).Interpolate(this); set => _attributeName = value; }

    public string HumanReadable { get; set; }

    protected System.String @SymbolTail { get; set; }

    protected AXSharp.Connector.ITwinObject @Parent { get; set; }

    public AXSharp.Connector.Localizations.Translator Interpreter => integrated.PlcTranslator.Instance;
}