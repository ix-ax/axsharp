using AXSharp.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AXSharp.Presentation.Blazor.Controls.Templates
{
    public partial class TemplateBaseOnline<T> : TemplateBase<T>
    {
           
        protected override Task OnInitializedAsync()
        {
            AddToPolling(this.Onliner, this.PollingInterval);
            UpdateValuesOnChangeOutFocus(Onliner);
            return base.OnInitializedAsync();
        }

       

    }
}
