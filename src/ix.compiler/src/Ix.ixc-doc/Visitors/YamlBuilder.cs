using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using CommandLine;
using Ix.ixc_doc.Helpers;
using Ix.ixc_doc.Interfaces;
using Ix.ixc_doc.Mapper;
using Ix.ixc_doc.Schemas;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
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
                AddNamespaceReference(p, v);
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
            item.Assemblies = new string[] { GetAssembly(v) };
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
            AddReference(fieldDeclaration.Type, visitor);
        }

        public virtual void CreateMethodYaml(IMethodDeclaration methodDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(methodDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var returnType = methodDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
            if (returnType != null)
                AddReference(returnType.Type, visitor);
        }

        public virtual void CreateNamedValueTypeYaml(INamedValueTypeDeclaration namedValueTypeDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(namedValueTypeDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var tocSchemaItem = new TocSchemaList.ItemList(item.Uid, item.FullName);

            AddToTocSchema(visitor, tocSchemaItem, item.Namespace);

            //map helpers list to schema lists
            visitor.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(visitor.YamlHelper.Schema, namedValueTypeDeclaration.FullyQualifiedName);
            //clear schema for next use
            visitor.YamlHelper.Schema = new YamlSchema();
            visitor.YamlHelper.Items.Clear();
            visitor.YamlHelper.References.Clear();
        }

        public virtual void CreateInterfaceYaml(IInterfaceDeclaration interfaceDeclaration, MyNodeVisitor v)
        {
            var item = _mp.PopulateItem(interfaceDeclaration);
            item.Assemblies = new string[] { GetAssembly(v) };
            v.YamlHelper.Items.Add(item);

            var tocSchemaItem = new TocSchemaList.ItemList(item.Uid, item.FullName);

            if (item.Namespace != "$GLOBAL")
            {
                //interface is grouped in namespace
                AddToTocSchema(v, tocSchemaItem, item.Namespace);
            }
            else
            {
                // if global, interface is not grouped
                AddToTocSchema(v, tocSchemaItem, null);
            }

            interfaceDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(v, this));

            //map helpers list to schema lists
            v.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(v.YamlHelper.Schema, interfaceDeclaration.FullyQualifiedName);
            //clear schema for next use
            v.YamlHelper.Schema = new YamlSchema();
            v.YamlHelper.Items.Clear();
            v.YamlHelper.References.Clear();
        }

        public virtual void CreateMethodPrototypeYaml(IMethodPrototypeDeclaration methodPrototypeDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(methodPrototypeDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var returnType = methodPrototypeDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
            if (returnType != null)
                AddReference(returnType.Type, visitor);
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
                var item = FindTocGroup(visitor.YamlHelper.TocSchemaList.Items, tocGroup);
                if (item != null)
                {
                    item.Items.Add(tocSchemaItem);
                }
                else
                {
                    visitor.YamlHelper.TocSchemaList.Items.Add(tocSchemaItem);
                }
            }
        }

        //check for existing group in toc list
        private TocSchemaList.ItemList? FindTocGroup(List<TocSchemaList.ItemList> items, string tocGroup)
        {
            foreach (var item in Enumerable.Reverse(items).ToList())
            {
                if (item.Uid == tocGroup || item.Name == tocGroup)
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
        private void AddNamespaceReference(IDeclaration declaration, MyNodeVisitor v)
        {
            if (v.YamlHelper.NamespaceReferences.Where(a => a.Uid == declaration.FullyQualifiedName).Count() > 0)
                return;
            var reference = new Reference
            {
                Uid = declaration.FullyQualifiedName,
                Name = declaration.Name,
                FullName = declaration.FullyQualifiedName,
                NameWithType = declaration.Name
            };
            v.YamlHelper.NamespaceReferences.Add(reference);
        }

        private void AddReference(IDeclaration declaration, MyNodeVisitor v)
        {
            if (v.YamlHelper.References.Where(a => a.Uid == declaration.FullyQualifiedName).Count() > 0)
                return;
            var reference = new Reference
            {
                Uid = declaration.FullyQualifiedName,
                Name = declaration.Name,
                FullName = declaration.FullyQualifiedName,
                NameWithType = declaration.Name
            };
            v.YamlHelper.References.Add(reference);
        }

        private string GetAssembly(MyNodeVisitor visitor)
        {
            var reader = new StringReader(File.ReadAllText(visitor.axProject.ProjectFile));
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            Dictionary<string, object> deserializeDictionary = deserializer.Deserialize<Dictionary<string, object>>(reader);

            object name;
            deserializeDictionary.TryGetValue("name", out name);
            return name.ToString();
        }
    }
}
