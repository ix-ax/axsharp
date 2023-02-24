// Ix.Compiler.CsTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Collections.ObjectModel;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Init;
using AX.ST.Semantic.Pragmas;
using AX.ST.Semantic.Symbols;
using AX.ST.Semantic.Tree;
using AX.Text;
using Ix.Compiler.Cs;
using Xunit.Abstractions;

/* Unmerged change from project 'Ix.CompilerTests (net6.0)'
Before:
using System.Collections.ObjectModel;
After:
using System.Collections.ObjectModel;
using IX;
using IX.CompilerTests;
using Ix.CompilerTests.Cs;
*/

namespace Ix.Compiler.CsTests;

public class PragmasExtensionsTests
{
    private readonly ITestOutputHelper output;

    public PragmasExtensionsTests(ITestOutputHelper output)
    {
        this.output = output;
    }


    [Fact]
    public void should_get_attribute_source()
    {
        var expected = "[Container(Layoyt.Wrap)]\r\n[Group(Layoyt.GroupBox)]";
        IEnumerable<IPragma> pragmas = new[]
        {
            new("#ix-attr:[Container(Layoyt.Wrap)]"),
            new PragmaMock("#ix-attr:[Group(Layoyt.GroupBox)]")
        };

        var actual = pragmas.AddAttributes();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_declare_property()
    {
        var expected = "public string AttributeName { get; set; }";
        var field = new TypeMock("someField",
            new ReadOnlyCollection<IPragma>(new IPragma[]
            {
                new PragmaMock("#ix-prop:public string AttributeName")
            }));

        var actual = field.DeclareProperties();

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void should_set_property_string()
    {
        var expected = "someField.AttributeName = \"This is name\";";
        var field = new FieldMock("someField",
            new ReadOnlyCollection<IPragma>(new IPragma[]
            {
                new PragmaMock("#ix-set:AttributeName = \"This is name\"")
            }));

        var actual = field.SetProperties();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_set_property_number()
    {
        var expected = "someField.AttributeMinimum = 10.5f;";
        var field = new FieldMock("someField",
            new ReadOnlyCollection<IPragma>(new IPragma[]
            {
                new PragmaMock("#ix-set:AttributeMinimum = 10.5f")
            }));

        var actual = field.SetProperties();

        Assert.Equal(expected, actual);
    }
}

public class PragmaMock : IPragma
{
    public PragmaMock(string content)
    {
        Content = content;
    }

    public string Content { get; }

    public Location Location => throw new NotImplementedException();

    public IEnumerable<ISemanticNode> ChildNodes => throw new NotImplementedException();

    public void Accept<T>(ISemanticNodeVisitor<T> visitor, T data)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ISemanticNode> GetDescendentNodes()
    {
        throw new NotImplementedException();
    }
}

public class FieldMock : IFieldDeclaration
{
    public FieldMock(string name, IReadOnlyCollection<IPragma> pragmas)
    {
        Name = name;
        Pragmas = pragmas;
    }

    public AccessModifier AccessModifier => throw new NotImplementedException();

    public Section Section => throw new NotImplementedException();

    public ISemanticTypeAccess TypeAccess => throw new NotImplementedException();

    public IDirectAccess Address => throw new NotImplementedException();

    public ISemanticInitializer Initializer => throw new NotImplementedException();

    public StorageModifier StorageModifier => throw new NotImplementedException();

    public bool IsConstant => throw new NotImplementedException();

    public bool IsDeclaredConstant => throw new NotImplementedException();

    public bool IsDeclaredReadOnly => throw new NotImplementedException();

    public ISymbol Symbol => throw new NotImplementedException();

    public string Name { get; }

    public string FullyQualifiedName => throw new NotImplementedException();

    public DeclarationKind Kind => throw new NotImplementedException();

    public IScope ContainingScope => throw new NotImplementedException();

    public INamespaceDeclaration ContainingNamespace => throw new NotImplementedException();

    public ITypeDeclaration Type => throw new NotImplementedException();

    public IReadOnlyCollection<IPragma> Pragmas { get; }

    public Location Location => throw new NotImplementedException();

    public IEnumerable<ISemanticNode> ChildNodes => throw new NotImplementedException();

    public void Accept<T>(ISemanticNodeVisitor<T> visitor, T data)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IDeclaration? other)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ISemanticNode> GetDescendentNodes()
    {
        throw new NotImplementedException();
    }
}

public class TypeMock : ITypeDeclaration
{
    public TypeMock(string name, IReadOnlyCollection<IPragma> pragmas)
    {
        Name = name;
        Pragmas = pragmas;
    }

    public IReadOnlyCollection<ISemanticTypeAccess> ExtendedTypeAccesses => throw new NotImplementedException();

    public IReadOnlyCollection<ISemanticTypeAccess> ImplementedInterfaces => throw new NotImplementedException();

    public GenericDataType GenericType => throw new NotImplementedException();

    public IScope LocalScope => throw new NotImplementedException();

    public ISemanticInitializer Initializer => throw new NotImplementedException();

    public AccessModifier AccessModifier => throw new NotImplementedException();

    public ulong? BitWidth => throw new NotImplementedException();

    public ISymbol Symbol => throw new NotImplementedException();

    public string Name { get; }

    public string FullyQualifiedName => throw new NotImplementedException();

    public DeclarationKind Kind => throw new NotImplementedException();

    public IScope ContainingScope => throw new NotImplementedException();

    public INamespaceDeclaration ContainingNamespace => throw new NotImplementedException();

    public ITypeDeclaration Type => throw new NotImplementedException();

    public IReadOnlyCollection<IPragma> Pragmas { get; }

    public Location Location => throw new NotImplementedException();

    public IEnumerable<ISemanticNode> ChildNodes => throw new NotImplementedException();

    public void Accept<T>(ISemanticNodeVisitor<T> visitor, T data)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IDeclaration? other)
    {
        throw new NotImplementedException();
    }

    public bool Extends(ITypeDeclaration type)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<ITypeDeclaration> GetAllExtendedTypes()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<IInterfaceDeclaration> GetAllImplementedInterfaces()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<IInterfaceDeclaration> GetAllImplementedInterfacesUniquely()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ISemanticNode> GetDescendentNodes()
    {
        throw new NotImplementedException();
    }

    public T GetLiteralValue<T>(string literal)
    {
        throw new NotImplementedException();
    }

    public bool Implements(IInterfaceDeclaration @interface)
    {
        throw new NotImplementedException();
    }

    public bool IsAssignableTo(ITypeDeclaration other)
    {
        throw new NotImplementedException();
    }

    public bool IsEqualType(ITypeDeclaration other)
    {
        throw new NotImplementedException();
    }

    public bool IsOfGenericType(GenericDataType genericType)
    {
        throw new NotImplementedException();
    }
}