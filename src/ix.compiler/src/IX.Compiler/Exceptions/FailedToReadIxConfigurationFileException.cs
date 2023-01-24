// Ix.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace Ix.Compiler.Exceptions;

/// <summary>
/// Provides information about failure to process ix.config.json file.
/// </summary>
public class FailedToReadIxConfigurationFileException : Exception
{
    /// <summary>
    /// Create new instance of <see cref="FailedToReadIxConfigurationFileException"/>
    /// </summary>
    /// <param name="s">Exception details</param>
    /// <param name="exception">Inner exception</param>
    public FailedToReadIxConfigurationFileException(string s, Exception exception) : base(s, exception)
    {

    }
}