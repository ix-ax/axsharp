[wip]
# Ix.Framework.Blazor

This is Ix.Framework.Blazor framework. Providing automatic generation of simple UI from PLC objects for Blazor Server application.

---

## Prerequisites

TODO


- Blazor Server project based on .NET5
- TwinCAT 3 project
- IX installed with PLC Connector set up and running
- Entry.cs class with PLC instance (look at app example for this class) 

 ---
## Installing
After you have created your Blazor Server project, you need to do the following steps:

**Install latest NuGet package Ix.Framework.Blazor**

~~~
$ dotnet add package Ix.Presentation.Blazor.Controls --version 0.6.4-alpha.75
~~~

or modify you csproj file

~~~
<PackageReference Include="Ix.Presentation.Blazor.Controls" Version="0.6.4-alpha.75" />
~~~

**Add Ix.Framework renderable namespace to Blazor application.**

Add the following line to your `_Imports.razor` file:

```
@using Ix.Presentation.Blazor.Controls.RenderableContent
```
**Register Ix.Framework.Blazor services in DI container of your application.**

Add the following line to your `ConfigureServices` method located in `Startup.cs`:
```
builder.Services.AddIxBlazorServices();
```
Ix.Framework.Blazor services are located in *Ix.Presentation.Blazor.Services* namespace.

**Build and start your PLC Connector on startup of Blazor app.**

Add the following line to your `Configure` method located in `Startup.cs`:
```
Entry.Plc.Connector.BuildAndStart();
```
Note: Replace Plc with the name of your PLC instance.

---
## How to use


To access PLC variables and enable two-way binding between source PLC object and UI elements you can look at the following example.

```C#
@page "/"
@inherits RenderableComponentBase

<p>@Entry.Plc.Counter.Cyclic</p>

@code
{       
    protected override void OnInitialized()
    {
        UpdateValuesOnChange(Entry.Plc.MAIN);
    }
}
```
 
Thanks to the inheritance of *RenderableComponentBase* class and availability of *UpdateValuesOnChange* method values from PLC will be automatically updated in UI.

---

## RenderableContentControl

The following line demonstrates the basic usage of the RenderableContentControl component:

```
<RenderableContentControl Presentation="Control"
                          Context="@Entry.Plc.prgWeatherStations"/>
```

You can find the RenderableContentControl documentation in **[RENDERABLECONTENT](RENDERABLECONTENT.md)** file.

---
