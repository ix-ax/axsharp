using Ix.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.Presentation.Blazor.Controls.Templates.Base
{
    public partial class TemplateBase<T>
    {
        [Parameter]
        public OnlinerBase<T> Onliner { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        internal string AccessStatus { get; set; }
        internal string ComponentId { get; set; }

        protected override void OnInitialized()
        {
            UpdateValuesOnChange(Onliner);
            AccessStatus = Onliner.AccessStatus.Failure ? "is-invalid" : "";
            ComponentId = Onliner.AttributeName + "_" + Guid.NewGuid().ToString();
        }
    }
    
}
