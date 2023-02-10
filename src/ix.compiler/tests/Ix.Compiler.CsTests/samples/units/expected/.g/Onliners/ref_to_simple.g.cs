using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace RefToSimple
{
    public partial class ref_to_simple : Ix.Connector.ITwinObject
    {
        public ref_to_simple(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.RefToSimple.ref_to_simple> OnlineToPlainAsync()
        {
            Pocos.RefToSimple.ref_to_simple plain = new Pocos.RefToSimple.ref_to_simple();
            await this.ReadAsync();
            return plain;
        }

        protected async Task<Pocos.RefToSimple.ref_to_simple> OnlineToPlainAsync(Pocos.RefToSimple.ref_to_simple plain)
        {
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RefToSimple.ref_to_simple plain)
        {
            return await this.WriteAsync();
        }

        public async Task<Pocos.RefToSimple.ref_to_simple> ShadowToPlainAsync()
        {
            Pocos.RefToSimple.ref_to_simple plain = new Pocos.RefToSimple.ref_to_simple();
            return plain;
        }

        protected async Task<Pocos.RefToSimple.ref_to_simple> ShadowToPlainAsync(Pocos.RefToSimple.ref_to_simple plain)
        {
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.RefToSimple.ref_to_simple plain)
        {
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

    public partial class referenced : Ix.Connector.ITwinObject
    {
        public OnlinerInt b { get; }

        public referenced(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            b = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "b", "b");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.RefToSimple.referenced> OnlineToPlainAsync()
        {
            Pocos.RefToSimple.referenced plain = new Pocos.RefToSimple.referenced();
            await this.ReadAsync();
            plain.b = b.LastValue;
            return plain;
        }

        protected async Task<Pocos.RefToSimple.referenced> OnlineToPlainAsync(Pocos.RefToSimple.referenced plain)
        {
            plain.b = b.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RefToSimple.referenced plain)
        {
            b.Cyclic = plain.b;
            return await this.WriteAsync();
        }

        public async Task<Pocos.RefToSimple.referenced> ShadowToPlainAsync()
        {
            Pocos.RefToSimple.referenced plain = new Pocos.RefToSimple.referenced();
            plain.b = b.Shadow;
            return plain;
        }

        protected async Task<Pocos.RefToSimple.referenced> ShadowToPlainAsync(Pocos.RefToSimple.referenced plain)
        {
            plain.b = b.Shadow;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.RefToSimple.referenced plain)
        {
            b.Shadow = plain.b;
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