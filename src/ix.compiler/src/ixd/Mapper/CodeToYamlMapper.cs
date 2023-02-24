using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.Text;
using CommandLine;
using Ix.ixc_doc.Helpers;
using Ix.ixc_doc.Schemas;


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
                Uid = declaration.FullyQualifiedName,
                Id = declaration.Name,
                Name = declaration.Name,
                FullName = declaration.Name,
                Namespace = declaration.ContainingNamespace?.Name,
                Summary = _yh.GetComments(declaration.Location).summary,
                Remarks = _yh.GetComments(declaration.Location).remarks,
            };
        }

        public Item PopulateItem(INamespaceDeclaration namespaceDeclaration)
        {
            var children = namespaceDeclaration.Declarations.Select(p => p.FullyQualifiedName);

            var item = PopulateItem((IDeclaration)namespaceDeclaration);
            item.Children = children.ToArray();
            item.Type = "Namespace";

            return item;
        }

        public Item PopulateItem(IClassDeclaration classDeclaration)
        {
            var children = classDeclaration.Fields.Select(p => p.FullyQualifiedName);
            var methods = classDeclaration.Methods.Select(p => _yh.GetMethodUId(p));

            List<IFieldDeclaration> extendedFields = new List<IFieldDeclaration>();
            classDeclaration.GetAllExtendedTypes().ToList()
                .Select(p => ((IClassDeclaration)p).Fields.Where(f => _yh.CanBeFieldInherited(f, classDeclaration, p))).ToList()
                .ForEach(list => extendedFields.Concat(list));

            var item = PopulateItem((IDeclaration)classDeclaration);
            item.Parent = classDeclaration.ContainingNamespace.FullyQualifiedName;
            item.Children = children.Concat(methods).ToArray();
            item.Type = "Class";
            item.Syntax = new Syntax { Content = $"CLASS {classDeclaration.Name}" };
            item.Inheritance = classDeclaration.GetAllExtendedTypes().Select(p => p.FullyQualifiedName).ToArray();
            item.InheritedMembers = _yh.GetInheritedMembers(classDeclaration);

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
            item.Uid = _yh.GetMethodUId(methodDeclaration);
            item.Parent = methodDeclaration.ContainingClass.FullyQualifiedName;
            item.Type = "Method";
            item.Syntax = new Syntax
            {
                Content = declaration,
                Parameters = inputDeclaration.Item1.ToArray(),
                Return = new Return
                {
                    Type = returnType?.Type.FullyQualifiedName,
                    Description = comments.returns
                }
            };

            return item;
        }

        public Item PopulateItem(INamedValueTypeDeclaration namedValueTypeDeclaration)
        {
            var item = PopulateItem((IDeclaration)namedValueTypeDeclaration);
            item.Parent = namedValueTypeDeclaration.ContainingNamespace.FullyQualifiedName;
            item.Type = "Enum";
            item.Syntax = new Syntax { Content = $"{namedValueTypeDeclaration.Name} : {namedValueTypeDeclaration.Type.FullyQualifiedName}" };

            return item;
        }

        public Item PopulateItem(IInterfaceDeclaration interfaceDeclaration)
        {
            var methods = interfaceDeclaration.Methods.Select(p => _yh.GetMethodUId(p));

            var item = PopulateItem((IDeclaration)interfaceDeclaration);
            item.Parent = interfaceDeclaration.ContainingNamespace.FullyQualifiedName;
            item.Children = methods.ToArray();
            item.Type = "Interface";
            item.Syntax = new Syntax { Content = $"INTERFACE {interfaceDeclaration.Name}" };

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
            item.Uid = _yh.GetMethodUId(methodPrototypeDeclaration);
            item.Parent = methodPrototypeDeclaration.ContainingInterface.Name;
            item.Type = "Method";
            item.Syntax = new Syntax
            {
                Content = declaration,
                Parameters = inputDeclaration.Item1.ToArray(),
                Return = new Return
                {
                    Type = returnType?.Type.FullyQualifiedName,
                    Description = comments.returns
                }
            };

            return item;
        }
    }
}
