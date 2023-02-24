# Dummy

### [API](../../api/Ix.Connector/Ix.Connector/DummyConnector.md)

Provides the possibility to work with PLC twins without target system.

~~~C#
        public static <PLC_TWIN_TYPE> Plc { get; } = new(ConnectorAdapterBuilder.Build().CreateDummy());
~~~