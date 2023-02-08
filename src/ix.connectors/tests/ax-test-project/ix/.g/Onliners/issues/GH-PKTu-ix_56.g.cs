using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace GH.PKTu.ix_56
{
    public partial class ComplexMember : Ix.Connector.ITwinObject
    {
        public OnlinerInt Counter { get; }

        public ComplexMember(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Counter = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Counter", "Counter");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.GH.PKTu.ix_56.ComplexMember> OnlineToPlain()
        {
            Pocos.GH.PKTu.ix_56.ComplexMember plain = new Pocos.GH.PKTu.ix_56.ComplexMember();
            await this.ReadAsync();
            plain.Counter = Counter.LastValue;
            plain.Counter = Counter.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.GH.PKTu.ix_56.ComplexMember plain)
        {
            Counter.Cyclic = plain.Counter;
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

    public partial class Base : Ix.Connector.ITwinObject
    {
        public OnlinerString baseMember { get; }

        public GH.PKTu.ix_56.ComplexMember baseComplexMember { get; }

        public GH.PKTu.ix_56.DavidBase BaseDavid { get; }

        public Base(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            baseMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "baseMember", "baseMember");
            baseComplexMember = new GH.PKTu.ix_56.ComplexMember(this, "baseComplexMember", "baseComplexMember");
            BaseDavid = new GH.PKTu.ix_56.DavidBase(this, "BaseDavid", "BaseDavid");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.GH.PKTu.ix_56.Base> OnlineToPlain()
        {
            Pocos.GH.PKTu.ix_56.Base plain = new Pocos.GH.PKTu.ix_56.Base();
            await this.ReadAsync();
            plain.baseMember = baseMember.LastValue;
            plain.baseComplexMember = await baseComplexMember.OnlineToPlain();
            plain.BaseDavid = await BaseDavid.OnlineToPlain();
            plain.baseMember = baseMember.LastValue;
            plain.baseComplexMember = await baseComplexMember.OnlineToPlain();
            plain.BaseDavid = await BaseDavid.OnlineToPlain();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.GH.PKTu.ix_56.Base plain)
        {
            baseMember.Cyclic = plain.baseMember;
            await this.baseComplexMember.PlainToOnline(plain.baseComplexMember);
            await this.BaseDavid.PlainToOnline(plain.BaseDavid);
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

    public partial class FirstInheritance : Base
    {
        public OnlinerString FirstInheritanceMember { get; }

        public GH.PKTu.ix_56.ComplexMember FirstInheritanceComplexMember { get; }

        public GH.PKTu.ix_56.DavidBase FirstDavid { get; }

        public FirstInheritance(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            FirstInheritanceMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "FirstInheritanceMember", "FirstInheritanceMember");
            FirstInheritanceComplexMember = new GH.PKTu.ix_56.ComplexMember(this, "FirstInheritanceComplexMember", "FirstInheritanceComplexMember");
            FirstDavid = new GH.PKTu.ix_56.DavidBase(this, "FirstDavid", "FirstDavid");
        }

        public async Task<Pocos.GH.PKTu.ix_56.FirstInheritance> OnlineToPlain()
        {
            Pocos.GH.PKTu.ix_56.FirstInheritance plain = new Pocos.GH.PKTu.ix_56.FirstInheritance();
            await this.ReadAsync();
            plain.FirstInheritanceMember = FirstInheritanceMember.LastValue;
            plain.FirstInheritanceComplexMember = await FirstInheritanceComplexMember.OnlineToPlain();
            plain.FirstDavid = await FirstDavid.OnlineToPlain();
            plain = (Pocos.GH.PKTu.ix_56.FirstInheritance)await base.OnlineToPlain();
            plain.FirstInheritanceMember = FirstInheritanceMember.LastValue;
            plain.FirstInheritanceComplexMember = await FirstInheritanceComplexMember.OnlineToPlain();
            plain.FirstDavid = await FirstDavid.OnlineToPlain();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.GH.PKTu.ix_56.FirstInheritance plain)
        {
            await base.PlainToOnline(plain);
            FirstInheritanceMember.Cyclic = plain.FirstInheritanceMember;
            await this.FirstInheritanceComplexMember.PlainToOnline(plain.FirstInheritanceComplexMember);
            await this.FirstDavid.PlainToOnline(plain.FirstDavid);
            return await this.WriteAsync();
        }
    }

    public partial class SecondInheritance : FirstInheritance
    {
        public OnlinerString SecondInheritanceMember { get; }

        public GH.PKTu.ix_56.ComplexMember SecondInheritanceComplexMember { get; }

        public GH.PKTu.ix_56.DavidBase SecodnDavid { get; }

        public SecondInheritance(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            SecondInheritanceMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "SecondInheritanceMember", "SecondInheritanceMember");
            SecondInheritanceComplexMember = new GH.PKTu.ix_56.ComplexMember(this, "SecondInheritanceComplexMember", "SecondInheritanceComplexMember");
            SecodnDavid = new GH.PKTu.ix_56.DavidBase(this, "SecodnDavid", "SecodnDavid");
        }

        public async Task<Pocos.GH.PKTu.ix_56.SecondInheritance> OnlineToPlain()
        {
            Pocos.GH.PKTu.ix_56.SecondInheritance plain = new Pocos.GH.PKTu.ix_56.SecondInheritance();
            await this.ReadAsync();
            plain.SecondInheritanceMember = SecondInheritanceMember.LastValue;
            plain.SecondInheritanceComplexMember = await SecondInheritanceComplexMember.OnlineToPlain();
            plain.SecodnDavid = await SecodnDavid.OnlineToPlain();
            plain = (Pocos.GH.PKTu.ix_56.SecondInheritance)await base.OnlineToPlain();
            plain.SecondInheritanceMember = SecondInheritanceMember.LastValue;
            plain.SecondInheritanceComplexMember = await SecondInheritanceComplexMember.OnlineToPlain();
            plain.SecodnDavid = await SecodnDavid.OnlineToPlain();
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.GH.PKTu.ix_56.SecondInheritance plain)
        {
            await base.PlainToOnline(plain);
            SecondInheritanceMember.Cyclic = plain.SecondInheritanceMember;
            await this.SecondInheritanceComplexMember.PlainToOnline(plain.SecondInheritanceComplexMember);
            await this.SecodnDavid.PlainToOnline(plain.SecodnDavid);
            return await this.WriteAsync();
        }
    }

    public partial class PedroBase : Ix.Connector.ITwinObject
    {
        public OnlinerString p { get; }

        public PedroBase(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            p = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "p", "p");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.GH.PKTu.ix_56.PedroBase> OnlineToPlain()
        {
            Pocos.GH.PKTu.ix_56.PedroBase plain = new Pocos.GH.PKTu.ix_56.PedroBase();
            await this.ReadAsync();
            plain.p = p.LastValue;
            plain.p = p.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.GH.PKTu.ix_56.PedroBase plain)
        {
            p.Cyclic = plain.p;
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

    public partial class DavidBase : PedroBase
    {
        public OnlinerString d { get; }

        public DavidBase(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            d = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "d", "d");
        }

        public async Task<Pocos.GH.PKTu.ix_56.DavidBase> OnlineToPlain()
        {
            Pocos.GH.PKTu.ix_56.DavidBase plain = new Pocos.GH.PKTu.ix_56.DavidBase();
            await this.ReadAsync();
            plain.d = d.LastValue;
            plain = (Pocos.GH.PKTu.ix_56.DavidBase)await base.OnlineToPlain();
            plain.d = d.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnline(Pocos.GH.PKTu.ix_56.DavidBase plain)
        {
            await base.PlainToOnline(plain);
            d.Cyclic = plain.d;
            return await this.WriteAsync();
        }
    }
}