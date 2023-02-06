using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class testingProgram : Ix.Connector.ITwinObject
{
    [Container(Layout.Wrap)]
    public stTestPrimitive testPrimitive { get; }

    public stTestComplex testComplex { get; }

    [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(stTestEnum))]
    public OnlinerInt testEnum { get; }

    [Container(Layout.UniformGrid)]
    public stTestRenderIgnore testRenderIgnore { get; }

    public TestEmpty testEmpty { get; }

    public TestLayoutOverwriting testLayoutOverwrite { get; }

    public TestMixed testMixed { get; }

    public TestMultipleLayouts testMultipleLayouts { get; }

    public TestSimple testSimple { get; }

    public TestWithoutLayouts testWithoutLayouts { get; }

    public TestSimpleNested testSimpleNested { get; }

    [Container(Layout.Tabs)]
    public stTestMultipleNested testMultipleNested { get; }

    public stTestLayouts testLayouts { get; }

    public testingProgram(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        testPrimitive = new stTestPrimitive(this, "instanceOfStPrimitive", "testPrimitive");
        testPrimitive.AttributeName = "instanceOfStPrimitive";
        testComplex = new stTestComplex(this, "instanceOfStPrimitive", "testComplex");
        testComplex.AttributeName = "instanceOfStPrimitive";
        testEnum = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "instanceOfStEnum", "testEnum");
        testEnum.AttributeName = "instanceOfStEnum";
        testEnum.AttributeName = "instanceOfStEnum";
        testRenderIgnore = new stTestRenderIgnore(this, "testRenderIgnore", "testRenderIgnore");
        testEmpty = new TestEmpty(this, "testEmpty", "testEmpty");
        testLayoutOverwrite = new TestLayoutOverwriting(this, "testLayoutOverwrite", "testLayoutOverwrite");
        testMixed = new TestMixed(this, "testMixed", "testMixed");
        testMultipleLayouts = new TestMultipleLayouts(this, "testMultipleLayouts", "testMultipleLayouts");
        testSimple = new TestSimple(this, "testSimple", "testSimple");
        testWithoutLayouts = new TestWithoutLayouts(this, "testWithoutLayouts", "testWithoutLayouts");
        testSimpleNested = new TestSimpleNested(this, "testSimpleNested", "testSimpleNested");
        testMultipleNested = new stTestMultipleNested(this, "testMultipleNested", "testMultipleNested");
        testLayouts = new stTestLayouts(this, "testLayouts", "testLayouts");
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