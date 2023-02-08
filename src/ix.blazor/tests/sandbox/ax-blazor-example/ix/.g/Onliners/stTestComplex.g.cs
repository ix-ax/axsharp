using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class stTestComplex : Ix.Connector.ITwinObject
{
    public OnlinerInt testInteger { get; }

    public stComplex testComplexInstance { get; }

    public OnlinerString testString { get; }

    [Container(Layout.Stack)]
    public stComplexUnknown testComplexUnknownInstance { get; }

    public stTestComplex(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        testInteger = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "<#Integer From PLC#>", "testInteger");
        testInteger.AttributeName = "<#Integer From PLC#>";
        testComplexInstance = new stComplex(this, "<#Complex Instance#>", "testComplexInstance");
        testComplexInstance.AttributeName = "<#Complex Instance#>";
        testString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "<#String From PLC#>", "testString");
        testString.AttributeName = "<#String From PLC#>";
        testComplexUnknownInstance = new stComplexUnknown(this, "<#Complex Instance#>", "testComplexUnknownInstance");
        testComplexUnknownInstance.AttributeName = "<#Complex Instance#>";
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.stTestComplex> OnlineToPlainAsync()
    {
        Pocos.stTestComplex plain = new Pocos.stTestComplex();
        await this.ReadAsync();
        plain.testInteger = testInteger.LastValue;
        plain.testComplexInstance = await testComplexInstance.OnlineToPlainAsync();
        plain.testString = testString.LastValue;
        plain.testComplexUnknownInstance = await testComplexUnknownInstance.OnlineToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.stTestComplex> OnlineToPlainAsync(Pocos.stTestComplex plain)
    {
        plain.testInteger = testInteger.LastValue;
        plain.testComplexInstance = await testComplexInstance.OnlineToPlainAsync();
        plain.testString = testString.LastValue;
        plain.testComplexUnknownInstance = await testComplexUnknownInstance.OnlineToPlainAsync();
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.stTestComplex plain)
    {
        testInteger.Cyclic = plain.testInteger;
        await this.testComplexInstance.PlainToOnlineAsync(plain.testComplexInstance);
        testString.Cyclic = plain.testString;
        await this.testComplexUnknownInstance.PlainToOnlineAsync(plain.testComplexUnknownInstance);
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