using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

[Container(Layout.Wrap)]
public partial class test_primitive : Ix.Connector.ITwinObject
{
    public OnlinerInt testInteger { get; }

    public OnlinerInt testUInteger { get; }

    public OnlinerString testString { get; }

    public OnlinerWord testWord { get; }

    public OnlinerByte testByte { get; }

    public OnlinerReal testReal { get; }

    public OnlinerLReal testLReal { get; }

    public OnlinerBool testBool { get; }

    public OnlinerDate TestDate { get; }

    public OnlinerDateTime TestDateTime { get; }

    public OnlinerTimeOfDay TestTimeOfDay { get; }

    [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(enumStationStatus))]
    public OnlinerInt Status { get; }

    public test_primitive(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        testInteger = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "<#Integer From PLC#>", "testInteger");
        testInteger.AttributeName = "<#Integer From PLC#>";
        testUInteger = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "<#UInteger From PLC#>", "testUInteger");
        testUInteger.AttributeName = "<#UInteger From PLC#>";
        testString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "<#STRING From PLC#>", "testString");
        testString.AttributeName = "<#STRING From PLC#>";
        testWord = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this, "<#WORD From PLC#>", "testWord");
        testWord.AttributeName = "<#WORD From PLC#>";
        testByte = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this, "<#BYTE From PLC#>", "testByte");
        testByte.AttributeName = "<#BYTE From PLC#>";
        testReal = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "<#REAL From PLC#>", "testReal");
        testReal.AttributeName = "<#REAL From PLC#>";
        testLReal = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "<#LREAL From PLC#>", "testLReal");
        testLReal.AttributeName = "<#LREAL From PLC#>";
        testBool = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "<#BOOL From PLC#>", "testBool");
        testBool.AttributeName = "<#BOOL From PLC#>";
        TestDate = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "<#DATE From PLC#>", "TestDate");
        TestDate.AttributeName = "<#DATE From PLC#>";
        TestDateTime = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this, "<#DATE_AND_TIME From PLC#>", "TestDateTime");
        TestDateTime.AttributeName = "<#DATE_AND_TIME From PLC#>";
        TestTimeOfDay = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this, "<#TIME_OF_DAY From PLC#>", "TestTimeOfDay");
        TestTimeOfDay.AttributeName = "<#TIME_OF_DAY From PLC#>";
        Status = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "<#ENUM Station status#>", "Status");
        Status.AttributeName = "<#ENUM Station status#>";
        Status.AttributeName = "<#ENUM Station status#>";
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public Pocos.test_primitive OnlineToPlain()
    {
        Pocos.test_primitive plain = new Pocos.test_primitive();
        plain.testInteger = testInteger.LastValue;
        plain.testUInteger = testUInteger.LastValue;
        plain.testString = testString.LastValue;
        plain.testWord = testWord.LastValue;
        plain.testByte = testByte.LastValue;
        plain.testReal = testReal.LastValue;
        plain.testLReal = testLReal.LastValue;
        plain.testBool = testBool.LastValue;
        plain.TestDate = TestDate.LastValue;
        plain.TestDateTime = TestDateTime.LastValue;
        plain.TestTimeOfDay = TestTimeOfDay.LastValue;
        plain.Status = (enumStationStatus)Status.LastValue;
        ;
        return plain;
    }

    public void PlainToOnline(Pocos.test_primitive plain)
    {
        testInteger.Cyclic = plain.testInteger;
        testUInteger.Cyclic = plain.testUInteger;
        testString.Cyclic = plain.testString;
        testWord.Cyclic = plain.testWord;
        testByte.Cyclic = plain.testByte;
        testReal.Cyclic = plain.testReal;
        testLReal.Cyclic = plain.testLReal;
        testBool.Cyclic = plain.testBool;
        TestDate.Cyclic = plain.TestDate;
        TestDateTime.Cyclic = plain.TestDateTime;
        TestTimeOfDay.Cyclic = plain.TestTimeOfDay;
        Status.Cyclic = (short)plain.Status;
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