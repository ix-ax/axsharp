using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;

public partial class unitsTwinController : ITwinController
{
    public AXSharp.Connector.Connector Connector { get; }

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

    [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Colorss))]
    public OnlinerInt Colorss { get; }

    [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Colorsss))]
    public OnlinerULInt Colorsss { get; }

    [CompilerOmitsAttribute("POCO")]
    public OnlinerBool _must_be_omitted_in_poco { get; }

    public unitsTwinController(AXSharp.Connector.ConnectorAdapter adapter, object[] parameters)
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
        Colorss = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "Colorss");
        Colorsss = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Colorsss", "Colorsss");
        _must_be_omitted_in_poco = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "_must_be_omitted_in_poco");
    }

    public unitsTwinController(AXSharp.Connector.ConnectorAdapter adapter)
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
        Colorss = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "Colorss");
        Colorsss = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Colorsss", "Colorsss");
        _must_be_omitted_in_poco = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "_must_be_omitted_in_poco");
    }
}

public partial class ComplexForConfig : AXSharp.Connector.ITwinObject
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

    public Motor myMotor { get; }

    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public ComplexForConfig(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        PreConstruct(parent, readableTail, symbolTail);
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
        myMotor = new Motor(this, "myMotor", "myMotor");
        parent.AddChild(this);
        parent.AddKid(this);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public async virtual Task<T> OnlineToPlain<T>()
    {
        return await (dynamic)this.OnlineToPlainAsync();
    }

    public async Task<Pocos.ComplexForConfig> OnlineToPlainAsync()
    {
        Pocos.ComplexForConfig plain = new Pocos.ComplexForConfig();
        await this.ReadAsync<IgnoreOnPocoOperation>();
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
#pragma warning disable CS0612
        plain.myMotor = await myMotor._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<Pocos.ComplexForConfig> _OnlineToPlainNoacAsync()
    {
        Pocos.ComplexForConfig plain = new Pocos.ComplexForConfig();
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
#pragma warning disable CS0612
        plain.myMotor = await myMotor._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    protected async Task<Pocos.ComplexForConfig> _OnlineToPlainNoacAsync(Pocos.ComplexForConfig plain)
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
#pragma warning disable CS0612
        plain.myMotor = await myMotor._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain)
    {
        await this.PlainToOnlineAsync((dynamic)plain);
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
#pragma warning disable CS0612
        await this.myMotor._PlainToOnlineNoacAsync(plain.myMotor);
#pragma warning restore CS0612
        return await this.WriteAsync<IgnoreOnPocoOperation>();
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task _PlainToOnlineNoacAsync(Pocos.ComplexForConfig plain)
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
#pragma warning disable CS0612
        await this.myMotor._PlainToOnlineNoacAsync(plain.myMotor);
#pragma warning restore CS0612
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<Pocos.ComplexForConfig> ShadowToPlainAsync()
    {
        Pocos.ComplexForConfig plain = new Pocos.ComplexForConfig();
        plain.myBOOL = myBOOL.Shadow;
        plain.myBYTE = myBYTE.Shadow;
        plain.myWORD = myWORD.Shadow;
        plain.myDWORD = myDWORD.Shadow;
        plain.myLWORD = myLWORD.Shadow;
        plain.mySINT = mySINT.Shadow;
        plain.myINT = myINT.Shadow;
        plain.myDINT = myDINT.Shadow;
        plain.myLINT = myLINT.Shadow;
        plain.myUSINT = myUSINT.Shadow;
        plain.myUINT = myUINT.Shadow;
        plain.myUDINT = myUDINT.Shadow;
        plain.myULINT = myULINT.Shadow;
        plain.myREAL = myREAL.Shadow;
        plain.myLREAL = myLREAL.Shadow;
        plain.myTIME = myTIME.Shadow;
        plain.myLTIME = myLTIME.Shadow;
        plain.myDATE = myDATE.Shadow;
        plain.myLDATE = myLDATE.Shadow;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.Shadow;
        plain.myLTIME_OF_DAY = myLTIME_OF_DAY.Shadow;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.Shadow;
        plain.myLDATE_AND_TIME = myLDATE_AND_TIME.Shadow;
        plain.myCHAR = myCHAR.Shadow;
        plain.myWCHAR = myWCHAR.Shadow;
        plain.mySTRING = mySTRING.Shadow;
        plain.myWSTRING = myWSTRING.Shadow;
        plain.myMotor = await myMotor.ShadowToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.ComplexForConfig> ShadowToPlainAsync(Pocos.ComplexForConfig plain)
    {
        plain.myBOOL = myBOOL.Shadow;
        plain.myBYTE = myBYTE.Shadow;
        plain.myWORD = myWORD.Shadow;
        plain.myDWORD = myDWORD.Shadow;
        plain.myLWORD = myLWORD.Shadow;
        plain.mySINT = mySINT.Shadow;
        plain.myINT = myINT.Shadow;
        plain.myDINT = myDINT.Shadow;
        plain.myLINT = myLINT.Shadow;
        plain.myUSINT = myUSINT.Shadow;
        plain.myUINT = myUINT.Shadow;
        plain.myUDINT = myUDINT.Shadow;
        plain.myULINT = myULINT.Shadow;
        plain.myREAL = myREAL.Shadow;
        plain.myLREAL = myLREAL.Shadow;
        plain.myTIME = myTIME.Shadow;
        plain.myLTIME = myLTIME.Shadow;
        plain.myDATE = myDATE.Shadow;
        plain.myLDATE = myLDATE.Shadow;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.Shadow;
        plain.myLTIME_OF_DAY = myLTIME_OF_DAY.Shadow;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.Shadow;
        plain.myLDATE_AND_TIME = myLDATE_AND_TIME.Shadow;
        plain.myCHAR = myCHAR.Shadow;
        plain.myWCHAR = myWCHAR.Shadow;
        plain.mySTRING = mySTRING.Shadow;
        plain.myWSTRING = myWSTRING.Shadow;
        plain.myMotor = await myMotor.ShadowToPlainAsync();
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ComplexForConfig plain)
    {
        myBOOL.Shadow = plain.myBOOL;
        myBYTE.Shadow = plain.myBYTE;
        myWORD.Shadow = plain.myWORD;
        myDWORD.Shadow = plain.myDWORD;
        myLWORD.Shadow = plain.myLWORD;
        mySINT.Shadow = plain.mySINT;
        myINT.Shadow = plain.myINT;
        myDINT.Shadow = plain.myDINT;
        myLINT.Shadow = plain.myLINT;
        myUSINT.Shadow = plain.myUSINT;
        myUINT.Shadow = plain.myUINT;
        myUDINT.Shadow = plain.myUDINT;
        myULINT.Shadow = plain.myULINT;
        myREAL.Shadow = plain.myREAL;
        myLREAL.Shadow = plain.myLREAL;
        myTIME.Shadow = plain.myTIME;
        myLTIME.Shadow = plain.myLTIME;
        myDATE.Shadow = plain.myDATE;
        myLDATE.Shadow = plain.myLDATE;
        myTIME_OF_DAY.Shadow = plain.myTIME_OF_DAY;
        myLTIME_OF_DAY.Shadow = plain.myLTIME_OF_DAY;
        myDATE_AND_TIME.Shadow = plain.myDATE_AND_TIME;
        myLDATE_AND_TIME.Shadow = plain.myLDATE_AND_TIME;
        myCHAR.Shadow = plain.myCHAR;
        myWCHAR.Shadow = plain.myWCHAR;
        mySTRING.Shadow = plain.mySTRING;
        myWSTRING.Shadow = plain.myWSTRING;
        await this.myMotor.PlainToShadowAsync(plain.myMotor);
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.ComplexForConfig CreateEmptyPoco()
    {
        return new Pocos.ComplexForConfig();
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

    public AXSharp.Connector.Localizations.Translator Interpreter => global::units.PlcTranslator.Instance;
}

public enum Colorss
{
    Red,
    Green,
    Blue
}

public enum Colorsss : UInt64
{
    Red = 1,
    Green = 2,
    Blue = 3
}

public partial class Motor : AXSharp.Connector.ITwinObject
{
    public OnlinerBool isRunning { get; }

    public Motor(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        isRunning = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "isRunning", "isRunning");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async virtual Task<T> OnlineToPlain<T>()
    {
        return await (dynamic)this.OnlineToPlainAsync();
    }

    public async Task<Pocos.Motor> OnlineToPlainAsync()
    {
        Pocos.Motor plain = new Pocos.Motor();
        await this.ReadAsync<IgnoreOnPocoOperation>();
        plain.isRunning = isRunning.LastValue;
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<Pocos.Motor> _OnlineToPlainNoacAsync()
    {
        Pocos.Motor plain = new Pocos.Motor();
        plain.isRunning = isRunning.LastValue;
        return plain;
    }

    protected async Task<Pocos.Motor> OnlineToPlainAsync(Pocos.Motor plain)
    {
        plain.isRunning = isRunning.LastValue;
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain)
    {
        await this.PlainToOnlineAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Motor plain)
    {
        isRunning.Cyclic = plain.isRunning;
        return await this.WriteAsync<IgnoreOnPocoOperation>();
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task _PlainToOnlineNoacAsync(Pocos.Motor plain)
    {
        isRunning.Cyclic = plain.isRunning;
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<Pocos.Motor> ShadowToPlainAsync()
    {
        Pocos.Motor plain = new Pocos.Motor();
        plain.isRunning = isRunning.Shadow;
        return plain;
    }

    protected async Task<Pocos.Motor> ShadowToPlainAsync(Pocos.Motor plain)
    {
        plain.isRunning = isRunning.Shadow;
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Motor plain)
    {
        isRunning.Shadow = plain.isRunning;
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.Motor CreateEmptyPoco()
    {
        return new Pocos.Motor();
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

    public AXSharp.Connector.Localizations.Translator Interpreter => global::units.PlcTranslator.Instance;
}

public partial class Vehicle : AXSharp.Connector.ITwinObject
{
    public Motor m { get; }

    public OnlinerInt displacement { get; }

    public Vehicle(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        m = new Motor(this, "m", "m");
        displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async virtual Task<T> OnlineToPlain<T>()
    {
        return await (dynamic)this.OnlineToPlainAsync();
    }

    public async Task<Pocos.Vehicle> OnlineToPlainAsync()
    {
        Pocos.Vehicle plain = new Pocos.Vehicle();
        await this.ReadAsync<IgnoreOnPocoOperation>();
#pragma warning disable CS0612
        plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
        plain.displacement = displacement.LastValue;
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<Pocos.Vehicle> _OnlineToPlainNoacAsync()
    {
        Pocos.Vehicle plain = new Pocos.Vehicle();
#pragma warning disable CS0612
        plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
        plain.displacement = displacement.LastValue;
        return plain;
    }

    protected async Task<Pocos.Vehicle> OnlineToPlainAsync(Pocos.Vehicle plain)
    {
#pragma warning disable CS0612
        plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
        plain.displacement = displacement.LastValue;
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain)
    {
        await this.PlainToOnlineAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Vehicle plain)
    {
#pragma warning disable CS0612
        await this.m._PlainToOnlineNoacAsync(plain.m);
#pragma warning restore CS0612
        displacement.Cyclic = plain.displacement;
        return await this.WriteAsync<IgnoreOnPocoOperation>();
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task _PlainToOnlineNoacAsync(Pocos.Vehicle plain)
    {
#pragma warning disable CS0612
        await this.m._PlainToOnlineNoacAsync(plain.m);
#pragma warning restore CS0612
        displacement.Cyclic = plain.displacement;
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<Pocos.Vehicle> ShadowToPlainAsync()
    {
        Pocos.Vehicle plain = new Pocos.Vehicle();
        plain.m = await m.ShadowToPlainAsync();
        plain.displacement = displacement.Shadow;
        return plain;
    }

    protected async Task<Pocos.Vehicle> ShadowToPlainAsync(Pocos.Vehicle plain)
    {
        plain.m = await m.ShadowToPlainAsync();
        plain.displacement = displacement.Shadow;
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Vehicle plain)
    {
        await this.m.PlainToShadowAsync(plain.m);
        displacement.Shadow = plain.displacement;
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.Vehicle CreateEmptyPoco()
    {
        return new Pocos.Vehicle();
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

    public AXSharp.Connector.Localizations.Translator Interpreter => global::units.PlcTranslator.Instance;
}