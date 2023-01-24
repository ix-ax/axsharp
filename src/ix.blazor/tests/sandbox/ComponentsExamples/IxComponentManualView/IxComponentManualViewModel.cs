// ComponentsExamples
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ix.Presentation;
using Ix.Presentation.Blazor.Interfaces;

namespace ax_blazor_example
{
    public class IxComponentManualViewModel : RenderableViewModelBase
    {
        public IxComponentManualViewModel()
        {
        }
        public IxComponent Component { get; private set; }
        public override object Model { get => this.Component; set { this.Component = value as IxComponent; } }

        public string ViewModelProperty { get; set; } = "ahoj";
    }
}
