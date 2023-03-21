// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using AX.ST.Semantic.Model.Declarations;
using AX.ST.Syntax.Tree;

namespace Ix.Compiler.Cs.Helpers;

internal static class CsHelpers
{
    public static string Transform(this IAccessModifierSyntax syntax)
    {
        return $"{syntax.ModifierKeyword.Text.ToLower()} ";
    }

    public static string Transform(this AccessModifier semantics)
    {
        return $"{semantics.ToString().ToLower()} ";
    }

    public static string GetQualifiedName(this IDeclaration declaration)
    {
        return declaration.FullyQualifiedName;
    }

    public static string? n(this Type type)
    {
        return type.FullName;
    }

    public static string GetAttributeNameValue(this IDeclaration declaration, string memberName)
    {
        return declaration.GetPropertyValue("AttributeName", memberName);
    }

    public static string CreateGenericSwapperMethodToPlainer(string methodName, string pocoTypeName)
    {
        return $"public T {methodName}<T>(){{\n return (dynamic)this.{methodName}Async().Result;\n}}";
    }

    public static string CreateGenericSwapperMethodFromPlainer(string methodName, string pocoTypeName)
    {
        return $"public void {methodName}<T>(T plain){{\n this.{methodName}Async((dynamic)plain).Wait();\n}}";
    }
}