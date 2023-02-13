using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

internal partial class ClassWithComplexTypes : Ix.Connector.ITwinObject
{
    public ClassWithComplexTypes(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public async Task<Pocos.ClassWithComplexTypes> OnlineToPlainAsync()
    {
        Pocos.ClassWithComplexTypes plain = new Pocos.ClassWithComplexTypes();
        await this.ReadAsync();
        return plain;
    }

    protected async Task<Pocos.ClassWithComplexTypes> OnlineToPlainAsync(Pocos.ClassWithComplexTypes plain)
    {
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ClassWithComplexTypes plain)
    {
        return await this.WriteAsync();
    }

    public async Task<Pocos.ClassWithComplexTypes> ShadowToPlainAsync()
    {
        Pocos.ClassWithComplexTypes plain = new Pocos.ClassWithComplexTypes();
        return plain;
    }

    protected async Task<Pocos.ClassWithComplexTypes> ShadowToPlainAsync(Pocos.ClassWithComplexTypes plain)
    {
        return plain;
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ClassWithComplexTypes plain)
    {
        return this.RetrievePrimitives();
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