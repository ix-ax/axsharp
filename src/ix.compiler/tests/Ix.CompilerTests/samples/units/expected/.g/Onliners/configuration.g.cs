using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class unitsTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public ComplexForConfig Complex { get; }

    public OnlinerBool myBOOL { get; }

    public OnlinerByte myBYTE { get; }

    public OnlinerWord myWORD { get; }

    public OnlinerDWord myDWORD { get; }

    public OnlinerLWord myLWORD { get; }

    public OnlinerSInt mySINT { get; }

    public OnlinerInt myINT { get; }

    public OnlinerDInt myDINT { get; }

    public OnlinerLInt myLINT { get; }

    public OnlinerUSInt myUSINT { get; }

    public OnlinerUInt myUINT { get; }

    public OnlinerUDInt myUDINT { get; }

    public OnlinerULInt myULINT { get; }

    public OnlinerReal myREAL { get; }

    public OnlinerLReal myLREAL { get; }

    public OnlinerTime myTIME { get; }

    public OnlinerLTime myLTIME { get; }

    public OnlinerDate myDATE { get; }

    public OnlinerDate myLDATE { get; }

    public OnlinerTimeOfDay myTIME_OF_DAY { get; }

    public OnlinerTimeOfDay myLTIME_OF_DAY { get; }

    public OnlinerDateTime myDATE_AND_TIME { get; }

    public OnlinerDateTime myLDATE_AND_TIME { get; }

    public OnlinerString myCHAR { get; }

    public OnlinerString myWCHAR { get; }

    public OnlinerString mySTRING { get; }

    public OnlinerWString myWSTRING { get; }

    public unitsTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Complex = new ComplexForConfig(this.Connector, "", "Complex");
        myBOOL = @Connector.Online.AdapterFactory.CreateBOOL(this.Connector, "", "myBOOL");
        myBYTE = @Connector.Online.AdapterFactory.CreateBYTE(this.Connector, "", "myBYTE");
        myWORD = @Connector.Online.AdapterFactory.CreateWORD(this.Connector, "", "myWORD");
        myDWORD = @Connector.Online.AdapterFactory.CreateDWORD(this.Connector, "", "myDWORD");
        myLWORD = @Connector.Online.AdapterFactory.CreateLWORD(this.Connector, "", "myLWORD");
        mySINT = @Connector.Online.AdapterFactory.CreateSINT(this.Connector, "", "mySINT");
        myINT = @Connector.Online.AdapterFactory.CreateINT(this.Connector, "", "myINT");
        myDINT = @Connector.Online.AdapterFactory.CreateDINT(this.Connector, "", "myDINT");
        myLINT = @Connector.Online.AdapterFactory.CreateLINT(this.Connector, "", "myLINT");
        myUSINT = @Connector.Online.AdapterFactory.CreateUSINT(this.Connector, "", "myUSINT");
        myUINT = @Connector.Online.AdapterFactory.CreateUINT(this.Connector, "", "myUINT");
        myUDINT = @Connector.Online.AdapterFactory.CreateUDINT(this.Connector, "", "myUDINT");
        myULINT = @Connector.Online.AdapterFactory.CreateULINT(this.Connector, "", "myULINT");
        myREAL = @Connector.Online.AdapterFactory.CreateREAL(this.Connector, "", "myREAL");
        myLREAL = @Connector.Online.AdapterFactory.CreateLREAL(this.Connector, "", "myLREAL");
        myTIME = @Connector.Online.AdapterFactory.CreateTIME(this.Connector, "", "myTIME");
        myLTIME = @Connector.Online.AdapterFactory.CreateLTIME(this.Connector, "", "myLTIME");
        myDATE = @Connector.Online.AdapterFactory.CreateDATE(this.Connector, "", "myDATE");
        myLDATE = @Connector.Online.AdapterFactory.CreateLDATE(this.Connector, "", "myLDATE");
        myTIME_OF_DAY = @Connector.Online.AdapterFactory.CreateTIME_OF_DAY(this.Connector, "", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.Online.AdapterFactory.CreateLTIME_OF_DAY(this.Connector, "", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.Online.AdapterFactory.CreateDATE_AND_TIME(this.Connector, "", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.Online.AdapterFactory.CreateLDATE_AND_TIME(this.Connector, "", "myLDATE_AND_TIME");
        myCHAR = @Connector.Online.AdapterFactory.CreateCHAR(this.Connector, "", "myCHAR");
        myWCHAR = @Connector.Online.AdapterFactory.CreateWCHAR(this.Connector, "", "myWCHAR");
        mySTRING = @Connector.Online.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING");
        myWSTRING = @Connector.Online.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING");
    }

    public unitsTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Complex = new ComplexForConfig(this.Connector, "", "Complex");
        myBOOL = @Connector.Online.AdapterFactory.CreateBOOL(this.Connector, "", "myBOOL");
        myBYTE = @Connector.Online.AdapterFactory.CreateBYTE(this.Connector, "", "myBYTE");
        myWORD = @Connector.Online.AdapterFactory.CreateWORD(this.Connector, "", "myWORD");
        myDWORD = @Connector.Online.AdapterFactory.CreateDWORD(this.Connector, "", "myDWORD");
        myLWORD = @Connector.Online.AdapterFactory.CreateLWORD(this.Connector, "", "myLWORD");
        mySINT = @Connector.Online.AdapterFactory.CreateSINT(this.Connector, "", "mySINT");
        myINT = @Connector.Online.AdapterFactory.CreateINT(this.Connector, "", "myINT");
        myDINT = @Connector.Online.AdapterFactory.CreateDINT(this.Connector, "", "myDINT");
        myLINT = @Connector.Online.AdapterFactory.CreateLINT(this.Connector, "", "myLINT");
        myUSINT = @Connector.Online.AdapterFactory.CreateUSINT(this.Connector, "", "myUSINT");
        myUINT = @Connector.Online.AdapterFactory.CreateUINT(this.Connector, "", "myUINT");
        myUDINT = @Connector.Online.AdapterFactory.CreateUDINT(this.Connector, "", "myUDINT");
        myULINT = @Connector.Online.AdapterFactory.CreateULINT(this.Connector, "", "myULINT");
        myREAL = @Connector.Online.AdapterFactory.CreateREAL(this.Connector, "", "myREAL");
        myLREAL = @Connector.Online.AdapterFactory.CreateLREAL(this.Connector, "", "myLREAL");
        myTIME = @Connector.Online.AdapterFactory.CreateTIME(this.Connector, "", "myTIME");
        myLTIME = @Connector.Online.AdapterFactory.CreateLTIME(this.Connector, "", "myLTIME");
        myDATE = @Connector.Online.AdapterFactory.CreateDATE(this.Connector, "", "myDATE");
        myLDATE = @Connector.Online.AdapterFactory.CreateLDATE(this.Connector, "", "myLDATE");
        myTIME_OF_DAY = @Connector.Online.AdapterFactory.CreateTIME_OF_DAY(this.Connector, "", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.Online.AdapterFactory.CreateLTIME_OF_DAY(this.Connector, "", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.Online.AdapterFactory.CreateDATE_AND_TIME(this.Connector, "", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.Online.AdapterFactory.CreateLDATE_AND_TIME(this.Connector, "", "myLDATE_AND_TIME");
        myCHAR = @Connector.Online.AdapterFactory.CreateCHAR(this.Connector, "", "myCHAR");
        myWCHAR = @Connector.Online.AdapterFactory.CreateWCHAR(this.Connector, "", "myWCHAR");
        mySTRING = @Connector.Online.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING");
        myWSTRING = @Connector.Online.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING");
    }
}

public partial class ComplexForConfig : Ix.Connector.ITwinObject
{
    public OnlinerBool myBOOL { get; }

    public OnlinerByte myBYTE { get; }

    public OnlinerWord myWORD { get; }

    public OnlinerDWord myDWORD { get; }

    public OnlinerLWord myLWORD { get; }

    public OnlinerSInt mySINT { get; }

    public OnlinerInt myINT { get; }

    public OnlinerDInt myDINT { get; }

    public OnlinerLInt myLINT { get; }

    public OnlinerUSInt myUSINT { get; }

    public OnlinerUInt myUINT { get; }

    public OnlinerUDInt myUDINT { get; }

    public OnlinerULInt myULINT { get; }

    public OnlinerReal myREAL { get; }

    public OnlinerLReal myLREAL { get; }

    public OnlinerTime myTIME { get; }

    public OnlinerLTime myLTIME { get; }

    public OnlinerDate myDATE { get; }

    public OnlinerDate myLDATE { get; }

    public OnlinerTimeOfDay myTIME_OF_DAY { get; }

    public OnlinerTimeOfDay myLTIME_OF_DAY { get; }

    public OnlinerDateTime myDATE_AND_TIME { get; }

    public OnlinerDateTime myLDATE_AND_TIME { get; }

    public OnlinerString myCHAR { get; }

    public OnlinerString myWCHAR { get; }

    public OnlinerString mySTRING { get; }

    public OnlinerWString myWSTRING { get; }

    public ComplexForConfig(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        myBOOL = @Connector.Online.AdapterFactory.CreateBOOL(this, "myBOOL", "myBOOL");
        myBYTE = @Connector.Online.AdapterFactory.CreateBYTE(this, "myBYTE", "myBYTE");
        myWORD = @Connector.Online.AdapterFactory.CreateWORD(this, "myWORD", "myWORD");
        myDWORD = @Connector.Online.AdapterFactory.CreateDWORD(this, "myDWORD", "myDWORD");
        myLWORD = @Connector.Online.AdapterFactory.CreateLWORD(this, "myLWORD", "myLWORD");
        mySINT = @Connector.Online.AdapterFactory.CreateSINT(this, "mySINT", "mySINT");
        myINT = @Connector.Online.AdapterFactory.CreateINT(this, "myINT", "myINT");
        myDINT = @Connector.Online.AdapterFactory.CreateDINT(this, "myDINT", "myDINT");
        myLINT = @Connector.Online.AdapterFactory.CreateLINT(this, "myLINT", "myLINT");
        myUSINT = @Connector.Online.AdapterFactory.CreateUSINT(this, "myUSINT", "myUSINT");
        myUINT = @Connector.Online.AdapterFactory.CreateUINT(this, "myUINT", "myUINT");
        myUDINT = @Connector.Online.AdapterFactory.CreateUDINT(this, "myUDINT", "myUDINT");
        myULINT = @Connector.Online.AdapterFactory.CreateULINT(this, "myULINT", "myULINT");
        myREAL = @Connector.Online.AdapterFactory.CreateREAL(this, "myREAL", "myREAL");
        myLREAL = @Connector.Online.AdapterFactory.CreateLREAL(this, "myLREAL", "myLREAL");
        myTIME = @Connector.Online.AdapterFactory.CreateTIME(this, "myTIME", "myTIME");
        myLTIME = @Connector.Online.AdapterFactory.CreateLTIME(this, "myLTIME", "myLTIME");
        myDATE = @Connector.Online.AdapterFactory.CreateDATE(this, "myDATE", "myDATE");
        myLDATE = @Connector.Online.AdapterFactory.CreateLDATE(this, "myLDATE", "myLDATE");
        myTIME_OF_DAY = @Connector.Online.AdapterFactory.CreateTIME_OF_DAY(this, "myTIME_OF_DAY", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.Online.AdapterFactory.CreateLTIME_OF_DAY(this, "myLTIME_OF_DAY", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.Online.AdapterFactory.CreateDATE_AND_TIME(this, "myDATE_AND_TIME", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.Online.AdapterFactory.CreateLDATE_AND_TIME(this, "myLDATE_AND_TIME", "myLDATE_AND_TIME");
        myCHAR = @Connector.Online.AdapterFactory.CreateCHAR(this, "myCHAR", "myCHAR");
        myWCHAR = @Connector.Online.AdapterFactory.CreateWCHAR(this, "myWCHAR", "myWCHAR");
        mySTRING = @Connector.Online.AdapterFactory.CreateSTRING(this, "mySTRING", "mySTRING");
        myWSTRING = @Connector.Online.AdapterFactory.CreateWSTRING(this, "myWSTRING", "myWSTRING");
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