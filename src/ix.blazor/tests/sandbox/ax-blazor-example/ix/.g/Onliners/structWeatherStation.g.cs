using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class structWeatherStation : Ix.Connector.ITwinObject
{
    public OnlinerString StationICAO { get; }

    [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(enumStationStatus))]
    public OnlinerInt StationStatus { get; }

    [RenderIgnore()]
    public OnlinerReal DewPoint { get; }

    public OnlinerReal Pressure { get; }

    public OnlinerReal Temp { get; }

    public OnlinerReal Visibility { get; }

    public OnlinerUInt WindHeading { get; }

    public OnlinerReal WindSpeed { get; }

    public OnlinerDate TestDate { get; }

    public OnlinerDateTime TestDateTime { get; }

    public OnlinerTimeOfDay TestTimeOfDay { get; }

    public structWeatherStation(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        StationICAO = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Station name (ICAO)", "StationICAO");
        StationICAO.AttributeName = "Station name (ICAO)";
        StationStatus = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Station status", "StationStatus");
        StationStatus.AttributeName = "Station status";
        StationStatus.AttributeName = "Station status";
        DewPoint = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Dew Point", "DewPoint");
        DewPoint.AttributeName = "Dew Point";
        DewPoint.AttributeUnits = "°C";
        Pressure = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Pressure", "Pressure");
        Pressure.AttributeName = "Pressure";
        Pressure.AttributeUnits = "Torr";
        Temp = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Temperature", "Temp");
        Temp.AttributeName = "Temperature";
        Temp.AttributeUnits = "°C";
        Visibility = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Visibility", "Visibility");
        Visibility.AttributeName = "Visibility";
        Visibility.AttributeUnits = "km";
        WindHeading = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this, "Wind heading", "WindHeading");
        WindHeading.AttributeName = "Wind heading";
        WindHeading.AttributeUnits = "Azimuth";
        WindSpeed = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Wind speed", "WindSpeed");
        WindSpeed.AttributeName = "Wind speed";
        WindSpeed.AttributeUnits = "m/s";
        TestDate = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "TestDate", "TestDate");
        TestDateTime = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this, "TestDateTime", "TestDateTime");
        TestTimeOfDay = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this, "TestTimeOfDay", "TestTimeOfDay");
        parent.AddChild(this);
        parent.AddKid(this);
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