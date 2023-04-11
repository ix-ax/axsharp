// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Exceptions;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs;

/// <summary>
///     Provides a series of helpers for semantics.
/// </summary>
public static class SemanticsHelpers
{
    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="field">Field declaration</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <param name="coBuilder">Lateral builder signature</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForTranspile(this IFieldDeclaration field, ISourceBuilder sourceBuilder, string coBuilder = "")
    {
        return field.AccessModifier == AccessModifier.Public 
               && field.Type.IsTypeEligibleForTranspile(sourceBuilder)
               && !IsToBeOmitted(field, sourceBuilder, coBuilder);
    }


    private static bool IsToBeOmitted(this IStorageDeclaration fieldDeclaration, ISourceBuilder sourceBuilder, string coBuilder)
    {

        var compilerOmitsAttribute = fieldDeclaration.Pragmas.FirstOrDefault(p =>
            p.Content.StartsWith("#ix-attr:[CompilerOmitsAttribute(") ||
            p.Content.StartsWith("#ix-attr:[CompilerOmits("));

        if (compilerOmitsAttribute == null)
        {
            return false;
        }

        try
        {
            var startParameters = compilerOmitsAttribute.Content.IndexOf('(');
            var parametersLength = compilerOmitsAttribute.Content.IndexOf(')') - startParameters - 1;

            if (startParameters >= 0 && parametersLength >= 0)
            {
                var parameters =
                    compilerOmitsAttribute.Content.Substring(startParameters + 1, parametersLength)
                        .Split(',').Select(p => p.Replace('"', ' ').Trim());

                var paramsArray = parameters as string[] ?? parameters.ToArray();
                return paramsArray.Any(p => p == sourceBuilder.BuilderType || p == coBuilder) || string.IsNullOrEmpty(paramsArray?.First());
            }
            else
            {
                // No parameters
                return true;
            }
        }
        catch (Exception e)
        {
            throw new FailedToParseCompilerOmittsPragma($"Failed to parse compiler omits pragma at: {compilerOmitsAttribute.Location.GetLineSpan()}", e);
        }
    }

    /// <summary>
    /// Determines whether the member or type is eligible for generation.
    /// </summary>
    /// <param name="typeDeclaration"></param>
    /// <param name="sourceBuilder"></param>
    /// <returns></returns>
    public static bool IsTypeEligibleForTranspile(this ITypeDeclaration typeDeclaration, ISourceBuilder sourceBuilder)
    {
        return !(typeDeclaration is IReferenceTypeDeclaration)
               &&
               (typeDeclaration is IScalarTypeDeclaration ||
                typeDeclaration is IStringTypeDeclaration ||
                typeDeclaration is IStructuredTypeDeclaration ||
                typeDeclaration is INamedValueTypeDeclaration ||
                sourceBuilder.Compilation.GetSemanticTree().Types.Any(p => p.FullyQualifiedName == typeDeclaration.FullyQualifiedName))
            ;
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="variable">Variable declaration</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <param name="coBuilder">Co-builder signature (e.g. POCO, Onliner, etc.)</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForTranspile(this IVariableDeclaration variable, ISourceBuilder sourceBuilder, string coBuilder = "")
    {
        return variable.IsInGlobalMemory 
               && variable.Type.IsTypeEligibleForTranspile(sourceBuilder)
               && !IsToBeOmitted(variable, sourceBuilder, coBuilder); 
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="field">Field declaration</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <param name="coBuilder">Lateral builder</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForConstructor(this IFieldDeclaration field, ISourceBuilder sourceBuilder, string coBuilder = "")
    {
        return field.AccessModifier == AccessModifier.Public && field.IsMemberEligibleForTranspile(sourceBuilder, coBuilder);
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="variable">Variable declaration</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <param name="coBuilder">Lateral builder</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForConstructor(this IVariableDeclaration variable, ISourceBuilder sourceBuilder, string coBuilder = "")
    {
        return variable.IsMemberEligibleForTranspile(sourceBuilder, coBuilder);
    }

    /// <summary>
    /// Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="arrayTypeDeclaration"></param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <returns></returns>
    public static bool IsMemberEligibleForConstructor(this IArrayTypeDeclaration arrayTypeDeclaration, ISourceBuilder sourceBuilder)
    {
        return arrayTypeDeclaration.ElementTypeAccess.Type.IsTypeEligibleForTranspile(sourceBuilder);
    }

    /// <summary>
    ///     Create triple-slash documentation.
    /// </summary>
    /// <param name="docComment">Documentation comment</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <returns></returns>
    public static string AddDocumentationComment(this IDocComment docComment, ISourceBuilder sourceBuilder)
    {
        return docComment.Content;
    }
}