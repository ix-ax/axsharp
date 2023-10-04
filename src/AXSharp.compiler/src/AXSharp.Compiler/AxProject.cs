// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;
using System;

namespace AXSharp.Compiler;

/// <summary>
///     Contains information about AX project.
/// </summary>
public class AxProject
{
    /// <summary>
    ///     Creates new instance of <see cref="AxProject" />.
    ///     <remarks>
    ///         It is expected that the project folder contains `apax.yml` file and `src` folder containing the sources of the
    ///         project.
    ///         It is also expected that the `.apax` folder contains the dependencies of the project.
    ///     </remarks>
    /// </summary>
    /// <param name="axProjectFolder">AX project folder.</param>
    public AxProject(string axProjectFolder)
    {
        ProjectFolder = axProjectFolder;
        ProjectFile = Path.Combine(ProjectFolder, "apax.yml");
        SrcFolder = Path.Combine(axProjectFolder, "src");
        ProjectInfo = Apax.CreateApaxDto(ProjectFile);
        Sources = Directory.GetFiles(Path.Combine(ProjectFolder, "src"), "*.st", SearchOption.AllDirectories)
            .Select(p => new SourceFileText(p));
    }

    /// <summary>
    ///     Creates new instance of <see cref="AxProject" />.
    ///     <remarks>
    ///         It is expected that the project folder contains `apax.yml` file and `src` folder containing the sources of the
    ///         project.
    ///         It is also expected that the `.apax` folder contains the dependencies of the project.
    ///     </remarks>
    /// </summary>
    /// <param name="axProjectFolder">AX project folder.</param>
    /// <param name="sourceFiles">List of source files to be included in the compilation.</param>
    public AxProject(string axProjectFolder, string[] sourceFiles)
    {
        ProjectFolder = axProjectFolder;
        ProjectFile = Path.Combine(ProjectFolder, "apax.yml");
        SrcFolder = Path.Combine(axProjectFolder, "src");
        ProjectInfo = Apax.CreateApaxDto(ProjectFile);
        Sources = sourceFiles.Select(p => new SourceFileText(p));
    }

    /// <summary>
    ///     Get apax project information.
    /// </summary>
    public Apax ProjectInfo { get; }

    /// <summary>
    ///     Gets project folder.
    /// </summary>
    public string ProjectFolder { get; }

    /// <summary>
    ///     Gets project file.
    /// </summary>
    public string ProjectFile { get; }

    /// <summary>
    ///     Get sources of the this instance of the <see cref="AxProject" />.
    /// </summary>
    public IEnumerable<SourceFileText> Sources { get; }

    /// <summary>
    ///     Gets the location of the source folder of this AX project.
    /// </summary>
    public string SrcFolder { get; }

    /// <summary>
    /// Contains list of near-by project in the directory structure (-2 levels up).
    /// </summary>
    private static List<NearByProjects> nearByProjects;

    private class NearByProjects
    {
        public Apax Apax { get; set; }
        public FileInfo ApaxFile { get; set; }
    }

    private class InstalledDependencies
    {
        public Apax Apax { get; set; }
        public CompanionInfo Companion { get; set; }

        public FileInfo ApaxFile { get; set; }
    }

    /// <summary>
    /// Gets paths of this project's references to other ix projects.
    /// </summary>
    public IEnumerable<object> AXSharpReferences => GetProjectDependencies();

    private IEnumerable<object> GetProjectDependencies()
    {
        var dependencies = ProjectInfo.Dependencies ?? new Dictionary<string, string>();
        var installedDependencies =
            dependencies.Select(p => Path.Combine(ProjectFolder, ".apax", "packages",
                    p.Key.Replace('/', Path.DirectorySeparatorChar)))
                .Select(p => new InstalledDependencies()
                {
                    Apax = Apax.TryCreateApaxDto(Path.Combine(p, "apax.yml")),
                    Companion = CompanionInfo.TryFromFile(Path.Combine(p, CompanionInfo.COMPANIONS_FILE_NAME)),
                    ApaxFile = new FileInfo(Path.Combine(p, "apax.yml"))
                }).ToList();


        nearByProjects ??= Directory.EnumerateFiles(
                Path.GetFullPath(Path.Combine(this.ProjectFolder, "../../..")),
                "apax.yml", SearchOption.AllDirectories)
            .Select(p => new FileInfo(p))
            .Where(p => !p.Directory.FullName.Contains(".apax"))
            .Select(a => new NearByProjects() { Apax = Apax.TryCreateApaxDto(a.FullName), ApaxFile = a })
            .ToList();

        var projectDependencies = new List<object>();

        foreach (var dependency in dependencies)
        {
            var hasSuchProject =
                nearByProjects.FirstOrDefault(p => p.Apax.Name == dependency.Key && p.Apax.Version == dependency.Value);
            if (hasSuchProject != null)
            {
                var pathAXSharpConfig =
                    Path.Combine(hasSuchProject.ApaxFile.Directory.FullName, AXSharpConfig.CONFIG_FILE_NAME);
                if (File.Exists(pathAXSharpConfig))
                {
                    projectDependencies.Add((AXSharpConfig.RetrieveAXSharpConfig(pathAXSharpConfig)));
                }
            }
        }

        foreach (var dependency in dependencies)
        {
            var dependencyWithCompanion = installedDependencies
                .FirstOrDefault(p => p.Apax != null && p.Apax.Name == dependency.Key && p.Apax.Version == dependency.Value);


            if (dependencyWithCompanion?.Companion != null)
            {
                var packageFile =
                    Path.Combine(dependencyWithCompanion.ApaxFile.Directory.FullName, "package.json");
                if(File.Exists(packageFile))
                    projectDependencies.Add(dependencyWithCompanion.Companion);
            }
        }

        if (!projectDependencies.Any())
        {
            Log.Logger.Information("Retrieving possible project references from .apax packages did not produce results. " +
                                   "If you have referenced AX# projects, the packages must be previously installed by 'apax install'");
        }

        return projectDependencies;
    }
}