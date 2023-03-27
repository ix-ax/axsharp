
// Build
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Build.FilteredSolution;
using Cake.Common;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Clean;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.DocFx;
using Cake.DocFx.Build;
using Cake.Frosting;
using Cake.Powershell;
using CliWrap;
using CommandLine;
using AXSharp.nuget.update;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Packaging;
using Octokit;
using Polly;
using Credentials = Octokit.Credentials;
using Path = System.IO.Path;
using ProductHeaderValue = Octokit.ProductHeaderValue;


public static class Program
{
    public static int Main(string[] args)
    {
        var retVal = 0;
        Parser.Default.ParseArguments<BuildParameters>(args)
            .WithParsed<BuildParameters>(o =>
            {
                retVal = new CakeHost()
                    .ConfigureServices(services => services.AddSingleton(o))
                    .UseContext<BuildContext>()
                    .Run(args);
            });

        return retVal;
    }
}

[TaskName("CleanUp")]
public sealed class CleanUpTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetClean(Path.Combine(context.ScrDir, "AXSharp.sln"), new DotNetCleanSettings() { Verbosity = context.BuildParameters.Verbosity});
        context.CleanDirectory(context.Artifacts);
        context.CleanDirectory(context.TestResults);
    }
}

[TaskName("Provision")]
[IsDependentOn(typeof(CleanUpTask))]
public sealed class ProvisionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        ProvisionProjectWideTools(context);
    }

    private static void ProvisionProjectWideTools(BuildContext context)
    {
        context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
        {
            Arguments = $" tool restore --no-cache --ignore-failed-sources ",

        });

        context.ProcessRunner.Start(Helpers.GetApaxCommand(), new Cake.Core.IO.ProcessSettings()
        {
            Arguments = $" install -L -c",
            WorkingDirectory = Path.Combine(context.ScrDir, "apax"),
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            Silent = false,

        }).WaitForExit();


        // Copy apax packages to different directory
        var destinationDir = Path.Combine(context.ScrDir, "apax//stc");
        var sourceDir = Path.Combine(context.ScrDir, $"apax//.apax//packages//@ax//{Helpers.GetStcNameByPlatform()}");
        Helpers.CopyApaxPackages(sourceDir, destinationDir, context.ScrDir);

    }
}

[TaskName("Build")]
[IsDependentOn(typeof(ProvisionTask))]
public sealed class BuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetBuild(Path.Combine(context.ScrDir, "AXSharp.compiler\\src\\ixc\\AXSharp.ixc.csproj"), context.DotNetBuildSettings);

        var axprojects = new List<string>()
        {
            Path.Combine(context.ScrDir, "AXSharp.blazor\\tests\\sandbox\\ax-blazor-example\\"),
            Path.Combine(context.ScrDir, "sanbox\\integration\\ix-integration-plc\\"),
            Path.Combine(context.ScrDir, "AXSharp.examples\\hello.world.console\\hello.world.console.plc"),
            Path.Combine(context.ScrDir, "AXSharp.connectors\\tests\\ax-test-project\\"),
            Path.Combine(context.ScrDir, "tests.integrations\\integrated\\src\\ax\\")
        };


        foreach (var axproject in axprojects)
        {
            context.DotNetRunSettings.WorkingDirectory = Path.Combine(context.ScrDir, axproject);
            context.DotNetRun(Path.Combine(context.ScrDir, "AXSharp.compiler\\src\\ixc\\AXSharp.ixc.csproj"), context.DotNetRunSettings);
        }

        context.DotNetBuild(Path.Combine(context.ScrDir, "AXSharp.sln"), context.DotNetBuildSettings);
        
    }
}

[TaskName("Test")]
[IsDependentOn(typeof(BuildTask))]
public sealed class TestsTask : FrostingTask<BuildContext>
{
    // Tasks can be asynchronous
    public override void Run(BuildContext context)
    {

        if (!context.BuildParameters.DoTest)
        {
            context.Log.Warning($"Skipping tests");
            return;
        }

      
        if (context.BuildParameters.TestLevel == 1)
        {
            context.RunTestsFromFilteredSolution(Path.Combine(context.ScrDir, "AXSharp-L1-tests.slnf"));
        }
        else if (context.BuildParameters.TestLevel == 2)
        {
            context.RunTestsFromFilteredSolution(Path.Combine(context.ScrDir, "AXSharp-L2-tests.slnf"));
        }
        else if (context.BuildParameters.TestLevel == 3)
        {
            context.RunTestsFromFilteredSolution(Path.Combine(context.ScrDir, "AXSharp-L3-tests.slnf"));
        }
        else
        {
            context.UploadTestPlc( 
                Path.GetFullPath(Path.Combine(context.WorkDirName, "..//..//src//AXSharp.connectors//tests//ax-test-project//")),
                Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET"),
                Environment.GetEnvironmentVariable("AXTARGETPLATFORMINPUT"));

            context.UploadTestPlc(
                Path.GetFullPath(Path.Combine(context.WorkDirName, "..//..//src//tests.integrations//integrated//src//ax")),
                Environment.GetEnvironmentVariable("AXTARGET"),
                Environment.GetEnvironmentVariable("AXTARGETPLATFORMINPUT"));

            context.RunTestsFromFilteredSolution(Path.Combine(context.ScrDir, "AXSharp-L3-tests.slnf"));
        }

        

    }

    

   
}
            foreach (var framework in context.TargetFrameworks)
            {
                context.DotNetTestSettings.VSTestReportPath = Path.Combine(context.TestResults, $"{project.Name}_{framework}.xml");
                context.DotNetTestSettings.Framework = framework;
                context.DotNetTest(Path.Combine(project.FullName), context.DotNetTestSettings);
            }
        }
    }

    private static void UploadTestPlc(BuildContext context, string workingDirectory, string targetIp,
        string targetPlatform)
    {

        context.Log.Information($"Installing dependencies for ax project '{workingDirectory}' at {targetIp}");




        context.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments = " install -L",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            Silent = false
        }).WaitForExit();


        context.Log.Information($"Building ax project '{workingDirectory}' at {targetIp}");




        context.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments = " apax build",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            RedirectedStandardOutputHandler = (a) => string.Join(Environment.NewLine, a),
            Silent = false
        }).WaitForExit();


        context.Log.Information($"Uploading ax project '{workingDirectory}' at {targetIp}");

        context.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
    }
}
                $" sld -t {targetIp} -i {targetPlatform} --accept-security-disclaimer --default-server-interface -r",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false
        }).WaitForExit();
    }
}

[TaskName("CreateArtifacts")]
[IsDependentOn(typeof(TestsTask))]
public sealed class CreateArtifactsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublish)
        {
            context.Log.Warning($"Skipping packaging.");
            return;
        }

        PackPackages(context, Path.Combine(context.ScrDir, "AXSharp-packable-only.slnf"));
    }

    

    private static void PackPackages(BuildContext context, string solutionToPack)
    {        
        context.DotNetPack(solutionToPack, 
            new Cake.Common.Tools.DotNet.Pack.DotNetPackSettings()
        {
            OutputDirectory = Path.Combine(context.Artifacts, @"nugets"),
            NoRestore = true,
            NoBuild = false,
        });
    }
}

[TaskName("GenerateApiDocumentation")]
[IsDependentOn(typeof(CreateArtifactsTask))]
public sealed class GenerateApiDocumentationTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoDocs)
        {
            context.Log.Warning($"Skipping documentation generation.");
            return;
        }

        context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
        {
        context.PushNugetPackages("nugets");
    }
}

            //GenerateApiDocumentation(context, @$"ix.connectors\src\Ix.Connector\bin\{context.DotNetBuildSettings.Configuration}\net6.0\Ix.Connector.dll", @"Ix.Connector");
            //GenerateApiDocumentation(context, @$"ix.connectors\src\Ix.Connector.S71500.WebAPI\bin\{context.DotNetBuildSettings.Configuration}\net6.0\Ix.Connector.S71500.WebAPI.dll", @"Ix.Connector.S71500.WebAPI");

            //GenerateApiDocumentation(context, @$"ix.compiler\src\IX.Compiler\bin\{context.DotNetBuildSettings.Configuration}\net6.0\IX.Compiler.dll", @"IX.Compiler");
            //GenerateApiDocumentation(context, @$"ix.compiler\src\IX.Cs.Compiler\bin\{context.DotNetBuildSettings.Configuration}\net6.0\IX.Compiler.Cs.dll", @"IX.Compiler.Cs");

            //GenerateApiDocumentation(context, @$"ix.abstractions\src\Ix.Abstractions\bin\{context.DotNetBuildSettings.Configuration}\net6.0\Ix.Abstractions.dll", @"Ix.Abstractions");
            //GenerateApiDocumentation(context, @$"ix.blazor\src\Ix.Presentation.Blazor\bin\{context.DotNetBuildSettings.Configuration}\net6.0\Ix.Presentation.Blazor.dll", @"Ix.Presentation.Blazor");
            //GenerateApiDocumentation(context, @$"ix.blazor\src\Ix.Presentation.Blazor.Controls\bin\{context.DotNetBuildSettings.Configuration}\net6.0\Ix.Presentation.Blazor.Controls.dll", @"Ix.Presentation.Blazor.Controls");
    }

    [Obsolete("Using docfx now...", true)]
    private static void GenerateApiDocumentation(BuildContext context, string assemblyFile, string outputDocDirectory)
    {
        //context.Log.Information($"Generating documentation for {assemblyFile}");
        //var docXmlFile = Path.Combine(context.RootDir, assemblyFile);
        //var docDirectory = Path.Combine(context.ApiDocumentationDir, outputDocDirectory);
        //context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
        //{
        //    Arguments = $"xmldocmd {docXmlFile} {docDirectory}"
        //}).WaitForExit();
    }
}


[TaskName("Check license compliance")]
[IsDependentOn(typeof(GenerateApiDocumentationTask))]
public sealed class LicenseComplianceCheckTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
    }
}


[TaskName("Templates build")]
[IsDependentOn(typeof(PublishReleaseTask))]
public sealed class TemplatesUpdateAndBuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublish)
        {
            context.Log.Warning($"Skipping template package push.");
            return;
        }
            "AX.*.*", 
        var templatesDirectory = Path.Combine(context.ScrDir, "AXSharp.templates\\working\\templates");
        var templateCsProjFiles = Directory.EnumerateFiles(templatesDirectory, "*.csproj", SearchOption.AllDirectories);

        foreach (var templateCsProjFile in templateCsProjFiles)
        {
            var packagesToUpdate = new List<string>()
            {
                "AXSharp.Abstractions",
                "AXSharp.Connector",
                "AXSharp.Connector.S71500.WebAPI",
                "AXSharp.Presentation.Blazor.Controls",
                "AXSharp.Presentation.Blazor"
            };

            foreach (var packageId in packagesToUpdate)
            {
                AXSharp.nuget.update.Program.Update(new Options() { NewVersion = GitVersionInformation.SemVer, PackageId = packageId, FileToUpdate = templateCsProjFile });
            }
        }
        var templateToolDotnetTools = Directory.EnumerateFiles(templatesDirectory, "dotnet-tools.json", SearchOption.AllDirectories);
        foreach (var templateCsProjFile in templateToolDotnetTools)
        {
            var packagesToUpdate = new List<string>() { "AXSharp.ixc" };
            foreach (var packageId in packagesToUpdate)
            {
                AXSharp.nuget.update.Program.Update(new Options() { NewVersion = GitVersionInformation.SemVer, PackageId = packageId, FileToUpdate = templateCsProjFile });
            }
        }

        foreach (var template in context.GetTemplateProjects())
        {
            context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
            {
                Arguments = $" tool restore --no-cache --ignore-failed-sources ",
                WorkingDirectory = template.ax

            });
        }

        foreach (var template in context.GetTemplateProjects())
        {
            context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
            {
                Arguments = $"ixc",
                WorkingDirectory = template.ax

            });
        }
            SearchOption.AllDirectories)
            .Select(p => new FileInfo(p));

        if (licensedFiles.Count() < 5)
            throw new Exception("");


        foreach (var nugetFile in Directory.EnumerateFiles(context.Artifacts, "*.nupkg", SearchOption.AllDirectories))
        {
            using (var zip = ZipFile.OpenRead(nugetFile))
            {
                var ouptutDir = Path.Combine(context.Artifacts, "verif");
                zip.ExtractToDirectory(Path.Combine(context.Artifacts, "verif"));

                if (Directory.EnumerateFiles(ouptutDir, "*.*", SearchOption.AllDirectories)
                    .Select(p => new FileInfo(p))
                    .Any(p => licensedFiles.Any(l => l.Name == p.Name)))
                {
                    throw new Exception("");
                }

                Directory.Delete(ouptutDir, true);
            }
        }
    }
}


[TaskName("PushPackages task")]
[IsDependentOn(typeof(LicenseComplianceCheckTask))]
public sealed class PushPackages : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublish)
        {
            context.Log.Warning($"Skipping package push.");
            return;
        }

        if (Helpers.CanReleaseInternal())
        {
            foreach (var nugetFile in Directory.EnumerateFiles(Path.Combine(context.Artifacts, @"nugets"), "*.nupkg")
                         .Select(p => new FileInfo(p)))
            {
                context.DotNetNuGetPush(nugetFile.FullName,
                    new Cake.Common.Tools.DotNet.NuGet.Push.DotNetNuGetPushSettings()
                    {
                        ApiKey = Environment.GetEnvironmentVariable("GH_TOKEN"),
                        Source = "https://nuget.pkg.github.com/ix-ax/index.json",
                        SkipDuplicate = true
                    });
            }
        }
    }
}

[TaskName("Publish release")]
[IsDependentOn(typeof(PushPackages))]
public sealed class PublishReleaseTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublishRelease)
        {
            context.Log.Warning($"Skipping package release.");
            return;
        }

        if (Helpers.CanReleaseInternal())
        {
            var githubToken = context.Environment.GetEnvironmentVariable("GH_TOKEN");
            var githubClient = new GitHubClient(new ProductHeaderValue("AXSHARP"));
            githubClient.Credentials = new Credentials(githubToken);

            var release = githubClient.Repository.Release.Create(
                "ix-ax",
                "axsharp",
                new NewRelease($"{GitVersionInformation.SemVer}")
                {
                    Name = $"{GitVersionInformation.SemVer}",
                    TargetCommitish = GitVersionInformation.Sha,
                    Body = $"Release v{GitVersionInformation.SemVer}",
                    Draft = !Helpers.CanReleasePublic(),
                    Prerelease = !string.IsNullOrEmpty(GitVersionInformation.PreReleaseTag)
                }
            ).Result;
        }
    }
}



        foreach (var template in context.GetTemplateProjects())
        {
            context.DotNetBuild(Path.Combine(context.ScrDir, template.solution), context.DotNetBuildSettings);
        }

        
    }
}

[TaskName("Template tests")]
[IsDependentOn(typeof(TemplatesUpdateAndBuildTask))]
public class TemplateTests : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublish || !context.BuildParameters.DoTest )
        {
            context.Log.Warning($"Skipping template package push.");
            return;
        }

        if (context.BuildParameters.TestLevel >= 3)
        {
            foreach (var template in context.GetTemplateProjects())
            {
                context.UploadTestPlc(
                    Path.GetFullPath(Path.Combine(template.ax)),
                    Environment.GetEnvironmentVariable("AXTARGET"),
                    Environment.GetEnvironmentVariable("AXTARGETPLATFORMINPUT"));

                // context.DotNetRun(template.approject, context.DotNetRunSettings);
            }
        }
    }
}

[TaskName("Templates pack")]
[IsDependentOn(typeof(TemplateTests))]
public class TemplatesPackTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPack)
        {
            context.Log.Warning($"Skipping template build.");
            return;
        }

        PackTemplatePackages(context,
            Path.Combine(context.ScrDir, "AXSharp.templates\\working\\AXSharp.templates.sln"));
        context.PushNugetPackages("templates");
    }

    private static void PackTemplatePackages(BuildContext context, string solutionToPack)
        {
            context.DotNetPack(solutionToPack,
                new Cake.Common.Tools.DotNet.Pack.DotNetPackSettings()
                {
                    OutputDirectory = Path.Combine(context.Artifacts, @"templates"),
                    Sources = new List<string>() { Path.Combine(context.Artifacts, "templates") },
                    NoRestore = false,
                    NoBuild = false,
                });
        }

}

[TaskName("Templates push")]
[IsDependentOn(typeof(TemplatesPackTask))]
public class TemplatesPush : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            if (!context.BuildParameters.DoPublish)
            {
                context.Log.Warning($"Skipping template build.");
                return;
            }

            context.PushNugetPackages("templates");
        }
    }

[TaskName("Default")]
[IsDependentOn(typeof(TemplatesPush))]
public class DefaultTask : FrostingTask
{
}
