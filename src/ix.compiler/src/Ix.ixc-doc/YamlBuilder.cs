using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using Ix.ixc_doc.Interfaces;
using Ix.ixc_doc.Mapper;
using Ix.ixc_doc.Schemas;
using ix_doc_compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixc_doc
{
    public class YamlBuilder : MyTreeVisitor
    {
        private CodeToYamlMapper _mp { get; set; }
        public YamlBuilder()
        {
            _mp = new CodeToYamlMapper();
        }
        //operation on semantic tree
        public virtual void CreateClassYaml(IClassDeclaration classDeclaration, MyNodeVisitor visitor)
        {
            //var mapper = new CodeToYamlMapper();

            var item = _mp.PopulateItem(classDeclaration);
            visitor.Items.Add(item);

            classDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(visitor, this));

         
            visitor.Schema.Items = visitor.Items.ToArray();


            var tocSchemaItem = new TocSchema.Item { 
                Uid= item.Uid,
                Name= item.FullName
            };

            visitor.TocSchemaItems.Add(tocSchemaItem);

            var x = new YamlSerializer();
            x.SchemaToYaml(visitor.Schema, classDeclaration.FullyQualifiedName);
            visitor.Schema = new YamlSchema();
            visitor.Items.Clear();
        }


        //operation on semantic tree
        public virtual void CreateFieldYaml(IFieldDeclaration fieldDeclaration, MyNodeVisitor visitor)
        {

            var item = _mp.PopulateItem(fieldDeclaration);
            visitor.Items.Add(item);

        }

        public virtual void CreateMethodYaml(IMethodDeclaration methodDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(methodDeclaration);
            visitor.Items.Add(item);
        }

        public virtual void CreateBaseYaml(
        IDeclaration declaration,
        MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(declaration);
            visitor.Items.Add(item);
        }
    }
}
