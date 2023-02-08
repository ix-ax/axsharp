using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class stComplexUnknown : Ix.Connector.ITwinObject
{
    public OnlinerString stComplexUnknownString { get; }

    public OnlinerBool testBool { get; }

    public OnlinerInt stComplexUnknowInteger { get; }

    public OnlinerDate TestDate { get; }

    public stComplexUnknown(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        stComplexUnknownString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "<#String unknown From PLC#>", "stComplexUnknownString");
        stComplexUnknownString.AttributeName = "<#String unknown From PLC#>";
        testBool = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "<#BOOL unknown From PLC#>", "testBool");
        testBool.AttributeName = "<#BOOL unknown From PLC#>";
        stComplexUnknowInteger = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "<#Integer unknown From PLC#>", "stComplexUnknowInteger");
        stComplexUnknowInteger.AttributeName = "<#Integer unknown From PLC#>";
        TestDate = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "<#DATE unknown From PLC#>", "TestDate");
        TestDate.AttributeName = "<#DATE unknown From PLC#>";
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.stComplexUnknown> OnlineToPlainAsync()
    {
        Pocos.stComplexUnknown plain = new Pocos.stComplexUnknown();
        await this.ReadAsync();
        plain.stComplexUnknownString = stComplexUnknownString.LastValue;
        plain.testBool = testBool.LastValue;
        plain.stComplexUnknowInteger = stComplexUnknowInteger.LastValue;
        plain.TestDate = TestDate.LastValue;
        return plain;
    }

    protected async Task<Pocos.stComplexUnknown> OnlineToPlainAsync(Pocos.stComplexUnknown plain)
    {
        plain.stComplexUnknownString = stComplexUnknownString.LastValue;
        plain.testBool = testBool.LastValue;
        plain.stComplexUnknowInteger = stComplexUnknowInteger.LastValue;
        plain.TestDate = TestDate.LastValue;
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.stComplexUnknown plain)
    {
        stComplexUnknownString.Cyclic = plain.stComplexUnknownString;
        testBool.Cyclic = plain.testBool;
        stComplexUnknowInteger.Cyclic = plain.stComplexUnknowInteger;
        TestDate.Cyclic = plain.TestDate;
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