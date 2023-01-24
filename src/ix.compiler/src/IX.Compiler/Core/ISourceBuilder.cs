// Ix.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace Ix.Compiler.Core;

/// <summary>
///     Provides abstraction for source emitting classes.
/// </summary>
public interface ISourceBuilder
{
    /// <summary>
    ///     Gets string output of the builder.
    /// </summary>
    string Output { get; }

    /// <summary>
    ///     Get source group name. The output of this builder will be places into sub-folder name after this
    ///     <see cref="Group" /> property.
    /// </summary>
    string Group { get; }

    /// <summary>
    ///     Suffix of output files.
    /// </summary>
    string OutputFileSuffix { get; }
}