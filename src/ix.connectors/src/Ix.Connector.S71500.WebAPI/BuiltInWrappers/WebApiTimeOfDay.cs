// Ix.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Globalization;
using Ix.Connector.ValueTypes;

namespace Ix.Connector.S71500.WebApi;

/// <inheritdoc />
public class WebApiTimeOfDay : OnlinerTimeOfDay, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiTimeOfDay()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiTimeOfDay(ITwinObject parent,
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
    ApiPlcWriteRequest IWebApiPrimitive.PlcWriteRequestData => WebApiConnector.CreateWriteRequest(Symbol,
        (CyclicToWrite.TotalMilliseconds * 1000000).ToString(CultureInfo.InvariantCulture), _webApiConnector.DBName);

    /// <inheritdoc />
    public void Read(string result)
    {
        UpdateRead(TimeSpan.FromMilliseconds(long.Parse(result) / 1000000));
    }

    /// <inheritdoc />
    public override async Task<TimeSpan> GetAsync()
    {
        return TimeSpan.FromMilliseconds(long.Parse(await _webApiConnector.ReadAsync<string>(this)) / 1000000);
    }

    /// <inheritdoc />
    public override async Task<TimeSpan> SetAsync(TimeSpan value)
    {
        await _webApiConnector.WriteAsync(this, ((long)value.TotalMilliseconds * 1000000).ToString());
        return value;
    }
}