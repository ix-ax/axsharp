using Ix.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.Presentation.Blazor.Controls.Templates.Base.Shadow
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
