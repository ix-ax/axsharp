﻿using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.Text;
using Ix.ixc_doc.Enums;
using Ix.ixc_doc.Schemas;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace Ix.ixc_doc.Mapper
{
    internal class CodeToYamlMapper
    {


        public Item PopulateItem(IDeclaration declaration)
        {


            return new Item
            {
                Uid = declaration.Name,
                Id = declaration.Name,
                //Parent = classDeclaration.;
                //Children = children.Concat(methods).ToArray(),
                Name = declaration.Name,
                FullName = declaration.Name,
                Type = "Class",
                Namespace = declaration.Name,
                Summary = "Test class doc",
                Syntax = new Syntax { Content = "CLASS MyTestClass" },

                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }

        public Item PopulateItem(IClassDeclaration classDeclaration)
        {

            var children = classDeclaration.Fields.Select(p => p.FullyQualifiedName);
            var methods = classDeclaration.Methods.Select(p => p.FullyQualifiedName);
            var comments = GetComments(classDeclaration.Location);

            return new Item
            {
                Uid = classDeclaration.FullyQualifiedName,
                Id = classDeclaration.Name,
                Parent = classDeclaration.ExtendedType?.FullyQualifiedName,
                Children = children.Concat(methods).ToArray(),
                Name = classDeclaration.Name,
                FullName = classDeclaration.FullyQualifiedName,
                Type = ItemType.Class.ToString(),
                Namespace = classDeclaration.ContainingNamespace.Name,
                Summary = comments.summary,
                Syntax = new Syntax { Content = $"CLASS {classDeclaration.Name}" },


                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }

        public Item PopulateItem(IFieldDeclaration fieldDeclaration)
        {

            return new Item
            {
                Uid = fieldDeclaration.FullyQualifiedName,
                Id = fieldDeclaration.Name,
                //Parent = ,
                Name = fieldDeclaration.Name,
                FullName = fieldDeclaration.FullyQualifiedName,
                Type = ItemType.Property.ToString(),
                Namespace = fieldDeclaration.ContainingNamespace.Name,
                Summary = "Test class doc",
                Syntax = new Syntax { Content = "CLASS MyTestClass" },

                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }


        public Item PopulateItem(IMethodDeclaration methodDeclaration)
        {

            return new Item
            {
                Uid = methodDeclaration.FullyQualifiedName,
                Id = methodDeclaration.Name,
                //Parent = ,
                Name = methodDeclaration.Name,
                FullName = methodDeclaration.FullyQualifiedName,
                Type = ItemType.Method.ToString(),
                Namespace = methodDeclaration.ContainingNamespace.Name,
                Summary = "Test class doc",
                Syntax = new Syntax { Content = "CLASS MyTestClass" },
            };
        }

        public Item PopulateItem(INamedValueTypeDeclaration namedValueTypeDeclaration)
        {
            return new Item
            {
                Uid = namedValueTypeDeclaration.FullyQualifiedName,
                Id = namedValueTypeDeclaration.Name,
                //Parent = classDeclaration.;
                //Children = children.Concat(methods).ToArray(),
                Name = namedValueTypeDeclaration.Name,
                FullName = namedValueTypeDeclaration.FullyQualifiedName,
                Type = "Struct",
                Namespace = namedValueTypeDeclaration.Name,
                Summary = GetComments(namedValueTypeDeclaration.Location).summary,
                Syntax = new Syntax { Content = "CLASS MyTestClass" },

                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }

        public Item PopulateItem(IStructuredTypeDeclaration structuredTypeDeclaration)
        {
            return new Item
            {
                Uid = structuredTypeDeclaration.FullyQualifiedName,
                Id = structuredTypeDeclaration.Name,
                //Parent = classDeclaration.;
                //Children = children.Concat(methods).ToArray(),
                Name = structuredTypeDeclaration.Name,
                FullName = structuredTypeDeclaration.FullyQualifiedName,
                Type = "Struct",
                Namespace = structuredTypeDeclaration.Name,
                Summary = GetComments(structuredTypeDeclaration.Location).summary,
                Syntax = new Syntax { Content = "CLASS MyTestClass" },

                //Inheritance = new string[] { classDeclaration?.ExtendedType?.Name },
            };
        }

        private Comments GetComments(Location location)
        {
            string text = ((SourceLocation)location).SourceText.ToString();
            int lineStart = location.GetFullLineSpan().StartLinePosition.Line;
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
                xmlDoc.LoadXml("<root>" + commentsSection + "</root>");
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
            PropertyInfo? prop = comments.GetType().GetProperty(element.Name, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    prop.PropertyType.GetMethod("Add").Invoke(prop.GetValue(comments, null), new[] { element.ChildNodes[0].Value });
                }
                else
                {
                    prop.SetValue(comments, element.ChildNodes[0].Value, null);
                }
            }
            else
            {
                if (element.HasChildNodes)
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        GetClassFromXml(node, ref comments);
                    }
                }
            }
        }

        public class Comments
        {
            public string summary { get; set; }
            public List<string> param { get; set; } = new List<string>();
            public string example { get; set; }
            public string returns { get; set; }
        }
    }
}
