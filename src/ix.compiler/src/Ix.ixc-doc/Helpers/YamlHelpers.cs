using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Declarations;
using AX.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ix.ixc_doc.Schemas;

namespace Ix.ixc_doc.Helpers
{
    internal class YamlHelpers
    {
        internal string[] GetInheritedMembers(IClassDeclaration classDeclaration)
        {
            var extendedMethods = classDeclaration.GetMethodsFromExtendedTypes();
            IEnumerable<IFieldDeclaration> extendedFields = new List<IFieldDeclaration>();

            // TODO check, if IClassDeclaration in sufficient
            var members = classDeclaration.GetAllExtendedTypes().ToList()
                .Select(p => ((IClassDeclaration)p).Fields.Where(f => CanBeFieldInherited(f, classDeclaration, p))).ToList();

            classDeclaration.GetAllExtendedTypes().ToList()
               .Select(p => ((IClassDeclaration)p).Fields.Where(f => CanBeFieldInherited(f, classDeclaration, p))).ToList()
               .ForEach(member => extendedFields = extendedFields.Concat(member));

            return extendedFields.Select(f => f.FullyQualifiedName)
                                 .Concat(extendedMethods.Select(m => m.FullyQualifiedName)).ToArray();
        }
        internal bool CanBeFieldInherited(IFieldDeclaration field, ITypeDeclaration subClass, ITypeDeclaration baseClass)
        {
            return field.AccessModifier == AccessModifier.Public ||
                (field.AccessModifier == AccessModifier.Internal && subClass.ContainingNamespace == baseClass.ContainingNamespace);
        }

        internal Comments GetComments(Location location)
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

        internal void GetClassFromXml(XmlNode element, ref Comments comments)
        {
            PropertyInfo? prop = comments.GetType().GetProperty(element.Name, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    prop.PropertyType.GetMethod("Add").Invoke(prop.GetValue(comments, null), new[] { element.Attributes[0].Value, element.ChildNodes[0].Value });
                }
                else
                {
                    prop.SetValue(comments, GetTextFromXml(element, prop), null);
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

        private string GetTextFromXml(XmlNode element, PropertyInfo? prop)
        {
            string text = "";

            if (element.HasChildNodes)
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    if(node.Value != null)
                    {
                        text += node.Value;
                    } else
                    {
                        switch (node.Name)
                        {
                            case "code":
                                text += "<pre><code>";
                                break;
                            case "c":
                                text += "<code>";
                                break;
                            default:
                                text += "<" + node.Name + ">";
                                break;
                        }
                        
                        text += GetTextFromXml(node, prop);
                        
                        switch (node.Name)
                        {
                            case "code":
                                text += "</code></pre>";
                                break;
                            case "c":
                                text += "</code>";
                                break;
                            default:
                                text += "</" + node.Name + ">";
                                break;
                        }
                    }
                }
            }
            return text;
        }

        //creates parameter types for serialize purposes and creates declaration string
        public (List<Parameter>,string) CreateParametersAndDeclarationString(IList<IVariableDeclaration> variableDeclarations, Comments comments)
        { 
            var inputParams = new List<Parameter>();
            StringBuilder fullDeclaration = new StringBuilder();
            foreach (var p in variableDeclarations)
            {
                var parameter = new Parameter
                {
                    Id = p.Name,
                    Type = p.Type.FullyQualifiedName 
                 };

                //TODO add filtering by name
                string description = string.Empty;
                comments.param.TryGetValue(p.Name, out description);
                parameter.Description = description;

                inputParams.Add(parameter);
                fullDeclaration.Append($"in {parameter.Type} {parameter.Id}");
                if(variableDeclarations.Last() != p) 
                { 
                    fullDeclaration.Append(",");
                }
            }

            return(inputParams, fullDeclaration.ToString());
        }

        //get uid of method
        public string GetMethodUId(IMethodDeclaration methodDeclaration)
        {
            StringBuilder typeDeclaration = new StringBuilder();
            var inputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            foreach (var p in inputParamsDeclaration)
            {
                typeDeclaration.Append(p.Type);
                if(inputParamsDeclaration.Last() != p) 
                { 
                    typeDeclaration.Append(",");
                }
            }
            return $"{methodDeclaration.FullyQualifiedName}({typeDeclaration.ToString()})";
        }

        public string GetMethodUId(IMethodPrototypeDeclaration methodDeclaration)
        {
            StringBuilder typeDeclaration = new StringBuilder();
            var inputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            foreach (var p in inputParamsDeclaration)
            {
                typeDeclaration.Append(p.Type);
                if (inputParamsDeclaration.Last() != p)
                {
                    typeDeclaration.Append(",");
                }
            }
            return $"{methodDeclaration.FullyQualifiedName}({typeDeclaration.ToString()})";
        }

        public class Comments
        {
            public string summary { get; set; }
            public Dictionary<string, string> param { get; set; } = new();
            public string example { get; set; }
            public string returns { get; set; }
            public string remarks { get; set; }
        }
    }
}
