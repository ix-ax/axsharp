// Ix.ixc
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CliWrap;
using CommandLine;
using Ix.Compiler;
using Ix.Compiler.Cs.Onliner;
using Ix.Compiler.Cs.Plain;

namespace ixc;

public static class Program
{
    private const string Logo =
 @"| \ / 
=  =  
| / \
";

    private static IxProject? Project;

    public static void Main(string[] args)
    {

        LegalComplianceAcrobatics().Wait();

        DisplayInfo();

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                var recoverCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                    Project = GenerateIxProject(o);
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
    }

    private static async Task LegalComplianceAcrobatics()
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

    private static void SetupAssemblyResolverLegalAcrobatics(string entryAssemblyLocation)
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

    private static async Task ApaxInstallLegalAcrobatics(string entryAssemblyLocation)
    {
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();
        try
        {
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

    private static IEnumerable<Assembly> AXAssemblies { get; set; }

    private static Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
    {
        return AXAssemblies.FirstOrDefault(p => p.FullName == args.Name);
    }

    private static string GetFullPath(string path)
    {
        if (Path.IsPathRooted(path))
        {
            Console.WriteLine("Path si rooted.");
            return path;
        }
        else
        {
            var fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, path));
            Console.WriteLine($"Path si relative '{fullPath}'.");
            return fullPath;
        }
    }

    private static IxProject GenerateIxProject(Options o)
    {
        var axProjectFolder = string.IsNullOrEmpty(o.AxSourceProjectFolder)
            ? Environment.CurrentDirectory
            : o.AxSourceProjectFolder;

        Environment.CurrentDirectory = GetFullPath(axProjectFolder);

        var ax = new AxProject(Environment.CurrentDirectory);
        var project = new IxProject(ax, new[] { typeof(CsOnlinerSourceBuilder), typeof(CsPlainSourceBuilder) },
            typeof(CsProject), o);

        project.Generate();
        return project;
    }

    private static void DisplayInfo()
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(Logo);
        Console.ForegroundColor = originalColor;

        Console.WriteLine($"IX compiler CLI. Version: '{GitVersionInformation.SemVer}'");
        originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("THIS PROJECT IS POSSIBLE BECAUSE OF SOME AWESOME OPEN SOURCE PROJECTS\n" +
                          "THIRD PARTY LICENSES CAN BE FOUND AT \n" +
                          "https://github.com/ix-ax/ix/blob/master/notices.md");
        Console.ForegroundColor = originalColor;

        if (int.Parse(GitVersionInformation.Major) < 1 || string.IsNullOrEmpty(GitVersionInformation.PreReleaseLabel))
        {
            originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("THIS IS PRE-RELEASE VERSION DO NOT USE IN PRODUCTION ENVIRONMENT!!!");
            Console.ForegroundColor = originalColor;
        }
    }

    private static string GetStcNameByPlatform()
       => RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "stc-linux-x64" : "stc-win-x64";

}