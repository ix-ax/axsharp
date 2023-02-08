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

    public OnlinerLTimeOfDay myLTIME_OF_DAY { get; }

    public OnlinerDateTime myDATE_AND_TIME { get; }

    public OnlinerLDateTime myLDATE_AND_TIME { get; }

    public OnlinerChar myCHAR { get; }

    public OnlinerWChar myWCHAR { get; }

    public OnlinerString mySTRING { get; }

    public OnlinerWString myWSTRING { get; }

    [ReadOnce()]
    public OnlinerWString myWSTRING_readOnce { get; }

    [ReadOnly()]
    public OnlinerWString myWSTRING_readOnly { get; }

    [ReadOnce()]
    public ComplexForConfig cReadOnce { get; }

    [ReadOnly()]
    public ComplexForConfig cReadOnly { get; }

    public unitsTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Complex = new ComplexForConfig(this.Connector, "", "Complex");
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this.Connector, "", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this.Connector, "", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateDWORD(this.Connector, "", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateLWORD(this.Connector, "", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateSINT(this.Connector, "", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateDINT(this.Connector, "", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this.Connector, "", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUSINT(this.Connector, "", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this.Connector, "", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUDINT(this.Connector, "", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this.Connector, "", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this.Connector, "", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME(this.Connector, "", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME(this.Connector, "", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this.Connector, "", "myDATE");
        myLDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE(this.Connector, "", "myLDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this.Connector, "", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME_OF_DAY(this.Connector, "", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this.Connector, "", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE_AND_TIME(this.Connector, "", "myLDATE_AND_TIME");
        myCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateCHAR(this.Connector, "", "myCHAR");
        myWCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateWCHAR(this.Connector, "", "myWCHAR");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING");
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING");
        myWSTRING_readOnce = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_readOnce");
        myWSTRING_readOnce.MakeReadOnce();
        myWSTRING_readOnly = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_readOnly");
        myWSTRING_readOnly.MakeReadOnly();
        cReadOnce = new ComplexForConfig(this.Connector, "", "cReadOnce");
        cReadOnce.MakeReadOnce();
        cReadOnly = new ComplexForConfig(this.Connector, "", "cReadOnly");
        cReadOnly.MakeReadOnly();
    }

    public unitsTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Complex = new ComplexForConfig(this.Connector, "", "Complex");
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this.Connector, "", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this.Connector, "", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateDWORD(this.Connector, "", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateLWORD(this.Connector, "", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateSINT(this.Connector, "", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateDINT(this.Connector, "", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this.Connector, "", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUSINT(this.Connector, "", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this.Connector, "", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUDINT(this.Connector, "", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this.Connector, "", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this.Connector, "", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME(this.Connector, "", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME(this.Connector, "", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this.Connector, "", "myDATE");
        myLDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE(this.Connector, "", "myLDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this.Connector, "", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME_OF_DAY(this.Connector, "", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this.Connector, "", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE_AND_TIME(this.Connector, "", "myLDATE_AND_TIME");
        myCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateCHAR(this.Connector, "", "myCHAR");
        myWCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateWCHAR(this.Connector, "", "myWCHAR");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING");
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING");
        myWSTRING_readOnce = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_readOnce");
        myWSTRING_readOnce.MakeReadOnce();
        myWSTRING_readOnly = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_readOnly");
        myWSTRING_readOnly.MakeReadOnly();
        cReadOnce = new ComplexForConfig(this.Connector, "", "cReadOnce");
        cReadOnce.MakeReadOnce();
        cReadOnly = new ComplexForConfig(this.Connector, "", "cReadOnly");
        cReadOnly.MakeReadOnly();
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

    public OnlinerLTimeOfDay myLTIME_OF_DAY { get; }

    public OnlinerDateTime myDATE_AND_TIME { get; }

    public OnlinerLDateTime myLDATE_AND_TIME { get; }

    public OnlinerChar myCHAR { get; }

    public OnlinerWChar myWCHAR { get; }

    public OnlinerString mySTRING { get; }

    public OnlinerWString myWSTRING { get; }

    public ComplexForConfig(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myBOOL", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this, "myBYTE", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this, "myWORD", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateDWORD(this, "myDWORD", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateLWORD(this, "myLWORD", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateSINT(this, "mySINT", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "myINT", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateDINT(this, "myDINT", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this, "myLINT", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUSINT(this, "myUSINT", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this, "myUINT", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUDINT(this, "myUDINT", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "myULINT", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "myREAL", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "myLREAL", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME(this, "myTIME", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME(this, "myLTIME", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "myDATE", "myDATE");
        myLDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE(this, "myLDATE", "myLDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this, "myTIME_OF_DAY", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME_OF_DAY(this, "myLTIME_OF_DAY", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this, "myDATE_AND_TIME", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE_AND_TIME(this, "myLDATE_AND_TIME", "myLDATE_AND_TIME");
        myCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateCHAR(this, "myCHAR", "myCHAR");
        myWCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateWCHAR(this, "myWCHAR", "myWCHAR");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "mySTRING", "mySTRING");
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this, "myWSTRING", "myWSTRING");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.ComplexForConfig> OnlineToPlainAsync()
    {
        Pocos.ComplexForConfig plain = new Pocos.ComplexForConfig();
        await this.ReadAsync();
        plain.myBOOL = myBOOL.LastValue;
        plain.myBYTE = myBYTE.LastValue;
        plain.myWORD = myWORD.LastValue;
        plain.myDWORD = myDWORD.LastValue;
        plain.myLWORD = myLWORD.LastValue;
        plain.mySINT = mySINT.LastValue;
        plain.myINT = myINT.LastValue;
        plain.myDINT = myDINT.LastValue;
        plain.myLINT = myLINT.LastValue;
        plain.myUSINT = myUSINT.LastValue;
        plain.myUINT = myUINT.LastValue;
        plain.myUDINT = myUDINT.LastValue;
        plain.myULINT = myULINT.LastValue;
        plain.myREAL = myREAL.LastValue;
        plain.myLREAL = myLREAL.LastValue;
        plain.myTIME = myTIME.LastValue;
        plain.myLTIME = myLTIME.LastValue;
        plain.myDATE = myDATE.LastValue;
        plain.myLDATE = myLDATE.LastValue;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.LastValue;
        plain.myLTIME_OF_DAY = myLTIME_OF_DAY.LastValue;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.LastValue;
        plain.myLDATE_AND_TIME = myLDATE_AND_TIME.LastValue;
        plain.myCHAR = myCHAR.LastValue;
        plain.myWCHAR = myWCHAR.LastValue;
        plain.mySTRING = mySTRING.LastValue;
        plain.myWSTRING = myWSTRING.LastValue;
        return plain;
    }

    protected async Task<Pocos.ComplexForConfig> OnlineToPlainAsync(Pocos.ComplexForConfig plain)
    {
        plain.myBOOL = myBOOL.LastValue;
        plain.myBYTE = myBYTE.LastValue;
        plain.myWORD = myWORD.LastValue;
        plain.myDWORD = myDWORD.LastValue;
        plain.myLWORD = myLWORD.LastValue;
        plain.mySINT = mySINT.LastValue;
        plain.myINT = myINT.LastValue;
        plain.myDINT = myDINT.LastValue;
        plain.myLINT = myLINT.LastValue;
        plain.myUSINT = myUSINT.LastValue;
        plain.myUINT = myUINT.LastValue;
        plain.myUDINT = myUDINT.LastValue;
        plain.myULINT = myULINT.LastValue;
        plain.myREAL = myREAL.LastValue;
        plain.myLREAL = myLREAL.LastValue;
        plain.myTIME = myTIME.LastValue;
        plain.myLTIME = myLTIME.LastValue;
        plain.myDATE = myDATE.LastValue;
        plain.myLDATE = myLDATE.LastValue;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.LastValue;
        plain.myLTIME_OF_DAY = myLTIME_OF_DAY.LastValue;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.LastValue;
        plain.myLDATE_AND_TIME = myLDATE_AND_TIME.LastValue;
        plain.myCHAR = myCHAR.LastValue;
        plain.myWCHAR = myWCHAR.LastValue;
        plain.mySTRING = mySTRING.LastValue;
        plain.myWSTRING = myWSTRING.LastValue;
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ComplexForConfig plain)
    {
        myBOOL.Cyclic = plain.myBOOL;
        myBYTE.Cyclic = plain.myBYTE;
        myWORD.Cyclic = plain.myWORD;
        myDWORD.Cyclic = plain.myDWORD;
        myLWORD.Cyclic = plain.myLWORD;
        mySINT.Cyclic = plain.mySINT;
        myINT.Cyclic = plain.myINT;
        myDINT.Cyclic = plain.myDINT;
        myLINT.Cyclic = plain.myLINT;
        myUSINT.Cyclic = plain.myUSINT;
        myUINT.Cyclic = plain.myUINT;
        myUDINT.Cyclic = plain.myUDINT;
        myULINT.Cyclic = plain.myULINT;
        myREAL.Cyclic = plain.myREAL;
        myLREAL.Cyclic = plain.myLREAL;
        myTIME.Cyclic = plain.myTIME;
        myLTIME.Cyclic = plain.myLTIME;
        myDATE.Cyclic = plain.myDATE;
        myLDATE.Cyclic = plain.myLDATE;
        myTIME_OF_DAY.Cyclic = plain.myTIME_OF_DAY;
        myLTIME_OF_DAY.Cyclic = plain.myLTIME_OF_DAY;
        myDATE_AND_TIME.Cyclic = plain.myDATE_AND_TIME;
        myLDATE_AND_TIME.Cyclic = plain.myLDATE_AND_TIME;
        myCHAR.Cyclic = plain.myCHAR;
        myWCHAR.Cyclic = plain.myWCHAR;
        mySTRING.Cyclic = plain.mySTRING;
        myWSTRING.Cyclic = plain.myWSTRING;
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