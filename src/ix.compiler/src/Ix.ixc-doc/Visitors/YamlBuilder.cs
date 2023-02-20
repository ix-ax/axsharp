using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using Ix.ixc_doc.Interfaces;
using Ix.ixc_doc.Mapper;
using Ix.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public virtual void CreateNamespaceYaml(INamespaceDeclaration namespaceDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(namespaceDeclaration);
            //visitor.NamespaceItems.Add(item);

            var tocSchemaItem = new TocSchemaList.ItemList
            {
                Uid = namespaceDeclaration.FullyQualifiedName,
                Name = namespaceDeclaration.Name
            };

            if (namespaceDeclaration.FullyQualifiedName != "$GLOBAL")
                if(FindTocClass(visitor.TocSchemaList.Items, namespaceDeclaration.FullyQualifiedName) == null)
                    AddToTocSchema(visitor, tocSchemaItem, namespaceDeclaration.ContainingNamespace.FullyQualifiedName);

            namespaceDeclaration.Declarations.ToList().ForEach(p => p.Accept(visitor, this));

            visitor.Schema.Items = new Item[] { item };

            _s.SchemaToYaml(visitor.Schema, namespaceDeclaration.FullyQualifiedName);
            visitor.Schema = new YamlSchema();
            //visitor.NamespaceItems.Clear();
        }

        //operation on semantic tree
        public virtual void CreateClassYaml(IClassDeclaration classDeclaration, MyNodeVisitor visitor)
        {
            //var mapper = new CodeToYamlMapper();

            var item = _mp.PopulateItem(classDeclaration);
            visitor.Items.Add(item);

            var tocSchemaItem = new TocSchemaList.ItemList
            {
                Uid = item.Uid,
                Name = item.FullName
            };

            if (item.Namespace != "$GLOBAL")
            {
                AddToTocSchema(visitor, tocSchemaItem, item.Namespace);
            }
            else
            {
                AddToTocSchema(visitor, tocSchemaItem, null);
            }

            classDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(visitor, this));

            visitor.Schema.Items = visitor.Items.ToArray();

            _s.SchemaToYaml(visitor.Schema, classDeclaration.FullyQualifiedName);
            visitor.Schema = new YamlSchema();
            visitor.Items.Clear();
        }

        private void AddToTocSchema(MyNodeVisitor visitor, TocSchemaList.ItemList tocSchemaItem, string? tocClass)
        {
            if (tocClass == null || tocClass == "" || tocClass == "$GLOBAL")
            {
                visitor.TocSchemaList.Items.Add(tocSchemaItem);
            }
            else
            {
                FindTocClass(visitor.TocSchemaList.Items, tocClass).Items.Add(tocSchemaItem);
            }
        }

        private TocSchemaList.ItemList? FindTocClass(List<TocSchemaList.ItemList> items, string tocClass)
        {
            foreach (var item in Enumerable.Reverse(items).ToList())
            {
                if (item.Name == tocClass)
                    return item;
                if (item.Items.Count > 0){
                    var itemClass = FindTocClass(item.Items, tocClass);
                    if (itemClass != null)
                        return itemClass;
                }
            }
            return null;
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

            var tocSchemaItem = new TocSchemaList.ItemList
            {
                Uid = item.Uid,
                Name = item.FullName

            };

            visitor.TocSchemaList.Items.Add(tocSchemaItem);

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

            var tocSchemaItem = new TocSchemaList.ItemList
            {
                Uid = item.Uid,
                Name = item.FullName
            };

            visitor.TocSchemaList.Items.Add(tocSchemaItem);

            visitor.Schema.Items = visitor.Items.ToArray();

            _s.SchemaToYaml(visitor.Schema, structuredTypeDeclaration.FullyQualifiedName);
            visitor.Schema = new YamlSchema();
            visitor.Items.Clear();
        }
    }


}
