﻿// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.S71500.WebAPI;
using AXSharp.Connector.ValueTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using Serilog;
using Serilog.Events;
using Siemens.Simatic.S7.Webserver.API.Exceptions;
using Siemens.Simatic.S7.Webserver.API.Models.Responses;
using Siemens.Simatic.S7.Webserver.API.Services;
using Siemens.Simatic.S7.Webserver.API.Services.IdGenerator;
using Siemens.Simatic.S7.Webserver.API.Services.RequestHandling;
using System.Collections.Concurrent;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text;
using System.Xml.Serialization;

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
/// Provides connector to mediate connection with AX# twins over WebAPI connection.
/// </summary>
public class WebApiConnector : Connector
{
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
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        IPAddress = ipAddress;
        DBName = dbName;
        TargetPlatform = platform;
        UserName = userName;
        UserPassword = password;

        var serviceFactory = new ApiStandardServiceFactory();
        var client = serviceFactory.GetHttpClient(ipAddress, UserName, UserPassword);
        requestHandler = new ApiHttpClientRequestHandler(client,
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
    public WebApiConnector(string ipAddress, string userName, string password, bool ignoreSSLErros, eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        IPAddress = ipAddress;
        DBName = dbName;
        TargetPlatform = platform;
        UserName = userName;
        UserPassword = password;

        if (ignoreSSLErros)
            ServerCertificateCallback.CertificateCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

        var serviceFactory = new ApiStandardServiceFactory();
        Client = serviceFactory.GetHttpClient(ipAddress, UserName, UserPassword ?? string.Empty);

        requestHandler = new ApiHttpClientRequestHandler(Client,
            new ApiRequestFactory(ReqIdGenerator, RequestParameterChecker), ApiResponseChecker);

        requestHandler.ApiLogout();
        requestHandler.ApiLogin(UserName, UserPassword ?? string.Empty, true);

        NumberOfInstances++;
    }

    /// <inheritdoc />
#pragma warning disable CS8618
    internal WebApiConnector()
#pragma warning restore CS8618
    {
    }

    private readonly HttpClient Client;

    private GUIDGenerator ReqIdGenerator { get; } = new();
    private ApiRequestParameterChecker RequestParameterChecker { get; } = new();
    private ApiResponseChecker ApiResponseChecker { get; } = new();

    private readonly ApiHttpClientRequestHandler requestHandler;

    private ApiHttpClientRequestHandler RequestHandler
    {
        get
        {
            return requestHandler;
        }
    }

    private readonly object concurentCountMutex = new object();

    private async Task AntiThrottling(IEnumerable<ITwinPrimitive> primitives)
    {
        var concurrent = 0;

        do
        {
            lock (concurentCountMutex)
            {
                concurrent = concurrentRequest;
            }

            if (concurrent >= ConcurrentRequestMaxCount)
            {
                await Task.Delay(ConcurrentRequestDelay);
            }

        } while (concurrent >= ConcurrentRequestMaxCount);

        if (concurrent > ConcurrentRequestMaxCount)
        {
            throw new Exception($"Too many requests {concurrent}");
        }

        lock (concurentCountMutex)
        {
            concurrentRequest = concurrentRequest + 1;
        }
    }

    private void ReleaseConcurrent(IEnumerable<ITwinPrimitive> primitives)
    {
        lock (concurentCountMutex)
        {
            concurrentRequest = concurrentRequest - 1;
        }
    }

    /// <summary>
    ///     Gets number of instance of WebAPI connector in this application.
    /// </summary>
    public static int NumberOfInstances { get; private set; }

    /// <summary>
    ///     Get the address of the target system.
    /// </summary>
    internal string IPAddress { get; }

    internal string UserName { get; }
    internal string UserPassword { get; }

    internal string DBName { get; }

    /// <inheritdoc />
    public override Connector BuildAndStart()
    {
        StartReadWriteOps();
        return this;
    }

    public async Task ReLoginToConnectorApi()
    {
        bool Conncected = false;

        IsRwLoopSuspended = true; // suspen cyclic R/W operations

        do
        {
            Task.Delay(2000).Wait(); // wait
            try
            {
                requestHandler.ReLogin(UserName, UserPassword ?? string.Empty, true);
                Logger.Warning($"Plc {IPAddress} Api ReLogin Done!");

                Siemens.Simatic.S7.Webserver.API.Enums.ApiPlcOperatingMode mode;

                do
                {
                    Task.Delay(2000).Wait();
                    mode = requestHandler.PlcReadOperatingMode().Result;

                    Logger.Warning($"Plc {IPAddress} Has mode {mode.ToString()}!");
                } while (mode != Siemens.Simatic.S7.Webserver.API.Enums.ApiPlcOperatingMode.Run);

                Conncected = true;
                this.IsRwLoopSuspended = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } while (!Conncected);
    }

    internal void HandleCommFailure(Exception exception, string description, IEnumerable<ITwinPrimitive> primitives,
        ApiBulkResponse response, IEnumerable<ApiRequestBase> originalRequest)
    {
        string firstFailedItemParams = string.Empty;

        bool WasPermitionChanged = false;

        foreach (var errorResponse in response.ErrorResponses)
        {
            var failedItem = originalRequest.FirstOrDefault(p => p.Id == errorResponse.Id);
            var failedItemParams = string.Join(";", failedItem.Params.Select(p => $"{p.Key} : {p.Value}"));
            if (string.IsNullOrEmpty(firstFailedItemParams))
            {
                firstFailedItemParams = failedItemParams;
            }

            Logger.Error($"{failedItemParams} : {errorResponse.Error.Message} [{errorResponse.Error.Code}]");

            if (!WasPermitionChanged && (errorResponse.Error.Code == Siemens.Simatic.S7.Webserver.API.Enums.ApiErrorCode.PermissionDenied))
            {
                WasPermitionChanged = true;
            }
        }

        foreach (var primitive in primitives)
        {
            primitive?.AccessStatus.Update(RwCycleCount, $"{description}: '{exception.Message}' [{firstFailedItemParams}] ");
        }

        if (WasPermitionChanged)
        {
            ReLoginToConnectorApi(); // Start in async task.
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

    private const int MAX_READ_REQUEST_SEGMENT = (64 * 1024) - 628;
    private const int MAX_WRITE_REQUEST_SEGMENT = (64 * 1024) - 628;

    private System.Diagnostics.Stopwatch stopwatch = new();

    private volatile int concurrentRequest = 0;


    private AsyncRetryPolicy _retryPolicy;

    // TODO: This has been added for additional resiliency.
    // when target system responds with HTTP exception 'premature end...' the connector will recover.
    // it may take several second before the exception arises.
    private AsyncRetryPolicy RetryPolicy
    {
        get
        {
            return _retryPolicy ??= Polly.Policy
                .Handle<HttpRequestException>()
                .RetryAsync(5, (exception, i) =>
                {
                    Log.Logger.Error($"{exception.Message} : {exception.InnerException?.Message} | Number of concurrent requests {concurrentRequest}");
                    Task.Delay(100).Wait();
                });
        }
    }


    /// <inheritdoc />
    public override async Task ReadBatchAsync(IEnumerable<ITwinPrimitive>? primitives)
    {
        if (primitives == null) return;

        var responseData = new ApiBulkResponse();

        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();

        if (!twinPrimitives.Any()) return;
        try
        {
            if (Logger.IsEnabled(LogEventLevel.Debug))
            {
                stopwatch.Restart();
            }

            if (Logger.IsEnabled(LogEventLevel.Verbose))
            {
                this.Logger
                    .Verbose("{vars}",
                    string.Join("\n",
                        (twinPrimitives).Select(p =>
                            $"{((OnlinerBase)p).Symbol} | pollings: [{string.Join(";", ((OnlinerBase)p).PollingHolders.Select(a => a.Key.ToString()))}]")));
            }

            await AntiThrottling(primitives);

            var webApiPrimitives = twinPrimitives.Cast<IWebApiPrimitive>().Distinct().ToArray();

            foreach (var requestSegment in webApiPrimitives.SegmentReadRequest(MAX_READ_REQUEST_SEGMENT))
            {
                var apiPrimitives = requestSegment as IWebApiPrimitive[] ?? requestSegment.ToArray();
                var segment = apiPrimitives.Select(p => p.PeekPlcReadRequestData).ToList();
                try
                {
                    await RetryPolicy.ExecuteAsync(async () => responseData = await RequestHandler.ApiBulkAsync(segment));

                    // This is needed when unassigned CHAR or WCHAR are read; they won't be present in the response.
                    if (responseData.SuccessfulResponses.Count() != apiPrimitives.Length)
                    {
                        foreach (var response in responseData.SuccessfulResponses)
                        {
                            var a = apiPrimitives.FirstOrDefault(p => p.PeekPlcReadRequestData.Id == response.Id);
                            if (a == null) continue;
                            a.Read(response.Result.ToString());
                            a.AccessStatus.Update(RwCycleCount);
                        }
                    }
                    else
                    {
                        var position = 0;
                        apiPrimitives.ToList()
                            .ForEach(p =>
                            {
                                p.Read(responseData.SuccessfulResponses.ElementAt(position++).Result.ToString());
                                p.AccessStatus.Update(RwCycleCount);
                            });
                    }

                }
                catch (ApiBulkRequestException apiException)
                {
                    HandleCommFailure(apiException, "Batch read failed.", apiPrimitives, apiException.BulkResponse,
                        apiPrimitives.Select(p => p.PeekPlcReadRequestData));
                }
                catch (Exception e)
                {
                    HandleCommFailure(e, "Batch read failed.", apiPrimitives, responseData,
                        apiPrimitives.Select(p => p.PeekPlcReadRequestData));
                }
            }
        }
        finally
        {
            ReleaseConcurrent(primitives);
        }

        if (Logger.IsEnabled(LogEventLevel.Debug))
        {
            this.Logger.Debug($"Bulk reading: {twinPrimitives.Count()} items read in {stopwatch.ElapsedMilliseconds} ms.");
        }
    }

    /// <inheritdoc />
    public override async Task WriteBatchAsync(IEnumerable<ITwinPrimitive>? primitives)
    {
        if (primitives == null) return;

        try
        {
            await AntiThrottling(primitives);

            var responseData = new ApiBulkResponse();
            var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();

            if (twinPrimitives.Any())
            {
                if (Logger.IsEnabled(LogEventLevel.Verbose))
                    this.Logger.Verbose($"Bulk writing: {twinPrimitives.Count()} items.");
            }

            var webApiPrimitives = twinPrimitives.Cast<IWebApiPrimitive>().Distinct().ToArray();

            foreach (var requestSegment in webApiPrimitives.SegmentWriteRequest(MAX_WRITE_REQUEST_SEGMENT))
            {
                var apiPrimitives = requestSegment as IWebApiPrimitive[] ?? requestSegment.ToArray();
                try
                {
                    await RetryPolicy.ExecuteAsync(async () => await RequestHandler.ApiBulkAsync(apiPrimitives.Select(p => p.PeekPlcWriteRequestData)));
                }
                catch (ApiBulkRequestException apiException)
                {
                    HandleCommFailure(apiException, "Batch write failed.", twinPrimitives, apiException.BulkResponse,
                        apiPrimitives.Select(p => p.PeekPlcWriteRequestData));
                }
                catch (Exception e)
                {
                    HandleCommFailure(e, "Batch write failed.", twinPrimitives, responseData,
                        apiPrimitives.Select(p => p.PeekPlcWriteRequestData));
                }
            }
        }
        finally
        {
            ReleaseConcurrent(primitives);
        }
    }

    /// <inheritdoc />
    public override async void ReloadConnector()
    {
        await Task.Run(() => true);
    }

    internal new void ClearPeriodicReadSet()
    {
        base.ClearPeriodicReadSet();
        var hitCount = RwCycleCount;
        //Task.Delay(300).Wait(); //TODO: we have to address this differently... preventing concurrency...
    }

    internal async Task<(T result, ApiResultResponse<T> response)> ReadAsync<T>(string symbol)
    {
        if (symbol.StartsWith("\""))
        {
            var response = await RequestHandler.PlcProgramReadAsync<T>($"{symbol}");
            return (response.Result, response);
        }
        else
        {
            var response = await RequestHandler.PlcProgramReadAsync<T>($"{DBName}.{symbol}");
            return (response.Result, response);
        }

    }

    internal async Task<T> ReadAsync<T>(IWebApiPrimitive primitive)
    {
        await ReadBatchAsync(new IWebApiPrimitive[] { primitive });
        return ((OnlinerBase<T>)primitive).LastValue;
    }


    internal async Task<T> ReadAsync<T>(IWebApiPrimitive primitive, T value)
    {
        await ReadBatchAsync(new IWebApiPrimitive[] { primitive });
        return ((OnlinerBase<T>)primitive).LastValue;
    }

    internal async Task<T> WriteAsync<T>(IWebApiPrimitive primitive, T value)
    {
        ((OnlinerBase<T>)primitive).SetValueToWrite(value);
        await WriteBatchAsync(new List<IWebApiPrimitive>() { primitive });
        return value;
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

    internal static ApiPlcReadRequest CreateReadRequest(string symbol, string root = "\"TGlobalVariablesDB\"")
    {
        if (string.IsNullOrEmpty(root))
        {
            return new ApiPlcReadRequest($"{symbol}");
        }
        else
        {
            return new ApiPlcReadRequest($"{root}.{symbol}");
        }
    }

    internal static ApiPlcWriteRequest CreateWriteRequest(string symbol, object value,
        string root = "\"TGlobalVariablesDB\"")
    {
        if (string.IsNullOrEmpty(root))
        {
            return new ApiPlcWriteRequest($"{symbol}", value);
        }
        else
        {
            return new ApiPlcWriteRequest($"{root}.{symbol}", value);
        }

    }

    internal static WebApiConnector Cast(Connector connector)
    {
        return connector as WebApiConnector ?? new WebApiConnector();
    }

    internal override async Task ReadBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives)
    {
        await ReadBatchAsync(primitives);
    }

    internal override async Task WriteBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives)
    {
        await WriteBatchAsync(primitives);
    }

    public eTargetProjectPlatform TargetPlatform { get; } = eTargetProjectPlatform.SIMATICAX;
}
