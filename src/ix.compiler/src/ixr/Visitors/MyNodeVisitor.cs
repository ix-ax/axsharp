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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixc_doc.Visitors
{

    //semantic
    public partial class MyNodeVisitor : ISemanticNodeVisitor<Action<string>>
    {
        public Compiler.AxProject axProject { get; set; }

        public MyNodeVisitor(Compiler.AxProject axProject = null)
        {
            this.axProject = axProject;
        }

        public void Visit(IPartialSemanticTree partialSemanticTree, Action<string> data)
        {
            partialSemanticTree.ChildNodes.ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(ISymbol symbol, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IPragma pragma, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IConfigurationDeclaration configurationDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ITaskConfigurationDeclaration taskConfigurationDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ITaskDeclaration taskDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IProgramConfigurationDeclaration programConfigurationDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamespaceDeclaration namespaceDeclaration, Action<string> data)
        {
            namespaceDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(IUsingDirective usingDirective, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IProgramDeclaration programDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IClassDeclaration classDeclaration, Action<string> data)
        {
            classDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(IInterfaceDeclaration interfaceDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFunctionDeclaration functionDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFunctionBlockDeclaration functionBlockDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IMethodDeclaration methodDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IMethodPrototypeDeclaration methodPrototypeDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IScalarTypeDeclaration scalarType, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IStructuredTypeDeclaration structuredTypeDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IArrayTypeDeclaration arrayTypeDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IEnumTypeDeclaration enumTypeDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamedValueTypeDeclaration namedValueTypeDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IReferenceTypeDeclaration referenceTypeDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IStringTypeDeclaration stringTypeDeclaration, Action<string> data)
        {
            data(stringTypeDeclaration.FullyQualifiedName);
        }

        public void Visit(IDimension dimension, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IFieldDeclaration fieldDeclaration, Action<string> data)
        {
            fieldDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(IVariableDeclaration variableDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IEnumValueDeclaration enumValueDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(INamedValueDeclaration namedValueDeclaration, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticInitializerExpression initializerExpression, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticArrayInitializer arrayInitializer, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticStructureInitializer structureInitializer, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticMemberInitializer memberInitializer, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticTypeAccess semanticTypeAccess, Action<string> data)
        {
            //Tu som
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticInstructionList instrList, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAssignmentInstruction assignment, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticUnsafeAssignmentInstruction assignment, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAssignmentAttemptInstruction assignmentAttempt, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticExpressionInstruction expression, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticIfConditionalStatement condStatement, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IConditionalInstructionList condInstrList, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCaseStatement caseStatement, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCaseSelection caseSelection, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticSubrange subrange, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticForStatement forStatement, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticWhileStatement whileStatement, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticRepeatStatement repeatStatement, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticContinueInstruction continueInstruction, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticExitInstruction exitInstruction, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticReturnStatement returnStatement, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticAsmStatement asmStatement, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticConstantExpression constExpr, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticIdentifierAccess identifierAccess, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticBinaryExpression binExpr, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticUnaryExpression unaryExpression, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticQualifiedEnumAccess qualifiedEnumAccess, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticMemberAccessExpression memberAccessExpression, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticArrayAccessExpression arrayAccessExpression, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticCallExpression call, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticParameterList paramList, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(ISemanticParameterAssignment paramAssignment, Action<string> data)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IPartialAccessExpression partialAccessExpression, Action<string> data)
        {
            //throw new NotImplementedException();
        }
    }

}
