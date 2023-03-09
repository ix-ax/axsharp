// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using Ix.Compiler.Core;
using IX.Compiler.Core;
using Ix.Compiler.Cs.Helpers;
using Ix.Compiler.Cs.Helpers.Onliners;
using AX.ST.Syntax.Tree;
using Ix.Compiler.Cs.Helpers.Plain;
using Ix.Connector;

namespace Ix.Compiler.Cs.Onliner;

internal class CsOnlinerPlainerPlainToOnlineBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerPlainerPlainToOnlineBuilder(ISourceBuilder sourceBuilder)
    {
        SourceBuilder = sourceBuilder;
    }

    private ISourceBuilder SourceBuilder { get; }

    public string Output => _memberDeclarations.ToString().FormatCode();

    
    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        if (fieldDeclaration.IsMemberEligibleForTranspile(SourceBuilder, "POCO"))
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
        if (variableDeclaration.IsMemberEligibleForTranspile(SourceBuilder, "POCO"))
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
                        AddToSource($"{declaration.Name}.Select(p => p.Cyclic = plain.{declaration.Name}[_{declaration.Name}_i_FE8484DAB3++]).ToArray();");
                        break;
                }
                break;
            case IReferenceTypeDeclaration referenceTypeDeclaration:
                break;
            case IEnumTypeDeclaration enumTypeDeclaration:
                AddToSource($" {declaration.Name}.Cyclic = (short)plain.{declaration.Name};");
                break;
            case INamedValueTypeDeclaration namedValueTypeDeclaration:
                AddToSource($" {declaration.Name}.Cyclic = plain.{declaration.Name};");
                break;
            case IScalarTypeDeclaration scalarTypeDeclaration:
            case IStringTypeDeclaration stringTypeDeclaration:
                AddToSource($" {declaration.Name}.Cyclic = plain.{declaration.Name};");
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

    private static readonly string MethodName = TwinObjectExtensions.PlainToOnlineMethodName;

    public static CsOnlinerPlainerPlainToOnlineBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerPlainerPlainToOnlineBuilder(sourceBuilder);
        builder.AddToSource($"public async Task<IEnumerable<ITwinPrimitive>> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain){{\n");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource("return await this.WriteAsync();");

        builder.AddToSource($"}}");
        return builder;
    }

    public static CsOnlinerPlainerPlainToOnlineBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended)
    {
        var builder = new CsOnlinerPlainerPlainToOnlineBuilder(sourceBuilder);
        builder.AddToSource($"public async Task<IEnumerable<ITwinPrimitive>> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain){{\n");
       

        if (isExtended)
        {
            builder.AddToSource($"await base.{MethodName}(plain);");
        }

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource("return await this.WriteAsync();");

        builder.AddToSource($"}}");
        return builder;
    }
}