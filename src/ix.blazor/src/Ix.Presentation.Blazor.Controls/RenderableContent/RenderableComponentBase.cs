// Ix.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using Ix.Presentation.Blazor.Interfaces;
using Ix.Connector.ValueTypes.Online;

namespace Ix.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  Base class which implements methods to update UI when PLC values are changed.
    /// </summary>
    public partial class RenderableComponentBase : ComponentBase, IRenderableComponent
    {

        public bool IsFocus { get; set; }
        public string CssSymbol(string symbol) => symbol.Replace(".", "-");

        /// <summary>
        ///  Method, which updates are primitive values of ITwinObject instance
        /// <param name="element">ITwinObject instance.</param>
        /// </summary>
        public void UpdateValuesOnChange(ITwinObject element)
        {
            if (element != null)
            {
                var tags = element.GetValueTags();
                foreach (OnlinerBase tag in tags)
                {
                    tag.PropertyChanged += new PropertyChangedEventHandler(HandlePropertyChanged);
                   
                }
            }
        }
        /// <summary>
        ///  Method, which updates primitive value.
        /// <param name="tag">IValueTag instance.</param>
        /// </summary>
        public void UpdateValuesOnChange(OnlinerBase tag)
        {
            tag.PropertyChanged += new PropertyChangedEventHandler(HandlePropertyChanged);
        }

        /// <summary>
        ///  Method, which updates are primitive shadow values of ITwinObject
        /// <param name="element">ITwinObject instance.</param>
        /// </summary>
        public void UpdateShadowValuesOnChange(ITwinObject element)
        {
            if (element != null)
            {
                var tags = element.GetValueTags();
                foreach (dynamic tag in tags)
                {
                    tag.ShadowValueChangeEvent += new ValueChangedEventHandlerDelegate(HandleShadowPropertyChanged);
                }
            }
        }
        /// <summary>
        ///  Method, which updates shadow primitive value.
        /// <param name="tag">IValueTag instance.</param>
        /// </summary>
        public void UpdateShadowValuesOnChange(ITwinPrimitive tag)
        {
            ((dynamic)tag).ShadowValueChangeEvent += new ValueChangedEventHandlerDelegate(HandleShadowPropertyChanged);
        }


        /// <summary>
        ///  Method, which updates primitive value only, when element is out of focus.
        ///  Public property IsFocus can be set. If is true, element won't be updated.
        /// <param name="tag">IValueTag instance.</param>
        /// </summary>
        public void UpdateValuesOnChangeOutFocus(OnlinerBase tag)
        {
            tag.PropertyChanged += new PropertyChangedEventHandler(HandlePropertyChangedOnOutFocus);
        }

        protected void HandlePropertyChanged(object sender, PropertyChangedEventArgs a)
        {
            InvokeAsync(StateHasChanged);
        }

        protected void HandleShadowPropertyChanged(object sender, ValueChangedEventArgs a)
        {
            InvokeAsync(StateHasChanged);
        }

        protected void HandlePropertyChangedOnOutFocus(object sender, PropertyChangedEventArgs a)
        {
            if(!IsFocus) InvokeAsync(StateHasChanged);
        }

      
    }
}
