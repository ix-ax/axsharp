using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace ClassWithNonTraspilableMemberssNamespace
{
    public partial class ClassWithNonTraspilableMembers : Ix.Connector.ITwinObject
    {
        public ClassWithNonTraspilableMemberssNamespace.ComplexType1 myComplexType { get; }

        public ClassWithNonTraspilableMembers(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            myComplexType = new ClassWithNonTraspilableMemberssNamespace.ComplexType1(this, "myComplexType", "myComplexType");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> OnlineToPlainAsync()
        {
            Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain = new Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers();
            await this.ReadAsync();
            plain.myComplexType = await myComplexType.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> OnlineToPlainAsync(Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain)
        {
            plain.myComplexType = await myComplexType.OnlineToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain)
        {
            await this.myComplexType.PlainToOnlineAsync(plain.myComplexType);
            return await this.WriteAsync();
        }

        public async Task<Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> ShadowToPlainAsync()
        {
            Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain = new Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers();
            plain.myComplexType = await myComplexType.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> ShadowToPlainAsync(Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain)
        {
            plain.myComplexType = await myComplexType.ShadowToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain)
        {
            await this.myComplexType.PlainToShadowAsync(plain.myComplexType);
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

    public partial class ComplexType1 : Ix.Connector.ITwinObject
    {
        public ComplexType1(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> OnlineToPlainAsync()
        {
            Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain = new Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1();
            await this.ReadAsync();
            return plain;
        }

        protected async Task<Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> OnlineToPlainAsync(Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain)
        {
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain)
        {
            return await this.WriteAsync();
        }

        public async Task<Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> ShadowToPlainAsync()
        {
            Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain = new Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1();
            return plain;
        }

        protected async Task<Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> ShadowToPlainAsync(Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain)
        {
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain)
        {
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