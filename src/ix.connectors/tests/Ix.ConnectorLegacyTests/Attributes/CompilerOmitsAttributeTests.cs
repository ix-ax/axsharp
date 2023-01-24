// Ix.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ix.Connector;
using Ix.Connector.Attributes;

namespace Ix.ConnectorTests.Attributes
{
    [TestFixture]
    public class CompilerOmitsAttributeTests
    {
        [Test]
        public void CompilerOmitsAttributeCtorTest()
        {
            //-- Act

            var actual = new CompilerOmitsAttribute(CompilerOmissionGroups.BuilderOnliner, 
                                                    CompilerOmissionGroups.BuilderPlainer,
                                                    CompilerOmissionGroups.BuilderOnlinerInterface,
                                                    CompilerOmissionGroups.BuilderShadowerInterface);

            //-- Assert
            Assert.AreEqual(actual.Omissions.Count(), 4);
            Assert.AreEqual("BuilderOnliner", actual.Omissions.ToArray()[0]);
            Assert.AreEqual("BuilderPlainer", actual.Omissions.ToArray()[1]);
            Assert.AreEqual("BuilderOnlinerInterface", actual.Omissions.ToArray()[2]);
            Assert.AreEqual("BuilderShadowerInterface", actual.Omissions.ToArray()[3]);

        }
    }
}
