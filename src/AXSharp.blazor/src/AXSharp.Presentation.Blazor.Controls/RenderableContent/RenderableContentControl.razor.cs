// AXSharp.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using AXSharp.Connector;
using AXSharp.Abstractions.Presentation;
using AXSharp.Presentation.Attributes;
using AXSharp.Presentation.Blazor;
using AXSharp.Presentation.Blazor.Interfaces;
using System.Runtime.CompilerServices;
using AXSharp.Presentation.Blazor.Services;
using System.Collections.Generic;
using AXSharp.Presentation.Blazor.Exceptions;
using System.Reflection;
using System.ComponentModel;
using AXSharp.Connector.ValueTypes;
using System.Xml.Linq;

namespace AXSharp.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  This class implements main logic behind auto-generated UI. 
    /// </summary>
    public partial class RenderableContentControl : ComponentBase, IDisposable
    {
        private ITwinElement _context;

        /// <summary>
        /// Gets or sets polling interval for this control and nested controls.
        /// [!NOTE] Nested element can have different polling setting that will override this property.
        /// </summary>
        [Parameter] public int PollingInterval { get; set; } = 250;

        
        /// <summary>
        /// Parameter Context accept ITwinElement instance, which is used as base model for UI generation.
        /// </summary>
        [Parameter]
        public object Context
        {
            get => _context;
            set { _context =  value as ITwinElement;  }
        }

        /// <summary>
        /// Parameter Presentation specify mode, in which view UI is generated. Type PresentationType is used.
        /// </summary>
        [Parameter]
        public string Presentation { get; set; }


        /// <summary>
        /// Parameter Class, in which RenderableContentenControl will be wrapped.
        /// </summary>
        [Parameter]
        public string Class { get; set; }
        /// <summary>
        /// Parameter LayoutClass, in which layouts will be wrapped.
        /// </summary>
        [Parameter]
        public string LayoutClass { get; set; }
        /// <summary>
        /// Parameter LayoutChildrenClass, in which children of layouts will be wrapped.
        /// </summary>
        [Parameter]
        public string LayoutChildrenClass { get; set; }

        [Inject]
        public ComponentService ComponentService { get; set; }
        [Inject]
        public AttributesHandler AttributesHandler { get; set; }
        [Inject]
        private ViewModelCacheService _viewModelCache { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        private Type _groupContainer { get; set; }
        public Type MainLayoutType { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if(_context is null)
            {
                throw new ParameterWrongTypeRendererException(Context.GetType().ToString());
            }           
        }

        protected override void OnParametersSet()
        {
            
            Type layoutType = TryLoadLayoutTypeFromProperty(_context);
            if (layoutType == null)
            {
                layoutType = TryLoadLayoutType(_context);
            }
            if (layoutType != null) MainLayoutType = layoutType;

            _groupContainer = TryLoadGroupTypeFromProperty(_context);
            if (_groupContainer == null) _groupContainer = TryLoadGroupType(_context);

            if (String.IsNullOrEmpty(Presentation)) Presentation = "";
            _viewModelCache.ResetCounter();
        }

        private IList<IRenderableComponent> PolledComponents { get; } = new List<IRenderableComponent>();

        private bool _pollingStarted = false;

        private void SubscribeForPolling(IRenderableComponent component, ITwinElement element)
        {
            if (_pollingStarted) return;
            if (component == null) return;
            PolledComponents?.Add(component);
            component?.AddToPolling(element, this.PollingInterval);
            _pollingStarted = true;
        }

        private void UnSubscribeFromPolling()
        {
            foreach (var renderableComponent in PolledComponents)
            {
                renderableComponent.RemovePolledElements();
            }

            _pollingStarted = false;
        }

        /// <summary>
        /// Method to build component name from passed parameters, which instance will be found in assembly.
        /// <param name="twinType">Type of passed object.</param>
        /// <param name="presentationType">Type of presentation.</param>
        /// </summary>

        internal IRenderableComponent ViewLocatorBuilder(Type twinType, ITwinElement twin, string presentationType)
        {
            var namespc = twinType.Namespace;
            //set default namespace if is namespace of primitive types or empty
            if (string.IsNullOrEmpty(namespc) || namespc == "AXSharp.Connector.ValueTypes")
                namespc = "AXSharp.Presentation.Blazor.Controls.Templates";

            //end recursion if we are at object type
            if (twinType.FullName == "System.Object") return null;

            var pipeline = presentationType.Split('-');

            foreach (var item in pipeline)
            {
                var presentationName = item;
                if (presentationName.ToLower() == "base") presentationName = "";
                // try to find component view
                var component = GetComponent(twinType, twin, presentationName, namespc);
                if (component == null)
                {
                    //if not found, look at predecessor
                    component = ViewLocatorBuilder(twinType.BaseType, twin, presentationName);
                }
                if (component != null) return component;
            }
            return null;
        }

        /// <summary>
        /// Method to build Generic component name from passed parameters, which instance will be found in assembly.
        /// <param name="twinType">Generic type.</param>
        /// <param name="presentationType">Type of presentation.</param>
        /// </summary>
        internal IRenderableComponent ViewEnumLocatorBuilder(Type twinType, string presentationType)
        {
            var namespc = twinType.Namespace;
            //set default namespace if is primitive type or unknown namespace
            if (string.IsNullOrEmpty(namespc) || namespc == "AXSharp.Connector.ValueTypes")
                namespc = "AXSharp.Presentation.Blazor.Controls.Templates";

            var genericTypeArg = GetGenericInfo(twinType).Item2;
            var pipeline = presentationType.Split('-');
            foreach (var item in pipeline)
            {
                var presentationName = item;
                string buildedComponentName;
                buildedComponentName = $"{namespc}.EnumeratorContainer{presentationName}View`1";
                var component = ComponentService.GetGenericComponent(buildedComponentName, genericTypeArg);
                if (component != null) return component;
            }
            return null;
        }
        private RenderFragment CreatePrimitiveComponent(ITwinPrimitive twinPrimitive, IRenderableComponent primitiveComponent) => __builder =>
        {
            if (primitiveComponent == null) return;
            __builder.OpenComponent(1, primitiveComponent.GetType());
            __builder.AddAttribute(2, "Onliner", twinPrimitive);
            __builder.AddAttribute(3, "IsReadOnly", HasReadAccess(twinPrimitive));
            __builder.CloseComponent();
        };

        private RenderFragment CreateComplexComponent(ITwinObject twin, IRenderableComponent component) => __builder =>
        {
            if (component == null) return;
            __builder.OpenComponent(0, component.GetType());
            if (component is IRenderableComplexComponentBase)
            {
                __builder.AddAttribute(1, "Component", twin);
            }
            else if (component is IRenderableViewModelBase)
            {
                __builder.AddAttribute(1, "TwinContainer", new TwinContainerObject(twin, _viewModelCache.CreateCacheId(_navigationManager.Uri, twin.Symbol, Presentation.ToLower())));
            }
            else
            {
                __builder.AddAttribute(1, "Twin", twin);
            }

           

            __builder.CloseComponent();
        };

        private RenderFragment CreateEnumComponent(EnumeratorDiscriminatorAttribute enumDiscriminatorAttribute, ITwinPrimitive kid, IRenderableComponent component) => __builder =>
        {
            if (component == null) return;
            __builder.OpenComponent(0, component.GetType());
            __builder.AddAttribute(1, "Onliner", kid);
            __builder.AddAttribute(2, "EnumDiscriminatorAttribute", enumDiscriminatorAttribute);
            __builder.AddAttribute(3, "IsReadOnly", HasReadAccess(kid));
            __builder.CloseComponent();
        };

        internal Type TryLoadLayoutType(ITwinElement obj)
        {
            if (obj == null) return null;
            var typeAttribute = obj.GetType()
                  .GetCustomAttributes(true)
                  .ToList()
                  .Find(p => p is PresentationContainerAttribute) as PresentationContainerAttribute;

            if (typeAttribute != null)
                return Type.GetType(typeAttribute.FullTypeName);
            else
                return null;
        }
        internal Type TryLoadLayoutTypeFromProperty(ITwinElement obj)
        {
            if (obj == null) return null;
            var propertyInfo = AttributesHandler.GetPropertyViaSymbol(obj);
            if (propertyInfo != null)
            {
                var typeAttribute = propertyInfo.GetCustomAttributes(true)
                    .ToList()
                    .Where(p => p is PresentationContainerAttribute)
                    .FirstOrDefault()
                    as PresentationContainerAttribute;
                if (typeAttribute != null)
                    return Type.GetType(typeAttribute.FullTypeName);
            }
            return null;
        }


        internal Type TryLoadGroupTypeFromProperty(ITwinElement obj)
        {
            if (obj == null) return null;
            var propertyInfo = AttributesHandler.GetPropertyViaSymbol(obj);
            if (propertyInfo != null)
            {
                var typeAttribute = propertyInfo.GetCustomAttributes(true)
                    .ToList()
                    .Where(p => p is PresentationGroupAttribute)
                    .FirstOrDefault()
                    as PresentationGroupAttribute;
                if (typeAttribute != null)
                    return Type.GetType(typeAttribute.FullTypeName);
            }
            return null;
        }

        internal Type TryLoadGroupType(ITwinElement obj)
        {
            if (obj == null) return null;
            var typeAttribute = obj.GetType()
                  .GetCustomAttributes(true)
                  .ToList()
                  .Find(p => p is PresentationGroupAttribute) as PresentationGroupAttribute;

            if (typeAttribute != null)
                return Type.GetType(typeAttribute.FullTypeName);
            else
                return null;
        }


        internal Group CreateGroup(IEnumerator<ITwinElement> enumerator, ITwinElement child, Type groupLayout, Type parentLayout, bool canEnumerate)
        {
            var name = String.IsNullOrEmpty(child.AttributeName) ? child.GetSymbolTail() : child.AttributeName;
            var group = new Group(parentLayout, groupLayout, name);
            while (canEnumerate)
            {
                if (!HasRenderIgnoreAttribute(Presentation.ToString(), child))
                {
                    group.GroupElements.Add(child);
                }
                canEnumerate = enumerator.MoveNext();
                if (canEnumerate == false) break;
                child = enumerator.Current;
                var isEndOfGroup = TryLoadLayoutTypeFromProperty(child) != null;
                if (isEndOfGroup) break;
            }
            return group;
        }

        private bool HasRenderIgnoreAttribute(string presentationType, ITwinElement element)
        {
            var renderIngoreAttribute = AttributesHandler.GetIgnoreRenderingAttribute(element);
            if (renderIngoreAttribute != null)
            {
                return renderIngoreAttribute.HasIgnore(presentationType);
            }
            return false;
        }

        private bool IsEnumerator(ITwinElement obj)
        {
            var enumeratorDiscriminatorAttribute = AttributesHandler.GetEnumeratorDiscriminatorAttribute(obj);
            if (enumeratorDiscriminatorAttribute != null)
            {
                return true;
            }
            return false;
        }
        
        private (string, Type) GetGenericInfo(Type primitiveKidType)
        {
            if (primitiveKidType == null) return (null, null);

            var baseName = primitiveKidType?.BaseType?.Name;

            if (baseName == "OnlinerBase")
            {
                Type genericTypeArg = primitiveKidType.GenericTypeArguments[0];
                return (baseName, genericTypeArg);
            }
            else
            {
                return GetGenericInfo(primitiveKidType?.BaseType);
            }
        }
        private IRenderableComponent GetComponent(Type twinType, ITwinElement twin, string presentationName, string namespc)
        {
            switch (twin)
            {
                case OnlinerBase onliner:
                    if (twinType.IsGenericType)
                    {
                        var (baseName, genericTypeArg) = GetGenericInfo(twinType);
                        var onlinerName = $"{namespc}.{baseName}";
                        var onlinerBuildedComponentName = $"{onlinerName}{presentationName}View`1";
                        var genericComponent = ComponentService.GetGenericComponent(onlinerBuildedComponentName, genericTypeArg);
                        SubscribeForPolling(genericComponent, twin);
                        return genericComponent;
                    }
                    else
                    {
                        var onlinerName = $"{namespc}.{twinType.Name}";
                        var onlinerBuildedComponentName = $"{onlinerName}{presentationName}View";
                        var primitiveComponent = ComponentService.GetComponent(onlinerBuildedComponentName);
                        SubscribeForPolling(primitiveComponent, twin);
                        return primitiveComponent;
                    }
                default:
                    var name = $"{namespc}.{twinType.Name}";
                    var buildedComponentName = $"{name}{presentationName}View";
                    var defaultComponent = ComponentService.GetComponent(buildedComponentName);
                    SubscribeForPolling(defaultComponent, twin);
                    return defaultComponent;
            }
        }
        private bool HasReadAccess(ITwinPrimitive kid) => kid.ReadWriteAccess == ReadWriteAccess.Read;

        private bool CheckForArray(ITwinObject twinObject)
        {
            var tail = twinObject.GetSymbolTail();
            if (tail.Last() == ']')
            {
                return true;
            }
            return false;
        }

        private string GetDisplayPresentationIfEmpty()
        {
            if (string.IsNullOrEmpty(Presentation))
            {
                return "Display";
            }
            return Presentation;

        }

        public virtual void Dispose()
        {
            UnSubscribeFromPolling();
            _viewModelCache.ResetCounter();
        }
    }
}