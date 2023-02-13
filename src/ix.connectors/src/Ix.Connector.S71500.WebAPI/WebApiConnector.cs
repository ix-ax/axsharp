// Ix.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using Siemens.Simatic.S7.Webserver.API.Exceptions;
using Siemens.Simatic.S7.Webserver.API.Models.Responses;
using Siemens.Simatic.S7.Webserver.API.Services;
using Siemens.Simatic.S7.Webserver.API.Services.IdGenerator;
using Siemens.Simatic.S7.Webserver.API.Services.RequestHandling;

namespace Ix.Connector.S71500.WebApi;

/// <summary>
/// Provides connector to mediate connection with IX twins over WebAPI connection.
/// </summary>
public class WebApiConnector : Connector
{
    private readonly ApiHttpClientRequestHandler _requestHandler;

    private volatile object _locker = new();


    /// <summary>
    /// Creates new instance of <see cref="WebApiConnector"/>.
    /// </summary>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    /// <param name="customServerCertHandler">Customized server certificate handler.</param>
    /// <param name="dbName">Root DB name (AX uses 'TGlobalVariablesDB')</param>
    public WebApiConnector(string ipAddress, string userName, string password,
        Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>? customServerCertHandler,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        IPAddress = ipAddress;
        DBName = dbName;

        var serviceFactory = new ApiStandardServiceFactory();
        var client = serviceFactory.GetHttpClient(ipAddress, userName, password);
        _requestHandler = new ApiHttpClientRequestHandler(client,
            new ApiRequestFactory(ReqIdGenerator, RequestParameterChecker), ApiResponseChecker);

        NumberOfInstances++;
    }

    /// <summary>
    /// Creates new instance of <see cref="WebApiConnector"/>.
    /// </summary>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    /// <param name="ignoreSSLErros">When set to 'true' the connection will ignore SSL errors.</param>
    /// <param name="dbName">Root DB name (AX uses 'TGlobalVariablesDB')</param>
    public WebApiConnector(string ipAddress, string userName, string password, bool ignoreSSLErros,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        IPAddress = ipAddress;
        DBName = dbName;

        if (ignoreSSLErros)
            ServerCertificateCallback.CertificateCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

        var serviceFactory = new ApiStandardServiceFactory();
        var client = serviceFactory.GetHttpClient(ipAddress, userName, password);
        _requestHandler = new ApiHttpClientRequestHandler(client,
            new ApiRequestFactory(ReqIdGenerator, RequestParameterChecker), ApiResponseChecker);

        NumberOfInstances++;
    }

    /// <inheritdoc />
#pragma warning disable CS8618
    internal WebApiConnector()
#pragma warning restore CS8618
    {
    }

    private GUIDGenerator ReqIdGenerator { get; } = new();
    private ApiRequestParameterChecker RequestParameterChecker { get; } = new();
    private ApiResponseChecker ApiResponseChecker { get; } = new();

    private ApiHttpClientRequestHandler RequestHandler
    {
        get
        {
            lock (_locker)
            {
                return _requestHandler;
            }
        }
    }

    /// <summary>
    ///     Gets number of instance of WebAPI connector in this application.
    /// </summary>
    public static int NumberOfInstances { get; private set; }

    /// <summary>
    ///     Get the address of the target system.
    /// </summary>
    private string IPAddress { get; }

    internal string DBName { get; }

    /// <inheritdoc />
    public override Connector BuildAndStart()
    {
        StartReadWriteOps();
        return this;
    }

    internal void HandleCommFailure(Exception exception, string description, IEnumerable<ITwinPrimitive> primitives,
        ApiBulkResponse response, IEnumerable<ApiRequestBase> originalRequest)
    {
        string firstFailedItemParams = string.Empty;

        foreach (var errorResponse in response.ErrorResponses)
        {
            var failedItem = originalRequest.FirstOrDefault(p => p.Id ==errorResponse.Id);
            var failedItemParams = string.Join(";", failedItem.Params.Select(p => $"{p.Key} : {p.Value}"));
            if(string.IsNullOrEmpty(firstFailedItemParams))
            {
                firstFailedItemParams = failedItemParams;
            }

            Logger.Error($"{failedItemParams} : {errorResponse.Error.Message} [{errorResponse.Error.Code}]");
        }

        foreach (var primitive in primitives)
        {
            primitive?.AccessStatus.Update(RwCycleCount, $"{description}: '{exception.Message}' [{firstFailedItemParams}] ");
        }

        switch (ExceptionBehaviour)
        {
            case CommExceptionBehaviour.Ignore:
                break;
            case CommExceptionBehaviour.ReThrow:
                throw exception;
        }
    }

    internal void HandleCommFailure<T>(Exception exception, string description, ITwinPrimitive primitive,
        ApiResultResponse<T> response)
    {
        primitive.AccessStatus.Update(RwCycleCount, $"{description}: '{exception.Message}'");

        var errorResponse = response as ApiErrorModel;
        if (errorResponse != null)
            Logger.Error($"{errorResponse.Error.Message} [{errorResponse.Error.Code}]");
        else
            Logger.Error(
                $"There was an error accessing item {primitive.Symbol}, but we were not able to determine the reason.");

        switch (ExceptionBehaviour)
        {
            case CommExceptionBehaviour.Ignore:
                break;
            case CommExceptionBehaviour.ReThrow:
                throw exception;
        }
    }

    /// <inheritdoc />
    public override async Task ReadBatchAsync(IEnumerable<ITwinPrimitive>? primitives)
    {
        if (primitives == null) return;
        var responseData = new ApiBulkResponse();

        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();
        if (!twinPrimitives.Any()) return;
        var webApiPrimitives = twinPrimitives.Cast<IWebApiPrimitive>().ToArray();
        var request = webApiPrimitives.Select(p => p.PlcReadRequestData).ToList();

        try
        {
            responseData = await RequestHandler.ApiBulkAsync(request);
            var position = 0;
            webApiPrimitives.ToList()
                .ForEach(p =>
                {
                    p.Read(responseData.SuccessfulResponses.ElementAt(position++).Result.ToString());
                    p.AccessStatus.Update(RwCycleCount);
                });
        }
        catch (ApiBulkRequestException apiException)
        {
            HandleCommFailure(apiException, "Batch read failed.", primitives, apiException.BulkResponse, request);
        }
        catch (Exception e)
        {
            HandleCommFailure(e, "Batch read failed.", primitives, responseData, request);
        }
    }

    /// <inheritdoc />
    public override async Task WriteBatchAsync(IEnumerable<ITwinPrimitive>? primitives)
    {
        if (primitives == null) return;
        var responseData = new ApiBulkResponse();
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();
        if (!twinPrimitives.Any()) return;
        var webApiPrimitives = twinPrimitives.Cast<IWebApiPrimitive>().ToArray();
        var request = webApiPrimitives.Select(p => p.PlcWriteRequestData).ToList();

        try
        {
           
            responseData = await RequestHandler.ApiBulkAsync(request);
        }
        catch (ApiBulkRequestException apiException)
        {
            HandleCommFailure(apiException, "Batch write failed.", primitives, apiException.BulkResponse, request);
        }
        catch (Exception e)
        {
            HandleCommFailure(e, "Batch write failed.", primitives, responseData, request);
        }
    }

    /// <inheritdoc />
    public override async void ReloadConnector()
    {
        await Task.Run(() => true);
    }

    internal void ClearPeriodicReadSet()
    {
        base.ClearPeriodicReadSet();
        var hitCount = RwCycleCount;
        Task.Delay(300).Wait(); //TODO: we have to address this differently... preventing concurrency...
    }


    internal async Task<(T result, ApiResultResponse<T> response)> ReadAsync<T>(string symbol)
    {
        var response = await RequestHandler.PlcProgramReadAsync<T>($"{DBName}.{symbol}");
        return (response.Result, response);
    }

    internal async Task<T> ReadAsync<T>(IWebApiPrimitive primitive)
    {
        var response = new ApiResultResponse<T>();
        try
        {
            response = await RequestHandler.PlcProgramReadAsync<T>($"{DBName}.{primitive.Symbol}");
            return response.Result;
        }
        catch (Exception e)
        {
            HandleCommFailure(e, "Failed to read item", primitive, response);
            return ((dynamic)primitive).LastValue;
        }
    }

    internal async Task<T> ReadAsync<T>(string symbol, T value)
    {
        return (await ReadAsync<T>(symbol)).result;
    }

    internal async Task<T> ReadAsync<T>(IWebApiPrimitive primitive, T value)
    {
        var response = new ApiResultResponse<T>();
        try
        {
            var res = await ReadAsync<T>(primitive.Symbol);
            response = res.response;
            return response.Result;
        }
        catch (Exception e)
        {
            HandleCommFailure(e, "Failed to read item", primitive, response);
            return ((dynamic)primitive).LastValue;
        }
    }


    internal async Task<T> WriteAsync<T>(string symbol, T value)
    {
        await RequestHandler.PlcProgramWriteAsync($"{DBName}.{symbol}", value);
        return value;
    }

    internal async Task<T> WriteAsync<T>(IWebApiPrimitive primitive, T value)
    {
        var response = new ApiTrueOnSuccessResponse();

        try
        {
            response = await RequestHandler.PlcProgramWriteAsync($"{DBName}.{primitive.Symbol}", value);
            return value;
        }
        catch (Exception e)
        {
            HandleCommFailure(e, "Failed to write item", primitive, response);
            return ((dynamic)primitive).LastValue;
        }
    }

    private static volatile object mutex = new();
    private static int _id;

    internal static string GetId
    {
        get
        {
            lock (mutex)
            {
                return _id++.ToString(); // TODO: Consider something more appropriate here.
            }
        }
    }

    internal static ByteArrayContent ToByteArrayContent(object payload)
    {
        var apiLoginRequestString = JsonConvert.SerializeObject(payload,
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        var byteArr = Encoding.UTF8.GetBytes(apiLoginRequestString);
        var body = new ByteArrayContent(byteArr);
        body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return body;
    }

    internal static ApiPlcReadRequest CreateReadRequest(string symbol, string root = "\"TGlobalVariablesDB\"")
    {
        return new ApiPlcReadRequest($"{root}.{symbol}");
    }

    internal static ApiPlcWriteRequest CreateWriteRequest(string symbol, object value,
        string root = "\"TGlobalVariablesDB\"")
    {
        return new ApiPlcWriteRequest($"{root}.{symbol}", value);
    }

    internal static WebApiConnector Cast(Connector connector)
    {
        return connector as WebApiConnector ?? new WebApiConnector();
    }
}


