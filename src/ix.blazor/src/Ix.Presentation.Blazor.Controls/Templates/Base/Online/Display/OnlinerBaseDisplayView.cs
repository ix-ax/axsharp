// Ix.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Ix.Connector.ValueTypes;
using System;

namespace Ix.Presentation.Blazor.Controls.Templates.Base.Online.Display
{
    public partial class OnlinerBaseDisplayView<T> 
    {
        [Parameter]
        public OnlinerBase<T> Onliner { get; set; }

        private string id = Guid.NewGuid().ToString();

        protected override void OnInitialized()
        {
            UpdateValuesOnChange(Onliner);
        }


    }
}
