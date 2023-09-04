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
using Xunit.Abstractions;
using Microsoft.VisualStudio.TestPlatform.Utilities;

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

        private readonly ITestOutputHelper report;

        public ax_test_projectTwinController Plc { get; private set; }

        private int simultaneousCycles = 10;
        private int batchCycles = 2;

        public GH_PTKu_ix_xx(ITestOutputHelper output)
        {
            Plc = new ax_test_projectTwinController(ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET"), "Everybody", "", true));
            Plc.Connector.ReadWriteCycleDelay = 250;
            Plc.Connector.ExceptionBehaviour = CommExceptionBehaviour.ReThrow;
            Plc.Connector.SubscriptionMode = ReadSubscriptionMode.Polling;
            Plc.Connector.BuildAndStart();
            Plc.Hierarchy.StartPolling(250, this);
            Task.Delay(1000).Wait();
            report = output;
            Plc.Connector.ConcurrentRequestMaxCount = 1;
            Plc.Connector.ConcurrentRequestDelay = 3;
            report.WriteLine($"Max requests limit: {Plc.Connector.ConcurrentRequestMaxCount}");
            report.WriteLine($"Concurrent request delay: {Plc.Connector.ConcurrentRequestDelay}");

        }

        [Fact()]
        public async Task reproductionConsecutiveParalellRead()
        {

            //while (true)
            {
                var results = new List<Task>();
                

                
                  
                        for (int a = 0; a < batchCycles; a++)
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
        public async Task reproductionOverloadRead()
        {
           
            //while (true)
            {
                //System.Threading.Thread.Sleep(1000);
                var results = new List<Task>();
                var simulatneous = new List<Task>();

                for (int i = 0; i < simultaneousCycles; i++)
                {
                    simulatneous.Add(Task.Run(() =>
                    {
                        for (int a = 0; a < batchCycles; a++)
                        {
                            results.Add(Plc.maxs.ReadAsync());
                            results.Add(Plc.mins.ReadAsync());
                            results.Add(Plc.maxsmatch.ReadAsync());
                            results.Add(Plc.minsmatch.ReadAsync());
                            results.Add(Plc.Hierarchy.ReadAsync());
                        }
                    }));
                }
                
                foreach (var task in simulatneous)
                {
                    task.Wait();
                    report.WriteLine($"Simultaneous {task.Id}");
                }

                foreach (var task in results)
                {
                    task.Wait();
                    report.WriteLine($"Partial {task.Id}");
                }
            }

            
        }

        [Fact()]
        public async Task reproductionOverloadWrite()
        {

            //while (true)
            {
                //System.Threading.Thread.Sleep(1000);
                var results = new List<Task>();
                var simulatneous = new List<Task>();

                for (int i = 0; i < simultaneousCycles; i++)
                {
                    simulatneous.Add(Task.Run(() =>
                    {
                        for (int a = 0; a < batchCycles; a++)
                        {
                            results.Add(Plc.maxs.WriteAsync());
                            results.Add(Plc.mins.WriteAsync());
                            results.Add(Plc.maxsmatch.WriteAsync());
                            results.Add(Plc.minsmatch.WriteAsync());
                            results.Add(Plc.Hierarchy.WriteAsync());
                        }
                    }));
                }

                foreach (var task in simulatneous)
                {
                    task.Wait();
                    report.WriteLine($"Simultaneous {task.Id}");
                }

                foreach (var task in results)
                {
                    task.Wait();
                    report.WriteLine($"Partial {task.Id}");
                }
            }
        }

        [Fact()]
        public async Task reproductionConsecutiveParalellWrite()
        {

            //while (true)
            {

                var results = new List<Task>();
                for (int i = 0; i < batchCycles; i++)
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
        public async Task reproductionConsecutiveReadWrite()
        {
            {

                var results = new List<Task>();
                for (int i = 0; i < batchCycles; i++)
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

        [Fact()]
        public async Task reproductionOverloadReadWrite()
        {
            //while (true)
            {
                //System.Threading.Thread.Sleep(1000);
                var results = new List<Task>();
                var simulatneous = new List<Task>();

                for (int i = 0; i < simultaneousCycles; i++)
                {
                    simulatneous.Add(Task.Run(() =>
                    {
                        for (int a = 0; a < batchCycles; a++)
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
                    }));
                }

                foreach (var task in simulatneous)
                {
                    task.Wait();
                    report.WriteLine($"Simultaneous {task.Id}");
                }

                foreach (var task in results)
                {
                    task.Wait();
                    report.WriteLine($"Partial {task.Id}");
                }
            }


        }
    }
}