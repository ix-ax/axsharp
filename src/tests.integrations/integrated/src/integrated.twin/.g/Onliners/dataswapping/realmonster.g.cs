using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace RealMonsterData
{
    public partial class RealMonsterBase : Ix.Connector.ITwinObject
    {
        public OnlinerString Description { get; }

        public OnlinerULInt Id { get; }

        public OnlinerDate TestDate { get; }

        public OnlinerDateTime TestDateTime { get; }

        public OnlinerTimeOfDay TestTimeSpan { get; }

        public OnlinerByte[] ArrayOfBytes { get; }

        public RealMonsterData.DriveBaseNested[] ArrayOfDrives { get; }

        public RealMonsterBase(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Description = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Description", "Description");
            Id = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Id", "Id");
            TestDate = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "TestDate", "TestDate");
            TestDateTime = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this, "TestDateTime", "TestDateTime");
            TestTimeSpan = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this, "TestTimeSpan", "TestTimeSpan");
            ArrayOfBytes = new OnlinerByte[4];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfBytes, this, "ArrayOfBytes", "ArrayOfBytes", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(p, rt, st));
            ArrayOfDrives = new RealMonsterData.DriveBaseNested[4];
            Ix.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, this, "ArrayOfDrives", "ArrayOfDrives", (p, rt, st) => new RealMonsterData.DriveBaseNested(p, rt, st));
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.RealMonsterData.RealMonsterBase> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.RealMonsterBase plain = new Pocos.RealMonsterData.RealMonsterBase();
            await this.ReadAsync();
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.TestDate = TestDate.LastValue;
            plain.TestDateTime = TestDateTime.LastValue;
            plain.TestTimeSpan = TestTimeSpan.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.RealMonsterBase> OnlineToPlainAsync(Pocos.RealMonsterData.RealMonsterBase plain)
        {
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.TestDate = TestDate.LastValue;
            plain.TestDateTime = TestDateTime.LastValue;
            plain.TestTimeSpan = TestTimeSpan.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.OnlineToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.RealMonsterBase plain)
        {
            Description.Cyclic = plain.Description;
            Id.Cyclic = plain.Id;
            TestDate.Cyclic = plain.TestDate;
            TestDateTime.Cyclic = plain.TestDateTime;
            TestTimeSpan.Cyclic = plain.TestTimeSpan;
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
            ArrayOfBytes.Select(p => p.Cyclic = plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++]).ToArray();
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
            ArrayOfDrives.Select(p => p.PlainToOnlineAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
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

    public partial class RealMonster : RealMonsterBase
    {
        public RealMonsterData.DriveBaseNested DriveA { get; }

        public all_primitives primitives { get; }

        public RealMonster(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            DriveA = new RealMonsterData.DriveBaseNested(this, "DriveA", "DriveA");
            primitives = new all_primitives(this, "primitives", "primitives");
        }

        public async Task<Pocos.RealMonsterData.RealMonster> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.RealMonster plain = new Pocos.RealMonsterData.RealMonster();
            await this.ReadAsync();
            await base.OnlineToPlainAsync(plain);
            plain.DriveA = await DriveA.OnlineToPlainAsync();
            plain.primitives = await primitives.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.RealMonster> OnlineToPlainAsync(Pocos.RealMonsterData.RealMonster plain)
        {
            await base.OnlineToPlainAsync(plain);
            plain.DriveA = await DriveA.OnlineToPlainAsync();
            plain.primitives = await primitives.OnlineToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.RealMonster plain)
        {
            await base.PlainToOnlineAsync(plain);
            await this.DriveA.PlainToOnlineAsync(plain.DriveA);
            await this.primitives.PlainToOnlineAsync(plain.primitives);
            return await this.WriteAsync();
        }
    }

    public partial class DriveBaseNested : Ix.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        public RealMonsterData.NestedLevelOne NestedLevelOne { get; }

        public DriveBaseNested(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
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
            NestedLevelOne = new RealMonsterData.NestedLevelOne(this, "NestedLevelOne", "NestedLevelOne");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.RealMonsterData.DriveBaseNested> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.DriveBaseNested plain = new Pocos.RealMonsterData.DriveBaseNested();
            await this.ReadAsync();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            plain.NestedLevelOne = await NestedLevelOne.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.DriveBaseNested> OnlineToPlainAsync(Pocos.RealMonsterData.DriveBaseNested plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            plain.NestedLevelOne = await NestedLevelOne.OnlineToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.DriveBaseNested plain)
        {
            Position.Cyclic = plain.Position;
            Velo.Cyclic = plain.Velo;
            Acc.Cyclic = plain.Acc;
            Dcc.Cyclic = plain.Dcc;
            await this.NestedLevelOne.PlainToOnlineAsync(plain.NestedLevelOne);
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

    public partial class NestedLevelOne : Ix.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        public RealMonsterData.NestedLevelTwo NestedLevelTwo { get; }

        public NestedLevelOne(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
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
            NestedLevelTwo = new RealMonsterData.NestedLevelTwo(this, "NestedLevelTwo", "NestedLevelTwo");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.RealMonsterData.NestedLevelOne> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelOne plain = new Pocos.RealMonsterData.NestedLevelOne();
            await this.ReadAsync();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            plain.NestedLevelTwo = await NestedLevelTwo.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.NestedLevelOne> OnlineToPlainAsync(Pocos.RealMonsterData.NestedLevelOne plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            plain.NestedLevelTwo = await NestedLevelTwo.OnlineToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.NestedLevelOne plain)
        {
            Position.Cyclic = plain.Position;
            Velo.Cyclic = plain.Velo;
            Acc.Cyclic = plain.Acc;
            Dcc.Cyclic = plain.Dcc;
            await this.NestedLevelTwo.PlainToOnlineAsync(plain.NestedLevelTwo);
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

    public partial class NestedLevelTwo : Ix.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        public RealMonsterData.NestedLevelThree NestedLevelThree { get; }

        public NestedLevelTwo(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
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
            NestedLevelThree = new RealMonsterData.NestedLevelThree(this, "NestedLevelThree", "NestedLevelThree");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.RealMonsterData.NestedLevelTwo> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelTwo plain = new Pocos.RealMonsterData.NestedLevelTwo();
            await this.ReadAsync();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            plain.NestedLevelThree = await NestedLevelThree.OnlineToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.NestedLevelTwo> OnlineToPlainAsync(Pocos.RealMonsterData.NestedLevelTwo plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            plain.NestedLevelThree = await NestedLevelThree.OnlineToPlainAsync();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.NestedLevelTwo plain)
        {
            Position.Cyclic = plain.Position;
            Velo.Cyclic = plain.Velo;
            Acc.Cyclic = plain.Acc;
            Dcc.Cyclic = plain.Dcc;
            await this.NestedLevelThree.PlainToOnlineAsync(plain.NestedLevelThree);
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

    public partial class NestedLevelThree : Ix.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        public NestedLevelThree(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async Task<Pocos.RealMonsterData.NestedLevelThree> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelThree plain = new Pocos.RealMonsterData.NestedLevelThree();
            await this.ReadAsync();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.NestedLevelThree> OnlineToPlainAsync(Pocos.RealMonsterData.NestedLevelThree plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.NestedLevelThree plain)
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