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
        return field.AccessModifier == AccessModifier.Public
               && !(field.Type is IReferenceTypeDeclaration)
               &&
               (field.Type is IScalarTypeDeclaration ||
                field.Type is IStringTypeDeclaration ||
                field.Type is IStructuredTypeDeclaration ||
                compilation.GetSemanticTree().Types.Any(p => p.FullyQualifiedName == field.Type.FullyQualifiedName));
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="variable">Variable declaration</param>
    /// <param name="compilation">Compilation unit</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForTranspile(this IVariableDeclaration variable, Compilation compilation)
    {
        return variable.IsInGlobalMemory
               && !(variable.Type is IReferenceTypeDeclaration)
               &&
               (variable.Type is IScalarTypeDeclaration ||
                variable.Type is IStringTypeDeclaration ||
                compilation.GetSemanticTree().Types.Any(p => p.FullyQualifiedName == variable.Type.FullyQualifiedName));
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="field">Field declaration</param>
    /// <param name="compilation">Compilation unit</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForConstructor(this IFieldDeclaration field, Compilation compilation)
    {
        return field.IsMemberEligibleForTranspile(compilation) &&
               !(field.Type is IInterfaceDeclaration);
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="variable">Variable declaration</param>
    /// <param name="compilation">Compilation unit</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForConstructor(this IVariableDeclaration variable, Compilation compilation)
    {
        return variable.IsMemberEligibleForTranspile(compilation) && !(variable.Type is IEnumTypeDeclaration) &&
               !(variable.Type is IInterfaceDeclaration);
    }
}