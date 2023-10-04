// AXSharp.ixc
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using CommandLine;
using AXSharp.Compiler;

namespace ixc;

/// <summary>
///     Options for ixc cli.
/// </summary>
internal class Options : ICompilerOptions
{
    [Option('x', "source-project-folder", Required = false, HelpText = "Simatic-ax project folder")]
    public string? AxSourceProjectFolder { get; set; }

    [Option('o', "output-project-folder", Required = false,
        HelpText = "Output project folder where compiler emits result. It must be either absolute path or path relative to the Simatic-ax project folder.")]
    public string? OutputProjectFolder { get; set; }

    [Option('b', "use-base-symbol", Required = false, Default = false,
        HelpText = "Will use base symbol in inherited types")]
    public bool UseBase { get; set; }

    [Option('p', "project-file", Required = false, Default = "",
        HelpText = "Output project file")]
    public string? ProjectFile { get; set; }

    [Option('u', "no-dependency-update", Required = false, Default = false,
        HelpText = "Prevent dependency of twins from apax")]
    public bool NoDependencyUpdate { get; set; }
}

