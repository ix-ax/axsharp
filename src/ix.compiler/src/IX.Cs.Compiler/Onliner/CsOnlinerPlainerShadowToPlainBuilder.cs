using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using Ix.Compiler.Core;
using Ix.Compiler.Cs.Helpers;
using Ix.Connector;
using IX.Compiler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.Compiler.Cs.Onliner
{
    internal class CsOnlinerPlainerShadowToPlainBuilder : ICombinedThreeVisitor
    {
        private readonly StringBuilder _memberDeclarations = new();

        protected CsOnlinerPlainerShadowToPlainBuilder(Compilation compilation)
        {
            Compilation = compilation;
        }

        private Compilation Compilation { get; }

        public string Output => _memberDeclarations.ToString().FormatCode();


        public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
        {
            if (fieldDeclaration.IsMemberEligibleForTranspile(Compilation))
            {
                CreateAssignment(fieldDeclaration.Type, fieldDeclaration);
            }
        }

        public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
        {
            //
        }

        public void CreateVariableDeclaration(IVariableDeclaration variableDeclaration, IxNodeVisitor visitor)
        {
            if (variableDeclaration.IsMemberEligibleForTranspile(Compilation))
            {
                CreateAssignment(variableDeclaration.Type, variableDeclaration);
            }
        }

        internal void CreateAssignment(ITypeDeclaration typeDeclaration, IDeclaration declaration)
        {
            switch (typeDeclaration)
            {
                case IInterfaceDeclaration interfaceDeclaration:
                    break;
                case IClassDeclaration classDeclaration:
                //case IAnonymousTypeDeclaration anonymousTypeDeclaration:
                case IStructuredTypeDeclaration structuredTypeDeclaration:
                    AddToSource($" plain.{declaration.Name} = await {declaration.Name}.{MethodName}();");
                    break;
                case IArrayTypeDeclaration arrayTypeDeclaration:
                    switch (arrayTypeDeclaration.ElementTypeAccess.Type)
                    {
                        case IClassDeclaration classDeclaration:
                        case IStructuredTypeDeclaration structuredTypeDeclaration:
                            AddToSource($"plain.{declaration.Name} = {declaration.Name}.Select(async p => await p.{MethodName}()).Select(p => p.Result).ToArray();");
                            break;
                        case IScalarTypeDeclaration scalarTypeDeclaration:
                        case IStringTypeDeclaration stringTypeDeclaration:

                            AddToSource($"plain.{declaration.Name} = {declaration.Name}.Select(p => p.Shadow).ToArray();");
                            break;
                    }
                    break;
                case IReferenceTypeDeclaration referenceTypeDeclaration:
                    break;
                case IEnumTypeDeclaration enumTypeDeclaration:
                    AddToSource($" plain.{declaration.Name} = ({declaration.Type.FullyQualifiedName}){declaration.Name}.Shadow;");
                    break;
                case INamedValueTypeDeclaration namedValueTypeDeclaration:
                    AddToSource($" plain.{declaration.Name} = {declaration.Name}.Shadow;");
                    break;
                case IScalarTypeDeclaration scalarTypeDeclaration:
                case IStringTypeDeclaration stringTypeDeclaration:
                    AddToSource($" plain.{declaration.Name} = {declaration.Name}.Shadow;");
                    break;
            }
        }

        public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
        {
            //
        }


        protected void AddToSource(string token, string separator = " ")
        {
            _memberDeclarations.Append($"{token}{separator}");
        }

        public void AddTypeConstructionParameters(string parametersString)
        {
            AddToSource(parametersString);
        }

        protected static readonly string MethodName = TwinObjectExtensions.ShadowToPlainMethodName;

        public static CsOnlinerPlainerShadowToPlainBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
            Compilation compilation)
        {
            var builder = new CsOnlinerPlainerShadowToPlainBuilder(compilation);
            builder.AddToSource($"public async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}(){{\n");
            builder.AddToSource($"Pocos.{semantics.FullyQualifiedName} plain = new Pocos.{semantics.FullyQualifiedName}();");

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

            builder.AddToSource($"return plain;");
            builder.AddToSource($"}}");
            return builder;
        }

        public static CsOnlinerPlainerShadowToPlainBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
            Compilation compilation, bool isExtended)
        {
            var builder = new CsOnlinerPlainerShadowToPlainBuilder(compilation);
            builder.AddToSource($"public async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}(){{\n");
            builder.AddToSource($"Pocos.{semantics.FullyQualifiedName} plain = new Pocos.{semantics.FullyQualifiedName}();");

            if (isExtended)
            {
                builder.AddToSource($"await base.{MethodName}(plain);");
            }

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
            builder.AddToSource($"return plain;");
            builder.AddToSource($"}}");
            return builder;
        }
    }
}
