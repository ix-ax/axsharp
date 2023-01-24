using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

namespace ax_blazor_1
{
    public partial class ax_blazor_1 : ITwinController
    {
        public Ix.Connector.Connector Connector { get; }

        public OnlinerInt myINT { get; }

        public weather weather { get; }

        public weathers weathers { get; }

        public ax_blazor_1(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
        {
            this.Connector = adapter.GetConnector(parameters);
            myINT = new OnlinerInt(this.Connector, "", "myINT");
            weather = new weather(this.Connector, "", "weather");
            weathers = new weathers(this.Connector, "", "weathers");
        }
    }
}