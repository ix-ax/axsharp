using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace NamedValuesNamespace
{
    public enum LightColors : Int16
    {
        LRED = 12,
        LGREEN = 14,
        LBLUE = 23
    }

    public partial class using_type_named_values : Ix.Connector.ITwinObject
    {
        [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(NamedValuesNamespace.LightColors))]
        public OnlinerInt LColors { get; }

        public using_type_named_values(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            LColors = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "LColors", "LColors");
            parent.AddChild(this);
            parent.AddKid(this);
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.NamedValuesNamespace.using_type_named_values plain)
        {
            LColors.Cyclic = plain.LColors;
            return await this.WriteAsync();
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.NamedValuesNamespace.using_type_named_values plain)
        {
            LColors.Shadow = plain.LColors;
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
}