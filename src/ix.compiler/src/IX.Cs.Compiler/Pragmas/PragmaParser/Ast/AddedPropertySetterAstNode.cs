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

internal class AddedPropertySetterAstNode : AstNode
{
    public string? MemberName { get; set; }
    public string? PropertyName { get; set; }
    public string? InitValue { get; set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        var grammar = context.GetGrammar();
        if (grammar != null)
        {
            PropertyName = treeNode.GetTheOnlyNode(grammar.AddedPropertyIdentifier.Name).FindTokenAndGetText();
            InitValue = treeNode.GetTheOnlyNode(grammar.AddedPropertyInitializer.Name).FindTokenAndGetText();
            MemberName = context.GetContext()?.Declaration?.Name;
        }
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        if (visitor is PragmaVisitor v)
            v.Product = MemberName != null
                ? $"{MemberName}.{PropertyName} = {InitValue};"
                : $"{PropertyName} = {InitValue};";
    }
}