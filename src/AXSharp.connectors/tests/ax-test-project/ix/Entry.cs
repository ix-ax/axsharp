﻿// ax_test_project
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebApi;


namespace exploratory
{
    public static class Entry
    {
        public static ax_test_projectTwinController Plc { get; } = new ax_test_projectTwinController(new ConnectorAdapter(typeof(WebApiConnectorFactory)), new object[] { "192.168.0.1", "Everybody", "" });
    }
}
