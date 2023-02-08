using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class MAIN : Ix.Connector.ITwinObject
{
    public OnlinerString Hello_World { get; }

    public OnlinerInt cislo { get; }

    public OnlinerBool boolValue { get; }

    public OnlinerLReal Position { get; }

    public stTest instanceOfstTest { get; }

    public stTest2 instanceOfstTest2 { get; }

    public stTest3 instanceOfstTest3 { get; }

    public stBlazor instanceOfstBlazor { get; }

    public stTest[] arr1 { get; }

    public OnlinerInt[] arr2 { get; }

    public OnlinerDate dateVar { get; }

    public stComplex instanceOfstComplex { get; }

    public stTestPrimitive instanceOfstPrimitive { get; }

    public stMultipleLayouts instanceOfstMultipleLayouts { get; }

    public stLayouts instanceOfstMultipleLayouts2 { get; }

    public IxComponent instanceOfIxComponent { get; }

    public GroupBox_other groupBox_test { get; }

    public CU00x cu00 { get; }

    public CUBase cuBase { get; }

    public MAIN(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Hello_World = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "<#Hello#> <#World#>", "Hello_World");
        Hello_World.AttributeName = "<#Hello#> <#World#>";
        cislo = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "cislo", "cislo");
        cislo.AttributeName = "cislo";
        boolValue = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "bool_val", "boolValue");
        boolValue.AttributeName = "bool_val";
        Position = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Position", "Position");
        Position.AttributeName = "Position";
        instanceOfstTest = new stTest(this, "Struct", "instanceOfstTest");
        instanceOfstTest.AttributeName = "Struct";
        instanceOfstTest2 = new stTest2(this, "Struct2", "instanceOfstTest2");
        instanceOfstTest2.AttributeName = "Struct2";
        instanceOfstTest3 = new stTest3(this, "instanceOfStTest3", "instanceOfstTest3");
        instanceOfstTest3.AttributeName = "instanceOfStTest3";
        instanceOfstBlazor = new stBlazor(this, "BlazorStruct", "instanceOfstBlazor");
        instanceOfstBlazor.AttributeName = "BlazorStruct";
        arr1 = new stTest[3];
        Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(arr1, this, "arr1", "arr1", (p, rt, st) => new stTest(p, rt, st));
        arr2 = new OnlinerInt[6];
        Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(arr2, this, "arr2", "arr2", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateINT(p, rt, st));
        dateVar = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "dateVar", "dateVar");
        instanceOfstComplex = new stComplex(this, "instanceOfComplex", "instanceOfstComplex");
        instanceOfstComplex.AttributeName = "instanceOfComplex";
        instanceOfstPrimitive = new stTestPrimitive(this, "instanceOfStPrimitive", "instanceOfstPrimitive");
        instanceOfstPrimitive.AttributeName = "instanceOfStPrimitive";
        instanceOfstMultipleLayouts = new stMultipleLayouts(this, "instanceOfstMultipleLayouts", "instanceOfstMultipleLayouts");
        instanceOfstMultipleLayouts2 = new stLayouts(this, "instanceOfstMultipleLayouts2", "instanceOfstMultipleLayouts2");
        instanceOfIxComponent = new IxComponent(this, "instanceOfIxComponent", "instanceOfIxComponent");
        instanceOfIxComponent.AttributeName = "instanceOfIxComponent";
        groupBox_test = new GroupBox_other(this, "groupBox_test", "groupBox_test");
        cu00 = new CU00x(this, "cu00", "cu00");
        cuBase = new CUBase(this, "cuBase", "cuBase");
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.MAIN> OnlineToPlainAsync()
    {
        Pocos.MAIN plain = new Pocos.MAIN();
        await this.ReadAsync();
        plain.Hello_World = Hello_World.LastValue;
        plain.cislo = cislo.LastValue;
        plain.boolValue = boolValue.LastValue;
        plain.Position = Position.LastValue;
        plain.instanceOfstTest = await instanceOfstTest.OnlineToPlainAsync();
        plain.instanceOfstTest2 = await instanceOfstTest2.OnlineToPlainAsync();
        plain.instanceOfstTest3 = await instanceOfstTest3.OnlineToPlainAsync();
        plain.instanceOfstBlazor = await instanceOfstBlazor.OnlineToPlainAsync();
        plain.arr1 = arr1.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
        plain.arr2 = arr2.Select(p => p.LastValue).ToArray();
        plain.dateVar = dateVar.LastValue;
        plain.instanceOfstComplex = await instanceOfstComplex.OnlineToPlainAsync();
        plain.instanceOfstPrimitive = await instanceOfstPrimitive.OnlineToPlainAsync();
        plain.instanceOfstMultipleLayouts = await instanceOfstMultipleLayouts.OnlineToPlainAsync();
        plain.instanceOfstMultipleLayouts2 = await instanceOfstMultipleLayouts2.OnlineToPlainAsync();
        plain.instanceOfIxComponent = await instanceOfIxComponent.OnlineToPlainAsync();
        plain.groupBox_test = await groupBox_test.OnlineToPlainAsync();
        plain.cu00 = await cu00.OnlineToPlainAsync();
        plain.cuBase = await cuBase.OnlineToPlainAsync();
        return plain;
    }

    protected async Task<Pocos.MAIN> OnlineToPlainAsync(Pocos.MAIN plain)
    {
        plain.Hello_World = Hello_World.LastValue;
        plain.cislo = cislo.LastValue;
        plain.boolValue = boolValue.LastValue;
        plain.Position = Position.LastValue;
        plain.instanceOfstTest = await instanceOfstTest.OnlineToPlainAsync();
        plain.instanceOfstTest2 = await instanceOfstTest2.OnlineToPlainAsync();
        plain.instanceOfstTest3 = await instanceOfstTest3.OnlineToPlainAsync();
        plain.instanceOfstBlazor = await instanceOfstBlazor.OnlineToPlainAsync();
        plain.arr1 = arr1.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
        plain.arr2 = arr2.Select(p => p.LastValue).ToArray();
        plain.dateVar = dateVar.LastValue;
        plain.instanceOfstComplex = await instanceOfstComplex.OnlineToPlainAsync();
        plain.instanceOfstPrimitive = await instanceOfstPrimitive.OnlineToPlainAsync();
        plain.instanceOfstMultipleLayouts = await instanceOfstMultipleLayouts.OnlineToPlainAsync();
        plain.instanceOfstMultipleLayouts2 = await instanceOfstMultipleLayouts2.OnlineToPlainAsync();
        plain.instanceOfIxComponent = await instanceOfIxComponent.OnlineToPlainAsync();
        plain.groupBox_test = await groupBox_test.OnlineToPlainAsync();
        plain.cu00 = await cu00.OnlineToPlainAsync();
        plain.cuBase = await cuBase.OnlineToPlainAsync();
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.MAIN plain)
    {
        Hello_World.Cyclic = plain.Hello_World;
        cislo.Cyclic = plain.cislo;
        boolValue.Cyclic = plain.boolValue;
        Position.Cyclic = plain.Position;
        await this.instanceOfstTest.PlainToOnlineAsync(plain.instanceOfstTest);
        await this.instanceOfstTest2.PlainToOnlineAsync(plain.instanceOfstTest2);
        await this.instanceOfstTest3.PlainToOnlineAsync(plain.instanceOfstTest3);
        await this.instanceOfstBlazor.PlainToOnlineAsync(plain.instanceOfstBlazor);
        var _arr1_i_FE8484DAB3 = 0;
        arr1.Select(p => p.PlainToOnlineAsync(plain.arr1[_arr1_i_FE8484DAB3++])).ToArray();
        var _arr2_i_FE8484DAB3 = 0;
        arr2.Select(p => p.Cyclic = plain.arr2[_arr2_i_FE8484DAB3++]).ToArray();
        dateVar.Cyclic = plain.dateVar;
        await this.instanceOfstComplex.PlainToOnlineAsync(plain.instanceOfstComplex);
        await this.instanceOfstPrimitive.PlainToOnlineAsync(plain.instanceOfstPrimitive);
        await this.instanceOfstMultipleLayouts.PlainToOnlineAsync(plain.instanceOfstMultipleLayouts);
        await this.instanceOfstMultipleLayouts2.PlainToOnlineAsync(plain.instanceOfstMultipleLayouts2);
        await this.instanceOfIxComponent.PlainToOnlineAsync(plain.instanceOfIxComponent);
        await this.groupBox_test.PlainToOnlineAsync(plain.groupBox_test);
        await this.cu00.PlainToOnlineAsync(plain.cu00);
        await this.cuBase.PlainToOnlineAsync(plain.cuBase);
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