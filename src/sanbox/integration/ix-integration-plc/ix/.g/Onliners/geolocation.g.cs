using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

[Container(Layout.Stack)]
[Group(GroupLayout.GroupBox)]
public partial class GeoLocation : Ix.Connector.ITwinObject
{
    public OnlinerReal Latitude { get; }

    public OnlinerReal Longitude { get; }

    public OnlinerReal Altitude { get; }

    public OnlinerString Description { get; }

    public OnlinerString LongDescription { get; }

    public GeoLocation(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        AttributeName = "Location";
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Latitude = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Latitude [°]", "Latitude");
        Latitude.AttributeName = "Latitude [°]";
        Latitude.AttributeMinimum = -90.0f;
        Latitude.AttributeMaximum = 90.0f;
        Longitude = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Logitude [°]", "Longitude");
        Longitude.AttributeName = "Logitude [°]";
        Longitude.AttributeMinimum = 0.0f;
        Longitude.AttributeMaximum = 180.0f;
        Altitude = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Altitude [m]", "Altitude");
        Altitude.AttributeName = "Altitude [m]";
        Description = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Short descriptor", "Description");
        Description.AttributeName = "Short descriptor";
        LongDescription = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Long descriptor", "LongDescription");
        LongDescription.AttributeName = "Long descriptor";
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.GeoLocation> OnlineToPlain()
    {
        Pocos.GeoLocation plain = new Pocos.GeoLocation();
        await this.ReadAsync();
        plain.Latitude = Latitude.LastValue;
        plain.Longitude = Longitude.LastValue;
        plain.Altitude = Altitude.LastValue;
        plain.Description = Description.LastValue;
        plain.LongDescription = LongDescription.LastValue;
        plain.Latitude = Latitude.LastValue;
        plain.Longitude = Longitude.LastValue;
        plain.Altitude = Altitude.LastValue;
        plain.Description = Description.LastValue;
        plain.LongDescription = LongDescription.LastValue;
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.GeoLocation plain)
    {
        Latitude.Cyclic = plain.Latitude;
        Longitude.Cyclic = plain.Longitude;
        Altitude.Cyclic = plain.Altitude;
        Description.Cyclic = plain.Description;
        LongDescription.Cyclic = plain.LongDescription;
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