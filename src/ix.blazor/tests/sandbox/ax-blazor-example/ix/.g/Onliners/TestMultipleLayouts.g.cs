using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class TestMultipleLayouts : Ix.Connector.ITwinObject
{
    public OnlinerInt B1 { get; }

    [Container(Layout.Stack)]
    public OnlinerInt B2 { get; }

    [Container(Layout.Stack)]
    public OnlinerString Piston_A1 { get; }

    public OnlinerString Piston_A2 { get; }

    public OnlinerString Piston_A3 { get; }

    public OnlinerString Piston_A4 { get; }

    [Container(Layout.Tabs)]
    public OnlinerInt Piston_A21 { get; }

    public OnlinerInt Piston_A22 { get; }

    public OnlinerInt Piston_A23 { get; }

    public OnlinerInt Piston_A24 { get; }

    [Container(Layout.UniformGrid)]
    public OnlinerReal Piston_A31 { get; }

    public OnlinerReal Piston_A32 { get; }

    public OnlinerReal Piston_A33 { get; }

    public OnlinerReal Piston_A34 { get; }

    public OnlinerBool Piston_A41 { get; }

    public OnlinerBool Piston_A42 { get; }

    public OnlinerBool Piston_A43 { get; }

    public OnlinerBool Piston_A44 { get; }

    [Container(Layout.Stack)]
    public OnlinerString Servo_S1 { get; }

    [Container(Layout.Stack)]
    public OnlinerString Servo_S2 { get; }

    public TestMultipleLayouts(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        B1 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "B1", "B1");
        B1.AttributeName = "B1";
        B2 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "B2", "B2");
        B2.AttributeName = "B2";
        Piston_A1 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A1", "Piston_A1");
        Piston_A1.AttributeName = "A1";
        Piston_A2 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A2", "Piston_A2");
        Piston_A2.AttributeName = "A2";
        Piston_A3 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A3", "Piston_A3");
        Piston_A3.AttributeName = "A3";
        Piston_A4 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "A4", "Piston_A4");
        Piston_A4.AttributeName = "A4";
        Piston_A21 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Piston_A21", "Piston_A21");
        Piston_A22 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "A5", "Piston_A22");
        Piston_A22.AttributeName = "A5";
        Piston_A23 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "A6", "Piston_A23");
        Piston_A23.AttributeName = "A6";
        Piston_A24 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "A7", "Piston_A24");
        Piston_A24.AttributeName = "A7";
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