using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace ClassWithComplexTypesNamespace
{
    public partial class ClassWithComplexTypes : Ix.Connector.ITwinObject
    {
        public ClassWithComplexTypesNamespace.ComplexType1 myComplexType { get; }

        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithComplexTypes(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            myComplexType = new ClassWithComplexTypesNamespace.ComplexType1(this, "myComplexType", "myComplexType");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async Task<Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes> OnlineToPlainAsync()
        {
            Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes plain = new Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes();
            await this.ReadAsync();
            plain.myComplexType = await myComplexType.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes> OnlineToPlainAsync(Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes plain)
        {
            plain.myComplexType = await myComplexType.OnlineToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes plain)
        {
            await this.myComplexType.PlainToOnlineAsync(plain.myComplexType);
            return await this.WriteAsync();
        }

        public async Task<Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes> ShadowToPlainAsync()
        {
            Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes plain = new Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes();
            plain.myComplexType = await myComplexType.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes> ShadowToPlainAsync(Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes plain)
        {
            plain.myComplexType = await myComplexType.ShadowToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes plain)
        {
            await this.myComplexType.PlainToShadowAsync(plain.myComplexType);
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes CreateEmptyPoco()
        {
            return new Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes();
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

    public partial class ComplexType1 : Ix.Connector.ITwinObject
    {
        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ComplexType1(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async Task<Pocos.ClassWithComplexTypesNamespace.ComplexType1> OnlineToPlainAsync()
        {
            Pocos.ClassWithComplexTypesNamespace.ComplexType1 plain = new Pocos.ClassWithComplexTypesNamespace.ComplexType1();
            await this.ReadAsync();
            return plain;
        }

        protected async Task<Pocos.ClassWithComplexTypesNamespace.ComplexType1> OnlineToPlainAsync(Pocos.ClassWithComplexTypesNamespace.ComplexType1 plain)
        {
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ClassWithComplexTypesNamespace.ComplexType1 plain)
        {
            return await this.WriteAsync();
        }

        public async Task<Pocos.ClassWithComplexTypesNamespace.ComplexType1> ShadowToPlainAsync()
        {
            Pocos.ClassWithComplexTypesNamespace.ComplexType1 plain = new Pocos.ClassWithComplexTypesNamespace.ComplexType1();
            return plain;
        }

        protected async Task<Pocos.ClassWithComplexTypesNamespace.ComplexType1> ShadowToPlainAsync(Pocos.ClassWithComplexTypesNamespace.ComplexType1 plain)
        {
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ClassWithComplexTypesNamespace.ComplexType1 plain)
        {
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.ClassWithComplexTypesNamespace.ComplexType1 CreateEmptyPoco()
        {
            return new Pocos.ClassWithComplexTypesNamespace.ComplexType1();
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