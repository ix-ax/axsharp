using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace Layouts.Tabbed
{
    [Container(Layout.Tabs)]
    [Group(GroupLayout.GroupBox)]
    public partial class weather : weatherBase
    {
        public weather(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        }

        public Pocos.Layouts.Tabbed.weather OnlineToPlain()
        {
            Pocos.Layouts.Tabbed.weather plain = new Pocos.Layouts.Tabbed.weather();
            plain = (Pocos.Layouts.Tabbed.weather)base.OnlineToPlain();
            return plain;
        }

        public void PlainToOnline(Pocos.Layouts.Tabbed.weather plain)
        {
            base.PlainToOnline(plain);
        }
    }
}