using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.Text;
using CommandLine;
using Ix.ixc_doc.Enums;
using Ix.ixc_doc.Helpers;
using Ix.ixc_doc.Models;
using Ix.ixc_doc.Schemas;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;
using static Ix.ixc_doc.Helpers.YamlHelpers;

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
                FullName = namespaceDeclaration.Name,
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
                FullName = classDeclaration.Name,
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
                FullName = fieldDeclaration.Name,
                Type = ItemType.Property.ToString(),
                Namespace = fieldDeclaration.ContainingNamespace.Name,
                Summary = _yh.GetComments(fieldDeclaration.Location).summary,
                Syntax = new Syntax { Content = $"{fieldDeclaration.Name} : {fieldDeclaration.Type.FullyQualifiedName}", Return = new Return { Type = $"{fieldDeclaration.Type.FullyQualifiedName}" } },
            };
        }

        private (List<Parameter>,string) CreateParametersAndDeclarationString(IList<IVariableDeclaration> variableDeclarations, Comments comments)
        { 
            var inputParams = new List<Parameter>();
            StringBuilder sb = new StringBuilder();
            foreach (var p in variableDeclarations)
            {
                var parameter = new Parameter
                {
                    Id = p.Name,
                    Type = p.Type.FullyQualifiedName 
                 };

                //TODO add filtering by name
                var description = comments.param.FirstOrDefault();
                parameter.Description = description;
                inputParams.Add(parameter);

                sb.Append($"in {parameter.Type} {parameter.Id}");
                if(variableDeclarations.Last() != p) sb.Append(", ");
                
            }

            return(inputParams, sb.ToString());
        }
        public Item PopulateItem(IMethodDeclaration methodDeclaration)
        {
            //TODO implement documentation for methods parameters and return values
            // when xml documentation is detected, add only description
            var comments = _yh.GetComments(methodDeclaration.Location);
            
            var returnType = methodDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
            var inputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            var outputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Output).ToList();
            

           var inputDeclaration = CreateParametersAndDeclarationString(inputParamsDeclaration,comments);
           var outputDeclaration = CreateParametersAndDeclarationString(outputParamsDeclaration,comments);
            
           string declaration = $"{methodDeclaration.AccessModifier} {returnType?.Type} {methodDeclaration.Name}({inputDeclaration.Item2}{outputDeclaration.Item2})";


           
            return new Item
            {
                Uid = methodDeclaration.FullyQualifiedName,
                Id = methodDeclaration.Name,
                //Parent = ,
                Name = methodDeclaration.Name,
                FullName = methodDeclaration.Name,
                Type = ItemType.Method.ToString(),
                Namespace = methodDeclaration.ContainingNamespace.Name,
                Summary = comments.summary,
                Syntax = new Syntax 
                { 
                    Content = declaration,
                    Parameters = inputDeclaration.Item1.ToArray() ,
                    Return =  new Return
                    { 
                        Type = returnType?.Type.FullyQualifiedName, 
                        Description = comments.returns
                    }
                    
                    },
               
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
                FullName = namedValueTypeDeclaration.Name,
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
                FullName = structuredTypeDeclaration.Name,
                Type = "Struct",
                Namespace = structuredTypeDeclaration.Name,
                Summary = _yh.GetComments(structuredTypeDeclaration.Location).summary,
                Syntax = new Syntax { Content = "CLASS MyTestClass" },

                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }
    }
}
