using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class full_of_primitives_match : Ix.Connector.ITwinObject
{
    public OnlinerBool myBOOL { get; }

    public OnlinerBool myBYTE { get; }

    public OnlinerBool myWORD { get; }

    public OnlinerBool myDWORD { get; }

    public OnlinerBool myLWORD { get; }

    public OnlinerBool mySINT { get; }

    public OnlinerBool myINT { get; }

    public OnlinerBool myDINT { get; }

    public OnlinerBool myLINT { get; }

    public OnlinerBool myUSINT { get; }

    public OnlinerBool myUINT { get; }

    public OnlinerBool myUDINT { get; }

    public OnlinerBool myULINT { get; }

    public OnlinerBool myREAL { get; }

    public OnlinerBool myLREAL { get; }

    public OnlinerBool myTIME { get; }

    public OnlinerBool myLTIME { get; }

    public OnlinerBool myDATE { get; }

    public OnlinerBool myLDATE { get; }

    public OnlinerBool myTIME_OF_DAY { get; }

    public OnlinerBool myLTIME_OF_DAY { get; }

    public OnlinerBool myDATE_AND_TIME { get; }

    public OnlinerBool myLDATE_AND_TIME { get; }

    public OnlinerBool myCHAR { get; }

    public OnlinerBool myWCHAR { get; }

    public OnlinerBool mySTRING { get; }

    public OnlinerBool myWSTRING { get; }

    public full_of_primitives_match(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myBOOL", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myBYTE", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myWORD", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myDWORD", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myLWORD", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "mySINT", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myINT", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myDINT", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myLINT", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myUSINT", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myUINT", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myUDINT", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myULINT", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myREAL", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myLREAL", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myTIME", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myLTIME", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myDATE", "myDATE");
        myLDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myLDATE", "myLDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myTIME_OF_DAY", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myLTIME_OF_DAY", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myDATE_AND_TIME", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myLDATE_AND_TIME", "myLDATE_AND_TIME");
        myCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myCHAR", "myCHAR");
        myWCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myWCHAR", "myWCHAR");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "mySTRING", "mySTRING");
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myWSTRING", "myWSTRING");
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