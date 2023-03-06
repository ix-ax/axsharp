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
using NuGet.Packaging;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


namespace Ix.ixc_doc.Visitors
{
    public class YamlBuilder : IYamlBuiderVisitor
    {
        private YamlSerializer _s { get; set; }
        private YamlHelpers _yh { get; set; }
        private CodeToYamlMapper _mp { get; set; }
        internal YamlBuilder(YamlSerializer serializer, string projectPath)
        {
            _yh = new YamlHelpers(projectPath);
            _mp = new CodeToYamlMapper(_yh);
            _s = serializer;
            
        }

        public virtual void CreateNamespaceYaml(INamespaceDeclaration namespaceDeclaration, MyNodeVisitor v)
        {
            // create toc item
            var tocSchemaItem = new TocSchema.Item(namespaceDeclaration, namespaceDeclaration.Name);

            var hasTypes = namespaceDeclaration.Declarations.Any(p => p is IStructuredTypeDeclaration || p is IInterfaceDeclaration);
          

            // add to namespace group if is not global
            if (namespaceDeclaration.FullyQualifiedName != "$GLOBAL")
                if (_yh.FindTocGroup(v.YamlHelper.TocSchema.Items, _yh.GetBaseUid(namespaceDeclaration)) == null && hasTypes)
                    _yh.AddToTocSchema(v, tocSchemaItem, _yh.GetBaseUid(namespaceDeclaration));

            // iterate through children

            namespaceDeclaration.Declarations.ToList().ForEach(p =>
            {
                p.Accept(v, this);
                _yh.AddNamespaceReference(p, v);
            });

            if (namespaceDeclaration.FullyQualifiedName != "$GLOBAL")
            {
                //populate item of namespace
                var item = namespaces.FirstOrDefault(p => p.Id == Helpers.Helpers.GetBaseUid(namespaceDeclaration));
                if (item == null)
                {
                    namespaces.Add(_mp.PopulateItem(namespaceDeclaration));
                }
                else
                {
                    item.Children.AddRange(namespaceDeclaration.Declarations.Select(p => _yh.GetBaseUid(p)));
                }

                

                if (references.ContainsKey(Helpers.Helpers.GetBaseUid(namespaceDeclaration)))
                {
                    references[Helpers.Helpers.GetBaseUid(namespaceDeclaration)].AddRange(v.YamlHelper.NamespaceReferences);
                }
                else
                {
                    references[Helpers.Helpers.GetBaseUid(namespaceDeclaration)] = v.YamlHelper.NamespaceReferences;
                }



                v.YamlHelper.Schema.Items.AddRange(new Item[] { item });
                v.YamlHelper.Schema.References = references[Helpers.Helpers.GetBaseUid(namespaceDeclaration)].ToArray();
                _s.SchemaToYaml(v.YamlHelper.Schema, _yh.GetBaseUid(namespaceDeclaration));
                v.YamlHelper.Schema = new YamlSchema();
                v.YamlHelper.NamespaceReferences.Clear();
            }
        }

        private List<Item> namespaces = new List<Item>();
        private Dictionary<string, List<Reference>> references = new Dictionary<string, List<Reference>>();

        //operation on semantic tree
        public virtual void CreateClassYaml(IClassDeclaration classDeclaration, MyNodeVisitor v)
        {
            var item = _mp.PopulateItem(classDeclaration);
            
            //add to items
            v.YamlHelper.Items.Add(item);
            _yh.AddReferences(item.InheritedMembers, v);
            _yh.AddReferences(item.Implements, v);

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
            _s.SchemaToYaml(v.YamlHelper.Schema, _yh.GetBaseUid(classDeclaration));
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
            _s.SchemaToYaml(visitor.YamlHelper.Schema, _yh.GetBaseUid(namedValueTypeDeclaration));
            //clear schema for next use
            visitor.YamlHelper.Schema = new YamlSchema();
            visitor.YamlHelper.Items.Clear();
            visitor.YamlHelper.References.Clear();
        }

        public virtual void CreateInterfaceYaml(IInterfaceDeclaration interfaceDeclaration, MyNodeVisitor v)
        {
            var item = _mp.PopulateItem(interfaceDeclaration);
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
            _s.SchemaToYaml(v.YamlHelper.Schema, _yh.GetBaseUid(interfaceDeclaration));
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
