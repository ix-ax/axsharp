// Ix.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Connector.ValueTypes;

namespace Ix.Connector.S71500.WebApi;

/// <inheritdoc />
public class WebApiByte : OnlinerByte, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiByte(ITwinObject parent,
        string readableTail,
        string symbolTail)
        : base(parent,
            readableTail,
            symbolTail)
    {
        _webApiConnector = WebApiConnector.Cast(parent.GetConnector());
    }

    /// <inheritdoc />
    public WebApiByte()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    ApiPlcReadRequest IWebApiPrimitive.PlcReadRequestData =>
        WebApiConnector.CreateReadRequest(Symbol, _webApiConnector.DBName);

    /// <inheritdoc />
    ApiPlcWriteRequest IWebApiPrimitive.PlcWriteRequestData =>
        WebApiConnector.CreateWriteRequest(Symbol, CyclicToWrite, _webApiConnector.DBName);

    /// <inheritdoc />
    public void Read(string result)
    {
        UpdateRead(byte.Parse(result));
    }

    /// <inheritdoc />
    public override async Task<byte> GetAsync()
    {
        return await _webApiConnector.ReadAsync(this, LastValue);
    }

    /// <inheritdoc />
    public override async Task<byte> SetAsync(byte value)
    {
        return await _webApiConnector.WriteAsync(this, value);
    }
}