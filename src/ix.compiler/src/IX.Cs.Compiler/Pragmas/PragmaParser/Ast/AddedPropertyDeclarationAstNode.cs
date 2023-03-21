// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ix.Compiler.Cs.Pragmas.PragmaParser;

internal class AddedPropertyDeclarationAstNode : AstNode
{
    public string? AccessQualifier { get; set; }
    public string? Type { get; set; }
    public string? Identifier { get; set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        AccessQualifier = treeNode.ChildNodes[2].FindTokenAndGetText();
        Type = treeNode.ChildNodes[3].FindTokenAndGetText();
        Identifier = treeNode.ChildNodes[4].FindTokenAndGetText();
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        if (visitor is PragmaVisitor v)
        {
            if (Type.ToUpperInvariant() == "STRING")
            {
                v.Product = $"private {Type} _{Identifier};" +
                            $"\n{AccessQualifier} {Type} {Identifier} " +
                            $"{{ " +
                            $"get" +
                            $"{{ " +
                            $"return Ix.Localizations.LocalizationHelper.CleanUpLocalizationTokens(_{Identifier}); " +
                            $"}} " +
                            $"set " +
                            $"{{_{Identifier} = value;" +
                            $"}} " +
                            $"}}";
            }
            else
            {
                v.Product = $"{AccessQualifier} {Type} {Identifier} {{ get; set; }}";
            }
           
        }
    }
}