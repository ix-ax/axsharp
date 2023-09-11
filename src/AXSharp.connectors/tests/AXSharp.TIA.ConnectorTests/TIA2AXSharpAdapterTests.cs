using Xunit;
using AXSharp.TIA.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using System.Reflection;
using AXSharp.Connector.ValueTypes;
using Xunit.Abstractions;

namespace AXSharp.TIA2AXSharpTests
{
    public class TIA2AXSharpAdapterTests
    {

        private readonly ITestOutputHelper output;
        public TIA2AXSharpAdapterTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        //[Fact()]
        //public async void GoTest()
        //{
            
        //    var connector = new WebApiConnector("172.20.30.110", "Everybody", "", true, string.Empty);
        //    var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector);

        //    //var allVariables2 = adapter.First().RetrievePrimitives()
        //    //    .Where(p => p.Symbol.StartsWith("\"TGlobalVariablesDB\".Context")).ToList();

        //    var allVariables = adapter.First().RetrievePrimitives()
        //        .Where(p => p.Symbol.StartsWith("\"TGlobalVariablesDB\"")).ToList();

        //    await connector.ReadBatchAsync(allVariables);
        //    //connector.ReadBatchAsync(allVariables).Wait();

        //    //await (allVariables.First() as OnlinerULInt).GetAsync();

        //    // CHARs and STRINGS are problems!!!! 
        //    // in p.read, char parsing causing exception

        //    var x = allVariables.FirstOrDefault(p => p.Symbol.Contains("myBOOL"));
            
        //    foreach (var variable in allVariables)
        //    {
        //        output.WriteLine($"{variable.Symbol} : {((dynamic)variable).LastValue}");
        //    }

        //    Assert.True(true, "This test needs an implementation");
            
        //}

        [Fact()]
        public async void GoTemplateSimpleTest()
        {

            var connector = new WebApiConnector("172.20.30.110", "Everybody", "", true, string.Empty);
            var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector);

            //var allVariables2 = adapter.First().RetrievePrimitives()
            //    .Where(p => p.Symbol.StartsWith("\"TGlobalVariablesDB\".Context")).ToList();

            var allVariables = adapter.First().RetrievePrimitives()
                .Where(p => p.Symbol.StartsWith("\"TGlobalVariablesDB\"")).ToList();

            await connector.ReadBatchAsync(allVariables);
            //connector.ReadBatchAsync(allVariables).Wait();

            //await (allVariables.First() as OnlinerULInt).GetAsync();

            // CHARs and STRINGS are problems!!!! 
            // in p.read, char parsing causing exception

            var x = allVariables.FirstOrDefault(p => p.Symbol.Contains("myBOOL"));

            foreach (var variable in allVariables)
            {
                output.WriteLine($"{variable.Symbol} : {((dynamic)variable).LastValue}");
            }

            Assert.True(true, "This test needs an implementation");

        }
    }
}