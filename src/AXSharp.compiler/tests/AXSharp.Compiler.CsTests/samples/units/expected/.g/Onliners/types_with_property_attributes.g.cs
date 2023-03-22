using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

namespace TypesWithPropertyAttributes
{
    public partial class SomeAddedProperties : AXSharp.Connector.ITwinObject
    {
        private string _Description;
        public string Description
        {
            get
            {
                return AXSharp.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_Description);
            }

            set
            {
                _Description = value;
            }
        }

        public OnlinerInt Counter { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public SomeAddedProperties(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Description = "Some added property name value";
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Counter = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Pocitadlo", "Counter");
            Counter.AttributeName = "Pocitadlo";
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.TypesWithPropertyAttributes.SomeAddedProperties> OnlineToPlainAsync()
        {
            Pocos.TypesWithPropertyAttributes.SomeAddedProperties plain = new Pocos.TypesWithPropertyAttributes.SomeAddedProperties();
            await this.ReadAsync();
            plain.Counter = Counter.LastValue;
            return plain;
        }

        protected async Task<Pocos.TypesWithPropertyAttributes.SomeAddedProperties> OnlineToPlainAsync(Pocos.TypesWithPropertyAttributes.SomeAddedProperties plain)
        {
            plain.Counter = Counter.LastValue;
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.TypesWithPropertyAttributes.SomeAddedProperties plain)
        {
            Counter.Cyclic = plain.Counter;
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.TypesWithPropertyAttributes.SomeAddedProperties> ShadowToPlainAsync()
        {
            Pocos.TypesWithPropertyAttributes.SomeAddedProperties plain = new Pocos.TypesWithPropertyAttributes.SomeAddedProperties();
            plain.Counter = Counter.Shadow;
            return plain;
        }

        protected async Task<Pocos.TypesWithPropertyAttributes.SomeAddedProperties> ShadowToPlainAsync(Pocos.TypesWithPropertyAttributes.SomeAddedProperties plain)
        {
            plain.Counter = Counter.Shadow;
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.TypesWithPropertyAttributes.SomeAddedProperties plain)
        {
            Counter.Shadow = plain.Counter;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.TypesWithPropertyAttributes.SomeAddedProperties CreateEmptyPoco()
        {
            return new Pocos.TypesWithPropertyAttributes.SomeAddedProperties();
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