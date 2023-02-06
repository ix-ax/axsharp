﻿using Ix.Connector;
using Ix.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.Presentation.Blazor.Controls.Templates.Enumerators.Online
{
    public partial class EnumeratorsBaseOnline<T> : TemplateBase<T>
    {
     
        [Parameter]
        public EnumeratorDiscriminatorAttribute EnumDiscriminatorAttribute { get; set; }
       
        public Array Names { get; set; }
        public EnumToIntConverter EnumToIntConverter { get; set; }
        protected override Task OnInitializedAsync()
        {
            EnumToIntConverter = new EnumToIntConverter(EnumDiscriminatorAttribute);
            Names = Enum.GetNames(EnumDiscriminatorAttribute.EnumeratorType);
            UpdateValuesOnChange(Onliner);
            return base.OnInitializedAsync();
        }

    }
}
