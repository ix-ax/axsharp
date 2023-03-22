﻿// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Text;

namespace AXSharp.Abstractions.Presentation
{
    public class EmptyPresentationProvider : ILayoutProvider
    {
        public (string assembly, string fullTypeName) GetControl(Layout layoutType)
        {
            //throw new PresentationProviderNotAssignedException("Presentation provider was not created. " +
            //                                                           "You must assign appropriate presentation provider by using 'AXSharp.Abstractions.Presentation.PresentationProvider.Create method'");

            return ("Presentation provider was not created.",
                    "You must assign appropriate presentation provider by using 'AXSharp.Abstractions.Presentation.PresentationProvider.Create method'");

        }

    }
}
