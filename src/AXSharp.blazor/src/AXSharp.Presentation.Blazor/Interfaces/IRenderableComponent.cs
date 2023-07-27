// AXSharp.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector;

namespace AXSharp.Presentation.Blazor.Interfaces
{
    public interface IRenderableComponent
    {
        void AddToPolling(ITwinElement element, int pollingInterval = 250);

        void RemovePolledElements();

    }
}
