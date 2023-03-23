// Build
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Build.FilteredSolution;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNet.Run;
using Cake.Common.Tools.DotNet.Test;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;
using Path = System.IO.Path;

public class BuildContext : FrostingContext
{
    public string Artifacts  => Path.Combine(Environment.WorkingDirectory.FullPath, "..//artifacts//");

    public string TestResults => Path.Combine(Environment.WorkingDirectory.FullPath, "..//TestResults//");

    public string WorkDirName => Environment.WorkingDirectory.GetDirectoryName();

    public string DocumentationOutputDir => Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//docs//"));

    public string DocumentationSource => Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//docfx//"));

    public string ScrDir => Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//src//"));

    public Cake.Common.Tools.DotNet.Build.DotNetBuildSettings DotNetBuildSettings { get; }

    public Cake.Common.Tools.DotNet.Test.DotNetTestSettings DotNetTestSettings { get; }

    public DotNetRunSettings DotNetRunSettings { get; }

    public BuildParameters BuildParameters { get; }

    public BuildContext(ICakeContext context, BuildParameters buildParameters)
        : base(context)
    {

        BuildParameters = buildParameters;

        DotNetBuildSettings = new DotNetBuildSettings()
        {
            Verbosity = buildParameters.Verbosity,
            Configuration = buildParameters.Configuration,
            NoRestore = false,
            MSBuildSettings = new DotNetMSBuildSettings()
            {
                Verbosity = DotNetVerbosity.Quiet
            }
        };

        DotNetTestSettings = new DotNetTestSettings()
        {
            Verbosity = buildParameters.Verbosity,
            Configuration = buildParameters.Configuration,
            NoRestore = true,
            NoBuild = true,
            DiagnosticOutput = true,
            VSTestReportPath = TestResults,
        };

        DotNetRunSettings = new DotNetRunSettings()
        {
            Verbosity = buildParameters.Verbosity,
            Framework = "net6.0",
            Configuration = buildParameters.Configuration,
            NoBuild = true,
            NoRestore = true,
        };
    }

    public void UploadTestPlc(string workingDirectory, string targetIp,
        string targetPlatform)
    {

        this.Log.Information($"Installing dependencies for ax project '{workingDirectory}' at {targetIp}");




        this.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments = " install -L",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            Silent = false
        }).WaitForExit();


        this.Log.Information($"Building ax project '{workingDirectory}' at {targetIp}");




        this.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments = " apax build",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            RedirectedStandardOutputHandler = (a) => string.Join(System.Environment.NewLine, a),
            Silent = false
        }).WaitForExit();


        this.Log.Information($"Uploading ax project '{workingDirectory}' at {targetIp}");

        this.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments =
                $" sld -t {targetIp} -i {targetPlatform} --accept-security-disclaimer --default-server-interface -r",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false
        }).WaitForExit();
    }

    public void RunTestsFromFilteredSolution(string filteredSolutionFile)
    {
        foreach (var project in FilteredSolution.Parse(filteredSolutionFile).solution.projects
                     .Select(p => new FileInfo(Path.Combine(this.ScrDir, p)))
                     .Where(p => p.Name.ToUpperInvariant().Contains("TEST")))
        {
            foreach (var framework in this.TargetFrameworks)
            {
                this.DotNetTestSettings.VSTestReportPath = Path.Combine(this.TestResults, $"{project.Name}_{framework}.xml");
                this.DotNetTestSettings.Framework = framework;
                this.DotNetTest(Path.Combine(project.FullName), this.DotNetTestSettings);
            }
        }
    }

    public void PushNugetPackages(string artifactDirectory)
    {
        if (Helpers.CanReleaseInternal())
        {
            foreach (var nugetFile in Directory.EnumerateFiles(Path.Combine(this.Artifacts, artifactDirectory), "*.nupkg")
                         .Select(p => new FileInfo(p)))
            {
                this.DotNetNuGetPush(nugetFile.FullName,
                    new Cake.Common.Tools.DotNet.NuGet.Push.DotNetNuGetPushSettings()
                    {
                        ApiKey = Environment.GetEnvironmentVariable("GH_TOKEN"),
                        Source = "https://nuget.pkg.github.com/ix-ax/index.json",
                        SkipDuplicate = true
                    });
            }
        }
    }

    public IEnumerable<string> TargetFrameworks { get; } = new List<string>() { "net6.0", "net7.0" };
}