using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

namespace Enums
{
    public partial class ClassWithEnums : AXSharp.Connector.ITwinObject
    {
        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Enums.Colors))]
        public OnlinerInt colors { get; }

        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Enums.NamedValuesColors))]
        public OnlinerString NamedValuesColors { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithEnums(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            colors = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "colors", "colors");
            NamedValuesColors = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "NamedValuesColors", "NamedValuesColors");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.Enums.ClassWithEnums> OnlineToPlainAsync()
        {
            Pocos.Enums.ClassWithEnums plain = new Pocos.Enums.ClassWithEnums();
            await this.ReadAsync();
            plain.colors = (Enums.Colors)colors.LastValue;
            plain.NamedValuesColors = NamedValuesColors.LastValue;
            return plain;
        }

        protected async Task<Pocos.Enums.ClassWithEnums> OnlineToPlainAsync(Pocos.Enums.ClassWithEnums plain)
        {
            plain.colors = (Enums.Colors)colors.LastValue;
            plain.NamedValuesColors = NamedValuesColors.LastValue;
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Enums.ClassWithEnums plain)
        {
            colors.Cyclic = (short)plain.colors;
            NamedValuesColors.Cyclic = plain.NamedValuesColors;
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.Enums.ClassWithEnums> ShadowToPlainAsync()
        {
            Pocos.Enums.ClassWithEnums plain = new Pocos.Enums.ClassWithEnums();
            plain.colors = (Enums.Colors)colors.Shadow;
            plain.NamedValuesColors = NamedValuesColors.Shadow;
            return plain;
        }

        protected async Task<Pocos.Enums.ClassWithEnums> ShadowToPlainAsync(Pocos.Enums.ClassWithEnums plain)
        {
            plain.colors = (Enums.Colors)colors.Shadow;
            plain.NamedValuesColors = NamedValuesColors.Shadow;
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Enums.ClassWithEnums plain)
        {
            colors.Shadow = (short)plain.colors;
            NamedValuesColors.Shadow = plain.NamedValuesColors;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.Enums.ClassWithEnums CreateEmptyPoco()
        {
            return new Pocos.Enums.ClassWithEnums();
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

    public enum Colors
    {
        Red,
        Green,
        Blue
    }

    public enum NamedValuesColors : String
    {
        Red = 49,
        Green = 50,
        Blue = 51
    }
}

namespace misc
{
    public partial class VariousMembers : AXSharp.Connector.ITwinObject
    {
        public misc.SomeClass _SomeClass { get; }

        public misc.Motor _Motor { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public VariousMembers(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            _SomeClass = new misc.SomeClass(this, "_SomeClass", "_SomeClass");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.misc.VariousMembers> OnlineToPlainAsync()
        {
            Pocos.misc.VariousMembers plain = new Pocos.misc.VariousMembers();
            await this.ReadAsync();
            plain._SomeClass = await _SomeClass.OnlineToPlainAsync();
            plain._Motor = await _Motor.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.misc.VariousMembers> OnlineToPlainAsync(Pocos.misc.VariousMembers plain)
        {
            plain._SomeClass = await _SomeClass.OnlineToPlainAsync();
            plain._Motor = await _Motor.OnlineToPlainAsync();
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.misc.VariousMembers plain)
        {
            await this._SomeClass.PlainToOnlineAsync(plain._SomeClass);
            await this._Motor.PlainToOnlineAsync(plain._Motor);
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.misc.VariousMembers> ShadowToPlainAsync()
        {
            Pocos.misc.VariousMembers plain = new Pocos.misc.VariousMembers();
            plain._SomeClass = await _SomeClass.ShadowToPlainAsync();
            plain._Motor = await _Motor.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.misc.VariousMembers> ShadowToPlainAsync(Pocos.misc.VariousMembers plain)
        {
            plain._SomeClass = await _SomeClass.ShadowToPlainAsync();
            plain._Motor = await _Motor.ShadowToPlainAsync();
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.misc.VariousMembers plain)
        {
            await this._SomeClass.PlainToShadowAsync(plain._SomeClass);
            await this._Motor.PlainToShadowAsync(plain._Motor);
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.misc.VariousMembers CreateEmptyPoco()
        {
            return new Pocos.misc.VariousMembers();
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

    public partial class SomeClass : AXSharp.Connector.ITwinObject
    {
        public OnlinerString SomeClassVariable { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public SomeClass(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            SomeClassVariable = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "SomeClassVariable", "SomeClassVariable");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.misc.SomeClass> OnlineToPlainAsync()
        {
            Pocos.misc.SomeClass plain = new Pocos.misc.SomeClass();
            await this.ReadAsync();
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        protected async Task<Pocos.misc.SomeClass> OnlineToPlainAsync(Pocos.misc.SomeClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.misc.SomeClass plain)
        {
            SomeClassVariable.Cyclic = plain.SomeClassVariable;
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.misc.SomeClass> ShadowToPlainAsync()
        {
            Pocos.misc.SomeClass plain = new Pocos.misc.SomeClass();
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        protected async Task<Pocos.misc.SomeClass> ShadowToPlainAsync(Pocos.misc.SomeClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.misc.SomeClass plain)
        {
            SomeClassVariable.Shadow = plain.SomeClassVariable;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.misc.SomeClass CreateEmptyPoco()
        {
            return new Pocos.misc.SomeClass();
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

    public partial class Motor : AXSharp.Connector.ITwinObject
    {
        public OnlinerBool isRunning { get; }

        public Motor(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            isRunning = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "isRunning", "isRunning");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.misc.Motor> OnlineToPlainAsync()
        {
            Pocos.misc.Motor plain = new Pocos.misc.Motor();
            await this.ReadAsync();
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        protected async Task<Pocos.misc.Motor> OnlineToPlainAsync(Pocos.misc.Motor plain)
        {
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.misc.Motor plain)
        {
            isRunning.Cyclic = plain.isRunning;
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.misc.Motor> ShadowToPlainAsync()
        {
            Pocos.misc.Motor plain = new Pocos.misc.Motor();
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        protected async Task<Pocos.misc.Motor> ShadowToPlainAsync(Pocos.misc.Motor plain)
        {
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.misc.Motor plain)
        {
            isRunning.Shadow = plain.isRunning;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.misc.Motor CreateEmptyPoco()
        {
            return new Pocos.misc.Motor();
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

    public partial class Vehicle : AXSharp.Connector.ITwinObject
    {
        public misc.Motor m { get; }

        public OnlinerInt displacement { get; }

        public Vehicle(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.misc.Vehicle> OnlineToPlainAsync()
        {
            Pocos.misc.Vehicle plain = new Pocos.misc.Vehicle();
            await this.ReadAsync();
            plain.m = await m.OnlineToPlainAsync();
            plain.displacement = displacement.LastValue;
            return plain;
        }

        protected async Task<Pocos.misc.Vehicle> OnlineToPlainAsync(Pocos.misc.Vehicle plain)
        {
            plain.m = await m.OnlineToPlainAsync();
            plain.displacement = displacement.LastValue;
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.misc.Vehicle plain)
        {
            await this.m.PlainToOnlineAsync(plain.m);
            displacement.Cyclic = plain.displacement;
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.misc.Vehicle> ShadowToPlainAsync()
        {
            Pocos.misc.Vehicle plain = new Pocos.misc.Vehicle();
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        protected async Task<Pocos.misc.Vehicle> ShadowToPlainAsync(Pocos.misc.Vehicle plain)
        {
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.misc.Vehicle plain)
        {
            await this.m.PlainToShadowAsync(plain.m);
            displacement.Shadow = plain.displacement;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.misc.Vehicle CreateEmptyPoco()
        {
            return new Pocos.misc.Vehicle();
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

namespace UnknownArraysShouldNotBeTraspiled
{
    public partial class ClassWithArrays : AXSharp.Connector.ITwinObject
    {
        public UnknownArraysShouldNotBeTraspiled.Complex[] _complexKnown { get; }

        public OnlinerByte[] _primitive { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithArrays(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            _complexKnown = new UnknownArraysShouldNotBeTraspiled.Complex[11];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_complexKnown, this, "_complexKnown", "_complexKnown", (p, rt, st) => new UnknownArraysShouldNotBeTraspiled.Complex(p, rt, st));
            _primitive = new OnlinerByte[11];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_primitive, this, "_primitive", "_primitive", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(p, rt, st));
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays> OnlineToPlainAsync()
        {
            Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays plain = new Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays();
            await this.ReadAsync();
            plain._complexKnown = _complexKnown.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
            plain._primitive = _primitive.Select(p => p.LastValue).ToArray();
            return plain;
        }

        protected async Task<Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays> OnlineToPlainAsync(Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays plain)
        {
            plain._complexKnown = _complexKnown.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
            plain._primitive = _primitive.Select(p => p.LastValue).ToArray();
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays plain)
        {
            var __complexKnown_i_FE8484DAB3 = 0;
            _complexKnown.Select(p => p.PlainToOnlineAsync(plain._complexKnown[__complexKnown_i_FE8484DAB3++])).ToArray();
            var __primitive_i_FE8484DAB3 = 0;
            _primitive.Select(p => p.Cyclic = plain._primitive[__primitive_i_FE8484DAB3++]).ToArray();
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays> ShadowToPlainAsync()
        {
            Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays plain = new Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays();
            plain._complexKnown = _complexKnown.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            plain._primitive = _primitive.Select(p => p.Shadow).ToArray();
            return plain;
        }

        protected async Task<Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays> ShadowToPlainAsync(Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays plain)
        {
            plain._complexKnown = _complexKnown.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            plain._primitive = _primitive.Select(p => p.Shadow).ToArray();
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays plain)
        {
            var __complexKnown_i_FE8484DAB3 = 0;
            _complexKnown.Select(p => p.PlainToShadowAsync(plain._complexKnown[__complexKnown_i_FE8484DAB3++])).ToArray();
            var __primitive_i_FE8484DAB3 = 0;
            _primitive.Select(p => p.Shadow = plain._primitive[__primitive_i_FE8484DAB3++]).ToArray();
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays CreateEmptyPoco()
        {
            return new Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays();
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

    public partial class Complex : AXSharp.Connector.ITwinObject
    {
        public OnlinerString HelloString { get; }

        public OnlinerULInt Id { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Complex(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            HelloString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "HelloString", "HelloString");
            Id = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Id", "Id");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.UnknownArraysShouldNotBeTraspiled.Complex> OnlineToPlainAsync()
        {
            Pocos.UnknownArraysShouldNotBeTraspiled.Complex plain = new Pocos.UnknownArraysShouldNotBeTraspiled.Complex();
            await this.ReadAsync();
            plain.HelloString = HelloString.LastValue;
            plain.Id = Id.LastValue;
            return plain;
        }

        protected async Task<Pocos.UnknownArraysShouldNotBeTraspiled.Complex> OnlineToPlainAsync(Pocos.UnknownArraysShouldNotBeTraspiled.Complex plain)
        {
            plain.HelloString = HelloString.LastValue;
            plain.Id = Id.LastValue;
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.UnknownArraysShouldNotBeTraspiled.Complex plain)
        {
            HelloString.Cyclic = plain.HelloString;
            Id.Cyclic = plain.Id;
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.UnknownArraysShouldNotBeTraspiled.Complex> ShadowToPlainAsync()
        {
            Pocos.UnknownArraysShouldNotBeTraspiled.Complex plain = new Pocos.UnknownArraysShouldNotBeTraspiled.Complex();
            plain.HelloString = HelloString.Shadow;
            plain.Id = Id.Shadow;
            return plain;
        }

        protected async Task<Pocos.UnknownArraysShouldNotBeTraspiled.Complex> ShadowToPlainAsync(Pocos.UnknownArraysShouldNotBeTraspiled.Complex plain)
        {
            plain.HelloString = HelloString.Shadow;
            plain.Id = Id.Shadow;
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.UnknownArraysShouldNotBeTraspiled.Complex plain)
        {
            HelloString.Shadow = plain.HelloString;
            Id.Shadow = plain.Id;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.UnknownArraysShouldNotBeTraspiled.Complex CreateEmptyPoco()
        {
            return new Pocos.UnknownArraysShouldNotBeTraspiled.Complex();
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