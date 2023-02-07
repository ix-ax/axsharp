using Ix.Connector.ValueTypes;
using Ix.Presentation.Blazor.Controls.RenderableContent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ix.Presentation.Blazor.Controls.Templates
{
    public partial class TemplateBase<T> : RenderableComponentBase
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
        internal string OnlinerSymbol { get => Onliner.Symbol.Replace(".", "-"); } 
        protected override Task OnInitializedAsync()
        {
            AccessStatus = Onliner.AccessStatus.Failure ? "is-invalid" : "";
            ComponentId = Onliner.GetSymbolTail() + "_" + Guid.NewGuid().ToString();
            return base.OnInitializedAsync();
        }
    }
}
