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

namespace Ix.Presentation.Blazor.Controls.Templates.Base.Online.Control
{
    public partial class OnlinerTimeOfDayControlView
    {
        [Parameter]
        public OnlinerTimeOfDay Onliner { get; set; }

        private string id = Guid.NewGuid().ToString();

        protected override void OnInitialized()
        {
            UpdateValuesOnChange(Onliner);
        }

        private void ValueChanged(ChangeEventArgs args)
        {
            try
            {
                var parsedTimeSpan = TimeSpan.Parse(args.Value.ToString());
                Onliner.Cyclic = parsedTimeSpan;

            }
            catch
            {
                //do nothing
            }

        }
    }
}
