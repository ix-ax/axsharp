// Ix.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.Presentation.UIExceptions
{
    public static class UIExceptionHandling
    {
        public delegate void AppUiExceptionHandlerDelegate(object sender, Exception e);

        public static AppUiExceptionHandlerDelegate AppUiExceptionHandler;
    }
}
