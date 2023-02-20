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

namespace Ix.ixc_doc.Visitors
{

    //semantic
    public partial class MyNodeVisitor : ISemanticNodeVisitor<IYamlBuiderVisitor>
    {
        public List<Item> Items { get; set; }
        public List<Item> NamespaceItems { get; set; }
        public YamlSchema Schema { get; set; }
        public TocSchema TocSchema { get; set; }

        public List<Reference> References { get; set; }
        public List<TocSchema.Item> TocSchemaItems { get; set; }
        public TocSchemaList TocSchemaList { get; set; }
        public MyNodeVisitor()
        {
            Schema = new YamlSchema();
            NamespaceItems = new List<Item>();
            TocSchema = new TocSchema();
            Items = new List<Item>();
            TocSchemaItems = new List<TocSchema.Item>();
            References= new List<Reference>();
            TocSchemaList = new TocSchemaList();
        }
        public void Visit(IPartialSemanticTree partialSemanticTree, IYamlBuiderVisitor data)
        {
            partialSemanticTree.ChildNodes.ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(ISymbol symbol, IYamlBuiderVisitor data)
        {
            //symbol
            //CreateFieldDocumentation()
        }

        public void Visit(IPragma pragma, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IConfigurationDeclaration configurationDeclaration, IYamlBuiderVisitor data)
        {
            //Console.WriteLine("Configuration declaration!");
            //data.CreateBaseYaml(configurationDeclaration, this);
        }

        public void Visit(ITaskConfigurationDeclaration taskConfigurationDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ITaskDeclaration taskDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IProgramConfigurationDeclaration programConfigurationDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamespaceDeclaration namespaceDeclaration, IYamlBuiderVisitor data)
        {
            //Console.WriteLine("namespace declaration!");
            data.CreateNamespaceYaml(namespaceDeclaration, this);
            //namespaceDeclaration.Declarations.ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(IUsingDirective usingDirective, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IProgramDeclaration programDeclaration, IYamlBuiderVisitor data)
        {
            //data.CreateBaseYaml(programDeclaration, this);
            //Console.WriteLine("program declaration!");
            ///*throw new NotImplementedException();*/
        }

        public void Visit(IClassDeclaration classDeclaration, IYamlBuiderVisitor data)
        {
            //Console.WriteLine("class declaration!");

            //classDeclaration.lo
            data.CreateClassYaml(classDeclaration, this);
            //Console.WriteLine("I'm class declaration section!");
            ///*throw new NotImplementedException();*/
        }

        public void Visit(IInterfaceDeclaration interfaceDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFunctionDeclaration functionDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFunctionBlockDeclaration functionBlockDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IMethodDeclaration methodDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateMethodYaml(methodDeclaration, this);
            ///*throw new NotImplementedException();*/
        }

        public void Visit(IMethodPrototypeDeclaration methodPrototypeDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IScalarTypeDeclaration scalarType, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IStructuredTypeDeclaration structuredTypeDeclaration, IYamlBuiderVisitor data)
        {
            //data.CreateStructuredTypeYaml(structuredTypeDeclaration, this);
            //Console.WriteLine("structured type!");
            //data.CreateBaseYaml(structuredTypeDeclaration, this);
        }

        public void Visit(IArrayTypeDeclaration arrayTypeDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IEnumTypeDeclaration enumTypeDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamedValueTypeDeclaration namedValueTypeDeclaration, IYamlBuiderVisitor data)
        {
            //data.CreateNamedValueTypeYaml(namedValueTypeDeclaration, this);
            //throw new NotImplementedException();
        }

        public void Visit(IReferenceTypeDeclaration referenceTypeDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IStringTypeDeclaration stringTypeDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IDimension dimension, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFieldDeclaration fieldDeclaration, IYamlBuiderVisitor data)
        {
            //create field doc
            data.CreateFieldYaml(fieldDeclaration, this);
        }

        public void Visit(IVariableDeclaration variableDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IEnumValueDeclaration enumValueDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamedValueDeclaration namedValueDeclaration, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticInitializerExpression initializerExpression, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticArrayInitializer arrayInitializer, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticStructureInitializer structureInitializer, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticMemberInitializer memberInitializer, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticTypeAccess semanticTypeAccess, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticInstructionList instrList, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAssignmentInstruction assignment, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticUnsafeAssignmentInstruction assignment, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAssignmentAttemptInstruction assignmentAttempt, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticExpressionInstruction expression, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticIfConditionalStatement condStatement, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IConditionalInstructionList condInstrList, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCaseStatement caseStatement, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCaseSelection caseSelection, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticSubrange subrange, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticForStatement forStatement, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticWhileStatement whileStatement, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticRepeatStatement repeatStatement, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticContinueInstruction continueInstruction, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticExitInstruction exitInstruction, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticReturnStatement returnStatement, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAsmStatement asmStatement, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticConstantExpression constExpr, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticIdentifierAccess identifierAccess, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticBinaryExpression binExpr, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticUnaryExpression unaryExpression, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticQualifiedEnumAccess qualifiedEnumAccess, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticMemberAccessExpression memberAccessExpression, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticArrayAccessExpression arrayAccessExpression, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCallExpression call, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticParameterList paramList, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticParameterAssignment paramAssignment, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IPartialAccessExpression partialAccessExpression, IYamlBuiderVisitor data)
        {
            //throw new NotImplementedException();
        }

    }

}
