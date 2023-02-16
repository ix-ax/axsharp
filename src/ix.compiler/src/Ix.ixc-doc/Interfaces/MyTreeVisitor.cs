using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Pragmas;
using AX.ST.Syntax.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ix_doc_compiler;

namespace Ix.ixc_doc.Interfaces
{
    public interface MyTreeVisitor
    {
        /// <summary>
        ///     Creates file declaration from <see cref="IFileSyntax" /> node of given syntax tree.
        /// </summary>
        /// <param name="fileSyntax">File syntax node.</param>
        /// <param name="visitor">Associated visitor.</param>
        public virtual void CreateFile(IFileSyntax fileSyntax, MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }
        public virtual void CreateClassYaml(
          IClassDeclaration classDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateFieldYaml(
          IFieldDeclaration fieldDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateMethodYaml(
         IMethodDeclaration methodDeclaration,
         MyNodeVisitor visitor)
            {
            throw new NotImplementedException();
        }


        public virtual void CreateBaseYaml(
         IDeclaration declaration,
         MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }


    }
}
