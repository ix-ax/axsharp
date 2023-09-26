using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;

namespace makereadonly
{
    public partial class MembersWithMakeReadOnly : AXSharp.Connector.ITwinObject
    {
        [ReadOnly()]
        public OnlinerString makeReadOnceMember { get; }

        public OnlinerString someOtherMember { get; }

        [ReadOnly()]
        public makereadonly.ComplexMember makeReadComplexMember { get; }

        public makereadonly.ComplexMember someotherComplexMember { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public MembersWithMakeReadOnly(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            makeReadOnceMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "makeReadOnceMember", "makeReadOnceMember");
            makeReadOnceMember.MakeReadOnly();
            someOtherMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someOtherMember", "someOtherMember");
            makeReadComplexMember = new makereadonly.ComplexMember(this, "makeReadComplexMember", "makeReadComplexMember");
            makeReadComplexMember.MakeReadOnly();
            someotherComplexMember = new makereadonly.ComplexMember(this, "someotherComplexMember", "someotherComplexMember");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.makereadonly.MembersWithMakeReadOnly> OnlineToPlainAsync()
        {
            Pocos.makereadonly.MembersWithMakeReadOnly plain = new Pocos.makereadonly.MembersWithMakeReadOnly();
            await this.ReadAsync<IgnoreOnPocoOperation>();
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

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
            makeReadOnceMember.Cyclic = plain.makeReadOnceMember;
            someOtherMember.Cyclic = plain.someOtherMember;
            await this.makeReadComplexMember.PlainToOnlineAsync(plain.makeReadComplexMember);
            await this.someotherComplexMember.PlainToOnlineAsync(plain.someotherComplexMember);
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
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

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
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

        public Pocos.makereadonly.MembersWithMakeReadOnly CreateEmptyPoco()
        {
            return new Pocos.makereadonly.MembersWithMakeReadOnly();
        }

        private IList<AXSharp.Connector.ITwinObject> Children { get; } = new List<AXSharp.Connector.ITwinObject>();
        public IEnumerable<AXSharp.Connector.ITwinObject> GetChildren()
        {
            return Children;
        }

        private IList<AXSharp.Connector.ITwinElement> Kids { get; } = new List<AXSharp.Connector.ITwinElement>();
        public IEnumerable<AXSharp.Connector.ITwinElement> GetKids()
        {
            return Kids;
        }

        private IList<AXSharp.Connector.ITwinPrimitive> ValueTags { get; } = new List<AXSharp.Connector.ITwinPrimitive>();
        public IEnumerable<AXSharp.Connector.ITwinPrimitive> GetValueTags()
        {
            return ValueTags;
        }

        public void AddValueTag(AXSharp.Connector.ITwinPrimitive valueTag)
        {
            ValueTags.Add(valueTag);
        }

        public void AddKid(AXSharp.Connector.ITwinElement kid)
        {
            Kids.Add(kid);
        }

        public void AddChild(AXSharp.Connector.ITwinObject twinObject)
        {
            Children.Add(twinObject);
        }

        protected AXSharp.Connector.Connector @Connector { get; }

        public AXSharp.Connector.Connector GetConnector()
        {
            return this.@Connector;
        }

        public string GetSymbolTail()
        {
            return this.SymbolTail;
        }

        public AXSharp.Connector.ITwinObject GetParent()
        {
            return this.@Parent;
        }

        public string Symbol { get; protected set; }

        private string _attributeName;
        public System.String AttributeName { get => string.IsNullOrEmpty(_attributeName) ? SymbolTail : this.Translate(_attributeName).Interpolate(this); set => _attributeName = value; }

        public string HumanReadable { get; set; }

        protected System.String @SymbolTail { get; set; }

        protected AXSharp.Connector.ITwinObject @Parent { get; set; }

        public AXSharp.Connector.Localizations.Translator Interpreter => global::units.PlcTranslator.Instance;
    }

    public partial class ComplexMember : AXSharp.Connector.ITwinObject
    {
        public OnlinerString someMember { get; }

        public OnlinerString someOtherMember { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ComplexMember(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
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

        public async Task<Pocos.makereadonly.ComplexMember> OnlineToPlainAsync()
        {
            Pocos.makereadonly.ComplexMember plain = new Pocos.makereadonly.ComplexMember();
            await this.ReadAsync<IgnoreOnPocoOperation>();
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

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.makereadonly.ComplexMember plain)
        {
            someMember.Cyclic = plain.someMember;
            someOtherMember.Cyclic = plain.someOtherMember;
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
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

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
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

        public Pocos.makereadonly.ComplexMember CreateEmptyPoco()
        {
            return new Pocos.makereadonly.ComplexMember();
        }

        private IList<AXSharp.Connector.ITwinObject> Children { get; } = new List<AXSharp.Connector.ITwinObject>();
        public IEnumerable<AXSharp.Connector.ITwinObject> GetChildren()
        {
            return Children;
        }

        private IList<AXSharp.Connector.ITwinElement> Kids { get; } = new List<AXSharp.Connector.ITwinElement>();
        public IEnumerable<AXSharp.Connector.ITwinElement> GetKids()
        {
            return Kids;
        }

        private IList<AXSharp.Connector.ITwinPrimitive> ValueTags { get; } = new List<AXSharp.Connector.ITwinPrimitive>();
        public IEnumerable<AXSharp.Connector.ITwinPrimitive> GetValueTags()
        {
            return ValueTags;
        }

        public void AddValueTag(AXSharp.Connector.ITwinPrimitive valueTag)
        {
            ValueTags.Add(valueTag);
        }

        public void AddKid(AXSharp.Connector.ITwinElement kid)
        {
            Kids.Add(kid);
        }

        public void AddChild(AXSharp.Connector.ITwinObject twinObject)
        {
            Children.Add(twinObject);
        }

        protected AXSharp.Connector.Connector @Connector { get; }

        public AXSharp.Connector.Connector GetConnector()
        {
            return this.@Connector;
        }

        public string GetSymbolTail()
        {
            return this.SymbolTail;
        }

        public AXSharp.Connector.ITwinObject GetParent()
        {
            return this.@Parent;
        }

        public string Symbol { get; protected set; }

        private string _attributeName;
        public System.String AttributeName { get => string.IsNullOrEmpty(_attributeName) ? SymbolTail : this.Translate(_attributeName).Interpolate(this); set => _attributeName = value; }

        public string HumanReadable { get; set; }

        protected System.String @SymbolTail { get; set; }

        protected AXSharp.Connector.ITwinObject @Parent { get; set; }

        public AXSharp.Connector.Localizations.Translator Interpreter => global::units.PlcTranslator.Instance;
    }
}