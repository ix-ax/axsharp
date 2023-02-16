using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Init;
using AX.ST.Semantic.Pragmas;
using AX.ST.Semantic.Symbols;
using AX.ST.Semantic.Tree;
using AX.ST.Syntax.Tree;
using AX.Text;
using Ix.ixc_doc.Interfaces;
using Ix.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ix_doc_compiler
{
   
    //semantic
    public partial class MyNodeVisitor : ISemanticNodeVisitor<MyTreeVisitor>
    {
        public List<Item> Items { get; set; }
        public YamlSchema Schema { get; set; }
        public TocSchema TocSchema { get; set; }
        public List<TocSchema.Item> TocSchemaItems { get; set; }
        public MyNodeVisitor()
        {
            Schema = new YamlSchema();
            TocSchema = new TocSchema();
            Items = new List<Item>();
            TocSchemaItems = new List<TocSchema.Item>();
        }
        public void Visit(IPartialSemanticTree partialSemanticTree, MyTreeVisitor data)
        {
            partialSemanticTree.ChildNodes.ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(ISymbol symbol, MyTreeVisitor data)
        {
            //symbol
            //CreateFieldDocumentation()
        }

        public void Visit(IPragma pragma, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IConfigurationDeclaration configurationDeclaration, MyTreeVisitor data)
        {
            //Console.WriteLine("Configuration declaration!");
            data.CreateBaseYaml(configurationDeclaration, this);
        }

        public void Visit(ITaskConfigurationDeclaration taskConfigurationDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ITaskDeclaration taskDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IProgramConfigurationDeclaration programConfigurationDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamespaceDeclaration namespaceDeclaration, MyTreeVisitor data)
        {
            //Console.WriteLine("namespace declaration!");
            namespaceDeclaration.Declarations.ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(IUsingDirective usingDirective, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IProgramDeclaration programDeclaration, MyTreeVisitor data)
        {
            data.CreateBaseYaml(programDeclaration, this);
            //Console.WriteLine("program declaration!");
            ///*throw new NotImplementedException();*/
        }

        public void Visit(IClassDeclaration classDeclaration, MyTreeVisitor data)
        {
            //Console.WriteLine("class declaration!");

            //classDeclaration.lo
            data.CreateClassYaml(classDeclaration, this);
            //Console.WriteLine("I'm class declaration section!");
            ///*throw new NotImplementedException();*/
        }

        public void Visit(IInterfaceDeclaration interfaceDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFunctionDeclaration functionDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFunctionBlockDeclaration functionBlockDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IMethodDeclaration methodDeclaration, MyTreeVisitor data)
        {
            data.CreateMethodYaml(methodDeclaration, this);
            ///*throw new NotImplementedException();*/
        }

        public void Visit(IMethodPrototypeDeclaration methodPrototypeDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IScalarTypeDeclaration scalarType, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IStructuredTypeDeclaration structuredTypeDeclaration, MyTreeVisitor data)
        {
            //Console.WriteLine("structured type!");
            data.CreateBaseYaml(structuredTypeDeclaration, this);

        }

        public void Visit(IArrayTypeDeclaration arrayTypeDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IEnumTypeDeclaration enumTypeDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamedValueTypeDeclaration namedValueTypeDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IReferenceTypeDeclaration referenceTypeDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IStringTypeDeclaration stringTypeDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IDimension dimension, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFieldDeclaration fieldDeclaration, MyTreeVisitor data)
        {
            //create field doc
            data.CreateFieldYaml(fieldDeclaration, this);
        }

        public void Visit(IVariableDeclaration variableDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IEnumValueDeclaration enumValueDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamedValueDeclaration namedValueDeclaration, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticInitializerExpression initializerExpression, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticArrayInitializer arrayInitializer, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticStructureInitializer structureInitializer, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticMemberInitializer memberInitializer, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticTypeAccess semanticTypeAccess, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticInstructionList instrList, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAssignmentInstruction assignment, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticUnsafeAssignmentInstruction assignment, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAssignmentAttemptInstruction assignmentAttempt, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticExpressionInstruction expression, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticIfConditionalStatement condStatement, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IConditionalInstructionList condInstrList, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCaseStatement caseStatement, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCaseSelection caseSelection, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticSubrange subrange, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticForStatement forStatement, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticWhileStatement whileStatement, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticRepeatStatement repeatStatement, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticContinueInstruction continueInstruction, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticExitInstruction exitInstruction, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticReturnStatement returnStatement, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAsmStatement asmStatement, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticConstantExpression constExpr, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticIdentifierAccess identifierAccess, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticBinaryExpression binExpr, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticUnaryExpression unaryExpression, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticQualifiedEnumAccess qualifiedEnumAccess, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticMemberAccessExpression memberAccessExpression, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticArrayAccessExpression arrayAccessExpression, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCallExpression call, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticParameterList paramList, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticParameterAssignment paramAssignment, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IPartialAccessExpression partialAccessExpression, MyTreeVisitor data)
        {
            //throw new NotImplementedException();
        }

    }

}
