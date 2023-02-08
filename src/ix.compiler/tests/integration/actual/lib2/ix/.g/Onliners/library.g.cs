using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace lib2
{
    public partial class MyClass : Ix.Connector.ITwinObject
    {
        public OnlinerString MyString { get; }

        public OnlinerInt MyInt { get; }

        public MyClass(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            MyString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "MyString", "MyString");
            MyInt = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "MyInt", "MyInt");
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public async Task<Pocos.lib2.MyClass> OnlineToPlainAsync()
        {
            Pocos.lib2.MyClass plain = new Pocos.lib2.MyClass();
            await this.ReadAsync();
            plain.MyString = MyString.LastValue;
            plain.MyInt = MyInt.LastValue;
            return plain;
        }

        protected async Task<Pocos.lib2.MyClass> OnlineToPlainAsync(Pocos.lib2.MyClass plain)
        {
            plain.MyString = MyString.LastValue;
            plain.MyInt = MyInt.LastValue;
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.lib2.MyClass plain)
        {
            MyString.Cyclic = plain.MyString;
            MyInt.Cyclic = plain.MyInt;
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