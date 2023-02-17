using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.Text;
using CommandLine;
using Ix.ixc_doc.Enums;
using Ix.ixc_doc.Schemas;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.ObjectGraphVisitors;
using static Ix.ixc_doc.Visitors.YamlBuilder;

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

            var extendedMethods = classDeclaration.GetMethodsFromExtendedTypes();
            List<IFieldDeclaration> extendedFields = new List<IFieldDeclaration>();
            classDeclaration.GetAllExtendedTypes().ToList()
                .Select(p => ((IClassDeclaration)p).Fields.Where(f => CanBeFieldInherited(f, classDeclaration, p))).ToList()
                .ForEach(list => extendedFields.Concat(list));



            return new Item
            {
                Uid = classDeclaration.FullyQualifiedName,
                Id = classDeclaration.Name,
                Parent = classDeclaration.ContainingNamespace.Name,
                Children = children.Concat(methods).ToArray(),
                Name = classDeclaration.Name,
                FullName = classDeclaration.FullyQualifiedName,
                Type = ItemType.Class.ToString(),
                Namespace = classDeclaration.ContainingNamespace.Name,
                Summary = comments.summary,
                Syntax = new Syntax { Content = $"CLASS {classDeclaration.Name}" },
                Inheritance = classDeclaration.GetAllExtendedTypes().Select(p => p.FullyQualifiedName).ToArray(),
                InheritedMembers = GetInheritedMembers(classDeclaration),


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

            };
        }


        public Item PopulateItem(IMethodDeclaration methodDeclaration)
        {
            //TODO implement documentation for methods parameters and return values
            // when xml documentation is detected, add only description

          

            //var inputVariables = methodDeclaration.Variables.Where(v => v.Section == Section.Input)
            //    .Select(v => v.Section.ToString()).ToArray();

            //var returnType = methodDeclaration.Variables.Where(v => v.Section == Section.Return)
            //    .Select(v => v.Section.ToString()).ToArray();

            //foreach (var item in x)
            //{
            //    var section = item.Section;
            //}
            var methods = methodDeclaration.GetDescendentNodes();
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
                Syntax = new Syntax
                {
                    Content = "CLASS MyTestClass"

                },


            };
        }


        private string[] GetInheritedMembers(IClassDeclaration classDeclaration)
        {
            var extendedMethods = classDeclaration.GetMethodsFromExtendedTypes();
            var extendedFields = new List<IFieldDeclaration>();

            // TODO check, if IClassDeclaration in sufficient
            classDeclaration.GetAllExtendedTypes().ToList()
                .Select(p => ((IClassDeclaration)p).Fields.Where(f => CanBeFieldInherited(f, classDeclaration, p))).ToList()
                .ForEach(list => extendedFields.Concat(list));

            return extendedFields.Select(f => f.FullyQualifiedName)
                                 .Concat(extendedMethods.Select(m => m.FullyQualifiedName)).ToArray();
        }
        private bool CanBeFieldInherited(IFieldDeclaration field, ITypeDeclaration subClass, ITypeDeclaration baseClass)
        {
         
            return field.AccessModifier == AccessModifier.Public || 
                (field.AccessModifier == AccessModifier.Internal && subClass.ContainingNamespace == baseClass.ContainingNamespace);
        }

     

        private Comments GetComments(Location location)
        {
            int start = location.FullSpan.Start;
            string text = ((SourceLocation)location).SourceText.ToString();
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
