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
using Ix.ixc_doc.Visitors;
using YamlDotNet.Serialization;
using Ix.ixc_doc.Models;

namespace Ix.ixc_doc.Helpers
{
    public class YamlHelpers
    {
        // return all inherited members from class declaration
        public string[] GetInheritedMembers(IClassDeclaration classDeclaration)
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
                                 .Concat(extendedMethods.Select(m => GetMethodUId(m))).ToArray();
        }
        // check if field can be inherited
        public bool CanBeFieldInherited(IFieldDeclaration field, ITypeDeclaration subClass, ITypeDeclaration baseClass)
        {
            return field.AccessModifier == AccessModifier.Public ||
                (field.AccessModifier == AccessModifier.Internal && subClass.ContainingNamespace == baseClass.ContainingNamespace);
        }

        //acquire comments from source code
        public Comments GetComments(Location location)
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
        // parse xml comments
        public void GetClassFromXml(XmlNode element, ref Comments comments)
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
        // return formatted text from xml comments
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

                string description;
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

    
        //create toc schema, grouped if namespace exists, or only global
        public void AddToTocSchema(MyNodeVisitor visitor, TocSchema.Item tocSchemaItem, string? tocGroup)
        {
            if (tocGroup == null || tocGroup == "" || tocGroup == "$GLOBAL")
            {
                visitor.YamlHelper.TocSchema.Items.Add(tocSchemaItem);
            }
            else
            {
                var item = FindTocGroup(visitor.YamlHelper.TocSchema.Items, tocGroup);
                if (item != null)
                {
                    item.Items.Add(tocSchemaItem);
                }
                else
                {
                    visitor.YamlHelper.TocSchema.Items.Add(tocSchemaItem);
                }
            }
        }

        //check for existing group in toc list
        public TocSchema.Item? FindTocGroup(List<TocSchema.Item> items, string tocGroup)
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

        //acquiring assembly of ax project
        public string GetAssembly(MyNodeVisitor visitor)
        {
            var reader = new StringReader(File.ReadAllText(visitor.axProject.ProjectFile));
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            Dictionary<string, object> deserializeDictionary = deserializer.Deserialize<Dictionary<string, object>>(reader);

            object name;
            deserializeDictionary.TryGetValue("name", out name);
            return name.ToString();
        }

          //add references of inherited members
        public void AddInheritedMembersReferences(Item item, MyNodeVisitor v)
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

        //add references for namespace
        public void AddNamespaceReference(IDeclaration declaration, MyNodeVisitor v)
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
        //add general reference
        public void AddReference(IDeclaration declaration, MyNodeVisitor v)
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
    }
}
