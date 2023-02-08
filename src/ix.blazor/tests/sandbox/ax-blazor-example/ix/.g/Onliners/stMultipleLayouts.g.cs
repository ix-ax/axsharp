using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class stMultipleLayouts : Ix.Connector.ITwinObject
{
    [Container(Layout.Tabs)]
    public TestStructOneGroupWithLayout Servo_S5 { get; }

    [Container(Layout.Tabs)]
    public TestStruct Servo_S6 { get; }

    [Container(Layout.Tabs)]
    public TestStructMultipleGroups Servo_S7 { get; }

    [Container(Layout.Tabs)]
    public TestStructWithMainLayout Servo_S8 { get; }

    [Container(Layout.Stack)]
    public OnlinerString Piston_A1 { get; }

    public OnlinerString Piston_A2 { get; }

    public OnlinerString Piston_A3 { get; }

    public OnlinerString Piston_A4 { get; }

    [Container(Layout.Wrap)]
    public OnlinerInt Piston_A21 { get; }

    public OnlinerInt Piston_A22 { get; }

    public OnlinerInt Piston_A23 { get; }

    public OnlinerInt Piston_A24 { get; }

    [Container(Layout.Tabs)]
    public OnlinerReal Piston_A31 { get; }

    public OnlinerReal Piston_A32 { get; }

    public OnlinerReal Piston_A33 { get; }

    public OnlinerReal Piston_A34 { get; }

    [Container(Layout.UniformGrid)]
    public OnlinerBool Piston_A41 { get; }

    public OnlinerBool Piston_A42 { get; }

    public OnlinerBool Piston_A43 { get; }

    public OnlinerBool Piston_A44 { get; }

    [Container(Layout.Stack)]
    public OnlinerString Servo_S1 { get; }

    [Container(Layout.Stack)]
    public OnlinerString Servo_S2 { get; }

    [Container(Layout.Tabs)]
    public IxComponent component { get; }

    public stTest3 Servo_S3 { get; }

    public stTest3 Servo_S4 { get; }

    public stMultipleLayouts(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Servo_S5 = new TestStructOneGroupWithLayout(this, "With One Group Layout", "Servo_S5");
        Servo_S5.AttributeName = "With One Group Layout";
        Servo_S6 = new TestStruct(this, "Without Group Layout", "Servo_S6");
        Servo_S6.AttributeName = "Without Group Layout";
        Servo_S7 = new TestStructMultipleGroups(this, "With Multiple Groups", "Servo_S7");
        Servo_S7.AttributeName = "With Multiple Groups ";
        Servo_S8 = new TestStructWithMainLayout(this, "TestStruct With Main Layout", "Servo_S8");
        Servo_S8.AttributeName = "TestStruct With Main Layout  ";
        Piston_A1 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A1", "Piston_A1");
        Piston_A1.AttributeName = "A1";
        Piston_A2 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A2", "Piston_A2");
        Piston_A2.AttributeName = "A2";
        Piston_A3 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A3", "Piston_A3");
        Piston_A3.AttributeName = "A3";
        Piston_A4 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A4", "Piston_A4");
        Piston_A4.AttributeName = "A4";
        Piston_A21 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "A5", "Piston_A21");
        Piston_A21.AttributeName = "A5";
        Piston_A22 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "A6", "Piston_A22");
        Piston_A22.AttributeName = "A6";
        Piston_A23 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "A7", "Piston_A23");
        Piston_A23.AttributeName = "A7";
        Piston_A24 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "A8", "Piston_A24");
        Piston_A24.AttributeName = "A8";
        Piston_A31 = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Piston_A31", "Piston_A31");
        Piston_A32 = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "A8", "Piston_A32");
        Piston_A32.AttributeName = "A8";
        Piston_A33 = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "A9", "Piston_A33");
        Piston_A33.AttributeName = "A9";
        Piston_A34 = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "A10", "Piston_A34");
        Piston_A34.AttributeName = "A10";
        Piston_A41 = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "Piston_A41", "Piston_A41");
        Piston_A42 = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "A11", "Piston_A42");
        Piston_A42.AttributeName = "A11";
        Piston_A43 = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "A12", "Piston_A43");
        Piston_A43.AttributeName = "A12";
        Piston_A44 = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "A13", "Piston_A44");
        Piston_A44.AttributeName = "A13";
        Servo_S1 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Servo S1", "Servo_S1");
        Servo_S1.AttributeName = "Servo S1";
        Servo_S2 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "ABB S2", "Servo_S2");
        Servo_S2.AttributeName = "ABB S2";
        component = new IxComponent(this, "My ix component", "component");
        component.AttributeName = "My ix component";
        Servo_S3 = new stTest3(this, "Servo S3", "Servo_S3");
        Servo_S3.AttributeName = "Servo S3";
        Servo_S4 = new stTest3(this, "Servo S4", "Servo_S4");
        Servo_S4.AttributeName = "Servo S4";
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.stMultipleLayouts> OnlineToPlainAsync()
    {
        Pocos.stMultipleLayouts plain = new Pocos.stMultipleLayouts();
        await this.ReadAsync();
        plain.Servo_S5 = await Servo_S5.OnlineToPlainAsync();
        plain.Servo_S6 = await Servo_S6.OnlineToPlainAsync();
        plain.Servo_S7 = await Servo_S7.OnlineToPlainAsync();
        plain.Servo_S8 = await Servo_S8.OnlineToPlainAsync();
        plain.Piston_A1 = Piston_A1.LastValue;
        plain.Piston_A2 = Piston_A2.LastValue;
        plain.Piston_A3 = Piston_A3.LastValue;
        plain.Piston_A4 = Piston_A4.LastValue;
        plain.Piston_A21 = Piston_A21.LastValue;
        plain.Piston_A22 = Piston_A22.LastValue;
        plain.Piston_A23 = Piston_A23.LastValue;
        plain.Piston_A24 = Piston_A24.LastValue;
        plain.Piston_A31 = Piston_A31.LastValue;
        plain.Piston_A32 = Piston_A32.LastValue;
        plain.Piston_A33 = Piston_A33.LastValue;
        plain.Piston_A34 = Piston_A34.LastValue;
        plain.Piston_A41 = Piston_A41.LastValue;
        plain.Piston_A42 = Piston_A42.LastValue;
        plain.Piston_A43 = Piston_A43.LastValue;
        plain.Piston_A44 = Piston_A44.LastValue;
        plain.Servo_S1 = Servo_S1.LastValue;
        plain.Servo_S2 = Servo_S2.LastValue;
        plain.component = await component.OnlineToPlainAsync();
        plain.Servo_S3 = await Servo_S3.OnlineToPlainAsync();
        plain.Servo_S4 = await Servo_S4.OnlineToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.stMultipleLayouts> OnlineToPlainAsync(Pocos.stMultipleLayouts plain)
    {
        plain.Servo_S5 = await Servo_S5.OnlineToPlainAsync();
        plain.Servo_S6 = await Servo_S6.OnlineToPlainAsync();
        plain.Servo_S7 = await Servo_S7.OnlineToPlainAsync();
        plain.Servo_S8 = await Servo_S8.OnlineToPlainAsync();
        plain.Piston_A1 = Piston_A1.LastValue;
        plain.Piston_A2 = Piston_A2.LastValue;
        plain.Piston_A3 = Piston_A3.LastValue;
        plain.Piston_A4 = Piston_A4.LastValue;
        plain.Piston_A21 = Piston_A21.LastValue;
        plain.Piston_A22 = Piston_A22.LastValue;
        plain.Piston_A23 = Piston_A23.LastValue;
        plain.Piston_A24 = Piston_A24.LastValue;
        plain.Piston_A31 = Piston_A31.LastValue;
        plain.Piston_A32 = Piston_A32.LastValue;
        plain.Piston_A33 = Piston_A33.LastValue;
        plain.Piston_A34 = Piston_A34.LastValue;
        plain.Piston_A41 = Piston_A41.LastValue;
        plain.Piston_A42 = Piston_A42.LastValue;
        plain.Piston_A43 = Piston_A43.LastValue;
        plain.Piston_A44 = Piston_A44.LastValue;
        plain.Servo_S1 = Servo_S1.LastValue;
        plain.Servo_S2 = Servo_S2.LastValue;
        plain.component = await component.OnlineToPlainAsync();
        plain.Servo_S3 = await Servo_S3.OnlineToPlainAsync();
        plain.Servo_S4 = await Servo_S4.OnlineToPlainAsync();
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.stMultipleLayouts plain)
    {
        await this.Servo_S5.PlainToOnlineAsync(plain.Servo_S5);
        await this.Servo_S6.PlainToOnlineAsync(plain.Servo_S6);
        await this.Servo_S7.PlainToOnlineAsync(plain.Servo_S7);
        await this.Servo_S8.PlainToOnlineAsync(plain.Servo_S8);
        Piston_A1.Cyclic = plain.Piston_A1;
        Piston_A2.Cyclic = plain.Piston_A2;
        Piston_A3.Cyclic = plain.Piston_A3;
        Piston_A4.Cyclic = plain.Piston_A4;
        Piston_A21.Cyclic = plain.Piston_A21;
        Piston_A22.Cyclic = plain.Piston_A22;
        Piston_A23.Cyclic = plain.Piston_A23;
        Piston_A24.Cyclic = plain.Piston_A24;
        Piston_A31.Cyclic = plain.Piston_A31;
        Piston_A32.Cyclic = plain.Piston_A32;
        Piston_A33.Cyclic = plain.Piston_A33;
        Piston_A34.Cyclic = plain.Piston_A34;
        Piston_A41.Cyclic = plain.Piston_A41;
        Piston_A42.Cyclic = plain.Piston_A42;
        Piston_A43.Cyclic = plain.Piston_A43;
        Piston_A44.Cyclic = plain.Piston_A44;
        Servo_S1.Cyclic = plain.Servo_S1;
        Servo_S2.Cyclic = plain.Servo_S2;
        await this.component.PlainToOnlineAsync(plain.component);
        await this.Servo_S3.PlainToOnlineAsync(plain.Servo_S3);
        await this.Servo_S4.PlainToOnlineAsync(plain.Servo_S4);
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