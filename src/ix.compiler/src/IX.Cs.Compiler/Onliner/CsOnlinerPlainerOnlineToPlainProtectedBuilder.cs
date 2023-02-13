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

internal class CsOnlinerPlainerOnlineToPlainProtectedBuilder : CsOnlinerPlainerOnlineToPlainBuilder
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerPlainerOnlineToPlainProtectedBuilder(Compilation compilation) : base(compilation)
    {
        
    }

    public static CsOnlinerPlainerOnlineToPlainProtectedBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        Compilation compilation)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainProtectedBuilder(compilation);
        builder.AddToSource($"protected async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain){{\n");
        
        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"return plain;");
        builder.AddToSource($"}}");
        return builder;
    }

    public static CsOnlinerPlainerOnlineToPlainProtectedBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        Compilation compilation, bool isExtended)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainProtectedBuilder(compilation);
        builder.AddToSource($"protected async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain){{\n");
        
        
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