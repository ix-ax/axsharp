// Ix.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Ix.Connector;
using Ix.Abstractions.Presentation;
using Ix.Presentation.Attributes;
using Ix.Presentation.Blazor;
using Ix.Presentation.Blazor.Interfaces;
using System.Runtime.CompilerServices;
using Ix.Presentation.Blazor.Services;
using System.Collections.Generic;
using Ix.Presentation.Blazor.Exceptions;
using System.Reflection;
using System.ComponentModel;
using Ix.Connector.ValueTypes;

namespace Ix.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  This class implements main logic behind auto-generated UI. 
    /// </summary>
    public partial class RenderableContentControl : ComponentBase, IDisposable
    {
        /// <summary>
        /// Parameter Context accept ITwinElement instance, which is used as base model for UI generation.
        /// </summary>
        [Parameter]
        public object Context { get; set; }
        /// <summary>
        /// Parameter Presentation specify mode, in which view UI is generated. Type PresentationType is used.
        /// </summary>
        [Parameter]
        public string Presentation { get; set; }
        /// <summary>
        /// Parameter Class serves for styling of elements.
        /// </summary>
        [Parameter]
        public string Class { get; set; }
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

        private ITwinElement _context { get; set; }
        protected override void OnInitialized()
        {
            try
            {
                _context = (ITwinElement)Context;
            }
            catch 
            {
                throw new ParameterWrongTypeRendererException(Context.GetType().ToString());
            }
            
            base.OnInitialized();
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
            if(_groupContainer == null) _groupContainer = TryLoadGroupType(_context);

            if (String.IsNullOrEmpty(Presentation)) Presentation = "";
            _viewModelCache.ResetCounter();
        }


        /// <summary>
        /// Method to build component name from passed parameters, which instance will be found in assembly.
        /// <param name="twinType">Type of passed object.</param>
        /// <param name="presentationType">Type of presentation.</param>
        /// </summary>

        internal IRenderableComponent ViewLocatorBuilder(Type twinType, string presentationType)
        {
            var namespc = twinType.Namespace;
            //set default namespace if is namespace of primitive types or empty
            if (string.IsNullOrEmpty(namespc) || namespc == "Ix.Connector.ValueTypes")
                namespc = "Ix.Presentation.Blazor.Controls.Templates";

            //end recursion if we are at object type
            if (twinType.FullName == "System.Object") return null;

            var pipeline = presentationType.Split('-');

            foreach (var item in pipeline)
            {
                var presentationName = item;
                if (presentationName.ToLower() == "base") presentationName = "";
                // try to find component view
                var component = GetComponent(twinType, presentationName, namespc);
                if (component == null)
                {
                    //if not found, look at predecessor
                    component = ViewLocatorBuilder(twinType.BaseType, presentationName);
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
            if (string.IsNullOrEmpty(namespc) || namespc == "Ix.Connector.ValueTypes")
                namespc = "Ix.Presentation.Blazor.Controls.Templates";

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
            else if(component is IRenderableViewModelBase)
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
            var baseName = primitiveKidType.BaseType.Name;

            if (baseName == "OnlinerBase")
            {
                Type genericTypeArg = primitiveKidType.GenericTypeArguments[0];
                return (baseName, genericTypeArg);
            } else
            {
                return GetGenericInfo(primitiveKidType.BaseType);
            }
        }
        private IRenderableComponent GetComponent(Type twinType, string presentationName, string namespc)
        {
            // if is generic type, render generic component
            if (twinType.IsGenericType)
            {
                var (baseName, genericTypeArg) = GetGenericInfo(twinType);
                var name = $"{namespc}.{baseName}";
                var buildedComponentName = $"{name}{presentationName}View`1";
                return ComponentService.GetGenericComponent(buildedComponentName, genericTypeArg);
            }
            else
            {
                var name = $"{namespc}.{twinType.Name}";
                var buildedComponentName = $"{name}{presentationName}View";
                return ComponentService.GetComponent(buildedComponentName);
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
        public void Dispose()
        {
            _viewModelCache.ResetCounter();
        }
    }
}
