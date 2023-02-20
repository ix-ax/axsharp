using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace TypeWithNameAttributes
{
    [Container(Layout.Wrap)]
    public partial class Motor : Ix.Connector.ITwinObject
    {
        public OnlinerBool isRunning { get; }

        public Motor(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            isRunning = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "isRunning", "isRunning");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.TypeWithNameAttributes.Motor> OnlineToPlainAsync()
        {
            Pocos.TypeWithNameAttributes.Motor plain = new Pocos.TypeWithNameAttributes.Motor();
            await this.ReadAsync();
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        protected async Task<Pocos.TypeWithNameAttributes.Motor> OnlineToPlainAsync(Pocos.TypeWithNameAttributes.Motor plain)
        {
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.TypeWithNameAttributes.Motor plain)
        {
            isRunning.Cyclic = plain.isRunning;
            return await this.WriteAsync();
        }

        public async Task<Pocos.TypeWithNameAttributes.Motor> ShadowToPlainAsync()
        {
            Pocos.TypeWithNameAttributes.Motor plain = new Pocos.TypeWithNameAttributes.Motor();
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        protected async Task<Pocos.TypeWithNameAttributes.Motor> ShadowToPlainAsync(Pocos.TypeWithNameAttributes.Motor plain)
        {
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.TypeWithNameAttributes.Motor plain)
        {
            isRunning.Shadow = plain.isRunning;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
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

    public partial class Vehicle : Ix.Connector.ITwinObject
    {
        public TypeWithNameAttributes.Motor m { get; }

        public OnlinerInt displacement { get; }

        public Vehicle(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.TypeWithNameAttributes.Vehicle> OnlineToPlainAsync()
        {
            Pocos.TypeWithNameAttributes.Vehicle plain = new Pocos.TypeWithNameAttributes.Vehicle();
            await this.ReadAsync();
            plain.m = await m.OnlineToPlainAsync();
            plain.displacement = displacement.LastValue;
            return plain;
        }

        protected async Task<Pocos.TypeWithNameAttributes.Vehicle> OnlineToPlainAsync(Pocos.TypeWithNameAttributes.Vehicle plain)
        {
            plain.m = await m.OnlineToPlainAsync();
            plain.displacement = displacement.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.TypeWithNameAttributes.Vehicle plain)
        {
            await this.m.PlainToOnlineAsync(plain.m);
            displacement.Cyclic = plain.displacement;
            return await this.WriteAsync();
        }

        public async Task<Pocos.TypeWithNameAttributes.Vehicle> ShadowToPlainAsync()
        {
            Pocos.TypeWithNameAttributes.Vehicle plain = new Pocos.TypeWithNameAttributes.Vehicle();
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        protected async Task<Pocos.TypeWithNameAttributes.Vehicle> ShadowToPlainAsync(Pocos.TypeWithNameAttributes.Vehicle plain)
        {
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.TypeWithNameAttributes.Vehicle plain)
        {
            await this.m.PlainToShadowAsync(plain.m);
            displacement.Shadow = plain.displacement;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
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

    public partial class NoAccessModifierClass : Ix.Connector.ITwinObject
    {
        public string AttributeName { get; set; }

        public OnlinerString SomeClassVariable { get; }

        public NoAccessModifierClass(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            SomeClassVariable = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "SomeClassVariable", "SomeClassVariable");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.TypeWithNameAttributes.NoAccessModifierClass> OnlineToPlainAsync()
        {
            Pocos.TypeWithNameAttributes.NoAccessModifierClass plain = new Pocos.TypeWithNameAttributes.NoAccessModifierClass();
            await this.ReadAsync();
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        protected async Task<Pocos.TypeWithNameAttributes.NoAccessModifierClass> OnlineToPlainAsync(Pocos.TypeWithNameAttributes.NoAccessModifierClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.TypeWithNameAttributes.NoAccessModifierClass plain)
        {
            SomeClassVariable.Cyclic = plain.SomeClassVariable;
            return await this.WriteAsync();
        }

        public async Task<Pocos.TypeWithNameAttributes.NoAccessModifierClass> ShadowToPlainAsync()
        {
            Pocos.TypeWithNameAttributes.NoAccessModifierClass plain = new Pocos.TypeWithNameAttributes.NoAccessModifierClass();
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        protected async Task<Pocos.TypeWithNameAttributes.NoAccessModifierClass> ShadowToPlainAsync(Pocos.TypeWithNameAttributes.NoAccessModifierClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.TypeWithNameAttributes.NoAccessModifierClass plain)
        {
            SomeClassVariable.Shadow = plain.SomeClassVariable;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
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
}