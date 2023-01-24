// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AX.ST.Syntax.Tree;
using Ix.Compiler.Core;
using IX.Compiler.Core;
using Ix.Compiler.Cs.Helpers;
using Ix.Compiler.Cs.Helpers.Plain;
using Ix.Connector;

namespace Ix.Compiler.Cs.Onliner;

/// <summary>
///     Source builder for <see cref="Onliner" /> twins.
/// </summary>
public class CsOnlinerSourceBuilder : ICombinedThreeVisitor, ISourceBuilder
{
    private readonly StringBuilder _sourceBuilder = new();

    /// <summary>
    ///     Creates new instance of <see cref="CsOnlinerSourceBuilder" />
    /// </summary>
    /// <param name="project">Ix project name.</param>
    /// <param name="compilation">AX compilation</param>
    public CsOnlinerSourceBuilder(IxProject project,
        Compilation compilation)
    {
        Project = project;
        Compilation = compilation;
    }

    private Compilation Compilation { get; }


    private IxProject Project { get; }

    /// <inheritdoc />
    public void CreateFile(IFileSyntax fileSyntax, IxNodeVisitor visitor)
    {
        AddToSource("using System;");
        AddToSource("using Ix.Connector;");
        AddToSource("using Ix.Connector.ValueTypes;");
        AddToSource("using System.Collections.Generic;");
        fileSyntax.Declarations.ToList().ForEach(p => p.Visit(visitor, this));
    }


    /// <inheritdoc />
    public void CreateClassDeclaration(IClassDeclarationSyntax classDeclarationSyntax,
        IClassDeclaration classDeclaration,
        IxNodeVisitor visitor)
    {
        classDeclarationSyntax.UsingDirectives.ToList().ForEach(p => p.Visit(visitor, this));

        AddToSource(classDeclaration.Pragmas.AddAttributes());
        AddToSource($"{classDeclaration.AccessModifier.Transform()}partial class {classDeclaration.Name}");
        AddToSource(":");

        var isExtended = false;
        if (Compilation.GetSemanticTree().Types
            .Any(p => p.FullyQualifiedName == classDeclaration.ExtendedType?.Type.FullyQualifiedName))
        {
            AddToSource($"{classDeclarationSyntax.BaseClassName.FullyQualifiedIdentifier}");
            isExtended = true;
        }
        else
        {
            AddToSource(typeof(ITwinObject).n());
        }

        AddToSource(classDeclarationSyntax.ImplementsList != null ? ", " : string.Empty);
        classDeclarationSyntax.ImplementsList?.Visit(visitor, this);
        AddToSource("\n{");

        AddToSource(CsOnlinerMemberBuilder.Create(visitor, classDeclaration, Compilation).Output);

        AddToSource(CsOnlinerConstructorBuilder.Create(visitor, classDeclaration, Compilation, isExtended).Output);

        if (!isExtended) CreateITwinObjectImplementation();

        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateConfigDeclaration(IConfigDeclarationSyntax configDeclarationSyntax,
        IConfigurationDeclaration configurationDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(
            $"public partial class {Project.TargetProject.ProjectRootNamespace}TwinController : ITwinController {{");
        AddToSource($"public {typeof(Connector.Connector).n()} Connector {{ get; }}");
        AddToSource(CsOnlinerMemberBuilder.Create(visitor, configurationDeclaration, Compilation).Output);
        AddToSource(CsOnlinerConfigurationConstructorBuilder
            .Create(visitor, configurationDeclaration, Project, Compilation).Output);
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateConfigDeclaration(IConfigurationDeclaration configurationDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"public partial class {Project.TargetProject.ProjectRootNamespace} : ITwinController {{");
        AddToSource(@$"public {typeof(Connector.Connector).n()} Connector {{ get; }}");
        AddToSource(CsOnlinerConstructorBuilder.Create(visitor, configurationDeclaration, Project, Compilation).Output);
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateEnumTypeDeclaration(IEnumTypeDeclarationSyntax enumTypeDeclarationSyntax,
        ITypeDeclaration typeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource($"public enum {enumTypeDeclarationSyntax.Name.Text} {{");
        AddToSource(string.Join("\n,", enumTypeDeclarationSyntax.EnumValues.Select(p => p.Name.Text)));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateNamedValueTypeDeclaration(INamedValueTypeDeclarationSyntax namedValueTypeDeclarationSyntax,
        INamedValueTypeDeclaration namedValueTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource(
            $"public enum {namedValueTypeDeclarationSyntax.Name.Text} : {namedValueTypeDeclarationSyntax.Type.TransformType()} {{");

        // TODO: Value re-interpretation should be done according to the type.

        AddToSource(string.Join(",\n",
            namedValueTypeDeclaration.Values.Select(p => $"{p.Name} = {p.Value.ReinterpretValue<int>()}")));

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
    public void CreatePragma(IPragma pragma, ICombinedThreeVisitor visitor)
    {
        if (pragma.Content.StartsWith("#ix:")) AddToSource(pragma.Content.Remove(0, 4));
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
    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CreateStructuredType(IStructTypeDeclarationSyntax structTypeDeclarationSyntax,
        IStructuredTypeDeclaration structuredTypeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(structuredTypeDeclaration.Pragmas.AddAttributes());
        AddToSource(
            $"{structuredTypeDeclaration.AccessModifier.Transform()}partial class {structTypeDeclarationSyntax.Name.Text}");
        AddToSource(":");

        AddToSource(typeof(ITwinObject).n());

        AddToSource("\n{");

        AddToSource(CsOnlinerMemberBuilder.Create(visitor, structuredTypeDeclaration, Compilation).Output);

        AddToSource(CsOnlinerConstructorBuilder.Create(visitor, structuredTypeDeclaration, Compilation).Output);

        CreateITwinObjectImplementation();

        AddToSource("}");
    }


    /// <inheritdoc />
    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string Group => "Onliners";

    /// <inheritdoc />
    public string Output => _sourceBuilder.ToString().FormatCode();

    /// <inheritdoc />
    public string OutputFileSuffix => ".g.cs";

    private void CreateITwinObjectImplementation()
    {
        AddToSource(
            $"private IList<{typeof(ITwinObject).n()}> Children {{ get; }} = new List<{typeof(ITwinObject).n()}>();" +
            $"public IEnumerable<{typeof(ITwinObject).n()}> GetChildren() {{ return Children;}}" +
            $"private IList<{typeof(ITwinElement).n()}> Kids {{get;}} = new List<{typeof(ITwinElement).n()}>();" +
            $"public IEnumerable<{typeof(ITwinElement).n()}> GetKids() {{ return Kids;}}" +
            $"private IList<{typeof(ITwinPrimitive).n()}> ValueTags {{get;}} = new List<{typeof(ITwinPrimitive).n()}>();" +
            $"public IEnumerable<{typeof(ITwinPrimitive).n()}> GetValueTags() {{ return ValueTags;}}" +
            $"public void AddValueTag({typeof(ITwinPrimitive).n()} valueTag) {{ ValueTags.Add(valueTag); }}" +
            $"public void AddKid({typeof(ITwinElement).n()} kid) {{ Kids.Add(kid); }}" +
            $"public void AddChild({typeof(ITwinObject).n()} twinObject) {{ Children.Add(twinObject); }}" +
            $"protected {typeof(Connector.Connector).n()} @Connector{{ get; }}" +
            $"public {typeof(Connector.Connector).n()} GetConnector() {{ return this.@Connector; }}" +
            "public string GetSymbolTail() { return this.SymbolTail; }" +
            $"public {typeof(ITwinObject).n()} GetParent() {{ return this.@Parent; }}" +
            "public string Symbol { get; protected set; }" +
            "public System.String AttributeName { get; set; }" +
            "public string HumanReadable { get; set; }" +
            "protected System.String @SymbolTail { get; set;}" +
            $"protected {typeof(ITwinObject).n()} @Parent {{ get; set; }}"
        );
    }

    private void AddToSource(string token, string separator = " ")
    {
        _sourceBuilder.Append($"{token}{separator}");
    }
}