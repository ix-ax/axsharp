using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ax_blazor_exampleTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public testingProgram testingProgram { get; }

    public prgWeatherStations prgWeatherStations { get; }

    public MAIN MAINC { get; }

    public ax_blazor_exampleTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        testingProgram = new testingProgram(this.Connector, "", "testingProgram");
        prgWeatherStations = new prgWeatherStations(this.Connector, "", "prgWeatherStations");
        MAINC = new MAIN(this.Connector, "", "MAINC");
    }

    public ax_blazor_exampleTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        testingProgram = new testingProgram(this.Connector, "", "testingProgram");
        prgWeatherStations = new prgWeatherStations(this.Connector, "", "prgWeatherStations");
        MAINC = new MAIN(this.Connector, "", "MAINC");
    }
}