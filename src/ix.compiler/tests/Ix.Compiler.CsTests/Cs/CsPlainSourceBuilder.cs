// Ix.Compiler.CsTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Compiler.Cs.Plain;
using Xunit.Abstractions;

namespace Ix.Compiler.CsTests;

public class CsPlainSourceBuilderTests : CsSourceBuilderTests
{
    public CsPlainSourceBuilderTests(ITestOutputHelper output) : base(output)
    {
        OutputSubFolder = "POCO";
        builders = new[] { typeof(CsPlainSourceBuilder) };
    }
}