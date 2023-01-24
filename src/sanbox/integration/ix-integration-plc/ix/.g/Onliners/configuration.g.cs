using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ix_integration_plcTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public all_primitives all_primitives { get; }

    public weather weather { get; }

    public weathers weathers { get; }

    public Layouts.Stacked.weather weather_stacked { get; }

    public Layouts.Wrapped.weather weather_wrapped { get; }

    public Layouts.Tabbed.weather weather_tabbed { get; }

    [ReadOnce()]
    public Layouts.Stacked.weather weather_readOnce { get; }

    [ReadOnly()]
    public Layouts.Stacked.weather weather_readOnly { get; }

    public ix_integration_plcTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        all_primitives = new all_primitives(this.Connector, "", "all_primitives");
        weather = new weather(this.Connector, "", "weather");
        weathers = new weathers(this.Connector, "", "weathers");
        weather_stacked = new Layouts.Stacked.weather(this.Connector, "", "weather_stacked");
        weather_stacked.AttributeName = "Weather in a stack pannel and grouped in group box";
        weather_wrapped = new Layouts.Wrapped.weather(this.Connector, "", "weather_wrapped");
        weather_wrapped.AttributeName = "Weather in a wrap pannel and grouped in group box";
        weather_tabbed = new Layouts.Tabbed.weather(this.Connector, "", "weather_tabbed");
        weather_tabbed.AttributeName = "Weather in a tabs and grouped in group box";
        weather_readOnce = new Layouts.Stacked.weather(this.Connector, "", "weather_readOnce");
        weather_readOnce.AttributeName = "Weather structure set to read once";
        weather_readOnce.MakeReadOnce();
        weather_readOnly = new Layouts.Stacked.weather(this.Connector, "", "weather_readOnly");
        weather_readOnly.AttributeName = "Weather structure set to read only";
        weather_readOnly.MakeReadOnly();
    }

    public ix_integration_plcTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        all_primitives = new all_primitives(this.Connector, "", "all_primitives");
        weather = new weather(this.Connector, "", "weather");
        weathers = new weathers(this.Connector, "", "weathers");
        weather_stacked = new Layouts.Stacked.weather(this.Connector, "", "weather_stacked");
        weather_stacked.AttributeName = "Weather in a stack pannel and grouped in group box";
        weather_wrapped = new Layouts.Wrapped.weather(this.Connector, "", "weather_wrapped");
        weather_wrapped.AttributeName = "Weather in a wrap pannel and grouped in group box";
        weather_tabbed = new Layouts.Tabbed.weather(this.Connector, "", "weather_tabbed");
        weather_tabbed.AttributeName = "Weather in a tabs and grouped in group box";
        weather_readOnce = new Layouts.Stacked.weather(this.Connector, "", "weather_readOnce");
        weather_readOnce.AttributeName = "Weather structure set to read once";
        weather_readOnce.MakeReadOnce();
        weather_readOnly = new Layouts.Stacked.weather(this.Connector, "", "weather_readOnly");
        weather_readOnly.AttributeName = "Weather structure set to read only";
        weather_readOnly.MakeReadOnly();
    }
}