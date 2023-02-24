// Ix.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Runtime.Serialization;

namespace Ix.Compiler;

#pragma warning disable CS1591
[Serializable]
public class FailedToCreateSourceBuilderException : Exception, ISerializable
{
    public FailedToCreateSourceBuilderException(string s) : base(s)
    {
    }
}
#pragma warning restore CS1591