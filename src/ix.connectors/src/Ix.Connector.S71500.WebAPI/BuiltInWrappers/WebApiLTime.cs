// Ix.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Connector.ValueTypes;

namespace Ix.Connector.S71500.WebApi;

/// <inheritdoc />
public class WebApiLTime : OnlinerLTime, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiLTime()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiLTime(ITwinObject parent,
        string readableTail,
        string symbolTail)
        : base(parent,
            readableTail,
            symbolTail)
    {
        _webApiConnector = WebApiConnector.Cast(parent.GetConnector());
    }

    /// <inheritdoc />
    ApiPlcReadRequest IWebApiPrimitive.PlcReadRequestData =>
        WebApiConnector.CreateReadRequest(Symbol, _webApiConnector.DBName);

    /// <inheritdoc />
    ApiPlcWriteRequest IWebApiPrimitive.PlcWriteRequestData =>
        WebApiConnector.CreateWriteRequest(Symbol, CyclicToWrite.Ticks.ToString(),
            _webApiConnector
                .DBName); // TODO: review this casting to string... reason: there is some problem while creating request from long.

    /// <inheritdoc />
    public void Read(string value)
    {
        UpdateRead(TimeSpan.FromMilliseconds(long.Parse(value) / 1000000));
    }

    /// <inheritdoc />
    public override async Task<TimeSpan> GetAsync()
    {
        return TimeSpan.FromTicks(long.Parse(await _webApiConnector.ReadAsync<string>(this)));
    }

    /// <inheritdoc />
    public override async Task<TimeSpan> SetAsync(TimeSpan value)
    {
        await _webApiConnector.WriteAsync(this, value.Ticks.ToString());
        return value;
    }
}