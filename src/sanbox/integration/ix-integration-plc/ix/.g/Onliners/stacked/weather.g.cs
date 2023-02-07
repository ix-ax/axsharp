using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace Layouts.Stacked
{
    [Container(Layout.Stack)]
    [Group(GroupLayout.GroupBox)]
    public partial class weather : weatherBase
    {
        public weather(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        }

        public Pocos.Layouts.Stacked.weather OnlineToPlain()
        {
            Pocos.Layouts.Stacked.weather plain = new Pocos.Layouts.Stacked.weather();
            plain = (Pocos.Layouts.Stacked.weather)base.OnlineToPlain();
            return plain;
        }

        public void PlainToOnline(Pocos.Layouts.Stacked.weather plain)
        {
            base.PlainToOnline(plain);
        }
    }
}