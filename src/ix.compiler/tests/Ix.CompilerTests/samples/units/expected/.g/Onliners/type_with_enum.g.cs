using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace Simatic.Ax.StateFramework
{
    public interface IGuard
    {
    }
}

namespace Simatic.Ax.StateFramework
{
    public enum Condition
    {
        GT,
        EQ,
        LT,
        NE,
        GE,
        LE
    }

    public partial class CompareGuardLint : Ix.Connector.ITwinObject, IGuard
    {
        public OnlinerLInt Value { get; }

        public OnlinerLInt CompareToValue { get; }

        [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(Simatic.Ax.StateFramework.Condition))]
        public OnlinerInt Condition { get; }

        public CompareGuardLint(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            CompareToValue = @Connector.Online.AdapterFactory.CreateLINT(this, "CompareToValue", "CompareToValue");
            Condition = @Connector.Online.AdapterFactory.CreateINT(this, "Condition", "Condition");
            parent.AddChild(this);
            parent.AddKid(this);
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