// Ix.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace Ix.ConnectorTests
{
    using Ix.Connector;
    using System;
    using Xunit;

    public class InterpolatedAncestorTests
    {
        private InterpolatedAncestor _testClass;

        public InterpolatedAncestorTests()
        {
            _testClass = new InterpolatedAncestor();
        }

        [Fact]
        public void CanSetAndGetAncestor()
        {
            // Arrange
            var testValue = 1644023009;

            // Act
            _testClass.Ancestor = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.Ancestor);
        }

        [Fact]
        public void CanSetAndGetInterpolated()
        {
            // Arrange
            var testValue = "TestValue1381599442";

            // Act
            _testClass.Interpolated = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.Interpolated);
        }
    }
}