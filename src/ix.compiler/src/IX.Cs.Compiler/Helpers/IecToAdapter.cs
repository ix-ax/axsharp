// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AX.ST.Semantic.Model.Declarations.Types;

namespace Ix.Compiler.Cs
{
    internal static class IecToAdapterExtensions
    {
        public static string ToAdapterType(IStringTypeDeclaration type)
        {
            return type.NameWithoutCapacity;
        }

        public static string ToAdapterType(IScalarTypeDeclaration type)
        {
            return type.Name;
        }
    }
}
