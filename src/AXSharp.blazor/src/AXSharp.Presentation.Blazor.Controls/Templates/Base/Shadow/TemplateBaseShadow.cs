using AXSharp.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Presentation.Blazor.Controls.Templates
{
    public partial class TemplateBaseShadow<T> : TemplateBase<T>
    {
        protected override Task OnInitializedAsync()
        {
            UpdateShadowValuesOnChange(Onliner);
            return base.OnInitializedAsync();
        }
    }
}
