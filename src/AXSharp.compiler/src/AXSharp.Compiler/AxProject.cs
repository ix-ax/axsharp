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

    private static string GetStartDirectory(string givenDirectory, int levelsUp)
    {
        try
        {
            // Move 'levelsUp' safely
            DirectoryInfo dirInfo = new DirectoryInfo(givenDirectory);

            for (int i = 0; i < levelsUp; i++)
            {
                if (dirInfo.Parent != null)
                {
                    dirInfo = dirInfo.Parent;
                }
                else
                {
                    return dirInfo.FullName; // Return root if we hit it before moving the desired levels up
                }
            }

            return dirInfo.FullName;
        }
        catch
        {
            return null; // return null if any error occurs
        }
    }

    private static IEnumerable<string> SearchForApaxFiles(string directory, int currentDepth, int maxDepth)
    {
        var apaxFilesList = new List<string>();

        if (currentDepth > maxDepth) return apaxFilesList;

        try
        {
            apaxFilesList.AddRange(Directory.GetFiles(directory, "apax.yml"));

            foreach (var subDir in Directory.GetDirectories(directory))
            {
                // Exclude '.apax' directories
                if (Path.GetFileName(subDir) != ".apax")
                {
                    apaxFilesList.AddRange(SearchForApaxFiles(subDir, currentDepth + 1, maxDepth));
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Handle permissions issues, if any
            Console.WriteLine($"Access denied to: {directory}");
        }

        return apaxFilesList;
    }

    static bool AreVersionsCompatible(string v1, string v2)
    {
        bool v1SemverCompliant = Version.TryParse(v1, out Version? versionA);
        bool v2SemverCompliant = Version.TryParse(v2, out Version? versionB);
        

        if(!v1SemverCompliant || !v2SemverCompliant)
        {
            return v1?.Trim() == v2?.Trim();
        }


        if (v1.StartsWith("^") || v2.StartsWith("^"))
        {
            if (versionA.Major == 0 || versionB.Major == 0)
            {
                // Compare both major and minor for versions starting with 0
                return versionA.Major == versionB.Major && versionA.Minor == versionB.Minor;
            }
            else
            {
                // Compare only major for other versions
                return versionA.Major == versionB.Major;
            }
        }
        else if (v1.StartsWith("~") || v2.StartsWith("~"))
        {
            // Compare both major and minor for tilde versions
            return versionA.Major == versionB.Major && versionA.Minor == versionB.Minor;
        }
        else
        {
            // Direct version comparison if no symbol is used
            return versionA.Equals(versionB);
        }
    }

    static Version ParseVersion(string versionString)
    {
        // Check for caret or tilde and remove it
        if (versionString.StartsWith("^") || versionString.StartsWith("~"))
        {
            versionString = versionString.Substring(1);
        }

        // Parsing version string and creating a Version object
        return Version.Parse(versionString);
    }



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

        nearByProjects = SearchForApaxFiles(GetStartDirectory(this.ProjectFolder, 2), 0, 2)
            .Select(p => new FileInfo(p))
            .Where(p => !p.Directory.FullName.Contains(".apax"))
            .Select(a => new NearByProjects() { Apax = Apax.TryCreateApaxDto(a.FullName), ApaxFile = a })
            .ToList(); ;

        var projectDependencies = new List<object>();

        foreach (var dependency in dependencies)
        {
            var hasSuchProject =
                nearByProjects.FirstOrDefault(p => p.Apax.Name == dependency.Key && AreVersionsCompatible(p.Apax.Version, dependency.Value));
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
                .FirstOrDefault(p => p.Apax != null && p.Apax.Name == dependency.Key && AreVersionsCompatible(p.Apax.Version, dependency.Value));


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