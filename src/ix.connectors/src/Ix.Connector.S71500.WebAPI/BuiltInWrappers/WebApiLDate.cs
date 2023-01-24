// Ix.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Connector.ValueTypes;

namespace Ix.Connector.S71500.WebApi;

/// <inheritdoc />
public class WebApiLDate : OnlinerDate, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiLDate()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiLDate(ITwinObject parent,
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

    public void Read(string result)
    {
        DateOnly dt;
        if (DateOnly.TryParse(result, out dt))
            UpdateRead(dt);
    }

    /// <inheritdoc />
    public override async Task<DateOnly> GetAsync()
    {
        var dt = await _webApiConnector.ReadAsync<long>(this) / 100;
        return DateOnly.FromDateTime(DateTime.FromBinary(dt).AddYears(1969));
    }


    private string GetFromDate(DateOnly date)
    {
        if (date <= MinValue)
            date = MinValue;

        var retval = date.ToDateTime(TimeOnly.MinValue) - MinValue.ToDateTime(TimeOnly.MinValue);
        return (new DateTime().AddDays(retval.TotalDays).ToBinary() * 100).ToString();
    }

    /// <inheritdoc />
    public override async Task<DateOnly> SetAsync(DateOnly value)
    {
        await _webApiConnector.WriteAsync(this, GetFromDate(value));
        return value;
    }
}