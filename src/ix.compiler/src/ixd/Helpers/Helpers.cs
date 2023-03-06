using AX.ST.Semantic.Model.Declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixc_doc.Helpers
{
    internal static class Helpers
    {
        public static string GetBaseUid(IDeclaration declaration) => $"plc.{declaration.FullyQualifiedName}";
        public static string GetBaseUid(string @namespace) => $"plc.{@namespace}";
    }
}
