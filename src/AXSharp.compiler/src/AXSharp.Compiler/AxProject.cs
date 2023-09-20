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
        ProjectInfo = Apax.CreateApax(ProjectFile);
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
        ProjectInfo = Apax.CreateApax(ProjectFile);
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
    /// Gets paths of this project's references to other ix projects.
    /// </summary>
    public IEnumerable<AXSharpConfig> AXSharpReferences
    {
        get
        {
            var dependencies = ProjectInfo.Dependencies ?? new Dictionary<string, string>();

            var packagesDirectories = 
                dependencies.Select(p => Path.Combine(ProjectFolder, ".apax", "packages", p.Key.Replace('/',Path.DirectorySeparatorChar)));

            
            var retVal = packagesDirectories
                .Where(p => Directory.Exists(p))
                .Select(p => new DirectoryInfo(p))
                .Select(p => Directory.EnumerateFiles(p.LinkTarget ?? p.FullName, AXSharpConfig.CONFIG_FILE_NAME, SearchOption.TopDirectoryOnly))
                .SelectMany(p => p).Select(c => AXSharpConfig.RetrieveIxConfig(c));

            if (retVal.Count() == 0)
            {
                Log.Logger.Information("Retrieving possible project references from .apax packages did not produce results. " +
                                       "If you have referenced AX# projects, the packages must be previously installed by 'apax install'");
            }

            return retVal;
        }
    }
}