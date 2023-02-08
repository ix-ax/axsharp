using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class integratedTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public MonsterData.Monster Monster { get; }

    public integratedTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Monster = new MonsterData.Monster(this.Connector, "", "Monster");
    }

    public integratedTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Monster = new MonsterData.Monster(this.Connector, "", "Monster");
    }
}