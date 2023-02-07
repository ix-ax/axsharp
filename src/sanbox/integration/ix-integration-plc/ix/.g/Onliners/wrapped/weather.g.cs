using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace Layouts.Wrapped
{
    [Container(Layout.Wrap)]
    [Group(GroupLayout.GroupBox)]
    public partial class weather : weatherBase
    {
        public weather(Ix.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail + ".$base")
        {
            Symbol = Ix.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        }
    }
}