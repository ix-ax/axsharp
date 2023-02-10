using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace Simatic.Ax.StateFramework
{
    public partial class State1Transition : AbstractState
    {
        public State1Transition(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> OnlineToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.State1Transition plain = new Pocos.Simatic.Ax.StateFramework.State1Transition();
            await this.ReadAsync();
            await base.OnlineToPlainAsync(plain);
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> OnlineToPlainAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
            await base.OnlineToPlainAsync(plain);
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
            await base.PlainToOnlineAsync(plain);
            return await this.WriteAsync();
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> ShadowToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.State1Transition plain = new Pocos.Simatic.Ax.StateFramework.State1Transition();
            await base.ShadowToPlainAsync(plain);
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> ShadowToPlainAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
            await base.ShadowToPlainAsync(plain);
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
            await base.PlainToShadowAsync(plain);
            return this.RetrievePrimitives();
        }
    }
}

namespace Simatic.Ax.StateFramework
{
    public partial class AbstractState : Ix.Connector.ITwinObject, IState, IStateMuteable
    {
        public OnlinerInt StateID { get; }

        public OnlinerString StateName { get; }

        public AbstractState(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            StateID = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "StateID", "StateID");
            StateName = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "StateName", "StateName");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> OnlineToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.AbstractState plain = new Pocos.Simatic.Ax.StateFramework.AbstractState();
            await this.ReadAsync();
            plain.StateID = StateID.LastValue;
            plain.StateName = StateName.LastValue;
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> OnlineToPlainAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
            plain.StateID = StateID.LastValue;
            plain.StateName = StateName.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
            StateID.Cyclic = plain.StateID;
            StateName.Cyclic = plain.StateName;
            return await this.WriteAsync();
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> ShadowToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.AbstractState plain = new Pocos.Simatic.Ax.StateFramework.AbstractState();
            plain.StateID = StateID.Shadow;
            plain.StateName = StateName.Shadow;
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> ShadowToPlainAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
            plain.StateID = StateID.Shadow;
            plain.StateName = StateName.Shadow;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
            StateID.Shadow = plain.StateID;
            StateName.Shadow = plain.StateName;
            return this.RetrievePrimitives();
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