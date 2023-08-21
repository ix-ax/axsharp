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
    public class GH_PTKu_ix_xx
    {
        [Fact()]
        public async Task reproductionRead()
        {

            Entry.Plc.Connector.ExceptionBehaviour = CommExceptionBehaviour.ReThrow;
            //while (true)
            {
                var results = new List<Task>();
                for (int i = 0; i < 100; i++)
                {
                    results.Add(Entry.Plc.maxs.ReadAsync());
                    results.Add(Entry.Plc.mins.ReadAsync());
                    results.Add(Entry.Plc.maxsmatch.ReadAsync());
                    results.Add(Entry.Plc.minsmatch.ReadAsync());

                    //await Entry.Plc.maxs.ReadAsync();
                    //await Entry.Plc.mins.ReadAsync();
                    //await Entry.Plc.maxsmatch.ReadAsync();
                    //await Entry.Plc.minsmatch.ReadAsync();
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

            Entry.Plc.Connector.ExceptionBehaviour = CommExceptionBehaviour.ReThrow;
            //while (true)
            {

                var results = new List<Task>();
                for (int i = 0; i < 100; i++)
                {
                    results.Add(Entry.Plc.maxs.WriteAsync());
                    results.Add(Entry.Plc.mins.WriteAsync());
                    results.Add(Entry.Plc.maxsmatch.WriteAsync());
                    results.Add(Entry.Plc.minsmatch.WriteAsync());
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

            Entry.Plc.Connector.ExceptionBehaviour = CommExceptionBehaviour.ReThrow;
            //while (true)
            {

                var results = new List<Task>();
                for (int i = 0; i < 100; i++)
                {
                    results.Add(Entry.Plc.maxs.WriteAsync());
                    results.Add(Entry.Plc.maxs.ReadAsync());
                    results.Add(Entry.Plc.mins.WriteAsync());
                    results.Add(Entry.Plc.mins.ReadAsync());
                    results.Add(Entry.Plc.maxsmatch.WriteAsync());
                    results.Add(Entry.Plc.maxsmatch.ReadAsync());
                    results.Add(Entry.Plc.minsmatch.WriteAsync());
                    results.Add(Entry.Plc.minsmatch.ReadAsync());
                }

                foreach (var task in results)
                {
                    task.Wait();
                }
            }


        }
    }
}