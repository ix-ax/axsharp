using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ixcomponent : Ix.Connector.ITwinObject
{
    public OnlinerInt my_int { get; }

    public OnlinerString my_string { get; }

    public OnlinerBool my_bool { get; }

    public ixcomponent(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        my_int = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "My integer", "my_int");
        my_int.AttributeName = "My integer";
        my_string = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "My string", "my_string");
        my_string.AttributeName = "My string";
        my_bool = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "My bool", "my_bool");
        my_bool.AttributeName = "My bool";
        parent.AddChild(this);
        parent.AddKid(this);
    }

    public Pocos.ixcomponent OnlineToPlain()
    {
        Pocos.ixcomponent plain = new Pocos.ixcomponent();
        plain.my_int = my_int.LastValue;
        plain.my_string = my_string.LastValue;
        plain.my_bool = my_bool.LastValue;
        return plain;
    }

    public void PlainToOnline(Pocos.ixcomponent plain)
    {
        my_int.Cyclic = plain.my_int;
        my_string.Cyclic = plain.my_string;
        my_bool.Cyclic = plain.my_bool;
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

namespace MySecondNamespace
{
    public partial class ixcomponent : Ix.Connector.ITwinObject
    {
        public OnlinerInt my_int { get; }

        public OnlinerString my_string { get; }

        public OnlinerBool my_bool { get; }

        public ixcomponent(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            my_int = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "My integer", "my_int");
            my_int.AttributeName = "My integer";
            my_string = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "My string", "my_string");
            my_string.AttributeName = "My string";
            my_bool = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "My bool", "my_bool");
            my_bool.AttributeName = "My bool";
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public Pocos.MySecondNamespace.ixcomponent OnlineToPlain()
        {
            Pocos.MySecondNamespace.ixcomponent plain = new Pocos.MySecondNamespace.ixcomponent();
            plain.my_int = my_int.LastValue;
            plain.my_string = my_string.LastValue;
            plain.my_bool = my_bool.LastValue;
            return plain;
        }

        public void PlainToOnline(Pocos.MySecondNamespace.ixcomponent plain)
        {
            my_int.Cyclic = plain.my_int;
            my_string.Cyclic = plain.my_string;
            my_bool.Cyclic = plain.my_bool;
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

namespace ThirdNamespace
{
    public partial class ixcomponent : Ix.Connector.ITwinObject
    {
        public OnlinerInt my_int { get; }

        public OnlinerString my_string { get; }

        public OnlinerBool my_bool { get; }

        public ixcomponent(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = Ix.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            my_int = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "My integer", "my_int");
            my_int.AttributeName = "My integer";
            my_string = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "My string", "my_string");
            my_string.AttributeName = "My string";
            my_bool = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "My bool", "my_bool");
            my_bool.AttributeName = "My bool";
            parent.AddChild(this);
            parent.AddKid(this);
        }

        public Pocos.ThirdNamespace.ixcomponent OnlineToPlain()
        {
            Pocos.ThirdNamespace.ixcomponent plain = new Pocos.ThirdNamespace.ixcomponent();
            plain.my_int = my_int.LastValue;
            plain.my_string = my_string.LastValue;
            plain.my_bool = my_bool.LastValue;
            return plain;
        }

        public void PlainToOnline(Pocos.ThirdNamespace.ixcomponent plain)
        {
            my_int.Cyclic = plain.my_int;
            my_string.Cyclic = plain.my_string;
            my_bool.Cyclic = plain.my_bool;
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