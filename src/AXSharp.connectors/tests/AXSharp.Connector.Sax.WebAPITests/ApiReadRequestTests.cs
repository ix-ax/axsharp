﻿// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Xunit;
using AXSharp.Connector.S71500.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Connector.S71500.WebApi.Tests
{
    public class ApiReadRequestTests
    {
        [Fact()]
        public void should_create_instance_with_given_paramters()
        {
            var expectedSymbol = "some.sybmol";
            var expectedMethod = "PlcProgram.Read";
            var actual = new ApiPlcReadRequest(expectedSymbol);

            Assert.Equal(actual.Params["var"], expectedSymbol);
            Assert.Equal(actual.Method, expectedMethod);
        }
    }
}