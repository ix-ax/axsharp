using AX.ST.Semantic.Model.Declarations;
using Ix.ixc_doc.Enums;
using Ix.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.ObjectGraphVisitors;
using static Ix.ixc_doc.YamlBuilder;

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

        public Item PopulateItem(IClassDeclaration classDeclaration, Comments comments)
        {

            var children = classDeclaration.Fields.Select(p => p.FullyQualifiedName);
            var methods = classDeclaration.Methods.Select(p => p.FullyQualifiedName);


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
    }
}
