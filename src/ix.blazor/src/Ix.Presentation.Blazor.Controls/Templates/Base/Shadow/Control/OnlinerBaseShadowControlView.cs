// Ix.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Ix.Connector.ValueTypes;

namespace Ix.Presentation.Blazor.Controls.Templates.Base.Shadow.Control
{
    public partial class OnlinerBaseShadowControlView<T> 
    {
        [Parameter]
        public OnlinerBase<T> Onliner { get; set; }

        private string id = Guid.NewGuid().ToString();

        protected override void OnInitialized()
        {
            UpdateShadowValuesOnChange(Onliner);
            
        }
        private bool IsNumeric(Type type)
        {
            if (type == null) return false;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
            }
            return false;
        }

    }
}
