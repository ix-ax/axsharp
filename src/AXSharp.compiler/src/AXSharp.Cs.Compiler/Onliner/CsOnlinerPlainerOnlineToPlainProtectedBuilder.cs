﻿// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Compiler.Cs.Helpers.Onliners;
using AXSharp.Compiler.Cs.Helpers.Plain;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs.Onliner;

internal class CsOnlinerPlainerOnlineToPlainProtectedBuilder : CsOnlinerPlainerOnlineToPlainBuilder
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerPlainerOnlineToPlainProtectedBuilder(ISourceBuilder sourceBuilder) : base(sourceBuilder)
    {
        
    }

    public new static CsOnlinerPlainerOnlineToPlainProtectedBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainProtectedBuilder(sourceBuilder);
        builder.AddToSource($"protected async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}Async(Pocos.{semantics.FullyQualifiedName} plain){{\n");
        
        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"return plain;");
        builder.AddToSource($"}}");
        return builder;
    }

    public new static CsOnlinerPlainerOnlineToPlainProtectedBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainProtectedBuilder(sourceBuilder);

        //var qualifier = isExtended ? "new" : string.Empty;
        var qualifier = string.Empty;
        builder.AddToSource($"protected {qualifier} async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}Async(Pocos.{semantics.FullyQualifiedName} plain){{\n");
        
        
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