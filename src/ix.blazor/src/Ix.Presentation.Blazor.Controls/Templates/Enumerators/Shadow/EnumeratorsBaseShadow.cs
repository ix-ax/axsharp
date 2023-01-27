using Ix.Connector;
using Ix.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.Presentation.Blazor.Controls.Templates.Enumerators.Shadow
{
    public partial class EnumeratorsBaseShadow<T>
    {
        [Parameter]
        public OnlinerBase<T> Onliner { get; set; }
        [Parameter]
        public EnumeratorDiscriminatorAttribute EnumDiscriminatorAttribute { get; set; }
        [Parameter]
        public bool IsReadOnly { get; set; }
        public string AccessStatus { get; set; }
        public Array Names { get; set; }
        public EnumToIntConverter EnumToIntConverter { get; set; }
        internal string ComponentId { get; set; }
        protected override void OnInitialized()
        {
            EnumToIntConverter = new EnumToIntConverter(EnumDiscriminatorAttribute);
            Names = Enum.GetNames(EnumDiscriminatorAttribute.EnumeratorType);
            UpdateValuesOnChange(Onliner);
            AccessStatus = Onliner.AccessStatus.Failure ? "is-invalid" : "";
            ComponentId = Onliner.GetSymbolTail() + "_" + Guid.NewGuid().ToString();
            base.OnInitialized();
        }
    }
}
