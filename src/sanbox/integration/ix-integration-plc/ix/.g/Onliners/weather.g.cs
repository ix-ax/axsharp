using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class weather : Ix.Connector.ITwinObject
{
    public GeoLocation GeoLocation { get; }

    public OnlinerReal Temperature { get; }

    public OnlinerReal Humidity { get; }

    public OnlinerString Location { get; }

    public OnlinerReal ChillFactor { get; }

    [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(Feeling))]
    public OnlinerInt Feeling { get; }

    public weather(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        GeoLocation = new GeoLocation(this, "GeoLocation", "GeoLocation");
        Temperature = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Temperature", "Temperature");
        Humidity = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Humidity", "Humidity");
        Location = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Location", "Location");
        ChillFactor = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "ChillFactor", "ChillFactor");
        Feeling = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Feeling", "Feeling");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public Pocos.weather OnlineToPlain()
    {
        Pocos.weather plain = new Pocos.weather();
        plain.GeoLocation = GeoLocation.OnlineToPlain();
        plain.Temperature = Temperature.LastValue;
        plain.Humidity = Humidity.LastValue;
        plain.Location = Location.LastValue;
        plain.ChillFactor = ChillFactor.LastValue;
        plain.Feeling = (Feeling)Feeling.LastValue;
        ;
        return plain;
    }

    public void PlainToOnline(Pocos.weather plain)
    {
        this.GeoLocation.PlainToOnline(plain.GeoLocation);
        Temperature.Cyclic = plain.Temperature;
        Humidity.Cyclic = plain.Humidity;
        Location.Cyclic = plain.Location;
        ChillFactor.Cyclic = plain.ChillFactor;
        Feeling.Cyclic = (short)plain.Feeling;
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

public partial class weathers : Ix.Connector.ITwinObject
{
    public weatherBase[] i { get; }

    public weathers(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        i = new weatherBase[51];
        Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(i, this, "i", "i", (p, rt, st) => new weatherBase(p, rt, st));
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public Pocos.weathers OnlineToPlain()
    {
        Pocos.weathers plain = new Pocos.weathers();
        Ix.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<weatherBase, Pocos.weatherBase>(plain.i, i);
        return plain;
    }

    public void PlainToOnline(Pocos.weathers plain)
    {
        Ix.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<Pocos.weatherBase, weatherBase>(plain.i, i);
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

public enum Feeling
{
    Freezing,
    Cold,
    Lookworm,
    Warm,
    Hot
}

public enum Colors : Int16
{
    RED = 12,
    GREEN = 14,
    BLUE = 23
}