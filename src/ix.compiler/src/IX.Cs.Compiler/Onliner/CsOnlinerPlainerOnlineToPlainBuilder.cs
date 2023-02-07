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

namespace Ix.Compiler.Cs.Onliner;

internal class CsOnlinerPlainerOnlineToPlainBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerPlainerOnlineToPlainBuilder(Compilation compilation)
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
            case IClassDeclaration classDeclaration:
            //case IAnonymousTypeDeclaration anonymousTypeDeclaration:
            case IStructuredTypeDeclaration structuredTypeDeclaration:
                AddToSource($" plain.{declaration.Name} = {declaration.Name}.{MethodName}();");
                break;
            case IArrayTypeDeclaration arrayTypeDeclaration:
                AddToSource(
                    $"Ix.Connector.BuilderHelpers.Arrays.CopyOnlineToPlain<{arrayTypeDeclaration.ElementTypeAccess.Type.FullyQualifiedName},Pocos.{arrayTypeDeclaration.ElementTypeAccess.Type.FullyQualifiedName}>(plain.{declaration.Name}, {declaration.Name});");
                break;
            case IReferenceTypeDeclaration referenceTypeDeclaration:
                break;
            case IEnumTypeDeclaration enumTypeDeclaration:
                AddToSource($" plain.{declaration.Name} = ({declaration.Type.FullyQualifiedName}){declaration.Name}.LastValue;;");
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

    private static readonly string MethodName = "OnlineToPlain";

    public static CsOnlinerPlainerOnlineToPlainBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        Compilation compilation)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainBuilder(compilation);
        builder.AddToSource($"public Pocos.{semantics.FullyQualifiedName} {MethodName}(){{\n");
        builder.AddToSource($"Pocos.{semantics.FullyQualifiedName} plain = new Pocos.{semantics.FullyQualifiedName}();");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource($"return plain;");
        builder.AddToSource($"}}");
        return builder;
    }

    public static CsOnlinerPlainerOnlineToPlainBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        Compilation compilation, bool isExtended)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainBuilder(compilation);
        builder.AddToSource($"public Pocos.{semantics.FullyQualifiedName} {MethodName}(){{\n");
        builder.AddToSource($"Pocos.{semantics.FullyQualifiedName} plain = new Pocos.{semantics.FullyQualifiedName}();");

        if (isExtended)
        {
            builder.AddToSource($"plain = (Pocos.{semantics.FullyQualifiedName})base.{MethodName}();");
        }

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"return plain;");
        builder.AddToSource($"}}");
        return builder;
    }
}