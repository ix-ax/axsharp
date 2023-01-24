using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ixblazorTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public counter_oop oop_counter { get; }

    public ixblazorTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        oop_counter = new counter_oop(this.Connector, "", "oop_counter");
    }

    public ixblazorTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        oop_counter = new counter_oop(this.Connector, "", "oop_counter");
    }
}