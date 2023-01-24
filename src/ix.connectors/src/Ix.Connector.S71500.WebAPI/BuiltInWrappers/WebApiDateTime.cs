// Ix.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Connector.ValueTypes;

namespace Ix.Connector.S71500.WebApi;

/// <inheritdoc />
public class WebApiDateTime : OnlinerDateTime, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiDateTime()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiDateTime(ITwinObject parent,
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
        WebApiConnector.CreateWriteRequest(Symbol, GetFromDate(CyclicToWrite), _webApiConnector.DBName);

    /// <inheritdoc />
    public void Read(string value)
    {
        DateTime dt;
        if (DateTime.TryParse(value, out dt))
            UpdateRead(dt);
    }

    /// <inheritdoc />
    public override async Task<DateTime> GetAsync()
    {
        var dt = await _webApiConnector.ReadAsync<long>(this) / 100;
        return DateTime.FromBinary(dt).AddYears(1969);
    }


    private string GetFromDate(DateTime dateTime)
    {
        if (dateTime <= MinValue)
            dateTime = MinValue;

        var retval = dateTime - MinValue;
        return (new DateTime().AddMilliseconds(retval.TotalMilliseconds).ToBinary() * 100).ToString();
    }

    /// <inheritdoc />
    public override async Task<DateTime> SetAsync(DateTime value)
    {
        await _webApiConnector.WriteAsync(this, GetFromDate(value));
        return value;
    }
}