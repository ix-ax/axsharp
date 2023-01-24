// Ix.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Text;
using Ix.Connector;

namespace Ix.Presentation.Blazor
{
    public class TwinContainerObject
    {

        public TwinContainerObject(ITwinObject twin, string id)
        {
            Twin = twin;
            Id = id;
        }
        public ITwinObject Twin { get;  }
        public string Id { get;  }

    }
}
