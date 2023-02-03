using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace MeasurementExample
{
    [Container(Layout.Wrap)]
    [Group(GroupLayout.GroupBox)]
    public partial class Measurement : Ix.Connector.ITwinObject
    {
        public OnlinerReal Min { get; }

        [ReadOnly()]
        public OnlinerReal Acquired { get; }

        public OnlinerReal Max { get; }

        [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(MeasurementExample.Result))]
        public OnlinerInt Result { get; }

        public Measurement(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Min = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Minimum", "Min");
            Min.AttributeName = "Minimum";
            Acquired = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Measured", "Acquired");
            Acquired.AttributeName = "Measured";
            Acquired.MakeReadOnly();
            Max = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "Maximum", "Max");
            Max.AttributeName = "Maximum";
            Result = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Measurement Result", "Result");
            Result.AttributeName = "Measurement Result";
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

    public enum Result : Int16
    {
        None = 0,
        Passed = 10,
        Failed = 20
    }

    public partial class Measurements : Ix.Connector.ITwinObject
    {
        [Container(Layout.Stack)]
        [Group(GroupLayout.GroupBox)]
        public MeasurementExample.Measurement measurement_stack { get; }

        [Container(Layout.Wrap)]
        [Group(GroupLayout.GroupBox)]
        public MeasurementExample.Measurement measurement_wrap { get; }

        [Container(Layout.UniformGrid)]
        [Group(GroupLayout.GroupBox)]
        public MeasurementExample.Measurement measurement_grid { get; }

        [Container(Layout.Tabs)]
        [Group(GroupLayout.GroupBox)]
        public MeasurementExample.Measurement measurement_tabs { get; }

        public Measurements(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            measurement_stack = new MeasurementExample.Measurement(this, "Stack panel", "measurement_stack");
            measurement_stack.AttributeName = "Stack panel";
            measurement_wrap = new MeasurementExample.Measurement(this, "Wrap panel", "measurement_wrap");
            measurement_wrap.AttributeName = "Wrap panel";
            measurement_grid = new MeasurementExample.Measurement(this, "Grid", "measurement_grid");
            measurement_grid.AttributeName = "Grid";
            measurement_tabs = new MeasurementExample.Measurement(this, "Tabs", "measurement_tabs");
            measurement_tabs.AttributeName = "Tabs";
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