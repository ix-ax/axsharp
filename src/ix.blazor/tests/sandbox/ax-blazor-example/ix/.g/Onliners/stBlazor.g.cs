using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class stBlazor : Ix.Connector.ITwinObject
{
    public OnlinerInt testInteger { get; }

    [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(stTestEnum))]
    public OnlinerInt testEnum { get; }

    public OnlinerString testString { get; }

    public OnlinerReal testReal { get; }

    [RenderIgnore("Display")]
    public OnlinerLReal testLReal { get; }

    public OnlinerBool testBool { get; }

    [RenderIgnore()]
    public stComplex complexInstance { get; }

    public stTest3 testInstance { get; }

    public stTest3 testInstance2 { get; }

    public stTest testInstance3 { get; }

    public stBlazor(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        testInteger = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "<#Integer From PLC#>", "testInteger");
        testInteger.AttributeName = "<#Integer From PLC#>";
        testEnum = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "testEnum", "testEnum");
        testEnum.AttributeName = "testEnum";
        testEnum.AttributeName = "testEnum";
        testString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "<#String From PLC#>", "testString");
        testString.AttributeName = "<#String From PLC#>";
        testReal = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "testReal", "testReal");
        testLReal = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "testLReal", "testLReal");
        testBool = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "testBool", "testBool");
        complexInstance = new stComplex(this, "Complex", "complexInstance");
        complexInstance.AttributeName = "Complex";
        testInstance = new stTest3(this, "testInstance1", "testInstance");
        testInstance.AttributeName = "testInstance1";
        testInstance2 = new stTest3(this, "testInstance2", "testInstance2");
        testInstance2.AttributeName = "testInstance2";
        testInstance3 = new stTest(this, "testInstance3", "testInstance3");
        testInstance3.AttributeName = "testInstance3";
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