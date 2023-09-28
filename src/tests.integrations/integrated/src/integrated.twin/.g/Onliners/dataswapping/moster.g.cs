using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;

namespace MonsterData
{
    public partial class MonsterBase : AXSharp.Connector.ITwinObject
    {
        public OnlinerString Description { get; }

        public OnlinerULInt Id { get; }

        public OnlinerByte[] ArrayOfBytes { get; }

        public MonsterData.DriveBase[] ArrayOfDrives { get; }

        [IgnoreOnPocoOperation()]
        public MonsterData.DriveBase DriveBase_tobeignoredbypocooperations { get; }

        [IgnoreOnPocoOperation()]
        public OnlinerString Description_tobeignoredbypocooperations { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public MonsterBase(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Description = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Description", "Description");
            Id = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Id", "Id");
            ArrayOfBytes = new OnlinerByte[4];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfBytes, this, "ArrayOfBytes", "ArrayOfBytes", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(p, rt, st), new[] { (0, 3) });
            ArrayOfDrives = new MonsterData.DriveBase[4];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, this, "ArrayOfDrives", "ArrayOfDrives", (p, rt, st) => new MonsterData.DriveBase(p, rt, st), new[] { (0, 3) });
            DriveBase_tobeignoredbypocooperations = new MonsterData.DriveBase(this, "DriveBase_tobeignoredbypocooperations", "DriveBase_tobeignoredbypocooperations");
            Description_tobeignoredbypocooperations = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Description_tobeignoredbypocooperations", "Description_tobeignoredbypocooperations");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.MonsterData.MonsterBase> OnlineToPlainAsync()
        {
            Pocos.MonsterData.MonsterBase plain = new Pocos.MonsterData.MonsterBase();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveBase_tobeignoredbypocooperations = await DriveBase_tobeignoredbypocooperations._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.Description_tobeignoredbypocooperations = Description_tobeignoredbypocooperations.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.MonsterData.MonsterBase> _OnlineToPlainNoacAsync()
        {
            Pocos.MonsterData.MonsterBase plain = new Pocos.MonsterData.MonsterBase();
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveBase_tobeignoredbypocooperations = await DriveBase_tobeignoredbypocooperations._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.Description_tobeignoredbypocooperations = Description_tobeignoredbypocooperations.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.MonsterData.MonsterBase> _OnlineToPlainNoacAsync(Pocos.MonsterData.MonsterBase plain)
        {
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveBase_tobeignoredbypocooperations = await DriveBase_tobeignoredbypocooperations._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.Description_tobeignoredbypocooperations = Description_tobeignoredbypocooperations.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.MonsterData.MonsterBase plain)
        {
            Description.Cyclic = plain.Description;
            Id.Cyclic = plain.Id;
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
            ArrayOfBytes.Select(p => p.Cyclic = plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++]).ToArray();
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            ArrayOfDrives.Select(p => p._PlainToOnlineNoacAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.DriveBase_tobeignoredbypocooperations._PlainToOnlineNoacAsync(plain.DriveBase_tobeignoredbypocooperations);
#pragma warning restore CS0612
            Description_tobeignoredbypocooperations.Cyclic = plain.Description_tobeignoredbypocooperations;
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.MonsterData.MonsterBase plain)
        {
            Description.Cyclic = plain.Description;
            Id.Cyclic = plain.Id;
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
            ArrayOfBytes.Select(p => p.Cyclic = plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++]).ToArray();
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            ArrayOfDrives.Select(p => p._PlainToOnlineNoacAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.DriveBase_tobeignoredbypocooperations._PlainToOnlineNoacAsync(plain.DriveBase_tobeignoredbypocooperations);
#pragma warning restore CS0612
            Description_tobeignoredbypocooperations.Cyclic = plain.Description_tobeignoredbypocooperations;
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.MonsterData.MonsterBase> ShadowToPlainAsync()
        {
            Pocos.MonsterData.MonsterBase plain = new Pocos.MonsterData.MonsterBase();
            plain.Description = Description.Shadow;
            plain.Id = Id.Shadow;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.Shadow).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            plain.DriveBase_tobeignoredbypocooperations = await DriveBase_tobeignoredbypocooperations.ShadowToPlainAsync();
            plain.Description_tobeignoredbypocooperations = Description_tobeignoredbypocooperations.Shadow;
            return plain;
        }

        protected async Task<Pocos.MonsterData.MonsterBase> ShadowToPlainAsync(Pocos.MonsterData.MonsterBase plain)
        {
            plain.Description = Description.Shadow;
            plain.Id = Id.Shadow;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.Shadow).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            plain.DriveBase_tobeignoredbypocooperations = await DriveBase_tobeignoredbypocooperations.ShadowToPlainAsync();
            plain.Description_tobeignoredbypocooperations = Description_tobeignoredbypocooperations.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.MonsterData.MonsterBase plain)
        {
            Description.Shadow = plain.Description;
            Id.Shadow = plain.Id;
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
            ArrayOfBytes.Select(p => p.Shadow = plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++]).ToArray();
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
            ArrayOfDrives.Select(p => p.PlainToShadowAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
            await this.DriveBase_tobeignoredbypocooperations.PlainToShadowAsync(plain.DriveBase_tobeignoredbypocooperations);
            Description_tobeignoredbypocooperations.Shadow = plain.Description_tobeignoredbypocooperations;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.MonsterData.MonsterBase CreateEmptyPoco()
        {
            return new Pocos.MonsterData.MonsterBase();
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

        public AXSharp.Connector.Localizations.Translator Interpreter => global::integrated.PlcTranslator.Instance;
    }

    public partial class Monster : MonsterData.MonsterBase
    {
        public MonsterData.DriveBase DriveA { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Monster(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            DriveA = new MonsterData.DriveBase(this, "DriveA", "DriveA");
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async override Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public new async Task<Pocos.MonsterData.Monster> OnlineToPlainAsync()
        {
            Pocos.MonsterData.Monster plain = new Pocos.MonsterData.Monster();
            await this.ReadAsync<IgnoreOnPocoOperation>();
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveA = await DriveA._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public new async Task<Pocos.MonsterData.Monster> _OnlineToPlainNoacAsync()
        {
            Pocos.MonsterData.Monster plain = new Pocos.MonsterData.Monster();
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveA = await DriveA._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.MonsterData.Monster> _OnlineToPlainNoacAsync(Pocos.MonsterData.Monster plain)
        {
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveA = await DriveA._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        public async override Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.MonsterData.Monster plain)
        {
            await base._PlainToOnlineNoacAsync(plain);
#pragma warning disable CS0612
            await this.DriveA._PlainToOnlineNoacAsync(plain.DriveA);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.MonsterData.Monster plain)
        {
            await base._PlainToOnlineNoacAsync(plain);
#pragma warning disable CS0612
            await this.DriveA._PlainToOnlineNoacAsync(plain.DriveA);
#pragma warning restore CS0612
        }

        public async override Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public new async Task<Pocos.MonsterData.Monster> ShadowToPlainAsync()
        {
            Pocos.MonsterData.Monster plain = new Pocos.MonsterData.Monster();
            await base.ShadowToPlainAsync(plain);
            plain.DriveA = await DriveA.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.MonsterData.Monster> ShadowToPlainAsync(Pocos.MonsterData.Monster plain)
        {
            await base.ShadowToPlainAsync(plain);
            plain.DriveA = await DriveA.ShadowToPlainAsync();
            return plain;
        }

        public async override Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.MonsterData.Monster plain)
        {
            await base.PlainToShadowAsync(plain);
            await this.DriveA.PlainToShadowAsync(plain.DriveA);
            return this.RetrievePrimitives();
        }

        public new void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public new Pocos.MonsterData.Monster CreateEmptyPoco()
        {
            return new Pocos.MonsterData.Monster();
        }
    }

    public partial class DriveBase : AXSharp.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public DriveBase(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Position = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Position", "Position");
            Velo = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Velo", "Velo");
            Acc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Acc", "Acc");
            Dcc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Dcc", "Dcc");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.MonsterData.DriveBase> OnlineToPlainAsync()
        {
            Pocos.MonsterData.DriveBase plain = new Pocos.MonsterData.DriveBase();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.MonsterData.DriveBase> _OnlineToPlainNoacAsync()
        {
            Pocos.MonsterData.DriveBase plain = new Pocos.MonsterData.DriveBase();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.MonsterData.DriveBase> _OnlineToPlainNoacAsync(Pocos.MonsterData.DriveBase plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.MonsterData.DriveBase plain)
        {
            Position.Cyclic = plain.Position;
            Velo.Cyclic = plain.Velo;
            Acc.Cyclic = plain.Acc;
            Dcc.Cyclic = plain.Dcc;
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.MonsterData.DriveBase plain)
        {
            Position.Cyclic = plain.Position;
            Velo.Cyclic = plain.Velo;
            Acc.Cyclic = plain.Acc;
            Dcc.Cyclic = plain.Dcc;
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.MonsterData.DriveBase> ShadowToPlainAsync()
        {
            Pocos.MonsterData.DriveBase plain = new Pocos.MonsterData.DriveBase();
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            return plain;
        }

        protected async Task<Pocos.MonsterData.DriveBase> ShadowToPlainAsync(Pocos.MonsterData.DriveBase plain)
        {
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.MonsterData.DriveBase plain)
        {
            Position.Shadow = plain.Position;
            Velo.Shadow = plain.Velo;
            Acc.Shadow = plain.Acc;
            Dcc.Shadow = plain.Dcc;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.MonsterData.DriveBase CreateEmptyPoco()
        {
            return new Pocos.MonsterData.DriveBase();
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

        public AXSharp.Connector.Localizations.Translator Interpreter => global::integrated.PlcTranslator.Instance;
    }
}