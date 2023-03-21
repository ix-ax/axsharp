using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

namespace ClassWithComplexTypesNamespace
{
    public partial class ClassWithComplexTypes : AXSharp.Connector.ITwinObject
    {
        public ClassWithComplexTypesNamespace.ComplexType1 myComplexType { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithComplexTypes(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            myComplexType = new ClassWithComplexTypesNamespace.ComplexType1(this, "myComplexType", "myComplexType");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
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

        public void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ClassWithComplexTypesNamespace.ClassWithComplexTypes plain)
        {
            await this.myComplexType.PlainToOnlineAsync(plain.myComplexType);
            return await this.WriteAsync();
        }

        public T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
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

        public void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
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

    public partial class ComplexType1 : AXSharp.Connector.ITwinObject
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ComplexType1(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
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

        public void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ClassWithComplexTypesNamespace.ComplexType1 plain)
        {
            return await this.WriteAsync();
        }

        public T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
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

        public void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
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