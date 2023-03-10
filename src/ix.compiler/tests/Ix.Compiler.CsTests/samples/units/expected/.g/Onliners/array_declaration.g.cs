using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace ArrayDeclarationSimpleNamespace
{
    public partial class array_declaration_class : Ix.Connector.ITwinObject
    {
        public OnlinerInt[] primitive { get; }

        public ArrayDeclarationSimpleNamespace.some_complex_type[] complex { get; }

        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public array_declaration_class(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            primitive = new OnlinerInt[100];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(primitive, this, "primitive", "primitive", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateINT(p, rt, st));
            complex = new ArrayDeclarationSimpleNamespace.some_complex_type[100];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(complex, this, "complex", "complex", (p, rt, st) => new ArrayDeclarationSimpleNamespace.some_complex_type(p, rt, st));
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> OnlineToPlainAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain = new Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class();
            await this.ReadAsync();
            plain.primitive = primitive.Select(p => p.LastValue).ToArray();
            plain.complex = complex.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        protected async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> OnlineToPlainAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            plain.primitive = primitive.Select(p => p.LastValue).ToArray();
            plain.complex = complex.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            var _primitive_i_FE8484DAB3 = 0;
            primitive.Select(p => p.Cyclic = plain.primitive[_primitive_i_FE8484DAB3++]).ToArray();
            var _complex_i_FE8484DAB3 = 0;
            complex.Select(p => p.PlainToOnlineAsync(plain.complex[_complex_i_FE8484DAB3++])).ToArray();
            return await this.WriteAsync();
        }

        public async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> ShadowToPlainAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain = new Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class();
            plain.primitive = primitive.Select(p => p.Shadow).ToArray();
            plain.complex = complex.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        protected async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> ShadowToPlainAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            plain.primitive = primitive.Select(p => p.Shadow).ToArray();
            plain.complex = complex.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            var _primitive_i_FE8484DAB3 = 0;
            primitive.Select(p => p.Shadow = plain.primitive[_primitive_i_FE8484DAB3++]).ToArray();
            var _complex_i_FE8484DAB3 = 0;
            complex.Select(p => p.PlainToShadowAsync(plain.complex[_complex_i_FE8484DAB3++])).ToArray();
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class CreateEmptyPoco()
        {
            return new Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class();
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

    public partial class some_complex_type : Ix.Connector.ITwinObject
    {
        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public some_complex_type(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> OnlineToPlainAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain = new Pocos.ArrayDeclarationSimpleNamespace.some_complex_type();
            await this.ReadAsync();
            return plain;
        }

        protected async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> OnlineToPlainAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
        {
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
        {
            return await this.WriteAsync();
        }

        public async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> ShadowToPlainAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain = new Pocos.ArrayDeclarationSimpleNamespace.some_complex_type();
            return plain;
        }

        protected async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> ShadowToPlainAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
        {
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
        {
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.ArrayDeclarationSimpleNamespace.some_complex_type CreateEmptyPoco()
        {
            return new Pocos.ArrayDeclarationSimpleNamespace.some_complex_type();
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