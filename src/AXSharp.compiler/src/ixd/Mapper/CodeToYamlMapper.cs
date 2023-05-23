﻿using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.Text;
using CommandLine;
using AXSharp.ixc_doc.Helpers;
using AXSharp.ixc_doc.Schemas;
using System.Xml.Linq;
using AXSharp.ixc_doc.Visitors;
using NuGet.Packaging;


namespace AXSharp.ixc_doc.Mapper
{
    internal class CodeToYamlMapper
    {
        private YamlHelpers _yh { get; set; }
        public CodeToYamlMapper(YamlHelpers yh)
        {
            _yh = yh;
        }

        public Item PopulateItem(IDeclaration declaration)
        {
            return new Item
            {
                Uid = Helpers.Helpers.GetBaseUid(declaration),
                Id =  Helpers.Helpers.GetBaseUid(declaration.FullyQualifiedName),
                Name = declaration.Name,
                FullName = declaration.Name,
                Namespace = declaration is INamespaceDeclaration ? Helpers.Helpers.GetBaseUid(declaration.FullyQualifiedName) : Helpers.Helpers.GetBaseUid(declaration.ContainingNamespace?.FullyQualifiedName),
                Summary = _yh.GetComments(declaration.Location).summary,
                Remarks = _yh.GetComments(declaration.Location).remarks,
                //Assemblies = new string[] { _yh.GetAssembly(_yh.PathToProjectFile) },
                
                
            };
        }

        public Item PopulateItem(INamespaceDeclaration namespaceDeclaration)
        {
            var children = namespaceDeclaration.Declarations.Select(p => Helpers.Helpers.GetBaseUid(p));
            //_yh.NamespaceChildren.AddRange(children);
            var item = PopulateItem((IDeclaration)namespaceDeclaration);
            item.Name = Helpers.Helpers.GetBaseUid(namespaceDeclaration);
            item.Children = new List<string>();
            item.Type = "Namespace";

            return item;
        }

        public Item PopulateItem(IClassDeclaration classDeclaration)
        {
            var children = classDeclaration.Fields.Select(p => Helpers.Helpers.GetBaseUid(p));
            var methods = classDeclaration.Methods.Select(p => Helpers.Helpers.GetBaseUid(p));
            var implementedInterfaces = classDeclaration.GetAllImplementedInterfacesUniquely().Select(i => Helpers.Helpers.GetBaseUid(i));

            List<IFieldDeclaration> extendedFields = new List<IFieldDeclaration>();
            classDeclaration.GetAllExtendedTypes().OfType<IClassDeclaration>().ToList()
                .Select(p => p.Fields.Where(f => _yh.CanBeFieldInherited(f, classDeclaration, p))).ToList()
                .ForEach(list => extendedFields.Concat(list));

            var item = PopulateItem((IDeclaration)classDeclaration);
            item.Parent = Helpers.Helpers.GetBaseUid(classDeclaration.ContainingNamespace.FullyQualifiedName);
            item.Children = children.Concat(methods).ToList();
            item.Type = "Class";
            item.Syntax = new Syntax { Content = $"CLASS {classDeclaration.Name}" };
            item.Inheritance = classDeclaration.GetAllExtendedTypes().Select(p => Helpers.Helpers.GetBaseUid(p)).ToArray();
            item.InheritedMembers = _yh.GetInheritedMembers(classDeclaration);
            item.Implements = implementedInterfaces.ToArray();

            return item;
        }

        public Item PopulateItem(IFieldDeclaration fieldDeclaration)
        {
            var item = PopulateItem((IDeclaration)fieldDeclaration);
            item.Parent = fieldDeclaration.ContainingNamespace.FullyQualifiedName;
            item.Type = "Property";
            item.Syntax = new Syntax
            {
                Content = $"{fieldDeclaration.Name} : {fieldDeclaration.Type.FullyQualifiedName}",
                Return = new Return
                {
                    Type = $"{fieldDeclaration.Type.FullyQualifiedName}",
                    Description = _yh.GetComments(fieldDeclaration.Location).returns
                }
            };

            return item;
        }

        public Item PopulateItem(IMethodDeclaration methodDeclaration)
        {
            var comments = _yh.GetComments(methodDeclaration.Location);

            var returnType = methodDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
         
            var inputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();

            var inputDeclaration = _yh.CreateParametersAndDeclarationString(inputParamsDeclaration, comments);
            string declaration = $"{methodDeclaration.AccessModifier} {(returnType == null ? "VOID" : returnType.Type.FullyQualifiedName)} {methodDeclaration.Name}({inputDeclaration.Item2})";

            var item = PopulateItem((IDeclaration)methodDeclaration);
            item.Uid = Helpers.Helpers.GetBaseUid(methodDeclaration);
            item.Id = Helpers.Helpers.GetBaseUid(methodDeclaration);
            item.Parent = methodDeclaration.ContainingClass.FullyQualifiedName;
            item.Type = "Method";
            item.Syntax = new Syntax
            {
                Content = declaration,
                Parameters = inputDeclaration.Item1.ToArray(),
                Return = new Return
                {
                    Type = returnType == null ? "VOID" : Helpers.Helpers.GetBaseUid(returnType.Type) ,
                    Description = comments.returns
                }
            };

            return item;
        }

        public Item PopulateItem(IFunctionDeclaration functionDeclaration)
        {
            var comments = _yh.GetComments(functionDeclaration.Location);

            var returnType = functionDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
         
            var inputParamsDeclaration = functionDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();

            var inputDeclaration = _yh.CreateParametersAndDeclarationString(inputParamsDeclaration, comments);
            string declaration = $"{functionDeclaration.AccessModifier} {(returnType == null ? "VOID" : returnType.Type.FullyQualifiedName)} {functionDeclaration.Name}({inputDeclaration.Item2})";

            var item = PopulateItem((IDeclaration)functionDeclaration);
            item.Uid = Helpers.Helpers.GetBaseUid(functionDeclaration);
            item.Id = Helpers.Helpers.GetBaseUid(functionDeclaration);
            item.Parent = functionDeclaration.ContainingNamespace.FullyQualifiedName;
            item.Type = "Delegate";
            item.Syntax = new Syntax
            {
                Content = declaration,
                Parameters = inputDeclaration.Item1.ToArray(),
                Return = new Return
                {
                    Type = returnType == null ? "VOID" : Helpers.Helpers.GetBaseUid(returnType.Type) ,
                    Description = comments.returns
                }
            };

            return item;
        }

        public Item PopulateItem(INamedValueTypeDeclaration namedValueTypeDeclaration)
        {
            var item = PopulateItem((IDeclaration)namedValueTypeDeclaration);
            item.Parent = Helpers.Helpers.GetBaseUid(namedValueTypeDeclaration.ContainingNamespace.FullyQualifiedName);
            item.Type = "Enum";
            item.Syntax = new Syntax { Content = $"{namedValueTypeDeclaration.Name} : {namedValueTypeDeclaration.Type.FullyQualifiedName}" };

            return item;
        }

        public Item PopulateItem(IInterfaceDeclaration interfaceDeclaration)
        {
            var implementedInterfaces = interfaceDeclaration.GetAllImplementedInterfacesUniquely().Select(i => Helpers.Helpers.GetBaseUid(i));
            var methods = interfaceDeclaration.Methods.Select(p => Helpers.Helpers.GetBaseUid(p));

            var item = PopulateItem((IDeclaration)interfaceDeclaration);
            item.Parent = Helpers.Helpers.GetBaseUid(interfaceDeclaration.ContainingNamespace.FullyQualifiedName);
            item.Children = methods.ToList();
            item.Type = "Interface";
            item.Syntax = new Syntax { Content = $"INTERFACE {interfaceDeclaration.Name}" };
            item.Implements = implementedInterfaces.ToArray();
            return item;
        }

        public Item PopulateItem(IMethodPrototypeDeclaration methodPrototypeDeclaration)
        {

            var comments = _yh.GetComments(methodPrototypeDeclaration.Location);

            var returnType = methodPrototypeDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
       
            var inputParamsDeclaration = methodPrototypeDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();

            var inputDeclaration = _yh.CreateParametersAndDeclarationString(inputParamsDeclaration, comments);
            string declaration = $"{methodPrototypeDeclaration.AccessModifier} {(returnType == null ? "VOID" : returnType.Type.FullyQualifiedName)} {methodPrototypeDeclaration.Name}({inputDeclaration.Item2})";

            var item = PopulateItem((IDeclaration)methodPrototypeDeclaration);
            item.Uid = Helpers.Helpers.GetBaseUid(methodPrototypeDeclaration);
            item.Parent = methodPrototypeDeclaration.ContainingInterface.Name;
            item.Type = "Method";
            item.Syntax = new Syntax
            {
                Content = declaration,
                Parameters = inputDeclaration.Item1.ToArray(),
                Return = new Return
                {
                    Type = returnType == null ? "VOID" : Helpers.Helpers.GetBaseUid(returnType.Type) ,
                    Description = comments.returns
                }
            };

            return item;
        }
    }
}
