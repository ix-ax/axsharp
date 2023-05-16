using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;

namespace Generics
{
    public partial class Extender<TOnline, TPlain> : AXSharp.Connector.ITwinObject where TOnline : ITwinObject
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Extender(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.Generics.Extender> OnlineToPlainAsync()
        {
            Pocos.Generics.Extender plain = new Pocos.Generics.Extender();
            await this.ReadAsync();
            return plain;
        }

        protected async Task<Pocos.Generics.Extender> OnlineToPlainAsync(Pocos.Generics.Extender plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Generics.Extender plain)
        {
            return await this.WriteAsync();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.Generics.Extender> ShadowToPlainAsync()
        {
            Pocos.Generics.Extender plain = new Pocos.Generics.Extender();
            return plain;
        }

        protected async Task<Pocos.Generics.Extender> ShadowToPlainAsync(Pocos.Generics.Extender plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Generics.Extender plain)
        {
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.Generics.Extender CreateEmptyPoco()
        {
            return new Pocos.Generics.Extender();
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

    public partial class Extendee : Generics.Extender<Generics.SomeType, Pocos.Generics.SomeType>
    {
        public Generics.SomeType SomeType { get; }

        public Generics.SomeType SomeTypeAsPoco { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Extendee(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            SomeType = new Generics.SomeType(this, "SomeType", "SomeType");
            SomeTypeAsPoco = new Generics.SomeType(this, "SomeTypeAsPoco", "SomeTypeAsPoco");
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async override Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public new async Task<Pocos.Generics.Extendee> OnlineToPlainAsync()
        {
            Pocos.Generics.Extendee plain = new Pocos.Generics.Extendee();
            await this.ReadAsync();
            await base.OnlineToPlainAsync(plain);
            plain.SomeType = await SomeType.OnlineToPlainAsync();
            plain.SomeTypeAsPoco = await SomeTypeAsPoco.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.Generics.Extendee> OnlineToPlainAsync(Pocos.Generics.Extendee plain)
        {
            await base.OnlineToPlainAsync(plain);
            plain.SomeType = await SomeType.OnlineToPlainAsync();
            plain.SomeTypeAsPoco = await SomeTypeAsPoco.OnlineToPlainAsync();
            return plain;
        }

        public async override Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Generics.Extendee plain)
        {
            await base.PlainToOnlineAsync(plain);
            await this.SomeType.PlainToOnlineAsync(plain.SomeType);
            await this.SomeTypeAsPoco.PlainToOnlineAsync(plain.SomeTypeAsPoco);
            return await this.WriteAsync();
        }

        public async override Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public new async Task<Pocos.Generics.Extendee> ShadowToPlainAsync()
        {
            Pocos.Generics.Extendee plain = new Pocos.Generics.Extendee();
            await base.ShadowToPlainAsync(plain);
            plain.SomeType = await SomeType.ShadowToPlainAsync();
            plain.SomeTypeAsPoco = await SomeTypeAsPoco.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.Generics.Extendee> ShadowToPlainAsync(Pocos.Generics.Extendee plain)
        {
            await base.ShadowToPlainAsync(plain);
            plain.SomeType = await SomeType.ShadowToPlainAsync();
            plain.SomeTypeAsPoco = await SomeTypeAsPoco.ShadowToPlainAsync();
            return plain;
        }

        public async override Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Generics.Extendee plain)
        {
            await base.PlainToShadowAsync(plain);
            await this.SomeType.PlainToShadowAsync(plain.SomeType);
            await this.SomeTypeAsPoco.PlainToShadowAsync(plain.SomeTypeAsPoco);
            return this.RetrievePrimitives();
        }

        public new void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public new Pocos.Generics.Extendee CreateEmptyPoco()
        {
            return new Pocos.Generics.Extendee();
        }
    }

    public partial class Extendee2 : Generics.Extender<Generics.SomeType, Pocos.Generics.SomeType>
    {
        public Generics.SomeType SomeType { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Extendee2(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            SomeType = new Generics.SomeType(this, "SomeType", "SomeType");
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async override Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public new async Task<Pocos.Generics.Extendee2> OnlineToPlainAsync()
        {
            Pocos.Generics.Extendee2 plain = new Pocos.Generics.Extendee2();
            await this.ReadAsync();
            await base.OnlineToPlainAsync(plain);
            plain.SomeType = await SomeType.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.Generics.Extendee2> OnlineToPlainAsync(Pocos.Generics.Extendee2 plain)
        {
            await base.OnlineToPlainAsync(plain);
            plain.SomeType = await SomeType.OnlineToPlainAsync();
            return plain;
        }

        public async override Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Generics.Extendee2 plain)
        {
            await base.PlainToOnlineAsync(plain);
            await this.SomeType.PlainToOnlineAsync(plain.SomeType);
            return await this.WriteAsync();
        }

        public async override Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public new async Task<Pocos.Generics.Extendee2> ShadowToPlainAsync()
        {
            Pocos.Generics.Extendee2 plain = new Pocos.Generics.Extendee2();
            await base.ShadowToPlainAsync(plain);
            plain.SomeType = await SomeType.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.Generics.Extendee2> ShadowToPlainAsync(Pocos.Generics.Extendee2 plain)
        {
            await base.ShadowToPlainAsync(plain);
            plain.SomeType = await SomeType.ShadowToPlainAsync();
            return plain;
        }

        public async override Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Generics.Extendee2 plain)
        {
            await base.PlainToShadowAsync(plain);
            await this.SomeType.PlainToShadowAsync(plain.SomeType);
            return this.RetrievePrimitives();
        }

        public new void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public new Pocos.Generics.Extendee2 CreateEmptyPoco()
        {
            return new Pocos.Generics.Extendee2();
        }
    }

    public partial class SomeType : AXSharp.Connector.ITwinObject
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public SomeType(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.Generics.SomeType> OnlineToPlainAsync()
        {
            Pocos.Generics.SomeType plain = new Pocos.Generics.SomeType();
            await this.ReadAsync();
            return plain;
        }

        protected async Task<Pocos.Generics.SomeType> OnlineToPlainAsync(Pocos.Generics.SomeType plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Generics.SomeType plain)
        {
            return await this.WriteAsync();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.Generics.SomeType> ShadowToPlainAsync()
        {
            Pocos.Generics.SomeType plain = new Pocos.Generics.SomeType();
            return plain;
        }

        protected async Task<Pocos.Generics.SomeType> ShadowToPlainAsync(Pocos.Generics.SomeType plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Generics.SomeType plain)
        {
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.Generics.SomeType CreateEmptyPoco()
        {
            return new Pocos.Generics.SomeType();
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