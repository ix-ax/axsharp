using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using CommandLine;
using Ix.ixc_doc.Interfaces;
using Ix.ixc_doc.Mapper;
using Ix.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixc_doc.Visitors
{
    public class YamlBuilder : IYamlBuiderVisitor
    {
        private YamlSerializer _s { get; set; }
        private CodeToYamlMapper _mp { get; set; }
        internal YamlBuilder(YamlSerializer serializer)
        {
            _mp = new CodeToYamlMapper();
            _s = serializer;
        }
        //operation on semantic tree
        public virtual void CreateClassYaml(IClassDeclaration classDeclaration, MyNodeVisitor visitor)
        {
            //var mapper = new CodeToYamlMapper();

            var item = _mp.PopulateItem(classDeclaration);
            visitor.Items.Add(item);

            //var references = new List<Reference>();
            foreach (var member in item.InheritedMembers) 
            {
                visitor.References.Add( new Reference()
                {
                    Uid = member,
                    CommentId= member,
                    Parent = member,
                    Name= member.Split('.').Last(),
                    NameWithType = member.Split('.').Last(),
                    FullName = member

                });
            }

            var tocSchemaItem = new TocSchema.Item
            {
                Uid = item.Uid,
                Name = item.FullName
            };

            visitor.TocSchemaItems.Add(tocSchemaItem);

            classDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(visitor, this));


            visitor.Schema.Items = visitor.Items.ToArray();
            visitor.Schema.References = visitor.References.ToArray();
            _s.SchemaToYaml(visitor.Schema, classDeclaration.FullyQualifiedName);
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

        public virtual void CreateNamedValueTypeYaml(
          INamedValueTypeDeclaration namedValueTypeDeclaration,
          MyNodeVisitor visitor)
        {

            var item = _mp.PopulateItem(namedValueTypeDeclaration);
            visitor.Items.Add(item);

            var tocSchemaItem = new TocSchema.Item
            {
                Uid = item.Uid,
                Name = item.FullName

            };

            visitor.TocSchemaItems.Add(tocSchemaItem);

            //classDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(visitor, this));


            visitor.Schema.Items = visitor.Items.ToArray();

            _s.SchemaToYaml(visitor.Schema, namedValueTypeDeclaration.FullyQualifiedName);
            visitor.Schema = new YamlSchema();
            visitor.Items.Clear();
        }

        public virtual void CreateStructuredTypeYaml(
          IStructuredTypeDeclaration structuredTypeDeclaration,
          MyNodeVisitor visitor)
        {

            var item = _mp.PopulateItem(structuredTypeDeclaration);
            visitor.Items.Add(item);

            var tocSchemaItem = new TocSchema.Item
            {
                Uid = item.Uid,
                Name = item.FullName

            };

            visitor.TocSchemaItems.Add(tocSchemaItem);


            visitor.Schema.Items = visitor.Items.ToArray();

            _s.SchemaToYaml(visitor.Schema, structuredTypeDeclaration.FullyQualifiedName);
            visitor.Schema = new YamlSchema();
            visitor.Items.Clear();
        }
    }


}
