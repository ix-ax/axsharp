// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AX.ST.Syntax.Tree;
using Ix.Compiler.Core;
using IX.Compiler.Core;
using Ix.Compiler.Cs.Helpers;
using Ix.Compiler.Cs.Helpers.Plain;

namespace Ix.Compiler.Cs.Plain;

/// <summary>
///     Provides builder for Plain twin objects.
/// </summary>
public class CsPlainSourceBuilder : ICombinedThreeVisitor, ISourceBuilder
{
    /// <summary>
    ///     Creates new instance of <see cref="CsPlainSourceBuilder" />
    /// </summary>
    /// <param name="project">Ix project</param>
    /// <param name="compilation">AX compilation</param>
    public CsPlainSourceBuilder(IxProject project,
        Compilation compilation)
    {
        Project = project;
        Compilation = compilation;
    }

    private IxProject Project { get; }

    private Compilation Compilation { get; }


    private StringBuilder _sourceBuilder { get; } = new();

    /// <inheritdoc />
    public void CreateClassDeclaration(IClassDeclarationSyntax classDeclarationSyntax,
        IClassDeclaration classDeclaration,
        IxNodeVisitor visitor)
    {
        classDeclarationSyntax.UsingDirectives.ToList().ForEach(p => p.Visit(visitor, this));
        AddToSource($"{classDeclaration.AccessModifier.Transform()}partial class {classDeclaration.Name}");

        if (Compilation.GetSemanticTree().Types
            .Any(p => p.FullyQualifiedName == classDeclaration.ExtendedType?.Type.FullyQualifiedName))
            AddToSource($": {classDeclarationSyntax.BaseClassName.FullyQualifiedIdentifier}");

        AddToSource(classDeclarationSyntax.ImplementsList != null && classDeclarationSyntax.BaseClassName != null
            ? ", "
            : string.Empty);
        AddToSource(classDeclarationSyntax.ImplementsList != null && classDeclarationSyntax.BaseClassName == null
            ? " : "
            : string.Empty);
        classDeclarationSyntax.ImplementsList?.Visit(visitor, this);
        AddToSource("{");
        classDeclarationSyntax.UsingDirectives.ToList().ForEach(p => p.Visit(visitor, this));
        classDeclaration.Fields.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateNamespaceDeclaration(INamespaceDeclarationSyntax namespaceDeclarationSyntax,
        IxNodeVisitor visitor)
    {
        AddToSource($"namespace {namespaceDeclarationSyntax.Name.FullyQualifiedIdentifier} {{");
        namespaceDeclarationSyntax.NamespaceElements.ToList().ForEach(p => p.Visit(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        if (fieldDeclaration.IsMemberEligibleForTranspile(Compilation))
        {
            fieldDeclaration.Pragmas.AddAttributes();
            AddToSource($"{fieldDeclaration.AccessModifier.Transform()}");
            fieldDeclaration.Type.Accept(visitor, this);
            AddToSource($" {fieldDeclaration.Name}");
            AddToSource("{get; set;}");

            switch (fieldDeclaration.Type)
            {
                case IStringTypeDeclaration:
                    AddToSource(" = string.Empty;");
                    break;
                case IScalarTypeDeclaration scalar:
                    if (scalar.IsNullablePrimitive())
                        AddToSource($" = default({scalar.TransformType()});\n");
                    break;
                case IReferenceTypeDeclaration d:
                case IStructuredTypeDeclaration s:
                    AddToSource(" = new ");
                    fieldDeclaration.Type.Accept(visitor, this);
                    AddToSource("();");
                    break;
            }
        }
    }

    /// <inheritdoc />
    public virtual void CreateNamedValueTypeDeclaration(INamedValueTypeDeclaration namedValueTypeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(namedValueTypeDeclaration.ValueTypeAccess.Type.TransformType());
    }

    /// <inheritdoc />
    public void CreateFile(IFileSyntax fileSyntax, IxNodeVisitor visitor)
    {
        AddToSource("using System;");
        AddToSource("namespace Pocos {");
        fileSyntax.Declarations.ToList().ForEach(p => p.Visit(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateConfigDeclaration(IConfigDeclarationSyntax configDeclarationSyntax,
        IConfigurationDeclaration configurationDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource($"public partial class {Project.TargetProject.ProjectRootNamespace}{{");
        configurationDeclaration.Variables.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreatePragma(IPragma pragma, ICombinedThreeVisitor visitor)
    {
        // if (semantics.Content.StartsWith("#ix")) OutputBuilder.AppendLine(semantics.Content.Remove(0, 3));
    }

    /// <inheritdoc />
    public void CreateEnumTypeDeclaration(IEnumTypeDeclarationSyntax enumTypeDeclarationSyntax,
        ITypeDeclaration typeDeclaration,
        IxNodeVisitor visitor)
    {
        // We do not compile enums in Plains, only in Onliners.
    }

    /// <inheritdoc />
    public void CreateUsingDirective(IUsingDirectiveSyntax usingDirectiveSyntax, ICombinedThreeVisitor visitor)
    {
        usingDirectiveSyntax.QualifiedIdentifierList.ListElements.ToList().ForEach(p =>
        {
            if (Compilation.GetSemanticTree().Namespaces
                .Any(n => n.FullyQualifiedName == p.Name.FullyQualifiedIdentifier))
                AddToSource($"using {p.Name.FullyQualifiedIdentifier};\n");
        });
    }

    /// <inheritdoc />
    public void CreateImplementsList(IImplementsListSyntax implementsListSyntax, ICombinedThreeVisitor visitor)
    {
        AddToSource(string.Join(", ",
            implementsListSyntax.QualifiedIdentifierList.ListElements.Select(p => p.Name.FullyQualifiedIdentifier)));
    }

    /// <inheritdoc />
    public void CreateInterfaceDeclaration(IInterfaceDeclarationSyntax interfaceDeclarationSyntax,
        IInterfaceDeclaration interfaceDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource($"{interfaceDeclaration.AccessModifier.Transform()} interface {interfaceDeclaration.Name} {{}}");
    }

    /// <inheritdoc />
    public void CreateConfigDeclaration(IConfigurationDeclaration configurationDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"public partial class {Project.TargetProject.ProjectRootNamespace}{{");
        configurationDeclaration.Variables.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateVariableDeclaration(IVariableDeclaration semantics, IxNodeVisitor visitor)
    {
        if (semantics.IsMemberEligibleForTranspile(Compilation))
        {
            semantics.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));

            switch (semantics.Type)
            {
                case IStringTypeDeclaration d:
                    AddToSource($"public String {semantics.Name} {{get; set;}} = string.Empty;");
                    break;
                case IScalarTypeDeclaration d:
                    if (d.IsNonNullablePrimitive())
                        AddToSource(
                            $"public {d.TransformType()} {semantics.Name} {{get; set;}}");
                    else if (d.IsNullablePrimitive())
                        AddToSource(
                            $"public {d.TransformType()} {semantics.Name} {{get; set;}} = default({d.TransformType()});\n");
                    break;
                case ITypeDeclaration d:
                    AddToSource(
                        $"public {ShortedQualifiedIfPossible(semantics)} {semantics.Name} {{get; set;}} = new {ShortedQualifiedIfPossible(semantics)}();");
                    break;
            }
        }
    }

    /// <inheritdoc />
    public void CreateStructuredType(IStructTypeDeclarationSyntax structTypeDeclarationSyntax,
        IStructuredTypeDeclaration structuredTypeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(
            $"{structuredTypeDeclaration.AccessModifier.Transform()}partial class {structTypeDeclarationSyntax.Name.Text} ");
        AddToSource("{");
        structuredTypeDeclaration.Fields.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateReferenceToDeclaration(IReferenceTypeDeclaration referenceTypeDeclaration, IxNodeVisitor visitor)
    {
        referenceTypeDeclaration.ReferencedType.Accept(visitor, this);
    }

    /// <inheritdoc />
    public void CreateSemanticTypeAccess(ISemanticTypeAccess semanticTypeAccess, IxNodeVisitor visitor)
    {
        semanticTypeAccess.Type.Accept(visitor, this);
    }

    /// <inheritdoc />
    public void CreateScalarTypeDeclaration(IScalarTypeDeclaration scalarTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource(scalarTypeDeclaration.TransformType());
    }

    /// <inheritdoc />
    public void CreateClassDeclaration(IClassDeclaration classDeclaration, IxNodeVisitor data)
    {
        AddToSource(classDeclaration.GetQualifiedName());
    }

    /// <inheritdoc />
    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        AddToSource(interfaceDeclaration.GetQualifiedName());
    }

    /// <inheritdoc />
    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
        arrayTypeDeclaration.ElementTypeAccess.Type.Accept(visitor, this);
        AddToSource("[]");
    }

    /// <inheritdoc />
    public void CreateStringTypeDeclaration(IStringTypeDeclaration stringTypeDeclaration, IxNodeVisitor visitor)
    {
        stringTypeDeclaration.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource($"{stringTypeDeclaration.TransformType()}");
    }

    /// <inheritdoc />
    public void CreateStructuredType(IStructuredTypeDeclaration structuredTypeDeclaration, IxNodeVisitor visitor)
    {
        structuredTypeDeclaration.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource($"{structuredTypeDeclaration.GetQualifiedName()}");
    }

    /// <inheritdoc />
    public void CreateEnumTypeDeclaration(IEnumTypeDeclaration enumTypeDeclaration, IxNodeVisitor visitor)
    {
        if (enumTypeDeclaration.Type.FullyQualifiedName == enumTypeDeclaration.Type.Name)
            AddToSource($"{enumTypeDeclaration.Type.FullyQualifiedName}");
        else
            AddToSource($"{Project.TargetProject.ProjectRootNamespace}.{enumTypeDeclaration.Type.FullyQualifiedName}");
    }

    /// <inheritdoc />
    public string Output => _sourceBuilder.ToString().FormatCode();

    /// <inheritdoc />
    public string Group => "POCO";

    /// <inheritdoc />
    public string OutputFileSuffix => ".g.cs";

    private void AddToSource(string token, string separator = " ")
    {
        _sourceBuilder.Append($"{token}{separator}");
    }

    private static string ShortedQualifiedIfPossible(IDeclaration semantics)
    {
        return semantics.Type.FullyQualifiedName.StartsWith($"{semantics.ContainingNamespace.FullyQualifiedName}.")
            ? semantics.Type.FullyQualifiedName.Remove(0, semantics.ContainingNamespace.FullyQualifiedName.Length + 1)
            : semantics.Type.FullyQualifiedName;
    }
}