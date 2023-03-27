using AX.ST.Semantic.Model.Declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.ixc_doc.Helpers
{
    internal static class Helpers
    {
        public static string GetBaseUid(IDeclaration declaration) => $"plc.{declaration.FullyQualifiedName}";
        
        public static string GetBaseUid(string @namespace) => $"plc.{@namespace}";

        public static string GetBaseUid(IFunctionDeclaration functionDeclaration)
        {
            var inputParamsDeclaration = functionDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            var declaration=GetMethodTypeDeclaration(inputParamsDeclaration);
            return $"plc.{functionDeclaration.FullyQualifiedName}({declaration})";
        }

        public static string GetBaseUid(IMethodDeclaration methodDeclaration)
        {
            var inputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            var declaration=GetMethodTypeDeclaration(inputParamsDeclaration);
            return $"plc.{methodDeclaration.FullyQualifiedName}({declaration})";
        }


        public static string GetBaseUid(IMethodPrototypeDeclaration methodDeclaration)
        {
            var inputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            var declaration=GetMethodTypeDeclaration(inputParamsDeclaration);
            return $"plc.{methodDeclaration.FullyQualifiedName}({declaration.ToString()})";
        }


        private static string GetMethodTypeDeclaration(IList<IVariableDeclaration> inputParams)
        { 
          StringBuilder typeDeclaration = new StringBuilder();
          
            foreach (var p in inputParams)
            {
                typeDeclaration.Append(p.Type);
                if(inputParams.Last() != p) 
                { 
                    typeDeclaration.Append(",");
                }
            }    
            return typeDeclaration.ToString();
        }
    }
}
