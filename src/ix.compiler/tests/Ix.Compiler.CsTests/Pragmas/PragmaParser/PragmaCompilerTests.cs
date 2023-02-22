using Xunit;
using Ix.Compiler.Cs.Pragmas.PragmaParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AX.ST.Semantic.Symbols;
using AX.ST.Semantic.Tree;
using AX.Text;

namespace Ix.Compiler.Cs.Pragmas.PragmaParser.Tests
{
    public class PragmaCompilerTests
    {
        [Fact()]
        public void CompileStringPropertyTests()
        {
            var pragmaContent = "#ix-prop:public string AA";
            var pragma = new DummyPragma(pragmaContent,
                new SourceLocation(new StringText(pragmaContent, "C:\\aa\\"), new TextSpan()));

            var expected = "public string AA { get; set; }";

            var actual = PragmaCompiler.Compile(pragma, new DummyDeclaration() { Name = "MyPropName", FullyQualifiedName = "MyNamespace.MyPropName" });

            Assert.Equal(expected, actual);
            
        }

        [Fact()]
        public void CompileAttributeTests()
        {
            var pragmaContent = "#ix-attr:[Container(Layout.Wrap)]";
            var pragma = new DummyPragma(pragmaContent,
                new SourceLocation(new StringText(pragmaContent, "C:\\aa\\"), new TextSpan()));

            var expected = "[Container(Layout.Wrap)]";

            var actual = PragmaCompiler.Compile(pragma, new DummyDeclaration() { Name = "MyPropName", FullyQualifiedName = "MyNamespace.MyPropName" });

            Assert.Equal(expected, actual);

        }

        [Fact()]
        public void CompileAddedPropertySetterOnTypeMember()
        {
            var pragmaContent = "#ix-set:AttributeName = \"Hello\"";
            var pragma = new DummyPragma(pragmaContent,
                new SourceLocation(new StringText(pragmaContent, "C:\\aa\\"), new TextSpan()));

            var expected = "MyPropName.AttributeName = \"Hello\";";

            var actual = PragmaCompiler.Compile(pragma, new DummyDeclaration() { Name = "MyPropName", FullyQualifiedName = "MyNamespace.MyPropName"});

            Assert.Equal(expected, actual);
        }

        [Fact()]
        public void CompileAddedPropertySetterOnType()
        {
            var pragmaContent = "#ix-set:AttributeName = \"Hello\"";
            var pragma = new DummyPragma(pragmaContent,
                new SourceLocation(new StringText(pragmaContent, "C:\\aa\\"), new TextSpan()));

            var expected = "AttributeName = \"Hello\";";

            var actual = PragmaCompiler.Compile(pragma);

            Assert.Equal(expected, actual);
        }

        public class DummyPragma : IPragma
        {
            public DummyPragma(string content, Location location)
            {
                this.Content = content;
                this.Location = location;
            }

            public IEnumerable<ISemanticNode> GetDescendentNodes()
            {
                throw new NotImplementedException();
            }

            public void Accept<T>(ISemanticNodeVisitor<T> visitor, T data)
            {
                throw new NotImplementedException();
            }

            public Location Location { get; }
            public IEnumerable<ISemanticNode> ChildNodes { get; }
            public string Content { get; }
        }


        public class DummyDeclaration : IDeclaration
        {

            public DummyDeclaration()
            {
                
            }

            public IEnumerable<ISemanticNode> GetDescendentNodes()
            {
                throw new NotImplementedException();
            }

            public void Accept<T>(ISemanticNodeVisitor<T> visitor, T data)
            {
                throw new NotImplementedException();
            }

            public Location Location { get; }
            public IEnumerable<ISemanticNode> ChildNodes { get; }
            public bool Equals(IDeclaration? other)
            {
                throw new NotImplementedException();
            }

            public ISymbol Symbol { get; }
            public string Name { get; set; }
            public string FullyQualifiedName { get; set; }
            public DeclarationKind Kind { get; }
            public IScope ContainingScope { get; }
            public INamespaceDeclaration ContainingNamespace { get; }
            public ITypeDeclaration Type { get; }
            public IReadOnlyCollection<IPragma> Pragmas { get; }
        }
    }
}