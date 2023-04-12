using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;

namespace Simatic.Ax.StateFramework
{
    public partial interface IGuard
    {
    }
}

namespace Simatic.Ax.StateFramework
{
    public enum Condition
    {
        GT,
        EQ,
        LT,
        NE,
        GE,
        LE
    }

    public partial class CompareGuardLint : AXSharp.Connector.ITwinObject, IGuard
    {
        public OnlinerLInt CompareToValue { get; }

        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Simatic.Ax.StateFramework.Condition))]
        public OnlinerInt Condition { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public CompareGuardLint(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            CompareToValue = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this, "CompareToValue", "CompareToValue");
            Condition = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Condition", "Condition");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.CompareGuardLint> OnlineToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain = new Pocos.Simatic.Ax.StateFramework.CompareGuardLint();
            await this.ReadAsync();
            plain.CompareToValue = CompareToValue.LastValue;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.LastValue;
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.CompareGuardLint> OnlineToPlainAsync(Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain)
        {
            plain.CompareToValue = CompareToValue.LastValue;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain)
        {
            CompareToValue.Cyclic = plain.CompareToValue;
            Condition.Cyclic = (short)plain.Condition;
            return await this.WriteAsync();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.CompareGuardLint> ShadowToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain = new Pocos.Simatic.Ax.StateFramework.CompareGuardLint();
            plain.CompareToValue = CompareToValue.Shadow;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.Shadow;
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.CompareGuardLint> ShadowToPlainAsync(Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain)
        {
            plain.CompareToValue = CompareToValue.Shadow;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain)
        {
            CompareToValue.Shadow = plain.CompareToValue;
            Condition.Shadow = (short)plain.Condition;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.Simatic.Ax.StateFramework.CompareGuardLint CreateEmptyPoco()
        {
            return new Pocos.Simatic.Ax.StateFramework.CompareGuardLint();
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