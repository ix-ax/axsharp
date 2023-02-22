using Irony.Interpreter.Ast;

namespace Ix.Compiler.CsTests.Pragmas.PragmaParser
{
    using Ix.Compiler.Cs.Pragmas.PragmaParser;
    using System;
    using Xunit;
    using Irony.Parsing;

    public class DeclarationParserTests
    {
        private PragmaParser _testClass;
        private Grammar _grammar;
        private LanguageData _language;
        private NonTerminal _root;

        public DeclarationParserTests()
        {
            _grammar = new Grammar();
            _language = new LanguageData(new Grammar());
            _root = new NonTerminal("TestValue727606470");
            _testClass = new PragmaParser(new PragmaGrammar());
        }

        [Fact]
        public void Parse_well_formed_added_property_pragma()
        {
            var parseTree = _testClass.Parse("#ix-prop:public string Hello");
            var pragmaVisitor = new PragmaVisitor();
            var astNode = parseTree.Root.AstNode as IVisitableNode;

            astNode.AcceptVisitor(pragmaVisitor);

            Assert.Equal("public string Hello { get; set; }", pragmaVisitor.Product);
        }

        [Fact]
        public void Parse_malformed_added_property_pragma()
        {
            var parseTree = _testClass.Parse("#ix-prop:public stringHello");
            var pragmaVisitor = new PragmaVisitor();
            Assert.ThrowsAny<Exception>(() =>
            {
                var astNode = parseTree.Root.AstNode as IVisitableNode;
                astNode.AcceptVisitor(pragmaVisitor);
            });
        }


        [Fact]
        public void CanGetGet()
        {
            // Assert
            Assert.IsType<PragmaParser>(new PragmaParser(new PragmaGrammar()));
        }
    }
}