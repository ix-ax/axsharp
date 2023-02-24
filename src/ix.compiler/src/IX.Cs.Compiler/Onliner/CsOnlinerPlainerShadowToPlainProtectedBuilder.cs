﻿// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IX.Compiler.Core;

namespace Ix.Compiler.Cs.Onliner
{
    internal class CsOnlinerPlainerShadowToPlainProtectedBuilder : CsOnlinerPlainerShadowToPlainBuilder
    {
        private readonly StringBuilder _memberDeclarations = new();

        protected CsOnlinerPlainerShadowToPlainProtectedBuilder(Compilation compilation) : base(compilation)
        {

        }

        public new static CsOnlinerPlainerShadowToPlainProtectedBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
            Compilation compilation)
        {
            var builder = new CsOnlinerPlainerShadowToPlainProtectedBuilder(compilation);
            builder.AddToSource($"protected async Task<Pocos.{semantics.FullyQualifiedName}> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain){{\n");

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
            builder.AddToSource($"return plain;");
            builder.AddToSource($"}}");
            return builder;
        }

        public new static CsOnlinerPlainerShadowToPlainProtectedBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
            Compilation compilation, bool isExtended)
        {
            var builder = new CsOnlinerPlainerShadowToPlainProtectedBuilder(compilation);
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
}
