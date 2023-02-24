﻿// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Declarations;
using Ix.Compiler.Cs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AX.ST.Semantic;
using Ix.Compiler.Core;
using IX.Compiler.Core;
using Ix.Connector;

namespace Ix.Compiler.Cs.Onliner
{
    internal class CsOnlinerPlainerPlainToShadowBuilder : ICombinedThreeVisitor
    {
        private readonly StringBuilder _memberDeclarations = new();

        protected CsOnlinerPlainerPlainToShadowBuilder(Compilation compilation)
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

        private void CreateAssignment(ITypeDeclaration typeDeclaration, IDeclaration declaration)
        {
            switch (typeDeclaration)
            {
                case IInterfaceDeclaration interfaceDeclaration:
                    break;
                case IClassDeclaration classDeclaration:
                //case IAnonymousTypeDeclaration anonymousTypeDeclaration:
                case IStructuredTypeDeclaration structuredTypeDeclaration:
                    AddToSource($" await this.{declaration.Name}.{MethodName}(plain.{declaration.Name});");
                    break;
                case IArrayTypeDeclaration arrayTypeDeclaration:


                    switch (arrayTypeDeclaration.ElementTypeAccess.Type)
                    {
                        case IClassDeclaration classDeclaration:
                        case IStructuredTypeDeclaration structuredTypeDeclaration:
                            AddToSource($"var _{declaration.Name}_i_FE8484DAB3 = 0;");
                            AddToSource($"{declaration.Name}.Select(p => p.{MethodName}(plain.{declaration.Name}[_{declaration.Name}_i_FE8484DAB3++])).ToArray();");
                            break;
                        case IScalarTypeDeclaration scalarTypeDeclaration:
                        case IStringTypeDeclaration stringTypeDeclaration:
                            AddToSource($"var _{declaration.Name}_i_FE8484DAB3 = 0;");
                            AddToSource($"{declaration.Name}.Select(p => p.Shadow = plain.{declaration.Name}[_{declaration.Name}_i_FE8484DAB3++]).ToArray();");
                            break;
                    }
                    break;
                case IReferenceTypeDeclaration referenceTypeDeclaration:
                    break;
                case IEnumTypeDeclaration enumTypeDeclaration:
                    AddToSource($" {declaration.Name}.Shadow = (short)plain.{declaration.Name};");
                    break;
                case INamedValueTypeDeclaration namedValueTypeDeclaration:
                    AddToSource($" {declaration.Name}.Shadow = plain.{declaration.Name};");
                    break;
                case IScalarTypeDeclaration scalarTypeDeclaration:
                case IStringTypeDeclaration stringTypeDeclaration:
                    AddToSource($" {declaration.Name}.Shadow = plain.{declaration.Name};");
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

        private static readonly string MethodName = TwinObjectExtensions.PlainToShadowMethodName;

        public static CsOnlinerPlainerPlainToShadowBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
            Compilation compilation)
        {
            var builder = new CsOnlinerPlainerPlainToShadowBuilder(compilation);
            builder.AddToSource($"public async Task<IEnumerable<ITwinPrimitive>> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain){{\n");

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

            builder.AddToSource("return this.RetrievePrimitives();");
            builder.AddToSource($"}}");
            return builder;
        }

        public static CsOnlinerPlainerPlainToShadowBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
            Compilation compilation, bool isExtended)
        {
            var builder = new CsOnlinerPlainerPlainToShadowBuilder(compilation);
            builder.AddToSource($"public async Task<IEnumerable<ITwinPrimitive>> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain){{\n");


            if (isExtended)
            {
                builder.AddToSource($"await base.{MethodName}(plain);");
            }

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
            builder.AddToSource("return this.RetrievePrimitives();");

            builder.AddToSource($"}}");
            return builder;
        }
    }
}
