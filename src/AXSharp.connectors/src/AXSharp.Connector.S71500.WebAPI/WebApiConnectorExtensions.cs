﻿// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AXSharp.Connector.S71500.WebApi;

namespace AXSharp.Connector;

/// <summary>
/// Provides extension methods for instantiating WebAPI connector.
/// </summary>
public static class WebApiConnectorExtensions
{
    /// <summary>
    /// Creates connector adapter for WebAPI communication.
    /// </summary>
    /// <param name="adapter">Adapter builder.</param>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    /// <param name="ignoreSSLErros">When true connection will ignore SSL errors.</param>
    /// <param name="dbName">Name of default DB. The DB used to store all data in an AX project is 'TGlobalVariablesDB'.</param>
    /// <returns>Connector adapter for WebAPI connection.</returns>
    public static ConnectorAdapter CreateWebApi(this ConnectorAdapterBuilder adapter,
        string ipAddress, string userName, string password, bool ignoreSSLErros,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        return new ConnectorAdapter(typeof(WebApiConnectorFactory))
            { Parameters = new object[] { ipAddress, userName, password, ignoreSSLErros } };
    }

    /// <summary>
    /// Creates connector adapter for WebAPI communication.
    /// </summary>
    /// <param name="adapter">Adapter builder.</param>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    /// <param name="customServerCertHandler">Customized server certificate handler.</param>
    /// <param name="dbName">Name of default DB. The DB used to store all data in an AX project is 'TGlobalVariablesDB'.</param>
    /// <returns>Connector adapter for WebAPI connection.</returns>
    public static ConnectorAdapter CreateWebApi(this ConnectorAdapterBuilder adapter,
        string ipAddress, string userName, string password,
        Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>? customServerCertHandler,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        return new ConnectorAdapter(typeof(WebApiConnectorFactory))
            { Parameters = new object[] { ipAddress, userName, password, customServerCertHandler, dbName } };
    }
}