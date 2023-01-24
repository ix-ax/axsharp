using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class hello_world_console_plcTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public OnlinerULInt Counter { get; }

    public OnlinerString HelloWorld { get; }

    public hello_world_console_plcTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Counter = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "Counter");
        HelloWorld = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "HelloWorld");
    }

    public hello_world_console_plcTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Counter = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "Counter");
        HelloWorld = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "HelloWorld");
    }
}