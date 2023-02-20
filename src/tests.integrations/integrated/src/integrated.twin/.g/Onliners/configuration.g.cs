using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;
using RealMonsterData;

public partial class integratedTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

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

    public integratedTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
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

    public integratedTwinController(Ix.Connector.ConnectorAdapter adapter)
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

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Pokus plain)
    {
        await this.Nested.PlainToOnlineAsync(plain.Nested);
        return await this.WriteAsync();
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

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Pokus plain)
    {
        await this.Nested.PlainToShadowAsync(plain.Nested);
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

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Nested plain)
    {
        SomeString.Cyclic = plain.SomeString;
        SomeInt.Cyclic = plain.SomeInt;
        SomeByte.Cyclic = plain.SomeByte;
        return await this.WriteAsync();
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