using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

public partial class all_primitives : AXSharp.Connector.ITwinObject
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

    public OnlinerTimeOfDay myTIME_OF_DAY { get; }

    public OnlinerDateTime myDATE_AND_TIME { get; }

    public OnlinerString mySTRING { get; }

    public OnlinerWString myWSTRING { get; }

    [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(myEnum))]
    public OnlinerInt myEnum { get; }

    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public all_primitives(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this, "myTIME_OF_DAY", "myTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this, "myDATE_AND_TIME", "myDATE_AND_TIME");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "mySTRING", "mySTRING");
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this, "myWSTRING", "myWSTRING");
        myEnum = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "myEnum", "myEnum");
        parent.AddChild(this);
        parent.AddKid(this);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public virtual T OnlineToPlain<T>()
    {
        return (dynamic)this.OnlineToPlainAsync().Result;
    }

    public async Task<Pocos.all_primitives> OnlineToPlainAsync()
    {
        Pocos.all_primitives plain = new Pocos.all_primitives();
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
        plain.myTIME_OF_DAY = myTIME_OF_DAY.LastValue;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.LastValue;
        plain.mySTRING = mySTRING.LastValue;
        plain.myWSTRING = myWSTRING.LastValue;
        plain.myEnum = (myEnum)myEnum.LastValue;
        return plain;
    }

    protected async Task<Pocos.all_primitives> OnlineToPlainAsync(Pocos.all_primitives plain)
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
        plain.myTIME_OF_DAY = myTIME_OF_DAY.LastValue;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.LastValue;
        plain.mySTRING = mySTRING.LastValue;
        plain.myWSTRING = myWSTRING.LastValue;
        plain.myEnum = (myEnum)myEnum.LastValue;
        return plain;
    }

    public virtual void PlainToOnline<T>(T plain)
    {
        this.PlainToOnlineAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.all_primitives plain)
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
        myTIME_OF_DAY.Cyclic = plain.myTIME_OF_DAY;
        myDATE_AND_TIME.Cyclic = plain.myDATE_AND_TIME;
        mySTRING.Cyclic = plain.mySTRING;
        myWSTRING.Cyclic = plain.myWSTRING;
        myEnum.Cyclic = (short)plain.myEnum;
        return await this.WriteAsync();
    }

    public virtual T ShadowToPlain<T>()
    {
        return (dynamic)this.ShadowToPlainAsync().Result;
    }

    public async Task<Pocos.all_primitives> ShadowToPlainAsync()
    {
        Pocos.all_primitives plain = new Pocos.all_primitives();
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
        plain.myTIME_OF_DAY = myTIME_OF_DAY.Shadow;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.Shadow;
        plain.mySTRING = mySTRING.Shadow;
        plain.myWSTRING = myWSTRING.Shadow;
        plain.myEnum = (myEnum)myEnum.Shadow;
        return plain;
    }

    protected async Task<Pocos.all_primitives> ShadowToPlainAsync(Pocos.all_primitives plain)
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
        plain.myTIME_OF_DAY = myTIME_OF_DAY.Shadow;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.Shadow;
        plain.mySTRING = mySTRING.Shadow;
        plain.myWSTRING = myWSTRING.Shadow;
        plain.myEnum = (myEnum)myEnum.Shadow;
        return plain;
    }

    public virtual void PlainToShadow<T>(T plain)
    {
        this.PlainToShadowAsync((dynamic)plain).Wait();
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.all_primitives plain)
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
        myTIME_OF_DAY.Shadow = plain.myTIME_OF_DAY;
        myDATE_AND_TIME.Shadow = plain.myDATE_AND_TIME;
        mySTRING.Shadow = plain.mySTRING;
        myWSTRING.Shadow = plain.myWSTRING;
        myEnum.Shadow = (short)plain.myEnum;
        return this.RetrievePrimitives();
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public Pocos.all_primitives CreateEmptyPoco()
    {
        return new Pocos.all_primitives();
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
    public System.String AttributeName
    {
        get
        {
            return AXSharp.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_attributeName);
        }

        set
        {
            _attributeName = value;
        }
    }

    public string HumanReadable { get; set; }

    protected System.String @SymbolTail { get; set; }

    protected AXSharp.Connector.ITwinObject @Parent { get; set; }
}