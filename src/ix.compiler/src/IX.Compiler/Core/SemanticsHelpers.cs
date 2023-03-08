// Ix.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;

namespace Ix.Compiler.Core;

/// <summary>
///     Provides a series of helpers for semantics.
/// </summary>
public static class SemanticsHelpers
{
    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="field">Field declaration</param>
    /// <param name="compilation">Compilation unit</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForTranspile(this IFieldDeclaration field, Compilation compilation)
    {
        return field.AccessModifier == AccessModifier.Public && field.Type.IsTypeEligibleForTranspile(compilation);
    }

    /// <summary>
    /// Determines whether the member or type is eligible for generation.
    /// </summary>
    /// <param name="typeDeclaration"></param>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static bool IsTypeEligibleForTranspile(this ITypeDeclaration typeDeclaration, Compilation compilation)
    {
        return !(typeDeclaration is IReferenceTypeDeclaration) 
               &&
               (typeDeclaration is IScalarTypeDeclaration ||
                typeDeclaration is IStringTypeDeclaration ||
                typeDeclaration is IStructuredTypeDeclaration ||
                typeDeclaration is INamedValueTypeDeclaration ||
                compilation.GetSemanticTree().Types.Any(p => p.FullyQualifiedName == typeDeclaration.FullyQualifiedName))

               ;
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="variable">Variable declaration</param>
    /// <param name="compilation">Compilation unit</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForTranspile(this IVariableDeclaration variable, Compilation compilation)
    {
        return variable.Type.IsTypeEligibleForTranspile(compilation);
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="field">Field declaration</param>
    /// <param name="compilation">Compilation unit</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForConstructor(this IFieldDeclaration field, Compilation compilation)
    {
        return field.AccessModifier == AccessModifier.Public && field.IsMemberEligibleForTranspile(compilation);
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="variable">Variable declaration</param>
    /// <param name="compilation">Compilation unit</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForConstructor(this IVariableDeclaration variable, Compilation compilation)
    {
        return variable.IsMemberEligibleForTranspile(compilation);
    }

    /// <summary>
    /// Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="arrayTypeDeclaration"></param>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static bool IsMemberEligibleForConstructor(this IArrayTypeDeclaration arrayTypeDeclaration, Compilation compilation)
    {
        return arrayTypeDeclaration.ElementTypeAccess.Type.IsTypeEligibleForTranspile(compilation);
    }
}