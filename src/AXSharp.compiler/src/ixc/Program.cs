// AXSharp.ixc
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
using AXSharp.Compiler;
using AXSharp.Compiler.Cs.Onliner;
using AXSharp.Compiler.Cs.Plain;

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
        LegalAcrobatics.LegalComplianceAcrobatics(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName).Wait();

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

}