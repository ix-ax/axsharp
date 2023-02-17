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
