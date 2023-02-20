using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.Text;
using CommandLine;
using Ix.ixc_doc.Enums;
using Ix.ixc_doc.Helpers;
using Ix.ixc_doc.Schemas;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace Ix.ixc_doc.Mapper
{
    internal class CodeToYamlMapper
    {
        private YamlHelpers _yh { get; set; }
        public CodeToYamlMapper()
        {
            _yh = new YamlHelpers();
        }

        public Item PopulateItem(IDeclaration declaration)
        {
            return new Item
            {
                Uid = declaration.Name,
                Id = declaration.Name,
                Parent = declaration.ContainingNamespace.Name,
                //Children = children.Concat(methods).ToArray(),
                Name = declaration.Name,
                FullName = declaration.FullyQualifiedName,
                //Type = "Class",
                Namespace = declaration.ContainingNamespace.Name,
                Summary = _yh.GetComments(declaration.Location).summary,
                //Syntax = new Syntax { Content = "CLASS MyTestClass" },
                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }

        public Item PopulateItem(INamespaceDeclaration namespaceDeclaration)
        {
            var children = namespaceDeclaration.Declarations.Select(p => p.FullyQualifiedName);

            return new Item
            {
                Uid = namespaceDeclaration.FullyQualifiedName,
                Id = namespaceDeclaration.Name,
                //Parent = namespaceDeclaration.ContainingNamespace.FullyQualifiedName,
                Children = children.ToArray(),
                Name = namespaceDeclaration.Name,
                FullName = namespaceDeclaration.FullyQualifiedName,
                Type = "Namespace",
                Summary = _yh.GetComments(namespaceDeclaration.Location).summary,
            };
        }

        public Item PopulateItem(IClassDeclaration classDeclaration)
        {
            var children = classDeclaration.Fields.Select(p => p.FullyQualifiedName);
            var methods = classDeclaration.Methods.Select(p => p.FullyQualifiedName);

            var extendedMethods = classDeclaration.GetMethodsFromExtendedTypes();
            List<IFieldDeclaration> extendedFields = new List<IFieldDeclaration>();
            classDeclaration.GetAllExtendedTypes().ToList()
                .Select(p => ((IClassDeclaration)p).Fields.Where(f => _yh.CanBeFieldInherited(f, classDeclaration, p))).ToList()
                .ForEach(list => extendedFields.Concat(list));

            return new Item
            {
                Uid = classDeclaration.FullyQualifiedName,
                Id = classDeclaration.Name,
                Parent = classDeclaration.ContainingNamespace.Name,
                Children = children.Concat(methods).ToArray(),
                Name = classDeclaration.Name,
                FullName = classDeclaration.FullyQualifiedName,
                Type = ItemType.Class.ToString(),
                Namespace = classDeclaration.ContainingNamespace.Name,
                Summary = _yh.GetComments(classDeclaration.Location).summary,
                Syntax = new Syntax { Content = $"CLASS {classDeclaration.Name}" },
                Inheritance = classDeclaration.GetAllExtendedTypes().Select(p => p.FullyQualifiedName).ToArray(),
                InheritedMembers = _yh.GetInheritedMembers(classDeclaration),
            };
        }

        public Item PopulateItem(IFieldDeclaration fieldDeclaration)
        {
            return new Item
            {
                Uid = fieldDeclaration.FullyQualifiedName,
                Id = fieldDeclaration.Name,
                Parent = fieldDeclaration.ContainingScope.Name,
                Name = fieldDeclaration.Name,
                FullName = fieldDeclaration.FullyQualifiedName,
                Type = ItemType.Property.ToString(),
                Namespace = fieldDeclaration.ContainingNamespace.Name,
                Summary = _yh.GetComments(fieldDeclaration.Location).summary,
                Syntax = new Syntax { Content = $"{fieldDeclaration.Name} : {fieldDeclaration.Type.FullyQualifiedName}", Return = new Return { Type = $"{fieldDeclaration.Type.FullyQualifiedName}" } },
            };
        }


        public Item PopulateItem(IMethodDeclaration methodDeclaration)
        {
            //TODO implement documentation for methods parameters and return values
            // when xml documentation is detected, add only description

          

            //var inputVariables = methodDeclaration.Variables.Where(v => v.Section == Section.Input)
            //    .Select(v => v.Section.ToString()).ToArray();

            //var returnType = methodDeclaration.Variables.Where(v => v.Section == Section.Return)
            //    .Select(v => v.Section.ToString()).ToArray();

            //foreach (var item in x)
            //{
            //    var section = item.Section;
            //}
            var methods = methodDeclaration.GetDescendentNodes();
            return new Item
            {
                Uid = methodDeclaration.FullyQualifiedName,
                Id = methodDeclaration.Name,
                //Parent = ,
                Name = methodDeclaration.Name,
                FullName = methodDeclaration.FullyQualifiedName,
                Type = ItemType.Method.ToString(),
                Namespace = methodDeclaration.ContainingNamespace.Name,
                Summary = "Test class doc",
                Syntax = new Syntax { Content = "CLASS MyTestClass" },
            };
        }

        public Item PopulateItem(INamedValueTypeDeclaration namedValueTypeDeclaration)
        {
            return new Item
            {
                Uid = namedValueTypeDeclaration.FullyQualifiedName,
                Id = namedValueTypeDeclaration.Name,
                //Parent = classDeclaration.;
                //Children = children.Concat(methods).ToArray(),
                Name = namedValueTypeDeclaration.Name,
                FullName = namedValueTypeDeclaration.FullyQualifiedName,
                Type = "Struct",
                Namespace = namedValueTypeDeclaration.Name,
                Summary = _yh.GetComments(namedValueTypeDeclaration.Location).summary,
                Syntax = new Syntax { Content = "CLASS MyTestClass" },

                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }

        public Item PopulateItem(IStructuredTypeDeclaration structuredTypeDeclaration)
        {
            return new Item
            {
                Uid = structuredTypeDeclaration.FullyQualifiedName,
                Id = structuredTypeDeclaration.Name,
                //Parent = classDeclaration.;
                //Children = children.Concat(methods).ToArray(),
                Name = structuredTypeDeclaration.Name,
                FullName = structuredTypeDeclaration.FullyQualifiedName,
                Type = "Struct",
                Namespace = structuredTypeDeclaration.Name,
                Summary = _yh.GetComments(structuredTypeDeclaration.Location).summary,
                Syntax = new Syntax { Content = "CLASS MyTestClass" },

                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }


        
    }
}
