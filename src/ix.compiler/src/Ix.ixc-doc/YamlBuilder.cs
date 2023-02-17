using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using AX.Text;
using Ix.ixc_doc.Interfaces;
using Ix.ixc_doc.Mapper;
using Ix.ixc_doc.Schemas;
using ix_doc_compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
            var a = GetComments(classDeclaration);
            //var mapper = new CodeToYamlMapper();

            var item = _mp.PopulateItem(classDeclaration, a);
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


        private Comments GetComments(IDeclaration declaration)
        {
            int start = declaration.Location.FullSpan.Start;
            string text = ((SourceLocation)declaration.Location).SourceText.ToString();
            int lineStart = text.Take(start).Count(a => a == '\n');
            string commentsSection = "";
            string[] lines = text.Split('\n');

            for (int i = lineStart - 1; i >= 0 && (lines[i].Trim() == "" || lines[i].Contains("///")); i--)
            {
                if (lines[i].Trim() != "")
                    commentsSection = lines[i].Trim().Substring(3).Trim() + commentsSection;
            }

            Comments comments = new Comments();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<root>" + commentsSection);
                foreach (XmlNode node in xmlDoc.ChildNodes)
                {
                    GetClassFromXml(node, ref comments);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
            }

            return comments;
        }

        private void GetClassFromXml(XmlNode element, ref Comments comments)
        {
            switch (element.Name)
            {
                case "summary":
                    comments.summary = element.ChildNodes[0].Value;
                    break;
                case "param":
                    if (comments.param == null)
                        comments.param = new();
                    comments.param.Add(element.ChildNodes[0].Value);
                    break;
                case "example":
                    comments.example = element.ChildNodes[0].Value;
                    break;
                case "returns":
                    comments.returns = element.ChildNodes[0].Value;
                    break;
                default:
                    if (element.HasChildNodes)
                    {
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            GetClassFromXml(node, ref comments);
                        }
                    }
                    break;
            }
        }

        public class Comments
        {
            public string summary;
            public List<string> param;
            public string example;
            public string returns;
        }
    }
}
