// Ix.Compiler.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

// ReSharper disable once CheckNamespace

namespace Ix.Compiler;

public interface ITargetProject
{
    /// <summary>
    ///     Get folder for project metadata.
    /// </summary>
    string GetMetaDataFolder { get; }

    string ProjectRootNamespace { get; }

    /// <summary>
    ///     Provisions project structure.
    /// </summary>
    void ProvisionProjectStructure();

    IEnumerable<IReference> LoadReferences();
}