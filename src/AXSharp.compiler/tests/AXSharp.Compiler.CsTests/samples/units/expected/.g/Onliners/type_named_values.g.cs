using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

namespace NamedValuesNamespace
{
    public enum LightColors : Int16
    {
        LRED = 12,
        LGREEN = 14,
        LBLUE = 23
    }

    public partial class using_type_named_values : AXSharp.Connector.ITwinObject
    {
        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(NamedValuesNamespace.LightColors))]
        public OnlinerInt LColors { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public using_type_named_values(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            LColors = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "LColors", "LColors");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.NamedValuesNamespace.using_type_named_values> OnlineToPlainAsync()
        {
            Pocos.NamedValuesNamespace.using_type_named_values plain = new Pocos.NamedValuesNamespace.using_type_named_values();
            await this.ReadAsync();
            plain.LColors = LColors.LastValue;
            return plain;
        }

        protected async Task<Pocos.NamedValuesNamespace.using_type_named_values> OnlineToPlainAsync(Pocos.NamedValuesNamespace.using_type_named_values plain)
        {
            plain.LColors = LColors.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.NamedValuesNamespace.using_type_named_values plain)
        {
            LColors.Cyclic = plain.LColors;
            return await this.WriteAsync();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.NamedValuesNamespace.using_type_named_values> ShadowToPlainAsync()
        {
            Pocos.NamedValuesNamespace.using_type_named_values plain = new Pocos.NamedValuesNamespace.using_type_named_values();
            plain.LColors = LColors.Shadow;
            return plain;
        }

        protected async Task<Pocos.NamedValuesNamespace.using_type_named_values> ShadowToPlainAsync(Pocos.NamedValuesNamespace.using_type_named_values plain)
        {
            plain.LColors = LColors.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.NamedValuesNamespace.using_type_named_values plain)
        {
            LColors.Shadow = plain.LColors;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.NamedValuesNamespace.using_type_named_values CreateEmptyPoco()
        {
            return new Pocos.NamedValuesNamespace.using_type_named_values();
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
        public System.String AttributeName
        {
            get
            {
                return AXSharp.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_attributeName);
            }

            set
            {
                _attributeName = value;
            }
        }

        public string HumanReadable { get; set; }

        protected System.String @SymbolTail { get; set; }

        protected AXSharp.Connector.ITwinObject @Parent { get; set; }
    }
}