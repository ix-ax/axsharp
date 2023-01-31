using Ix.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.Presentation.Blazor.Controls.Templates.Base.Shadow
{
    public partial class TemplateBaseShadow<T>
    {
        [Parameter]
        public OnlinerBase<T> Onliner { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        internal string AccessStatus { get; set; }
        internal string ComponentId { get; set; }

        protected override void OnInitialized()
        {
            UpdateShadowValuesOnChange(Onliner);
            AccessStatus = Onliner.AccessStatus.Failure ? "is-invalid" : "";
            ComponentId = Onliner.GetSymbolTail() + "_" + Guid.NewGuid().ToString();
        }
    }
}
