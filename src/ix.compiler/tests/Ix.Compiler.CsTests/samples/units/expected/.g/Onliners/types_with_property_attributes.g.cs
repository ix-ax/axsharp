using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace TypesWithPropertyAttributes
{
    public partial class SomeAddedProperties : Ix.Connector.ITwinObject
    {
        private string _Description;
        public string Description
        {
            get
            {
                return Ix.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_Description);
            }

            set;
            {
                _Description = value;
            }
        }

        public OnlinerInt Counter { get; }

        public SomeAddedProperties(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Description = "Some added property name value";
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Counter = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Pocitadlo", "Counter");
            Counter.AttributeName = "Pocitadlo";
            parent.AddChild(this);
            parent.AddKid(this);
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.TypesWithPropertyAttributes.SomeAddedProperties plain)
        {
            Counter.Cyclic = plain.Counter;
            return await this.WriteAsync();
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.TypesWithPropertyAttributes.SomeAddedProperties plain)
        {
            Counter.Shadow = plain.Counter;
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