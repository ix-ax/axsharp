using Ix.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ix.Presentation.Blazor.Controls.Templates
{
    public partial class TemplateBaseOnline<T> : TemplateBase<T>
    {
           
        protected override Task OnInitializedAsync()
        {
            UpdateValuesOnChangeOutFocus(Onliner);
            return base.OnInitializedAsync();
        }

       

    }
}
