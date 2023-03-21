// ix-integration-plc
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

//#define dummy

using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebApi;

namespace ix_integration_plc
{
    public static class Entry
    {
#if !dummy
        public static ix_integration_plcTwinController Plc { get; } = new (ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AXTARGET"), "Everybody", "", true));
#else
        public static ix_integration_plcTwinController Plc { get; } = new(ConnectorAdapterBuilder.Build().CreateDummy());
#endif
    }
}
