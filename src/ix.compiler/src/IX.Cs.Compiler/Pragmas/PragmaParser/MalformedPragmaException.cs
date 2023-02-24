﻿// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Irony.Parsing;

namespace Ix.Compiler.Cs.Pragmas.PragmaParser;

/// <summary>
/// Provides information about failure to parse pragma expression.
/// </summary>
internal class MalformedPragmaException : Exception
{
    public MalformedPragmaException(string message) :base(message)
    {
        
    }
}