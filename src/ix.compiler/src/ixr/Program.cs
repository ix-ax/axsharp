// See https://aka.ms/new-console-template for more information
using AX.ST.Semantic;
using AX.ST.Syntax.Parser;
using AX.ST.Syntax.Tree;
using AX.Text;
using AX.Text.Diagnostics;
using Ix.Compiler;
using Ix.ixc_doc;
using Ix.ixc_doc.Visitors;
using System;
using CommandLine;
using System.Reflection;
using System.Text;
using CliWrap;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

const string Logo =
@"| \ / 
=  =  
| / \
";


Console.WriteLine(Logo);
LegalAcrobatics.LegalComplianceAcrobatics().Wait();
Console.WriteLine("Ixr compiler");
Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                var recoverCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                    GenerateResx(o);
                   Console.WriteLine("Done.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    Environment.CurrentDirectory = recoverCurrentDirectory;
                }
            });


void GenerateResx(Options o)
{
    var axProject = new AxProject(o.AxSourceProjectFolder);
    Console.WriteLine($"Compiling project {axProject.ProjectInfo.Name}...");
    var projectSources = axProject.Sources.Select(p => (parseTree: STParser.ParseTextAsync(p).Result, source: p));

    var toCompile = projectSources.Select(p => p.parseTree);

    var compilation = Compilation.Create(toCompile, Compilation.Settings.Default).Result;

    var semanticTree = compilation.GetSemanticTree();

    //visit
    var myNodeVisitor = new MyNodeVisitor(axProject);

    semanticTree.GetRoot().Accept(myNodeVisitor, Console.WriteLine);

    //convert to res <#Test test#> 3 <#Test test#>
    //CreateResx(list);
}

public static class LegalAcrobatics
{ 
    public static async Task LegalComplianceAcrobatics()
    {
        /*
         We need this to comply with the Siemens legal requirement to allow only users that have access
         to simatic-ax to some APIs.
        */
        
        var entryAssemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
        var stcapipath = Path.GetFullPath(Path.Combine(entryAssemblyLocation, $".apax//.apax//packages//@ax//{GetStcNameByPlatform()}//bin//"));

        if(!Directory.Exists(stcapipath))
        {
            await ApaxInstallLegalAcrobatics(entryAssemblyLocation);
        }
        
        SetupAssemblyResolverLegalAcrobatics(entryAssemblyLocation);
    }

    static IEnumerable<Assembly> AXAssemblies { get; set; }


    static void SetupAssemblyResolverLegalAcrobatics(string entryAssemblyLocation)
    {
        var axAssemblies = new List<Assembly>();

        foreach (var assemblyFile in Directory.EnumerateFiles(
                     Path.GetFullPath(Path.Combine(entryAssemblyLocation, $".apax//.apax//packages//@ax//{GetStcNameByPlatform()}//bin//")), "*.dll"))
        {
            try
            {
                axAssemblies.Add(Assembly.LoadFile(assemblyFile));
            }
            catch (System.BadImageFormatException e)
            {
                // We just ignore this...there might be some libraries that we just cannot load, but we do not need them.
            }
        }

        AXAssemblies = axAssemblies;

        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    static async Task ApaxInstallLegalAcrobatics(string entryAssemblyLocation)
    {
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();
        try
        {
            Directory.CreateDirectory(Path.Combine(entryAssemblyLocation, ".apax"));
            Log.Logger.Information("Restoring apax packages.\n" +
                                   "You need have access to apax registry.\n" +
                                   "use 'apax login' if you did not do it yet.");
            var result = await Cli.Wrap("apax")
                .WithArguments("install")
                .WithWorkingDirectory(Path.Combine(entryAssemblyLocation, ".apax"))
                .WithValidation(CommandResultValidation.None)
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
                .ExecuteAsync();
        }
        catch(Exception ex)
        {
            Log.Logger.Error("There was a problem restoring apax packages.", ex);
        }
        finally
        {
            Log.Logger.Information(stdOutBuffer.ToString());
            Log.Logger.Information(stdErrBuffer.ToString());
        }
    }

    static Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
    {
        return AXAssemblies.FirstOrDefault(p => p.FullName == args.Name);
    }

  static string GetStcNameByPlatform()
       => RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "stc-linux-x64" : "stc-win-x64";
}


