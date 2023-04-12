﻿// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Globalization;
using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Compiler.Cs.Helpers.Onliners;
using AXSharp.Connector;
using AXSharp.Connector.BuilderHelpers;
using AXSharp.Compiler.Cs;

namespace AXSharp.Compiler.Cs.Onliner;

internal class CsOnlinerConstructorBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _constructorStatements = new();

    protected CsOnlinerConstructorBuilder(ISourceBuilder sourceBuilder)
    {
        SourceBuilder = sourceBuilder;
    }

    protected ISourceBuilder SourceBuilder { get; }

    public string Output => _constructorStatements.ToString().FormatCode();

    public void CreateClassDeclaration(IClassDeclaration classDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{classDeclaration.GetQualifiedName()}");
    }

    public void CreateReferenceToDeclaration(IReferenceTypeDeclaration referenceTypeDeclaration, IxNodeVisitor visitor)
    {
        referenceTypeDeclaration.ReferencedType.Accept(visitor, this);
    }

    public void CreateScalarTypeDeclaration(IScalarTypeDeclaration scalarTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{scalarTypeDeclaration.TransformType()}");
    }

    public void CreateSemanticTypeAccess(ISemanticTypeAccess semanticTypeAccess, IxNodeVisitor visitor)
    {
        semanticTypeAccess.Type.Accept(visitor, this);
    }

    public void CreateStringTypeDeclaration(IStringTypeDeclaration stringTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{stringTypeDeclaration.TransformType()}");
    }

    public void CreateStructuredType(IStructuredTypeDeclaration structuredTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{structuredTypeDeclaration.GetQualifiedName()}");
    }

    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        if (fieldDeclaration.IsMemberEligibleForConstructor(SourceBuilder))
        {
            switch (fieldDeclaration.Type)
            {
                case IArrayTypeDeclaration array:
                    AddArrayMemberInitialization(array, fieldDeclaration, visitor);
                    break;
                case IEnumTypeDeclaration @enum:
                    AddMemberInitialization(@enum, fieldDeclaration);
                    break;
                case INamedValueTypeDeclaration namedValue:
                    AddMemberInitialization(namedValue, fieldDeclaration, visitor);
                    break;
                case IScalarTypeDeclaration scalar:
                    AddMemberInitialization(scalar, fieldDeclaration);
                    break;
                case IStringTypeDeclaration @string:
                    AddMemberInitialization(@string, fieldDeclaration);
                    break;
                case IClassDeclaration @class:
                    AddMemberInitialization(@class, fieldDeclaration, visitor);
                    break;
            }

            AddToSource(fieldDeclaration.SetProperties());
            AddToSource(fieldDeclaration.AddAnnotations());
        }
    }

    public virtual void CreateNamedValueTypeDeclaration(INamedValueTypeDeclaration namedValueTypeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(namedValueTypeDeclaration.ValueTypeAccess.Type.Name.ToUpperInvariant());
    }

    public virtual void CreateVariableDeclaration(IVariableDeclaration semantics, IxNodeVisitor visitor)
    {
        if (semantics.IsMemberEligibleForConstructor(SourceBuilder))
        {
            AddToSource($"{semantics.Name}");
            semantics.Type.Accept(visitor, this);
            AddToSource($"(this, \"\", \"{semantics.Name}\");");
        }
    }

    public void CreateEnumTypeDeclaration(IEnumTypeDeclaration enumTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{enumTypeDeclaration.GetQualifiedName()}");
    }

    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        // No way to construct interfaces.
    }

    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
        arrayTypeDeclaration.ElementTypeAccess.Type.Accept(visitor, this);
        AddToSource("[");
        AddToSource(string.Join(",",
            arrayTypeDeclaration.Dimensions.Select(p => p.CountOfElements.ToString(CultureInfo.InvariantCulture))));
        AddToSource("]");
    }

    protected void AddToSource(string token, string separator = " ")
    {
        _constructorStatements.Append($"{token}{separator}");
    }

    public static CsOnlinerConstructorBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended)
    {
        var builder = new CsOnlinerConstructorBuilder(sourceBuilder);


        builder.AddToSource(
            $"public {semantics.Name}({typeof(ITwinObject).n()} parent, string readableTail, string symbolTail)");
        if (isExtended) builder.AddToSource(": base(parent, readableTail, symbolTail)");

        builder.AddToSource("{");

        builder.AddToSource(semantics.SetProperties());

        builder.AddToSource($"Symbol = {typeof(Connector.Connector).n()}.CreateSymbol(parent.Symbol, symbolTail);");

        if (!isExtended)
            builder.AddToSource(@$"this.@SymbolTail = symbolTail;
			    this.@Connector = parent.GetConnector();
			    this.@Parent = parent;
			    HumanReadable = {typeof(Connector.Connector).n()}.CreateHumanReadable(parent.HumanReadable, readableTail);");

        builder.AddToSource(@$"PreConstruct(parent, readableTail, symbolTail);");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        if (!isExtended)
        {
            builder.AddToSource("parent.AddChild(this);");
            builder.AddToSource("parent.AddKid(this);");
        }

        builder.AddToSource(@$"PostConstruct(parent, readableTail, symbolTail);");

        builder.AddToSource("}");
        return builder;
    }

    public static CsOnlinerConstructorBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerConstructorBuilder(sourceBuilder);


        builder.AddToSource(
            $"public {semantics.Name}({typeof(ITwinObject).n()} parent, string readableTail, string symbolTail)");


        builder.AddToSource("{");
        builder.AddToSource(@$"this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			HumanReadable = {typeof(Connector.Connector).n()}.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = {typeof(Connector.Connector).n()}.CreateSymbol(parent.Symbol, symbolTail);");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("parent.AddChild(this);");
        builder.AddToSource("parent.AddKid(this);");

        builder.AddToSource("}");

        return builder;
    }

    public static CsOnlinerConstructorBuilder Create(IxNodeVisitor visitor, IConfigurationDeclaration semantics,
        AXSharpProject project, ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerConstructorBuilder(sourceBuilder);
        builder.AddToSource(
            $"public {project.TargetProject.ProjectRootNamespace}({typeof(ConnectorAdapter).n()} adapter, object[] parameters) {{");
        builder.AddToSource("this.Connector = adapter.GetConnector(parameters);");

        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("}");
        return builder;
    }

    private void AddArrayMemberInitialization(IArrayTypeDeclaration type, IFieldDeclaration field,
        IxNodeVisitor visitor)
    {
        if(!type.IsMemberEligibleForConstructor(this.SourceBuilder))
            return;

        AddToSource($"{field.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        AddToSource(";");


        AddToSource($"{typeof(Arrays).n()}.InstantiateArray({field.Name}, " +
                    "this, " +
                    $"\"{field.GetAttributeNameValue(field.Name)}\", " +
                    $"\"{field.Name}\", " +
                    "(p, rt, st) => ");

        switch (type.ElementTypeAccess.Type)
        {
            
            case IClassDeclaration classDeclaration:
            case IStructuredTypeDeclaration structuredTypeDeclaration:
            case IEnumTypeDeclaration enumTypeDeclaration:
            case INamedValueTypeDeclaration namedValueTypeDeclaration:
                AddToSource("new");
                type.ElementTypeAccess.Type.Accept(visitor, this);
                break;
            case IScalarTypeDeclaration scalarTypeDeclaration:
                AddToSource($"@Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(scalarTypeDeclaration)}");
                break;
            case IStringTypeDeclaration stringTypeDeclaration:
                AddToSource($"@Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(stringTypeDeclaration)}");
                break;
        }

        AddToSource("(p, rt, st));");
    }

    private void AddMemberInitialization(IClassDeclaration type, IFieldDeclaration field, IxNodeVisitor visitor)
    {
        AddToSource($"{field.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
    }

    private void AddMemberInitialization(IScalarTypeDeclaration type, IFieldDeclaration field)
    {
        AddToSource($"{field.Name}");
        AddToSource($"= @Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(type)}");
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
    }

    private void AddMemberInitialization(IStringTypeDeclaration type, IFieldDeclaration field)
    {
        AddToSource($"{field.Name}");
        AddToSource($"= @Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(type)}");
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
    }

    // We get warning here about unused method, it is false positive, but we will need to investigate further the object hierarchy.
    private void AddMemberInitialization(IEnumTypeDeclaration enumType, IFieldDeclaration field)
    {
        AddToSource($"{field.Name}");
        AddToSource("= @Connector.ConnectorAdapter.AdapterFactory.CreateINT");
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
        AddToSource(field.SetProperties());
    }

    private void AddMemberInitialization(INamedValueTypeDeclaration namedValueType, IFieldDeclaration field,
        IxNodeVisitor visitor)
    {
        AddToSource($"{field.Name}");
        AddToSource("= @Connector.ConnectorAdapter.AdapterFactory.Create", string.Empty);
        namedValueType.Type.Accept(visitor, this);
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
    }

    public void AddTypeConstructionParameters(string parametersString)
    {
        AddToSource(parametersString);
    }

    /// <inheritdoc />
    public void CreateDocComment(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
    {
        AddToSource(semanticTypeAccess.AddDocumentationComment(SourceBuilder));
    }
}