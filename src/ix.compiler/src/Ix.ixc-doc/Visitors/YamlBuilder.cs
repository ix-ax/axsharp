using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using CommandLine;
using Ix.ixc_doc.Helpers;
using Ix.ixc_doc.Interfaces;
using Ix.ixc_doc.Mapper;
using Ix.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public virtual void CreateNamespaceYaml(INamespaceDeclaration namespaceDeclaration, MyNodeVisitor v)
        {
            //populate item of namepsace
            var item = _mp.PopulateItem(namespaceDeclaration);

            // create toc item
            var tocSchemaItem = new TocSchemaList.ItemList(namespaceDeclaration.FullyQualifiedName, namespaceDeclaration.Name);

            // add to namespace group if is not global
            if (namespaceDeclaration.FullyQualifiedName != "$GLOBAL")
                if (FindTocGroup(v.YamlHelper.TocSchemaList.Items, namespaceDeclaration.FullyQualifiedName) == null)
                    AddToTocSchema(v, tocSchemaItem, namespaceDeclaration.ContainingNamespace.FullyQualifiedName);

            // iterate through children
            namespaceDeclaration.Declarations.ToList().ForEach(p =>
            {
                p.Accept(v, this);
                AddToNamespaceReference(p, v);
            });

            if (namespaceDeclaration.FullyQualifiedName != "$GLOBAL")
            {
                v.YamlHelper.Schema.Items = new Item[] { item };
                v.YamlHelper.Schema.References = v.YamlHelper.NamespaceReferences.ToArray();
                _s.SchemaToYaml(v.YamlHelper.Schema, namespaceDeclaration.FullyQualifiedName);
                v.YamlHelper.Schema = new YamlSchema();
                v.YamlHelper.NamespaceReferences.Clear();
            }
        }

        //operation on semantic tree
        public virtual void CreateClassYaml(IClassDeclaration classDeclaration, MyNodeVisitor v)
        {
            var item = _mp.PopulateItem(classDeclaration);
            //add to items
            v.YamlHelper.Items.Add(item);
            AddInheritedMembersReferences(item, v);

            var tocSchemaItem = new TocSchemaList.ItemList(item.Uid, item.FullName);

            if (item.Namespace != "$GLOBAL")
            {
                //class is grouped in namespace
                AddToTocSchema(v, tocSchemaItem, item.Namespace);
            }
            else
            {
                // if global, class is not grouped
                AddToTocSchema(v, tocSchemaItem, null);
            }

            classDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(v, this));

            //map helpers list to schema lists
            v.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(v.YamlHelper.Schema, classDeclaration.FullyQualifiedName);
            //clear schema for next use
            v.YamlHelper.Schema = new YamlSchema();
            v.YamlHelper.Items.Clear();
            v.YamlHelper.References.Clear();
        }

        //operation on semantic tree
        public virtual void CreateFieldYaml(IFieldDeclaration fieldDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(fieldDeclaration);
            visitor.YamlHelper.Items.Add(item);
            AddFieldReference(fieldDeclaration, visitor);
        }

        public virtual void CreateMethodYaml(IMethodDeclaration methodDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(methodDeclaration);
            visitor.YamlHelper.Items.Add(item);
        }

        private void AddInheritedMembersReferences(Item item, MyNodeVisitor v)
        {
            foreach (var member in item.InheritedMembers)
            {
                v.YamlHelper.References.Add(new Reference()
                {
                    Uid = member,
                    CommentId = member,
                    Parent = member,
                    Name = member.Split('.').Last(),
                    NameWithType = member.Split('.').Last(),
                    FullName = member

                });
            }
        }

        //create toc schema, grouped if namespace exists, or only global
        private void AddToTocSchema(MyNodeVisitor visitor, TocSchemaList.ItemList tocSchemaItem, string? tocGroup)
        {
            if (tocGroup == null || tocGroup == "" || tocGroup == "$GLOBAL")
            {
                visitor.YamlHelper.TocSchemaList.Items.Add(tocSchemaItem);
            }
            else
            {
                FindTocGroup(visitor.YamlHelper.TocSchemaList.Items, tocGroup).Items.Add(tocSchemaItem);
            }
        }

        //check for existing group in toc list
        private TocSchemaList.ItemList? FindTocGroup(List<TocSchemaList.ItemList> items, string tocGroup)
        {
            foreach (var item in Enumerable.Reverse(items).ToList())
            {
                if (item.Name == tocGroup)
                    return item;
                if (item.Items.Count > 0)
                {
                    var itemClass = FindTocGroup(item.Items, tocGroup);
                    if (itemClass != null)
                        return itemClass;
                }
            }
            return null;
        }

        //add references for namespace
        private void AddToNamespaceReference(IDeclaration declaration, MyNodeVisitor v)
        {
            var reference = new Reference
            {
                Uid = declaration.FullyQualifiedName,
                Name = declaration.Name,
                FullName = declaration.FullyQualifiedName,
                NameWithType = declaration.Name
            };
            v.YamlHelper.NamespaceReferences.Add(reference);
        }

        private void AddFieldReference(IDeclaration declaration, MyNodeVisitor v)
        {
            var reference = new Reference
            {
                Uid = declaration.Type.FullyQualifiedName,
                Name = declaration.Type.Name,
                FullName = declaration.Type.FullyQualifiedName,
                NameWithType = declaration.Type.Name
            };
            v.YamlHelper.References.Add(reference);
        }
    }
}
