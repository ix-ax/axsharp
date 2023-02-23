using AX.ST.Semantic.Model.Declarations;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;
using Polly;
using System.Xml.Linq;

namespace Ix.Compiler.Cs.Pragmas.PragmaParser;

public class PragmaGrammar : Grammar
{
    public readonly IdentifierTerminal AccessQualifierTerminal =
        new(nameof(AccessQualifierTerminal));

    public readonly NonTerminal AddedPropertyDeclaration =
        new(nameof(AddedPropertyDeclaration), typeof(AstAddedPropertyDeclaration));

    public readonly IdentifierTerminal AddedPropertyIdentifier = new(nameof(AddedPropertyIdentifier));

    public readonly NonTerminal AddedPropertyInitializer = new (nameof(AddedPropertyInitializer));

    public readonly FreeTextLiteral AddedPropertyFreeTextLiteral =
        new FreeTextLiteral(nameof(AddedPropertyFreeTextLiteral), FreeTextOptions.AllowEmpty | FreeTextOptions.IncludeTerminator | FreeTextOptions.AllowEof);

    public readonly NonTerminal AddedPropertySetter = new(nameof(AddedPropertySetter), typeof(AstAddePropertySetter));
    public readonly Terminal assing = new(nameof(assing));

    public readonly NonTerminal Attribute = new(nameof(Attribute), typeof(AstAttribute));
    public readonly NonTerminal ClrAttribute = new(nameof(ClrAttribute));

    public readonly FreeTextLiteral ClrAttributeContent =
        new(nameof(ClrAttributeContent), FreeTextOptions.IncludeTerminator, "]");

    
    public readonly NonTerminal Pragmas = new(nameof(Pragmas), typeof(AstPragma));
    public readonly IdentifierTerminal PropertyIdentifierTerminal = new(nameof(PropertyIdentifierTerminal), IdOptions.None);
    public readonly IdentifierTerminal TypeIdentifierTerminal = new(nameof(TypeIdentifierTerminal), IdOptions.None);


    public Terminal ix_prop = new(nameof(ix_prop));
    public Terminal ix_set = new(nameof(ix_set));
    public Terminal colon = new(nameof(colon));
    public Terminal ix_attr = new(nameof(ix_attr));
    public Terminal squareClose = new(nameof(squareClose));
    public Terminal squareOpen = new(nameof(squareOpen));


    public PragmaGrammar(IDeclaration declaration) : this()
    {
        Declaration = declaration;
    }

    public PragmaGrammar()
    {
        colon = ToTerm(":", nameof(colon));
        ix_prop = ToTerm("#ix-prop", nameof(ix_prop));
        ix_attr = ToTerm("#ix-attr", nameof(ix_attr));
        ix_set = ToTerm("#ix-set", nameof(ix_set));
        squareOpen = ToTerm("[", nameof(squareOpen));
        squareClose = ToTerm("]", nameof(squareOpen));
        assing = ToTerm("=", nameof(assing));


        AddedPropertyDeclaration.Rule = ix_prop + ToTerm(":") +
                                        AccessQualifierTerminal + TypeIdentifierTerminal + PropertyIdentifierTerminal;

        ClrAttribute.Rule = squareOpen + ClrAttributeContent + squareClose;

        Attribute.Rule = ix_attr + colon + ClrAttribute;

        AddedPropertyInitializer.Rule = AddedPropertyFreeTextLiteral; 

        AddedPropertySetter.Rule =
            ix_set + colon + AddedPropertyIdentifier + assing + AddedPropertyInitializer;

        Pragmas.Rule = AddedPropertyDeclaration | Attribute | AddedPropertySetter;

        Root = Pragmas;

        LanguageFlags = LanguageFlags.CreateAst;
    }

    private IDeclaration Declaration { get; }

    public override void BuildAst(LanguageData language, ParseTree parseTree)
    {
        var astContext = new PragmaAstContext(language, parseTree, Declaration);
        var astBuilder = new AstBuilder(astContext);
        astBuilder.BuildAst(parseTree);
    }
}




public class AstAddedPropertyDeclaration : AstNode
{
    public string AccessQualifier { get; set; }
    public string Type { get; set; }
    public string Identifier { get; set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        AccessQualifier = treeNode.ChildNodes[2].FindTokenAndGetText();
        Type = treeNode.ChildNodes[3].FindTokenAndGetText();
        Identifier = treeNode.ChildNodes[4].FindTokenAndGetText();
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        var v = visitor as PragmaVisitor;
        v.Product = $"{AccessQualifier} {Type} {Identifier} {{ get; set; }}";
    }
}

public class AstAttribute : AstNode
{
    public string AttributeLiteral { get; set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        AttributeLiteral = $"[{treeNode.ChildNodes[2].ChildNodes[1].FindTokenAndGetText()}]";
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        var v = visitor as PragmaVisitor;
        v.Product = AttributeLiteral;
    }
}

public class AstAddePropertySetter : AstNode
{
    public string? MemberName { get; set; }
    public string PropertyName { get; set; }
    public string InitValue { get; set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        PropertyName = treeNode.GetTheOnlyNode(context.GetGrammar().AddedPropertyIdentifier.Name).FindTokenAndGetText();
        InitValue = treeNode.GetTheOnlyNode(context.GetGrammar().AddedPropertyInitializer.Name).FindTokenAndGetText();
        MemberName = context.GetContext()?.Declaration?.Name;
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        var v = visitor as PragmaVisitor;
        v.Product = MemberName != null ? $"{MemberName}.{PropertyName} = {InitValue};" : $"{PropertyName} = {InitValue};";
    }
}

public static class ParseTreeNodeHelpers
{
    public static List<ParseTreeNode> GetNodes(this ParseTreeNode node, string termName, List<ParseTreeNode> nodes = null)
    {
        if (nodes == null)
        {
            nodes = new List<ParseTreeNode>();
        }

        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                nodes.Add(childNode);
            }
            else
            {
                GetNodes(childNode, termName, nodes);
            }
        }

        return nodes;
    }

    public static ParseTreeNode GetThisLevelNode(this ParseTreeNode node, string termName)
    {
        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                return childNode;
            }
        }

        throw new MissingMemberException($"Internal error: the node {termName} is required in syntax tree.");
    }

    public static ParseTreeNode GetTheOnlyNode(this ParseTreeNode node, string termName, List<ParseTreeNode> nodes = null)
    {
        if (nodes == null)
        {
            nodes = new List<ParseTreeNode>();
        }

        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                nodes.Add(childNode);
            }
            else
            {
                GetNodes(childNode, termName, nodes);
            }
        }

        if (nodes.Count > 1)
            throw new Exception($"Internal error. Only one node {termName} is required.");

        if (nodes.Count == 0)
            throw new Exception($"Internal error. The node {termName} is required but was not found at expected position in the AstTree.");

        return nodes[0];
    }

    public static ParseTreeNode GetTheOnlyNodeOrNull(this ParseTreeNode node, string termName, List<ParseTreeNode> nodes = null)
    {
        if (nodes == null)
        {
            nodes = new List<ParseTreeNode>();
        }

        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                nodes.Add(childNode);
            }
            else
            {
                GetNodes(childNode, termName, nodes);
            }
        }

        if (nodes.Count > 1)
            throw new Exception($"Internal error. Only one node {termName} is required.");

        if (nodes.Count == 0)
            return null;

        return nodes[0];
    }

    public static ParseTreeNode GetTheFirstNode(this ParseTreeNode node, string termName, List<ParseTreeNode> nodes = null)
    {
        if (nodes == null)
        {
            nodes = new List<ParseTreeNode>();
        }

        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                nodes.Add(childNode);
            }
            else
            {
                GetNodes(childNode, termName, nodes);
            }
        }

        if (nodes.Count == 0)
            throw new Exception($"Internal error. The node {termName} is required but was not found in expected position in the AstTree.");

        return nodes[0];
    }

    public static bool HasTheOnlyNode(this ParseTreeNode node, string termName, List<ParseTreeNode> nodes = null)
    {
        if (nodes == null)
        {
            nodes = new List<ParseTreeNode>();
        }

        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                nodes.Add(childNode);
            }
            else
            {
                GetNodes(childNode, termName, nodes);
            }
        }

        if (nodes.Count > 1)
            throw new Exception($"Internal error the only node {termName} is required");

        return nodes.Count == 1;
    }

    public static List<ParseTreeNode> GetAllNodes(this ParseTreeNode node, string termName, List<ParseTreeNode> nodes = null)
    {
        if (nodes == null)
        {
            nodes = new List<ParseTreeNode>();
        }

        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                nodes.Add(childNode);
            }
            else
            {
                GetNodes(childNode, termName, nodes);
            }
        }

        return nodes;
    }
}