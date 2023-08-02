# S71500 WebAPI Connector Documentation

## [API](~/api/AXSharp.Connector.S71500.WebAPI.yml)

This connector offers an interface to interact with S7-15XX PLC systems through the WebAPI.

Here is an example of how to create an instance of this connector:

```C#
public static <PLC_TWIN_TYPE> Plc { get; } = new (ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AXTARGET") ?? "10.10.101.1", "Everybody", "", true));
```

## Considerations for WebAPI Connector Performance

When working with S7-1500 based PLC systems, especially hardware-based ones, there are limitations regarding the volume of items that can be communicated over a specific communication interface. To prevent performance degradation, it's vital to prudently manage the number of PLC items your application uses at any given time. It is recommended to utilize polling instead of an automatic variable subscription.

Here is an example on how to set polling subscription mode:

```C#
Entry.Plc.Connector.SubscriptionMode = AXSharp.Connector.ReadSubscriptionMode.Polling;
```

For more details about polling within your components, see [Polling Data from the Controller in Components](../blazor/LIBRARIES.md#polling-data-from-the-plc) and [General Polling](README.md#polling).

### Monitoring and Logging 

The WebAPI connector comes with a built-in logging feature to provide insights into communication performance. You can use any Serilog logger configuration to capture the logs from the connector. The default logging level is `Information`, which provides basic details about booting, possible communication errors, and warnings. If you need more granular data to debug performance issues, consider setting the logging level to `Debug`. For even more comprehensive information, use the `Verbose` level.

Below is an example of how to set a custom logger for the connector:

```C#
// This configuration logs to both console and `connector.log` file with `Debug` logging level.
Entry.Plc.Connector.SetLoggerConfiguration(new LoggerConfiguration()
    .WriteTo
    .Console()
    .WriteTo
    .File($"connector.log",
        outputTemplate: "{Timestamp:yyyy-MMM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
        fileSizeLimitBytes: 100000)
    .MinimumLevel.Debug()
    .CreateLogger());
```
