// Ix.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;


[assembly: TestCollectionOrderer("Ix.Connector.Sax.WebAPITests.DisplayNameOrderer", "Ix.Connector.Sax.WebAPITests")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Ix.Connector.Sax.WebAPITests;

public class DisplayNameOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        return testCollections.OrderBy(collection => collection.DisplayName);
    }
}