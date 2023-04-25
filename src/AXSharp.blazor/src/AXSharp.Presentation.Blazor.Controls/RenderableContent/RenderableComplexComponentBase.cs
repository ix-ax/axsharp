// AXSharp.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;
using AXSharp.Presentation.Blazor.Interfaces;

namespace AXSharp.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  Base class for complex componenets with only code-behind.
    /// </summary>
    public class RenderableComplexComponentBase<T> : RenderableComponentBase, 
        IRenderableComplexComponentBase, 
        IDisposable
    {
        [Parameter]
        public T Component { get; set; }

        [Parameter] public int PollingInterval { get; set; } = 250;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            (Component as ITwinElement)?.StartPolling(PollingInterval);
        }

        public virtual void Dispose()
        {
            (Component as ITwinElement)?.StopPolling();
        }
    }
}
