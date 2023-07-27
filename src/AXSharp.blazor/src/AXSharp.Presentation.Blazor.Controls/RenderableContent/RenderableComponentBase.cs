﻿// AXSharp.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using AXSharp.Presentation.Blazor.Interfaces;
using AXSharp.Connector.ValueTypes.Online;
using System.Xml.Linq;
using Serilog;

namespace AXSharp.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  Base class which implements methods to update UI when PLC values are changed.
    /// </summary>
    public partial class RenderableComponentBase : ComponentBase, IRenderableComponent, IDisposable
    {
        [Parameter] public int PollingInterval { get; set; }

        [Parameter] public bool AutoSubscribe { get; set; } = true;

        ///<inheritdoc/>        
        public virtual void Dispose()
        {
            RemovePolledElements();
        }

        private HashSet<ITwinElement> PolledElements { get; } = new HashSet<ITwinElement>();

        public bool HasFocus { get; set; }

        public void AddToPolling(ITwinElement element, int pollingInterval = 250)
        {
            if (AutoSubscribe)
            {
                element.StartPolling(pollingInterval, this);
                PolledElements.Add(element);
            }
        }

        public void RemovePolledElements()
        {
            PolledElements.ToList().ForEach(p =>
            {
                p.StopPolling(this);
            });

            PolledElements.Clear();
        }

        /// <summary>
        ///  Method, which updates are primitive values of ITwinObject instance
        /// <param name="element">ITwinObject instance.</param>
        /// <param name="pollingInterval">Polling interval</param>
        /// </summary>
        private void UpdateValuesOnChange(ITwinObject element, int pollingInterval = 250)
        {
            if (element != null)
            {
                foreach (var twinPrimitive in element.RetrievePrimitives())
                {
                    var tag = (OnlinerBase)twinPrimitive;
                    tag.PropertyChanged += new PropertyChangedEventHandler(HandlePropertyChanged);
                }
            }
        }

        /// <summary>
        ///  Method, which updates primitive value.
        /// <param name="tag">IValueTag instance.</param>
        /// <param name="pollingInterval">Polling interval</param>
        /// </summary>
        private void UpdateValuesOnChange(OnlinerBase tag, int pollingInterval = 250)
        {
            tag.PropertyChanged += new PropertyChangedEventHandler(HandlePropertyChanged);
        }

        /// <summary>
        ///  Method, which updates are primitive values of ITwinObject instance
        /// <param name="element">ITwinElement instance.</param>
        /// <param name="pollingInterval">Polling interval</param>
        /// </summary>
        public void UpdateValuesOnChange(ITwinElement element, int pollingInterval = 250)
        {
            AddToPolling(element, pollingInterval);

            switch (element)
            {
                case ITwinObject o:
                    UpdateValuesOnChange(o, pollingInterval);
                    break;
                case OnlinerBase b:
                    UpdateValuesOnChange(b, pollingInterval);
                    break;
            }
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
            if(!HasFocus) InvokeAsync(StateHasChanged);
        }
    }
}
