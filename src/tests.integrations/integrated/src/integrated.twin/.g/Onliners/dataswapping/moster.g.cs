using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace MonsterData
{
    public partial class MonsterBase : Ix.Connector.ITwinObject
    {
        public OnlinerByte[] ArrayOfBytes { get; }

        public MonsterData.DriveBase[] ArrayOfDrives { get; }

        public MonsterBase(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            ArrayOfBytes = new OnlinerByte[4];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfBytes, this, "ArrayOfBytes", "ArrayOfBytes", (p, rt, st) => new OnlinerByte(p, rt, st));
            ArrayOfDrives = new MonsterData.DriveBase[4];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, this, "ArrayOfDrives", "ArrayOfDrives", (p, rt, st) => new MonsterData.DriveBase(p, rt, st));
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.MonsterData.MonsterBase> OnlineToPlain()
        {
            Pocos.MonsterData.MonsterBase plain = new Pocos.MonsterData.MonsterBase();
            await this.ReadAsync();
            Ix.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<OnlinerByte, Byte>(plain.ArrayOfBytes, ArrayOfBytes);
            Ix.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<MonsterData.DriveBase, Pocos.MonsterData.DriveBase>(plain.ArrayOfDrives, ArrayOfDrives);
            Ix.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<OnlinerByte, Byte>(plain.ArrayOfBytes, ArrayOfBytes);
            Ix.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<MonsterData.DriveBase, Pocos.MonsterData.DriveBase>(plain.ArrayOfDrives, ArrayOfDrives);
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.MonsterData.MonsterBase plain)
        {
            Ix.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<Byte, OnlinerByte>(plain.ArrayOfBytes, ArrayOfBytes);
            Ix.Connector.BuilderHelpers.Arrays.CopyPlainToOnline<Pocos.MonsterData.DriveBase, MonsterData.DriveBase>(plain.ArrayOfDrives, ArrayOfDrives);
            return await this.WriteAsync();
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

    public partial class Monster : MonsterBase
    {
        public MonsterData.DriveBase DriveA { get; }

        public Monster(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            DriveA = new MonsterData.DriveBase(this, "DriveA", "DriveA");
        }

        public async Task<Pocos.MonsterData.Monster> OnlineToPlain()
        {
            Pocos.MonsterData.Monster plain = new Pocos.MonsterData.Monster();
            await this.ReadAsync();
            plain.DriveA = await DriveA.OnlineToPlain();
            plain = (Pocos.MonsterData.Monster)await base.OnlineToPlain();
            plain.DriveA = await DriveA.OnlineToPlain();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.MonsterData.Monster plain)
        {
            await base.PlainToOnline(plain);
            await this.DriveA.PlainToOnline(plain.DriveA);
            return await this.WriteAsync();
        }
    }

    public partial class DriveBase : Ix.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        public DriveBase(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Position = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Position", "Position");
            Velo = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Velo", "Velo");
            Acc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Acc", "Acc");
            Dcc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Dcc", "Dcc");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.MonsterData.DriveBase> OnlineToPlain()
        {
            Pocos.MonsterData.DriveBase plain = new Pocos.MonsterData.DriveBase();
            await this.ReadAsync();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.MonsterData.DriveBase plain)
        {
            Position.Cyclic = plain.Position;
            Velo.Cyclic = plain.Velo;
            Acc.Cyclic = plain.Acc;
            Dcc.Cyclic = plain.Dcc;
            return await this.WriteAsync();
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