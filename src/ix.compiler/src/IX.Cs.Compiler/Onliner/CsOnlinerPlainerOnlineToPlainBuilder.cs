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
using Ix.Compiler.Cs.Helpers.Plain;
using Ix.Connector;

namespace Ix.Compiler.Cs.Onliner;

internal class CsOnlinerPlainerOnlineToPlainBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerPlainerOnlineToPlainBuilder(ISourceBuilder sourceBuilder)
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

    internal void CreateAssignment(ITypeDeclaration typeDeclaration, IDeclaration declaration)
    {
        switch (typeDeclaration)
        {
            case IInterfaceDeclaration interfaceDeclaration:
                break;
            case IClassDeclaration classDeclaration:
            //case IAnonymousTypeDeclaration anonymousTypeDeclaration:
            case IStructuredTypeDeclaration structuredTypeDeclaration:
                AddToSource($" plain.{declaration.Name} = await {declaration.Name}.{MethodName}Async();");
                break;
            case IArrayTypeDeclaration arrayTypeDeclaration:
                switch (arrayTypeDeclaration.ElementTypeAccess.Type)
                {
                    case IClassDeclaration classDeclaration:
                    case IStructuredTypeDeclaration structuredTypeDeclaration:                        
                        AddToSource($"plain.{declaration.Name} = {declaration.Name}.Select(async p => await p.{MethodName}Async()).Select(p => p.Result).ToArray();");
                        break;
                    case IScalarTypeDeclaration scalarTypeDeclaration:
                    case IStringTypeDeclaration stringTypeDeclaration:                        
                        AddToSource($"plain.{declaration.Name} = {declaration.Name}.Select(p => p.LastValue).ToArray();");                      
                        break;
                }
                break;
            case IReferenceTypeDeclaration referenceTypeDeclaration:
                break;
            case IEnumTypeDeclaration enumTypeDeclaration:
                AddToSource($" plain.{declaration.Name} = ({declaration.Type.FullyQualifiedName}){declaration.Name}.LastValue;");
                break;
            case INamedValueTypeDeclaration namedValueTypeDeclaration:
                AddToSource($" plain.{declaration.Name} = {declaration.Name}.LastValue;");
                break;
            case IScalarTypeDeclaration scalarTypeDeclaration:
            case IStringTypeDeclaration stringTypeDeclaration:
                AddToSource($" plain.{declaration.Name} = {declaration.Name}.LastValue;");
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

    protected static readonly string MethodName = TwinObjectExtensions.OnlineToPlainMethodName;

    public static CsOnlinerPlainerOnlineToPlainBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainBuilder(sourceBuilder);

        builder.AddToSource(CsHelpers.CreateGenericSwapperMethodToPlainer(MethodName));

        builder.AddToSource($"public async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}Async(){{\n");
        builder.AddToSource($"Pocos.{semantics.FullyQualifiedName} plain = new Pocos.{semantics.FullyQualifiedName}();");
        builder.AddToSource("await this.ReadAsync();");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource($"return plain;");
        builder.AddToSource($"}}");
        return builder;
    }

    public static CsOnlinerPlainerOnlineToPlainBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainBuilder(sourceBuilder);

        builder.AddToSource(CsHelpers.CreateGenericSwapperMethodToPlainer(MethodName));

        builder.AddToSource($"public async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}Async(){{\n");
        builder.AddToSource($"Pocos.{semantics.FullyQualifiedName} plain = new Pocos.{semantics.FullyQualifiedName}();");
        builder.AddToSource("await this.ReadAsync();");

        if (isExtended)
        {
            builder.AddToSource($"await base.{MethodName}Async(plain);");
        }

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"return plain;");
        builder.AddToSource($"}}");
        return builder;
    }
}