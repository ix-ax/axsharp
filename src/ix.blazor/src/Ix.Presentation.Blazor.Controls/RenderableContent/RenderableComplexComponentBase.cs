// Ix.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ix.Connector;
using Ix.Presentation.Blazor.Interfaces;

namespace Ix.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  Base class for complex componenets with only code-behind.
    /// </summary>
    public class RenderableComplexComponentBase<T> : RenderableComponentBase, IRenderableComplexComponentBase
    {
        [Parameter]
        public T Component { get; set; }
    }
}
