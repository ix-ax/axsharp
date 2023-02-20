using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace makereadonly
{
    public partial class MembersWithMakeReadOnly : Ix.Connector.ITwinObject
    {
        [ReadOnly()]
        public OnlinerString makeReadOnceMember { get; }

        public OnlinerString someOtherMember { get; }

        [ReadOnly()]
        public makereadonly.ComplexMember makeReadComplexMember { get; }

        public makereadonly.ComplexMember someotherComplexMember { get; }

        public MembersWithMakeReadOnly(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            makeReadOnceMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "makeReadOnceMember", "makeReadOnceMember");
            makeReadOnceMember.MakeReadOnly();
            someOtherMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someOtherMember", "someOtherMember");
            makeReadComplexMember = new makereadonly.ComplexMember(this, "makeReadComplexMember", "makeReadComplexMember");
            makeReadComplexMember.MakeReadOnly();
            someotherComplexMember = new makereadonly.ComplexMember(this, "someotherComplexMember", "someotherComplexMember");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.makereadonly.MembersWithMakeReadOnly> OnlineToPlainAsync()
        {
            Pocos.makereadonly.MembersWithMakeReadOnly plain = new Pocos.makereadonly.MembersWithMakeReadOnly();
            await this.ReadAsync();
            plain.makeReadOnceMember = makeReadOnceMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            plain.makeReadComplexMember = await makeReadComplexMember.OnlineToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.makereadonly.MembersWithMakeReadOnly> OnlineToPlainAsync(Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
            plain.makeReadOnceMember = makeReadOnceMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            plain.makeReadComplexMember = await makeReadComplexMember.OnlineToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.OnlineToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
            makeReadOnceMember.Cyclic = plain.makeReadOnceMember;
            someOtherMember.Cyclic = plain.someOtherMember;
            await this.makeReadComplexMember.PlainToOnlineAsync(plain.makeReadComplexMember);
            await this.someotherComplexMember.PlainToOnlineAsync(plain.someotherComplexMember);
            return await this.WriteAsync();
        }

        public async Task<Pocos.makereadonly.MembersWithMakeReadOnly> ShadowToPlainAsync()
        {
            Pocos.makereadonly.MembersWithMakeReadOnly plain = new Pocos.makereadonly.MembersWithMakeReadOnly();
            plain.makeReadOnceMember = makeReadOnceMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            plain.makeReadComplexMember = await makeReadComplexMember.ShadowToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.makereadonly.MembersWithMakeReadOnly> ShadowToPlainAsync(Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
            plain.makeReadOnceMember = makeReadOnceMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            plain.makeReadComplexMember = await makeReadComplexMember.ShadowToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.ShadowToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
            makeReadOnceMember.Shadow = plain.makeReadOnceMember;
            someOtherMember.Shadow = plain.someOtherMember;
            await this.makeReadComplexMember.PlainToShadowAsync(plain.makeReadComplexMember);
            await this.someotherComplexMember.PlainToShadowAsync(plain.someotherComplexMember);
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

    public partial class ComplexMember : Ix.Connector.ITwinObject
    {
        public OnlinerString someMember { get; }

        public OnlinerString someOtherMember { get; }

        public ComplexMember(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            someMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someMember", "someMember");
            someOtherMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someOtherMember", "someOtherMember");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.makereadonly.ComplexMember> OnlineToPlainAsync()
        {
            Pocos.makereadonly.ComplexMember plain = new Pocos.makereadonly.ComplexMember();
            await this.ReadAsync();
            plain.someMember = someMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            return plain;
        }

        protected async Task<Pocos.makereadonly.ComplexMember> OnlineToPlainAsync(Pocos.makereadonly.ComplexMember plain)
        {
            plain.someMember = someMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.makereadonly.ComplexMember plain)
        {
            someMember.Cyclic = plain.someMember;
            someOtherMember.Cyclic = plain.someOtherMember;
            return await this.WriteAsync();
        }

        public async Task<Pocos.makereadonly.ComplexMember> ShadowToPlainAsync()
        {
            Pocos.makereadonly.ComplexMember plain = new Pocos.makereadonly.ComplexMember();
            plain.someMember = someMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            return plain;
        }

        protected async Task<Pocos.makereadonly.ComplexMember> ShadowToPlainAsync(Pocos.makereadonly.ComplexMember plain)
        {
            plain.someMember = someMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.makereadonly.ComplexMember plain)
        {
            someMember.Shadow = plain.someMember;
            someOtherMember.Shadow = plain.someOtherMember;
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