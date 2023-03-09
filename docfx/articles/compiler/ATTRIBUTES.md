# Attributes

Any type or type member (CLASS, STRUCT...) can have declared attributes from within `PLC` code using pragmas.

Syntax

~~~ iecst
{#ix-attr:[TypeAttribute()]}
~~~

Example

~~~iecst
{#ix-attr:[Container(Layoyt.Wrap)]} (* Example of an attribute declared at type level. *)
CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-attr:[Container(Layoyt.Tabs)]} (* Example of an attribute declared at member level.*)
        Nested : MyNestedStructure;
    END_VAR
END_CLASS
~~~

## Special attributes

### ReadOnce attribute

ReadOnce attribute instructs the communication layer to read a member (variable) only once during the application's lifetime.
This feature aims at reducing the load on the communication layer. You would typically use this attribute when you have immutable data
such as description strings needing to be accessed only once.
ReadOnce attribute will only limit reading the members during periodic (cyclic) access. It will not affect the direct reading of a variable 
or explicit call of batch reading on a complex structure.
ReadOnce attribute can be only applied to members (property, field), not type (CLASS, STRUCT) declaration.


Example

~~~iecst

CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-attr:[ReadOnce()]} // this structure will be read only once
        Nested : MyNestedStructure;
        {#ix-attr:[ReadOnce()]} // this variable will be read only once
        MyString : STRING;
    END_VAR
END_CLASS
~~~

### ReadOnly attribute

ReadOnly attribute render the member (variable) inaccessible for write operation from the IX application. It does not prevent the variable from writing in the PLC or by other means of connection.


~~~iecst

CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-attr:[ReadOnly()]} // this structure will be read only
        Nested : MyNestedStructure;
        {#ix-attr:[ReadOnly()]} // this variable will be read only
        MyString : STRING;
    END_VAR
END_CLASS
~~~

### CompilerOmits

CompilerOmits attribute instruct the compiler to skip the compilation of a member for specific output format. `IXC` supports these omissions:

|   Target    |                      Description                       |
| ----------- | ------------------------------------------------------ |
| Onliner     | Will skip the emission of the member for onliner twins |
| POCO        | Will skip the emission of the member for POCO twins    |
| <No params> | Will skip the emission for all output types            |


~~~iecst
CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-attr:[CompilerOmits()]} 
        MyStringIgnoredForAllOutputs : STRING;
        {#ix-attr:[CompilerOmits("Onliner")]} 
        MyStringIgnoredInOnliner : STRING;
        {#ix-attr:[CompilerOmits("POCO")]} 
        MyStringIgnoredInPocos : STRING;
        {#ix-attr:[CompilerOmits("POCO", "Onliner")]} 
        MyStringIgnoredInPocosAndOnliners : STRING;
    END_VAR
END_CLASS
~~~

### See also 

#### [RenderIgnore](../blazor/RENDERABLECONTENT.md#renderignore-and-custom-labels)