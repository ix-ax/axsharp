// Ix.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Xunit;
using Ix.Connector.S71500.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ix.Connector.S71500.WebAPITests.Primitives;
using Siemens.Simatic.S7.Webserver.API.Services;
using Xunit.Abstractions;
using System.IO;
using System.Reflection;

namespace Ix.Connector.S71500.WebApi.Tests.Issues
{

   
    public class GH_PTKu_ix_59
    {
        private readonly ITestOutputHelper output;

        public GH_PTKu_ix_59(ITestOutputHelper output)
        {
            this.output = output;
        }

        protected static WebApiConnector Connector { get; } = TestConnector.TestApiConnector as WebApiConnector;
       
        private string targetIP = System.Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET");

        [Fact]
        public async Task write_read_char()
        {
            return;
            Siemens.Simatic.S7.Webserver.API.Services.ServerCertificateCallback.CertificateCallback = (sender, cert, chain, sslPolicyErrors) => true;
            var serviceFactory = new ApiStandardServiceFactory();
            var reqHandler = await serviceFactory.GetApiHttpClientRequestHandlerAsync(targetIP, "Everybody", "");
            var variableSymbol = "\"TGlobalVariablesDB\".myCHAR";

            for (int i = 32; i < 128; i++)
            {

                this.output.WriteLine(i.ToString());
                var expected = (char)(i);

                if (!char.IsAscii(expected))
                    continue;

                await reqHandler.PlcProgramWriteAsync(variableSymbol, expected);
                var response = await reqHandler.PlcProgramReadAsync<char>(variableSymbol);
                Assert.Equal(expected, response.Result);
            }

        }

        [Fact]
        public async Task write_read_wchar()
        {
            return;
            Siemens.Simatic.S7.Webserver.API.Services.ServerCertificateCallback.CertificateCallback = (sender, cert, chain, sslPolicyErrors) => true;
            var serviceFactory = new ApiStandardServiceFactory();
            var reqHandler = await serviceFactory.GetApiHttpClientRequestHandlerAsync(targetIP, "Everybody", "");
            var variableSymbol = "\"TGlobalVariablesDB\".myWCHAR";

            for (int i = 32; i < 0xD7FF; i++)
            {
                var expected = (char)(i);
                try
                {
                    await reqHandler.PlcProgramWriteAsync(variableSymbol, expected);
                    var response = await reqHandler.PlcProgramReadAsync<char>(variableSymbol);
                    Assert.Equal(expected, response.Result);
                }
                catch (Exception e)
                {
                    output.WriteLine(i.ToString());
                }
                
            }

        }
    }
}