using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using AXSharp.TIA2AXSharp;

namespace TIA2AX.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connector = new WebApiConnector("192.168.0.4", "Everybody", "", true, string.Empty);

            var rootObject = await TIA2AXSharpAdapter.CreateTIARootObject(connector, new[] { "TGlobalVariablesDB" });

            var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector, rootObject);

            adapter.First().RetrievePrimitives().Where(p => p.Symbol.Contains("ProcessData")).ToList().ForEach(p => System.Console.WriteLine(p.Symbol));
        }
    }
}