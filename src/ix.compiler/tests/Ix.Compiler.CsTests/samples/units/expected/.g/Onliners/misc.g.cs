using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace misc
{
    public partial class VariousMembers : Ix.Connector.ITwinObject
    {
        public misc.SomeClass _SomeClass { get; }

        public misc.Motor _Motor { get; }

        public VariousMembers(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            _SomeClass = new misc.SomeClass(this, "_SomeClass", "_SomeClass");
            parent.AddChild(this);
            parent.AddKid(this);
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.misc.VariousMembers plain)
        {
            await this._SomeClass.PlainToOnlineAsync(plain._SomeClass);
            await this._Motor.PlainToOnlineAsync(plain._Motor);
            return await this.WriteAsync();
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

    public partial class SomeClass : Ix.Connector.ITwinObject
    {
        public OnlinerString SomeClassVariable { get; }

        public SomeClass(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            SomeClassVariable = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "SomeClassVariable", "SomeClassVariable");
            parent.AddChild(this);
            parent.AddKid(this);
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.misc.SomeClass plain)
        {
            SomeClassVariable.Cyclic = plain.SomeClassVariable;
            return await this.WriteAsync();
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.misc.SomeClass plain)
        {
            SomeClassVariable.Shadow = plain.SomeClassVariable;
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

    public partial class Motor : Ix.Connector.ITwinObject
    {
        public OnlinerBool isRunning { get; }

        public Motor(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            isRunning = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "isRunning", "isRunning");
            parent.AddChild(this);
            parent.AddKid(this);
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.misc.Motor plain)
        {
            isRunning.Cyclic = plain.isRunning;
            return await this.WriteAsync();
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.misc.Motor plain)
        {
            isRunning.Shadow = plain.isRunning;
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

    public partial class Vehicle : Ix.Connector.ITwinObject
    {
        public misc.Motor m { get; }

        public OnlinerInt displacement { get; }

        public Vehicle(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
            parent.AddChild(this);
            parent.AddKid(this);
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.misc.Vehicle plain)
        {
            await this.m.PlainToOnlineAsync(plain.m);
            displacement.Cyclic = plain.displacement;
            return await this.WriteAsync();
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

namespace UnknownArraysShouldNotBeTraspiled
{
    public partial class ClassWithArrays : Ix.Connector.ITwinObject
    {
        public UnknownArraysShouldNotBeTraspiled.Complex[] _complexKnown { get; }

        public OnlinerByte[] _primitive { get; }

        public ClassWithArrays(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            _complexKnown = new UnknownArraysShouldNotBeTraspiled.Complex[11];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(_complexKnown, this, "_complexKnown", "_complexKnown", (p, rt, st) => new UnknownArraysShouldNotBeTraspiled.Complex(p, rt, st));
            _primitive = new OnlinerByte[11];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(_primitive, this, "_primitive", "_primitive", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(p, rt, st));
            parent.AddChild(this);
            parent.AddKid(this);
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.UnknownArraysShouldNotBeTraspiled.ClassWithArrays plain)
        {
            var __complexKnown_i_FE8484DAB3 = 0;
            _complexKnown.Select(p => p.PlainToOnlineAsync(plain._complexKnown[__complexKnown_i_FE8484DAB3++])).ToArray();
            var __primitive_i_FE8484DAB3 = 0;
            _primitive.Select(p => p.Cyclic = plain._primitive[__primitive_i_FE8484DAB3++]).ToArray();
            return await this.WriteAsync();
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

    public partial class Complex : Ix.Connector.ITwinObject
    {
        public OnlinerString HelloString { get; }

        public OnlinerULInt Id { get; }

        public Complex(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            HelloString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "HelloString", "HelloString");
            Id = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Id", "Id");
            parent.AddChild(this);
            parent.AddKid(this);
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.UnknownArraysShouldNotBeTraspiled.Complex plain)
        {
            HelloString.Cyclic = plain.HelloString;
            Id.Cyclic = plain.Id;
            return await this.WriteAsync();
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