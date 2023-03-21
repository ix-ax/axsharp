using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
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

    public partial class using_type_named_values : Ix.Connector.ITwinObject
    {
        [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(Simatic.Ax.StateFramework.StateControllerStatus))]
        public OnlinerWord LColors { get; }

        partial void PreConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public using_type_named_values(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            LColors = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this, "LColors", "LColors");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public T OnlineToPlain<T>()
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

        public void PlainToOnline<T>(T plain)
        {
            this.PlainToOnlineAsync((dynamic)plain).Wait();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Simatic.Ax.StateFramework.using_type_named_values plain)
        {
            LColors.Cyclic = plain.LColors;
            return await this.WriteAsync();
        }

        public T ShadowToPlain<T>()
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

        public void PlainToShadow<T>(T plain)
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