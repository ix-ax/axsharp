// Ix.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using Ix.Compiler.Core;
using IX.Compiler.Core;
using Ix.Compiler.Cs.Helpers;
using Ix.Connector;
using Ix.Connector.BuilderHelpers;
using System;

namespace Ix.Compiler.Cs.Onliner;

internal class CsOnlinerConfigurationConstructorBuilder : CsOnlinerConstructorBuilder
{
    protected CsOnlinerConfigurationConstructorBuilder(Compilation compilation) : base(compilation)
    {
    }

    public new static CsOnlinerConfigurationConstructorBuilder Create(IxNodeVisitor visitor,
        IConfigurationDeclaration semantics, IxProject project, Compilation compilation)
    {
        var builder = new CsOnlinerConfigurationConstructorBuilder(compilation);
        builder.AddToSource(
            $"public {project.TargetProject.ProjectRootNamespace}TwinController({typeof(ConnectorAdapter).n()} adapter, object[] parameters) {{");
        builder.AddToSource("this.Connector = adapter.GetConnector(parameters);");

        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("}");


        builder.AddToSource(
            $"public {project.TargetProject.ProjectRootNamespace}TwinController({typeof(ConnectorAdapter).n()} adapter) {{");
        builder.AddToSource("this.Connector = adapter.GetConnector(adapter.Parameters);");

        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("}");

        return builder;
    }

    public override void CreateVariableDeclaration(IVariableDeclaration semantics, IxNodeVisitor visitor)
    {
        if (semantics.IsMemberEligibleForConstructor(Compilation))
        {
            switch (semantics.Type)
            {
                case IEnumTypeDeclaration @enum:
                    AddMemberInitialization(@enum, semantics, visitor);
                    break;
                case INamedValueTypeDeclaration namedValue:
                    AddMemberInitialization(namedValue, semantics, visitor);
                    break;
                case IArrayTypeDeclaration array:
                    AddArrayMemberInitialization(array, semantics, visitor);
                    break;
                case IScalarTypeDeclaration scalar:
                    AddMemberInitialization(scalar, semantics, visitor);
                    break;
                case IStringTypeDeclaration @string:
                    AddMemberInitialization(@string, semantics, visitor);
                    break;
                case IClassDeclaration @class:
                    AddMemberInitialization(@class, semantics, visitor);
                    break;
            }

            AddToSource(semantics.SetProperties());
            AddToSource(semantics.AddAnnotations());
        }
    }

    private void AddArrayMemberInitialization(IArrayTypeDeclaration type, IVariableDeclaration field,
        IxNodeVisitor visitor)
    {
        AddToSource($"{field.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        AddToSource(";");


        AddToSource($"{typeof(Arrays).n()}.InstantiateArray({field.Name}, " +
                    "this, " +
                    "\"\", " +
                    $"\"{field.Name}\", " +
                    "(p, rt, st) => new");
        type.ElementTypeAccess.Type.Accept(visitor, this);
        AddToSource("(p, rt, st));");
    }

    private void AddMemberInitialization(IClassDeclaration type, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        AddToSource($"(this.Connector, \"\", \"{variable.Name}\");");
    }

    private void AddMemberInitialization(IScalarTypeDeclaration type, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource($"= @Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(type)}");
        AddToSource($"(this.Connector, \"\", \"{variable.Name}\");");
    }

    private void AddMemberInitialization(IStringTypeDeclaration type, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource($"= @Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(type)}");
        AddToSource($"(this.Connector, \"\", \"{variable.Name}\");");
    }

    private void AddMemberInitialization(IEnumTypeDeclaration enumType, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource("= @Connector.ConnectorAdapter.AdapterFactory.CreateINT");
        AddToSource($"(this.Connector, \"\", \"{variable.Name}\");");
        AddToSource(variable.SetProperties());
    }

    private void AddMemberInitialization(INamedValueTypeDeclaration namedValueType, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource("= @Connector.ConnectorAdapter.AdapterFactory.Create", string.Empty);
        namedValueType.Type.Accept(visitor, this);
        AddToSource($"(this, \"{variable.GetAttributeNameValue(variable.Name)}\", \"{variable.Name}\");");
    }
}