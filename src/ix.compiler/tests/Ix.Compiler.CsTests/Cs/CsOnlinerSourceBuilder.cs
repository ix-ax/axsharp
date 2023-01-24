// Ix.Compiler.CsTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Compiler.Cs.Onliner;
using Xunit.Abstractions;

namespace Ix.Compiler.CsTests;

public class CsOnlinerSourceBuilderTests : CsSourceBuilderTests
{
    public CsOnlinerSourceBuilderTests(ITestOutputHelper output) : base(output)
    {
        OutputSubFolder = "Onliners";
        builders = new[] { typeof(CsOnlinerSourceBuilder) };
    }
}