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

internal class CsOnlinerPlainerBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerPlainerBuilder(Compilation compilation)
    {
        Compilation = compilation;
    }

    private Compilation Compilation { get; }

    public string Output => _memberDeclarations.ToString().FormatCode();

    
    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        if (fieldDeclaration.IsMemberEligibleForTranspile(Compilation))
        {
            AddToSource($" plain.{fieldDeclaration.Name} = {fieldDeclaration.Name};");
        }
    }

    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        //
    }

    public void CreateVariableDeclaration(IVariableDeclaration semantics, IxNodeVisitor visitor)
    {
        if (semantics.IsMemberEligibleForTranspile(Compilation))
        {
            AddToSource($" plain.{semantics.Name} = {semantics.Name};");
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

    public static CsOnlinerPlainerBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        Compilation compilation)
    {
        var builder = new CsOnlinerPlainerBuilder(compilation);
        builder.AddToSource($"public {semantics.Name} OnlineToPlain(){{");
        builder.AddToSource("var plain = new ")
        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"}}");
        return builder;
    }

    public static CsOnlinerPlainerBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        Compilation compilation)
    {
        var builder = new CsOnlinerPlainerBuilder(compilation);
        builder.AddToSource($"public {semantics.Name} OnlineToPlain(){{");
        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"}}");
        return builder;
    }

    public static CsOnlinerPlainerBuilder Create(IxNodeVisitor visitor, IConfigurationDeclaration semantics,
        Compilation compilation)
    {
        var builder = new CsOnlinerPlainerBuilder(compilation);
        builder.AddToSource($"public {semantics.Name} OnlineToPlain(){{");
        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"}}");
        return builder;
    }
}