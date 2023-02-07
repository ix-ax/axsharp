using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ixblazorTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public Counters counters { get; }

    public ixblazorTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        counters = new Counters(this.Connector, "", "counters");
    }

    public ixblazorTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        counters = new Counters(this.Connector, "", "counters");
    }
}