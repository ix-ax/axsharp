// Ix.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Connector.ValueTypes;

namespace Ix.Connector.S71500.WebApi;

/// <inheritdoc />
public class WebApiInt : OnlinerInt, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiInt()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiInt(ITwinObject parent,
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
        WebApiConnector.CreateWriteRequest(Symbol, CyclicToWrite, _webApiConnector.DBName);

    /// <inheritdoc />
    public void Read(string value)
    {
        UpdateRead(short.Parse(value));
    }

    /// <inheritdoc />
    public override async Task<short> GetAsync()
    {
        return await _webApiConnector.ReadAsync(this, LastValue);
    }

    /// <inheritdoc />
    public override async Task<short> SetAsync(short value)
    {
        return await _webApiConnector.WriteAsync(this, value);
    }
}