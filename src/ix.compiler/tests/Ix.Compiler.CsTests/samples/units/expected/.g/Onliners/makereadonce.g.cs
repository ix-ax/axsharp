using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace makereadonce
{
    public partial class MembersWithMakeReadOnce : Ix.Connector.ITwinObject
    {
        [ReadOnce()]
        public OnlinerString makeReadOnceMember { get; }

        public OnlinerString someOtherMember { get; }

        [ReadOnce()]
        public makereadonce.ComplexMember makeReadComplexMember { get; }

        public makereadonce.ComplexMember someotherComplexMember { get; }

        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public MembersWithMakeReadOnce(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            makeReadOnceMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "makeReadOnceMember", "makeReadOnceMember");
            makeReadOnceMember.MakeReadOnce();
            someOtherMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someOtherMember", "someOtherMember");
            makeReadComplexMember = new makereadonce.ComplexMember(this, "makeReadComplexMember", "makeReadComplexMember");
            makeReadComplexMember.MakeReadOnce();
            someotherComplexMember = new makereadonce.ComplexMember(this, "someotherComplexMember", "someotherComplexMember");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.makereadonce.MembersWithMakeReadOnce> OnlineToPlainAsync()
        {
            Pocos.makereadonce.MembersWithMakeReadOnce plain = new Pocos.makereadonce.MembersWithMakeReadOnce();
            await this.ReadAsync();
            plain.makeReadOnceMember = makeReadOnceMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            plain.makeReadComplexMember = await makeReadComplexMember.OnlineToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.makereadonce.MembersWithMakeReadOnce> OnlineToPlainAsync(Pocos.makereadonce.MembersWithMakeReadOnce plain)
        {
            plain.makeReadOnceMember = makeReadOnceMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            plain.makeReadComplexMember = await makeReadComplexMember.OnlineToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.OnlineToPlainAsync();
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.makereadonce.MembersWithMakeReadOnce plain)
        {
            makeReadOnceMember.Cyclic = plain.makeReadOnceMember;
            someOtherMember.Cyclic = plain.someOtherMember;
            await this.makeReadComplexMember.PlainToOnlineAsync(plain.makeReadComplexMember);
            await this.someotherComplexMember.PlainToOnlineAsync(plain.someotherComplexMember);
            return await this.WriteAsync();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.makereadonce.MembersWithMakeReadOnce> ShadowToPlainAsync()
        {
            Pocos.makereadonce.MembersWithMakeReadOnce plain = new Pocos.makereadonce.MembersWithMakeReadOnce();
            plain.makeReadOnceMember = makeReadOnceMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            plain.makeReadComplexMember = await makeReadComplexMember.ShadowToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.makereadonce.MembersWithMakeReadOnce> ShadowToPlainAsync(Pocos.makereadonce.MembersWithMakeReadOnce plain)
        {
            plain.makeReadOnceMember = makeReadOnceMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            plain.makeReadComplexMember = await makeReadComplexMember.ShadowToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.ShadowToPlainAsync();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.makereadonce.MembersWithMakeReadOnce plain)
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

        public Pocos.makereadonce.MembersWithMakeReadOnce CreateEmptyPoco()
        {
            return new Pocos.makereadonce.MembersWithMakeReadOnce();
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

        private string _attributeName;
        public System.String AttributeName
        {
            get
            {
                return Ix.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_attributeName);
            }

            set
            {
                _attributeName = value;
            }
        }

        public string HumanReadable { get; set; }

        protected System.String @SymbolTail { get; set; }

        protected Ix.Connector.ITwinObject @Parent { get; set; }
    }

    public partial class ComplexMember : Ix.Connector.ITwinObject
    {
        public OnlinerString someMember { get; }

        public OnlinerString someOtherMember { get; }

        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ComplexMember(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            someMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someMember", "someMember");
            someOtherMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someOtherMember", "someOtherMember");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.makereadonce.ComplexMember> OnlineToPlainAsync()
        {
            Pocos.makereadonce.ComplexMember plain = new Pocos.makereadonce.ComplexMember();
            await this.ReadAsync();
            plain.someMember = someMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            return plain;
        }

        protected async Task<Pocos.makereadonce.ComplexMember> OnlineToPlainAsync(Pocos.makereadonce.ComplexMember plain)
        {
            plain.someMember = someMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.makereadonce.ComplexMember plain)
        {
            someMember.Cyclic = plain.someMember;
            someOtherMember.Cyclic = plain.someOtherMember;
            return await this.WriteAsync();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.makereadonce.ComplexMember> ShadowToPlainAsync()
        {
            Pocos.makereadonce.ComplexMember plain = new Pocos.makereadonce.ComplexMember();
            plain.someMember = someMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            return plain;
        }

        protected async Task<Pocos.makereadonce.ComplexMember> ShadowToPlainAsync(Pocos.makereadonce.ComplexMember plain)
        {
            plain.someMember = someMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.makereadonce.ComplexMember plain)
        {
            someMember.Shadow = plain.someMember;
            someOtherMember.Shadow = plain.someOtherMember;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.makereadonce.ComplexMember CreateEmptyPoco()
        {
            return new Pocos.makereadonce.ComplexMember();
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

        private string _attributeName;
        public System.String AttributeName
        {
            get
            {
                return Ix.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_attributeName);
            }

            set
            {
                _attributeName = value;
            }
        }

        public string HumanReadable { get; set; }

        protected System.String @SymbolTail { get; set; }

        protected Ix.Connector.ITwinObject @Parent { get; set; }
    }
}