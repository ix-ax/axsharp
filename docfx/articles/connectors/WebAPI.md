# WebAPI Connector

## [API](~/api/Ix.Connector.S71500.WebAPI.yml)

Provides connectivity to S7-15XX PLC systems using WebAPI interface.

~~~C#
public static <PLC_TWIN_TYPE> Plc { get; } = new (ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AXTARGET") ?? "10.10.101.1", "Everybody", "", true));
~~~