using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

namespace Simatic.Ax.StateFramework
{
    public enum StateControllerStatus : UInt16
    {
        STATUS_NO_ERR = 28672,
        STATUS_IS_RUNNING = 28673,
        STATUS_STATE_CHANGED = 28674,
        STATUS_NO_INITIALSTATE = 33024,
        STATUS_NO_NEXTSTATE = 33025
    }

    public partial class using_type_named_values : AXSharp.Connector.ITwinObject
    {
        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Simatic.Ax.StateFramework.StateControllerStatus))]
        public OnlinerWord LColors { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public using_type_named_values(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            LColors = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this, "LColors", "LColors");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public virtual T OnlineToPlain<T>()
        {
            return (dynamic)this.OnlineToPlainAsync().Result;
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.using_type_named_values> OnlineToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.using_type_named_values plain = new Pocos.Simatic.Ax.StateFramework.using_type_named_values();
            await this.ReadAsync();
            plain.LColors = LColors.LastValue;
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.using_type_named_values> OnlineToPlainAsync(Pocos.Simatic.Ax.StateFramework.using_type_named_values plain)
        {
            plain.LColors = LColors.LastValue;
            return plain;
        }

        public virtual void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Simatic.Ax.StateFramework.using_type_named_values plain)
        {
            LColors.Cyclic = plain.LColors;
            return await this.WriteAsync();
        }

        public virtual T ShadowToPlain<T>()
        {
            return (dynamic)this.ShadowToPlainAsync().Result;
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.using_type_named_values> ShadowToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.using_type_named_values plain = new Pocos.Simatic.Ax.StateFramework.using_type_named_values();
            plain.LColors = LColors.Shadow;
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.using_type_named_values> ShadowToPlainAsync(Pocos.Simatic.Ax.StateFramework.using_type_named_values plain)
        {
            plain.LColors = LColors.Shadow;
            return plain;
        }

        public virtual void PlainToShadow<T>(T plain)
        {
            this.PlainToShadowAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Simatic.Ax.StateFramework.using_type_named_values plain)
        {
            LColors.Shadow = plain.LColors;
            return this.RetrievePrimitives();
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.Simatic.Ax.StateFramework.using_type_named_values CreateEmptyPoco()
        {
            return new Pocos.Simatic.Ax.StateFramework.using_type_named_values();
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