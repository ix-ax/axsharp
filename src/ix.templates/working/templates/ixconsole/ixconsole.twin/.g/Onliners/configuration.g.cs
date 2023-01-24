using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ixconsoleTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public OnlinerULInt Counter { get; }

    public OnlinerString ConterValueInWords { get; }

    public ixconsoleTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Counter = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "Counter");
        ConterValueInWords = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "ConterValueInWords");
    }

    public ixconsoleTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Counter = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "Counter");
        ConterValueInWords = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "ConterValueInWords");
    }
}