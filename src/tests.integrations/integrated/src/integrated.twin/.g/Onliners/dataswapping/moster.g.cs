using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace MonsterData
{
    public partial class MonsterBase : Ix.Connector.ITwinObject
    {
        public OnlinerString Description { get; }

        public OnlinerULInt Id { get; }

        public OnlinerByte[] ArrayOfBytes { get; }

        public MonsterData.DriveBase[] ArrayOfDrives { get; }

        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public MonsterBase(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Description = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Description", "Description");
            Id = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Id", "Id");
            ArrayOfBytes = new OnlinerByte[4];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfBytes, this, "ArrayOfBytes", "ArrayOfBytes", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(p, rt, st));
            ArrayOfDrives = new MonsterData.DriveBase[4];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, this, "ArrayOfDrives", "ArrayOfDrives", (p, rt, st) => new MonsterData.DriveBase(p, rt, st));
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.MonsterData.MonsterBase> OnlineToPlainAsync()
        {
            Pocos.MonsterData.MonsterBase plain = new Pocos.MonsterData.MonsterBase();
            await this.ReadAsync();
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        protected async Task<Pocos.MonsterData.MonsterBase> OnlineToPlainAsync(Pocos.MonsterData.MonsterBase plain)
        {
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.MonsterData.MonsterBase plain)
        {
            Description.Cyclic = plain.Description;
            Id.Cyclic = plain.Id;
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
            ArrayOfBytes.Select(p => p.Cyclic = plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++]).ToArray();
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
            ArrayOfDrives.Select(p => p.PlainToOnlineAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.MonsterData.MonsterBase> ShadowToPlainAsync()
        {
            Pocos.MonsterData.MonsterBase plain = new Pocos.MonsterData.MonsterBase();
            plain.Description = Description.Shadow;
            plain.Id = Id.Shadow;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.Shadow).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        protected async Task<Pocos.MonsterData.MonsterBase> ShadowToPlainAsync(Pocos.MonsterData.MonsterBase plain)
        {
            plain.Description = Description.Shadow;
            plain.Id = Id.Shadow;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.Shadow).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.MonsterData.MonsterBase plain)
        {
            Description.Shadow = plain.Description;
            Id.Shadow = plain.Id;
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
            ArrayOfBytes.Select(p => p.Shadow = plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++]).ToArray();
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
            ArrayOfDrives.Select(p => p.PlainToShadowAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
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

    public partial class Monster : MonsterBase
    {
        public MonsterData.DriveBase DriveA { get; }

        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Monster(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            DriveA = new MonsterData.DriveBase(this, "DriveA", "DriveA");
            PostConstruct(parent, readableTail, symbolTail);
        }

        public override T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.MonsterData.Monster> OnlineToPlainAsync()
        {
            Pocos.MonsterData.Monster plain = new Pocos.MonsterData.Monster();
            await this.ReadAsync();
            await base.OnlineToPlainAsync(plain);
            plain.DriveA = await DriveA.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.MonsterData.Monster> OnlineToPlainAsync(Pocos.MonsterData.Monster plain)
        {
            await base.OnlineToPlainAsync(plain);
            plain.DriveA = await DriveA.OnlineToPlainAsync();
            return plain;
        }

        public override void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.MonsterData.Monster plain)
        {
            await base.PlainToOnlineAsync(plain);
            await this.DriveA.PlainToOnlineAsync(plain.DriveA);
            return await this.WriteAsync();
        }

        public override T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.MonsterData.Monster> ShadowToPlainAsync()
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

        public override void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.MonsterData.Monster plain)
        {
            await base.PlainToShadowAsync(plain);
            await this.DriveA.PlainToShadowAsync(plain.DriveA);
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.MonsterData.Monster CreateEmptyPoco()
        {
            return new Pocos.MonsterData.Monster();
        }
    }

    public partial class DriveBase : Ix.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public DriveBase(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Position = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Position", "Position");
            Velo = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Velo", "Velo");
            Acc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Acc", "Acc");
            Dcc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Dcc", "Dcc");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.MonsterData.DriveBase> OnlineToPlainAsync()
        {
            Pocos.MonsterData.DriveBase plain = new Pocos.MonsterData.DriveBase();
            await this.ReadAsync();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        protected async Task<Pocos.MonsterData.DriveBase> OnlineToPlainAsync(Pocos.MonsterData.DriveBase plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.MonsterData.DriveBase plain)
        {
            Position.Cyclic = plain.Position;
            Velo.Cyclic = plain.Velo;
            Acc.Cyclic = plain.Acc;
            Dcc.Cyclic = plain.Dcc;
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
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

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
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