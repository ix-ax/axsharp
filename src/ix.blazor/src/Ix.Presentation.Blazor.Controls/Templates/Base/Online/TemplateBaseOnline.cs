using Ix.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ix.Presentation.Blazor.Controls.Templates.Base.Online
{
    public partial class TemplateBaseOnline<T>
    {
        [Parameter]
        public OnlinerBase<T> Onliner { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        protected T Value
        {
            get
            {
                return Onliner.Cyclic;
            }
            set
            {
                Onliner.Edit = value;
            }
        }

        internal string AccessStatus { get; set; }
        internal string ComponentId { get; set; }
     
      
        protected override Task OnInitializedAsync()
        {

            UpdateValuesOnChangeOutFocus(Onliner);
            AccessStatus = Onliner.AccessStatus.Failure ? "is-invalid" : "";
            ComponentId = Onliner.GetSymbolTail() + "_" + Guid.NewGuid().ToString();
            return base.OnInitializedAsync();
        }

       

    }
}
