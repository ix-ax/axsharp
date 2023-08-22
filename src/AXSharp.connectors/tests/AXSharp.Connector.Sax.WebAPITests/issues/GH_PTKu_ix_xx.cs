// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Xunit;
using AXSharp.Connector.S71500.WebApi;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebAPITests.Primitives;
using exploratory;

namespace AXSharp.Connector.S71500.WebApi.Tests.Issues
{
    public class GH_PTKu_ix_xx : IDisposable
    {

        public void Dispose()
        {
            Plc.Hierarchy.StopPolling( this);
            Plc = null;
            System.GC.Collect();
        }

        public ax_test_projectTwinController Plc { get; private set; }

        public GH_PTKu_ix_xx()
        {
            Plc = new ax_test_projectTwinController(ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET"), "Everybody", "", true));
            Plc.Connector.ReadWriteCycleDelay = 250;
            Plc.Connector.ExceptionBehaviour = CommExceptionBehaviour.ReThrow;
            Plc.Connector.SubscriptionMode = ReadSubscriptionMode.Polling;
            Plc.Connector.BuildAndStart();
            Plc.Hierarchy.StartPolling(250, this);
            Task.Delay(1000).Wait();

        }

        [Fact()]
        public async Task reproductionRead()
        {

            //while (true)
            {
                var results = new List<Task>();
                for (int i = 0; i < 100; i++)
                {
                    results.Add(Plc.maxs.ReadAsync());
                    results.Add(Plc.mins.ReadAsync());
                    results.Add(Plc.maxsmatch.ReadAsync());
                    results.Add(Plc.minsmatch.ReadAsync());
                    results.Add(Plc.Hierarchy.ReadAsync());
                }

                foreach (var task in results)
                {
                    task.Wait();
                }
            }

            
        }

        [Fact()]
        public async Task reproductionWrite()
        {

            //while (true)
            {

                var results = new List<Task>();
                for (int i = 0; i < 100; i++)
                {
                    results.Add(Plc.maxs.WriteAsync());
                    results.Add(Plc.mins.WriteAsync());
                    results.Add(Plc.maxsmatch.WriteAsync());
                    results.Add(Plc.minsmatch.WriteAsync());
                    results.Add(Plc.Hierarchy.WriteAsync());
                }

                foreach (var task in results)
                {
                    task.Wait();
                }
            }


        }

        [Fact()]
        public async Task reproductionReadWrite()
        {
            {

                var results = new List<Task>();
                for (int i = 0; i < 100; i++)
                {
                    results.Add(Plc.maxs.WriteAsync());
                    results.Add(Plc.maxs.ReadAsync());
                    results.Add(Plc.mins.WriteAsync());
                    results.Add(Plc.mins.ReadAsync());
                    results.Add(Plc.maxsmatch.WriteAsync());
                    results.Add(Plc.maxsmatch.ReadAsync());
                    results.Add(Plc.minsmatch.WriteAsync());
                    results.Add(Plc.minsmatch.ReadAsync());
                    results.Add(Plc.Hierarchy.ReadAsync());
                }

                foreach (var task in results)
                {
                    task.Wait();
                }
            }


        }
    }
}