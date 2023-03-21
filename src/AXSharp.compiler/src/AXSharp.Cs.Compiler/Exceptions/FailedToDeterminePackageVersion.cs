// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace AXSharp.Compiler;

public class FailedToDeterminePackageVersion : Exception
{
    public FailedToDeterminePackageVersion(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}