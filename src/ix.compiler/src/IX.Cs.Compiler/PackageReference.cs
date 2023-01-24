// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Xml.Linq;
using Ix.Compiler.Cs.Exceptions;
using NuGet.Configuration;
using NuGet.Packaging.Core;

namespace Ix.Compiler;

/// <summary>
///     Provides information about package reference from csproj file.
/// </summary>
public class PackageReference : IPackageReference
{
    private static readonly string NugetDir =
        SettingsUtility.GetGlobalPackagesFolder(Settings.LoadDefaultSettings(null));

    /// <summary>
    ///     Creates new instance of <see cref="PackageReference" />
    /// </summary>
    /// <param name="packageReferenceNode">Package reference node from csproj file.</param>
    /// <param name="projectFile">Csproj file.</param>
    /// <exception cref="FailedToRetrievePackageReferenceException"></exception>
    public PackageReference(XElement packageReferenceNode, string projectFile)
    {
        try
        {
            Include = packageReferenceNode.Attribute(XName.Get(nameof(Include)))?.Value ?? packageReferenceNode.Attribute(XName.Get("Update"))?.Value;
            Version = packageReferenceNode.Attribute(XName.Get(nameof(Version))).Value;
            ReferencePath = PackageReferenceNugetPath(this);
        }
        catch (Exception ex)
        {
            throw new FailedToRetrievePackageReferenceException(
                $"Could not parse 'Name' or 'Version' of the package '{Include}' reference in project {projectFile}",
                ex);
        }
    }

    /// <summary>
    ///     Create new instance of <see cref="PackageReference" /> from <see cref="PackageDependency" />
    /// </summary>
    /// <param name="packageDependency">Package dependency</param>
    public PackageReference(PackageDependency packageDependency)
    {
        Include = packageDependency.Id;
        Version = packageDependency.VersionRange.OriginalString;
        ReferencePath = PackageReferenceNugetPath(this);
    }

    /// <summary>
    ///     Gets package name.
    /// </summary>
    public string Include { get; internal set; }

    /// <summary>
    ///     Gets package name.
    /// </summary>
    public string Name => Include;

    /// <summary>
    ///     Gets package version.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    ///     Gets reference path.
    /// </summary>
    public string ReferencePath { get; }

    private static string PackageReferenceNugetPath(PackageReference package)
    {
        return Path.Combine(NugetDir, package.Name, CsProject.GetBestMatchedVersion(package));
    }
}