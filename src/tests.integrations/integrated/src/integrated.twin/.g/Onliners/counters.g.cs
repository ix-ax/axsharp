using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class Counters : Ix.Connector.ITwinObject
{
    [Container(Layout.Wrap)]
    public Counter wrap { get; }

    [Container(Layout.Stack)]
    public Counter stack { get; }

    [Container(Layout.UniformGrid)]
    public Counter grid { get; }

    [Container(Layout.Tabs)]
    public Counter tabs { get; }

    public Counters(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        wrap = new Counter(this, "wrap", "wrap");
        stack = new Counter(this, "stack", "stack");
        grid = new Counter(this, "grid", "grid");
        tabs = new Counter(this, "tabs", "tabs");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.Counters> OnlineToPlain()
    {
        Pocos.Counters plain = new Pocos.Counters();
        await this.ReadAsync();
        plain.wrap = await wrap.OnlineToPlain();
        plain.stack = await stack.OnlineToPlain();
        plain.grid = await grid.OnlineToPlain();
        plain.tabs = await tabs.OnlineToPlain();
        plain.wrap = await wrap.OnlineToPlain();
        plain.stack = await stack.OnlineToPlain();
        plain.grid = await grid.OnlineToPlain();
        plain.tabs = await tabs.OnlineToPlain();
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.Counters plain)
    {
        await this.wrap.PlainToOnline(plain.wrap);
        await this.stack.PlainToOnline(plain.stack);
        await this.grid.PlainToOnline(plain.grid);
        await this.tabs.PlainToOnline(plain.tabs);
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

public partial class Counter : Ix.Connector.ITwinObject
{
    public OnlinerULInt Counts { get; }

    public OnlinerBool AllowCounter { get; }

    public OnlinerBool ResetCounter { get; }

    public Counter(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Counts = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Number of revolutions", "Counts");
        Counts.AttributeName = "Number of revolutions";
        AllowCounter = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "Count revolutions", "AllowCounter");
        AllowCounter.AttributeName = "Count revolutions";
        ResetCounter = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "Reset counts", "ResetCounter");
        ResetCounter.AttributeName = "Reset counts";
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.Counter> OnlineToPlain()
    {
        Pocos.Counter plain = new Pocos.Counter();
        await this.ReadAsync();
        plain.Counts = Counts.LastValue;
        plain.AllowCounter = AllowCounter.LastValue;
        plain.ResetCounter = ResetCounter.LastValue;
        plain.Counts = Counts.LastValue;
        plain.AllowCounter = AllowCounter.LastValue;
        plain.ResetCounter = ResetCounter.LastValue;
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.Counter plain)
    {
        Counts.Cyclic = plain.Counts;
        AllowCounter.Cyclic = plain.AllowCounter;
        ResetCounter.Cyclic = plain.ResetCounter;
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