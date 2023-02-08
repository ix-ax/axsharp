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

        public async Task<Pocos.Layouts.Tabbed.weather> OnlineToPlainAsync()
        {
            Pocos.Layouts.Tabbed.weather plain = new Pocos.Layouts.Tabbed.weather();
            await this.ReadAsync();
            await base.OnlineToPlainAsync(plain);
            return plain;
        }

        protected async Task<Pocos.Layouts.Tabbed.weather> OnlineToPlainAsync(Pocos.Layouts.Tabbed.weather plain)
        {
            await base.OnlineToPlainAsync(plain);
            return plain;
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Layouts.Tabbed.weather plain)
        {
            await base.PlainToOnlineAsync(plain);
            return await this.WriteAsync();
        }
    }
}