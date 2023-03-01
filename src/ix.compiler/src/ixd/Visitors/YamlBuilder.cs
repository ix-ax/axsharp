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


namespace Ix.ixc_doc.Visitors
{
    public class YamlBuilder : IYamlBuiderVisitor
    {
        private YamlSerializer _s { get; set; }
        private YamlHelpers _yh { get; set; }
        private CodeToYamlMapper _mp { get; set; }
        internal YamlBuilder(YamlSerializer serializer)
        {
            _mp = new CodeToYamlMapper();
            _s = serializer;
            _yh = new YamlHelpers();
        }

        public virtual void CreateNamespaceYaml(INamespaceDeclaration namespaceDeclaration, MyNodeVisitor v)
        {
            //populate item of namepsace
            var item = _mp.PopulateItem(namespaceDeclaration);
            // create toc item
            var tocSchemaItem = new TocSchema.Item(namespaceDeclaration.FullyQualifiedName, namespaceDeclaration.Name);

            // add to namespace group if is not global
            if (namespaceDeclaration.FullyQualifiedName != "$GLOBAL")
                if (_yh.FindTocGroup(v.YamlHelper.TocSchema.Items, namespaceDeclaration.FullyQualifiedName) == null)
                    _yh.AddToTocSchema(v, tocSchemaItem, namespaceDeclaration.ContainingNamespace.FullyQualifiedName);

            // iterate through children
            namespaceDeclaration.Declarations.ToList().ForEach(p =>
            {
                p.Accept(v, this);
                _yh.AddNamespaceReference(p, v);
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
            item.Assemblies = new string[] { _yh.GetAssembly(v) };
            //add to items
            v.YamlHelper.Items.Add(item);
            _yh.AddInheritedMembersReferences(item, v);

            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            if (item.Namespace != "$GLOBAL")
            {
                //class is grouped in namespace
                _yh.AddToTocSchema(v, tocSchemaItem, item.Namespace);
            }
            else
            {
                // if global, class is not grouped
                _yh.AddToTocSchema(v, tocSchemaItem, null);
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
            _yh.AddReference(fieldDeclaration.Type, visitor);
        }

        public virtual void CreateMethodYaml(IMethodDeclaration methodDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(methodDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var returnType = methodDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
            if (returnType != null)
                _yh.AddReference(returnType.Type, visitor);
        }

        public virtual void CreateNamedValueTypeYaml(INamedValueTypeDeclaration namedValueTypeDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(namedValueTypeDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            _yh.AddToTocSchema(visitor, tocSchemaItem, item.Namespace);

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
            item.Assemblies = new string[] { _yh.GetAssembly(v) };
            v.YamlHelper.Items.Add(item);

            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            if (item.Namespace != "$GLOBAL")
            {
                //interface is grouped in namespace
                _yh.AddToTocSchema(v, tocSchemaItem, item.Namespace);
            }
            else
            {
                // if global, interface is not grouped
                _yh.AddToTocSchema(v, tocSchemaItem, null);
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
                _yh.AddReference(returnType.Type, visitor);
        }

        
    }
}
