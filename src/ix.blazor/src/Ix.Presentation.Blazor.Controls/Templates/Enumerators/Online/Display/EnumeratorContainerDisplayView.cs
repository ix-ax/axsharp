// Ix.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using Microsoft.AspNetCore.Components;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using Ix.Presentation.Blazor;

namespace Ix.Presentation.Blazor.Controls.Templates.Enumerators.Online.Display
{
    public partial class EnumeratorContainerDisplayView<T>
    {
        [Parameter]
        public OnlinerBase<T> Onliner { get; set; }

        [Parameter]
        public EnumeratorDiscriminatorAttribute EnumDiscriminatorAttribute { get; set; }

       
        public EnumToIntConverter EnumToIntConverter { get; set; }

        protected override void OnInitialized()
        {
            EnumToIntConverter = new EnumToIntConverter(EnumDiscriminatorAttribute);
            UpdateValuesOnChange(Onliner);
            base.OnInitialized();
        }
    }
}


