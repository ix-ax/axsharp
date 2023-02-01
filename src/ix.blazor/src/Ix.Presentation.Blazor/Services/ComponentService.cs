// Ix.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ix.Abstractions.Presentation;
using Ix.Presentation.Blazor.Attributes;
using Ix.Presentation.Blazor.Interfaces;

namespace Ix.Presentation.Blazor.Services
{
    /// <summary>
    ///  Component service class, which contains methods to load, find, and access blazor components dynamically within framework.
    /// </summary>
    public sealed class ComponentService : IComponentService
    {
        /// <summary>
        /// Creates new instance of <see cref="ComponentService"/>.
        /// </summary>
        public ComponentService()
        {
            PresentationProvider.Create(new BlazorLayoutProvider(), new BlazorGroupLayoutProvider());
            LoadComponents();
        }

        private readonly string _baseAssemblyName = "Ix.Presentation.Controls.Blazor";
        internal IEnumerable<Type> Components { get; private set; }
        internal IEnumerable<Type> BaseComponents { get; private set; }

        /// <summary>
        ///  Method to get instance dynamically of blazor component.
        /// <param name="fullName">Name of the component.</param>
        /// </summary>
        public IRenderableComponent GetComponent(string fullName)
        {

            var foundedType = Components.FirstOrDefault(x=> String.Equals(x.FullName, fullName, StringComparison.CurrentCultureIgnoreCase));
           
            if (foundedType != null)
            {
                return (IRenderableComponent)Activator.CreateInstance(foundedType);
            }
            else return null;
        }

        /// <summary>
        ///  Method to get base instance dynamically of blazor component.
        /// <param name="name">Name of the component.</param>
        /// </summary>
        public IRenderableComponent GetBaseComponent(string name)
        {
            name = name.Split('.').Last();
            var foundedType = BaseComponents.FirstOrDefault(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
          
            if (foundedType != null)
            {
                return (IRenderableComponent)Activator.CreateInstance(foundedType);
            }
            else return null;
        }
        /// <summary>
        ///  Method to get generic instance dynamically of blazor component.
        /// <param name="name">Name of the component.</param>
        /// <param name="typeArg">Generic type.</param>
        /// </summary>
        public IRenderableComponent GetGenericComponent(string name, Type typeArg)
        {
            var foundedType = BaseComponents.FirstOrDefault(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (foundedType != null)
            {
                Type genericType = foundedType.MakeGenericType(typeArg);
                return (IRenderableComponent)Activator.CreateInstance(genericType);
            }
            else return null;
        }
        /// <summary>
        ///  Method to Load all implemented components types from assembly.
        /// </summary>
        public void LoadComponents()
        {
            var loadedAssemblies = LoadAssemblies();
            var components = new List<Type>();
            var baseComponents = new List<Type>();
            var customBaseComponents = new List<Type>();

            var filteredAssemblies = loadedAssemblies.Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(RenderableBlazorAssemblyAttribute)));

            foreach (var assembly in filteredAssemblies)
            {
                if (assembly.GetName().Name == _baseAssemblyName)
                {
                    baseComponents.AddRange(GetTypesWithInterface(assembly));
                }
                else
                {
                    var types = GetTypesWithInterface(assembly);
                    var customBaseTypes = types.Where(x => x.Name.Contains("Onliner"));
                    types = types.Except(customBaseTypes);
                    customBaseComponents.AddRange(customBaseTypes);
                    components.AddRange(types);
                }
            }

            Components = components;
            BaseComponents = customBaseComponents.Concat(baseComponents).Concat(components);
        }

        private List<Assembly> LoadAssemblies()
        {
            var loadedAssemblies = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Prepend(Assembly.GetExecutingAssembly())
                .ToList();
            var loadedPaths = loadedAssemblies.Where(p => !p.IsDynamic).Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path =>
            {
                try
                {
                    loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path)));
                }
                catch (System.BadImageFormatException)
                {
                    // Ignore
                }
                catch (System.IO.FileLoadException)
                {
                    // Ignore
                }
            });
            return loadedAssemblies;
        }

        private IEnumerable<Type> GetTypesWithInterface(Assembly asm)
        {
            return GetLoadableTypes(asm).Where(typeof(IRenderableComponent).IsAssignableFrom).ToList();
        }

        private IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
