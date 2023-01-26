using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

[Container(Layout.Stack)]
[Group(Layout.GroupBox)]
public partial class weatherBase : Ix.Connector.ITwinObject
{
    public OnlinerReal Latitude { get; }

    public OnlinerReal Longitude { get; }

    public OnlinerReal Altitude { get; }

    public OnlinerString Description { get; }

    [ReadOnly()]
    public OnlinerString LongDescription { get; }

    [ReadOnce()]
    public OnlinerInt StartCounter { get; }

    [RenderIgnore()]
    public OnlinerString RenderIgnoreAllToghether { get; }

    [RenderIgnore("Control")]
    public OnlinerString RenderIgnoreWhenControl { get; }

    [RenderIgnore("Display")]
    public OnlinerString RenderIgnoreWhenDisplay { get; }

    [RenderIgnore("Control", "ShadowControl")]
    public OnlinerString RenderIgnoreWhenControlAndShadow { get; }

    [RenderIgnore("Display", "ShadowDisplay")]
    public OnlinerString RenderIgnoreWhenDisplayAndShadow { get; }

    public weatherBase(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Latitude = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Latitude", "Latitude");
        Longitude = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Longitude", "Longitude");
        Altitude = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Altitude", "Altitude");
        Description = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Description", "Description");
        LongDescription = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "LongDescription", "LongDescription");
        LongDescription.MakeReadOnly();
        StartCounter = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "this has [ReadOnce()] attribute will be readon only once...", "StartCounter");
        StartCounter.AttributeName = "this has [ReadOnce()] attribute will be readon only once...";
        StartCounter.MakeReadOnce();
        RenderIgnoreAllToghether = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "[RenderIgnore()] must not be displayed!", "RenderIgnoreAllToghether");
        RenderIgnoreAllToghether.AttributeName = "[RenderIgnore()] must not be displayed!";
        RenderIgnoreWhenControl = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "[RenderIgnore(''Control'')]", "RenderIgnoreWhenControl");
        RenderIgnoreWhenControl.AttributeName = "[RenderIgnore(''Control'')]";
        RenderIgnoreWhenDisplay = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "[RenderIgnore(''Display'')]", "RenderIgnoreWhenDisplay");
        RenderIgnoreWhenDisplay.AttributeName = "[RenderIgnore(''Display'')]";
        RenderIgnoreWhenControlAndShadow = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "[RenderIgnore(''Control'', ''ShadowControl'')]", "RenderIgnoreWhenControlAndShadow");
        RenderIgnoreWhenControlAndShadow.AttributeName = "[RenderIgnore(''Control'', ''ShadowControl'')]";
        RenderIgnoreWhenDisplayAndShadow = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "[RenderIgnore(''Display'', ''ShadowDisplay'')]", "RenderIgnoreWhenDisplayAndShadow");
        RenderIgnoreWhenDisplayAndShadow.AttributeName = "[RenderIgnore(''Display'', ''ShadowDisplay'')]";
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