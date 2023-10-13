# IXR - resx compiler for AX

Ixr is command line tool which can generate resx files for AX projects. Resx file contains localizable strings from project, that can be further used in translation.

## Quick start

### 1. Install ixr tool

~~~
dotnet tool install AXSharp.ixr --prerelease --local
~~~

### 2. Run ixr compiler

~~~
dotnet ixr -x $PATH_TO_AX_PROJECT -o $PATH_TO_RESX_FILE
~~~

where:  
`$PATH_TO_AX_PROJECT` - is path to **AX project**  
`$PATH_TO_RESX_FILE` - is path to **resx file**, where the localizable strings will be generated

Localizable string are exported only from string attributes and pragmas of attribute name.

`ixr` can be run without parameters from within an AX project folder. In that case it will use the settings from `ixc` `AXSharp.config.json` and emit the resource file into `.g\Resource\PlcStringResource.resx` file of the ax twin project.

>[!NOTE]
>More about implementing resx file into your project see [here](~/articles/connectors/README.md#controller-string-localization).

## Localizable string

Localizable string must start with `<#` and end with `#>` tags.  
For example:  
`'<#This is localizable string#>'`

Also you can connecting localizable and non-localizable string together.  
For example:  
`'This is non-localizable string <#This is localizable string#>'`

## Special characters

Ixr support all these special characters:  
``!"#$'()*+,-.:;<=>?@[\]^_`{|}~€``  
No other characters can be used.

## Localization in a client in server-side application

In order to get translation in given culture for a client in a server side application you should use methods `Get{ProperyName}(CultureInfo culture)` to get translation for client current culture. The properties will returned original string where localization tokens are removed.

~~~csharp
var notTranlsated = obj.AttributeName;
var translated = obj.GetAttribute(new CultureInfo("sk-SK"));
~~~

## Notes

Ixr is still in early development, so some features may be missing and bugs may occur. 