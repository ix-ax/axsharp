// See https://aka.ms/new-console-template for more information


using CommandLine;
using AXSharp.TIA2AXTool;
using AXSharp.Connector.S71500.WebApi;
using AXSharp.TIA.Connector;
using AXSharp.TIA2AXSharp;
using System.Diagnostics;
using Siemens.Simatic.S7.Webserver.API.Exceptions;

Parser.Default.ParseArguments<Options>(args)
.WithParsed(o =>
{
   
    Main(o);

});

void Main(Options o)
{
    Console.WriteLine("** TIA2AX **");
    Console.WriteLine("Generator of TwinObjects for TIA projects");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Generating...");

    Stopwatch sw = new Stopwatch();
    sw.Start();
    var connector = new WebApiConnector(o.Ip, o.Username, o.Password, true, string.Empty);
    TIARootObject root;

    try
    {
        root = TIA2AXSharpAdapter.CreateTIARootObject(connector, o.DataBlocks?.ToArray()).Result;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Cannot create adapter! \n {ex}");
        Environment.ExitCode = -1;
        return;
    }
    

    try
    {
        TIA2AXSharpSerializer.Serialize(root, o.Output);
    }  
    catch (Exception ex) 
    {
        Console.Error.WriteLine($"Serialization error occured! \n {ex}");
        Environment.ExitCode = -1;
        return;
    }


    sw.Stop();

    Console.WriteLine("Done.");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine($"Returned {root.TIABrowseElements.Count} datablocks.");
    Console.WriteLine($"Location: {o.Output}");
    Console.WriteLine($"Elapsed time: {sw.Elapsed}");
    Environment.ExitCode = 0;
    return;

}