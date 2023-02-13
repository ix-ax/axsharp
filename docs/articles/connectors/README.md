# IX connectors

Ix connectors provide connectivity between twin objects and a target system.

# Primitive Twins

Each elementary/primitive/base type is represented by a twin wrapper object that are summarily called **Onliners**. Onliners allow for direct access to the PLC variable via `Cyclic` (variable is read or written cyclically in specific time periods) or On demand `Get/SetAsync` (the variable is read or written as required by the consumer's program). There is also an off line value holder exposed via `Shadow` member (allows for manipulation and batch read and write of entire structures).

[Primitive types (Onliners)](../../api/Ix.Connector/Ix.Connector.md#ixconnectorvaluetypes-namespace)

## Cyclic access

**Cyclic access** allows for fast, low performance cost, two way access to the PLC variables. Cyclic values are being read and written in an optimized periodic loop. The controller twin object contains entire PLC program, it does not discriminate between the variables and object that are used by the consumer and those that are not. However the `Cyclic` values are accessed via communication interface only after they are accessed for the first time by the consumer program. Once a Primitive Twin is accessed via its `Cyclic` property it is queued for the cyclic reading.

Primitive Twins implement notification change when the cyclic property value changes via [INotifyPropertyChanged](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-7.0) interface implementation. This feature is particularly useful for visualization scenarios in presentation frameworks (UI/HMI) that support data binding (WPF, MAUI, Blazor, in limited ways WinForm).

Cyclic access may result in degraded performance when the cyclic loop contains too many cyclically accessed primitive twins. The perfomance will be largely dependtend on target PLC system's perfomance.

Primitive twin is accessed via `Cyclic` property

> **Warning**: `Cyclic` property is queued for reading in the perioding loop after the first access. The value is mediated via background cycle, therefor the validity of the value from `Cyclic` property may have jitter corresponing nature of the background periodic read.

~~~ C#
// Cyclic Read
/*
IMPORTANT! Notice that the property Cyclic will return type's default value when called for the first time.
The displayed value will the one that was valid at the last periodic read.
*/
Console.WriteLine($"{PlcTwin.Counter.Symbol} : {PlcTwin.Counter.Cyclic}");

// Cyclic Write
/*
Notice that the value of the Cyclic will be written to the PLC at the next iteration of the r/w cycle.
*/
PlcTwin.Counter.Cyclic = true;
~~~


## GetAsync and SetAsync methods

**GetAsync and SetAsync** provide read and write access to a single the PLC variable. In contrast to the cyclical access the Get/Set Async method accesses the variable uppon call immediately. `Awating` the call will result in accesing the variable and returning the control to the to the caller only after the operation was perfomed. 

~~~ C#
// GetAsync
public async Task Read()
{   
    var val = await PlcTwin.Counter.GetAsync(); 
    // Value has been read from the PLC at this point.
    Console.WriteLine($"{PlcTwin.Counter.Symbol} : {val}");
}

public async Task Write()
{
    // SetAsync
    await PlcTwin.RunCounter.SetAsync(true);
    // Value has been written to the PLC at this point.
}
~~~

>**Warning**: The `Get/SetAsync` methods access is more expensive on the communication. To to mitigate possible communication overload use batched access.

## Batched access

**Batched access** allows to read or write a group of variables in a single shot. Strictly speaking batched reading and writing are the operations that are performed with `TwinObjects`.There are several ways to access the data in a batched way. Easiest and the most straight forward way is to use extension methods *ReadAsync()* or *WriteAsync()**. 
During the batched **read** operation the values are stored in **LastValue** property of corresponding Primitive Twin.

During batched **write** operation the values written to controller are those that were stored in the **Cyclic** property of corresponding Primitive Twin.

~~~ C#
// The extension method for batched read/write live in this namespace
// remember to add following using statement to get access to those extension methods.
using Ix.Connector;

public class BatchedAccess
{
    public async Task ReadBatchedAsync()
    {
        // Reads whole structure settings
        await PlcTwin.Settings.ReadAsync();

        // Write values to the console
        Console.WriteLine($"{PlcTwin.Settings.PosX.Symbol}:{PlcTwin.Settings.PosX.LastValue});

        Console.WriteLine($"{PlcTwin.Settings.PosY.Symbol}:{PlcTwin.Settings.PosY.LastValue});

        Console.WriteLine($"{PlcTwin.Settings.PosZ.Symbol}:{PlcTwin.Settings.LastValue});
    }


    public void WriteBatchedAsync()
    {
        PlcController.MAIN.Settings.PosX.Cyclic = 100.0f;
        PlcController.MAIN.Settings.PosY.Cyclic = 120.0f;
        PlcController.MAIN.Settings.PosZ.Cyclic = 130.0f;

        // Writes all value of the settings structure.
        await PlcController.MAIN.Settings.WriteAsync();
    }
}
~~~

## Transfering values 

### From Online to Shadow
### From Shadow to Online


## Other useful properties of Primitive Twins

Let's have a sample program
```BASIC
PROGRAM MAIN
VAR
    {attribute addProperty Name "<#App#>" }
    _app : fbApp;
END_VAR
---
FUNCTION_BLOCK fbApp
VAR
    {attribute addProperty Name "<#Settings#>" }
    settings : stSettings;
END_VAR
---
TYPE stSettings :
    STRUCT
        {attribute addProperty Name "<#Lights off#>" }
        TurnLightsOff :BOOL;
    END_STRUCT
END_TYPE

```

### Symbol

Symbol is readonly property that is symbolic representation of the variable in the PLC program, which corresponds to instance path of the variable.

Symbol for `TurnLightsOff` is `MAIN._app.settings.TurnLightsOff` 

### AttributeName

AttributeName is default [added property](../../Inxton.vortex.compiler.console/Conceptual/AddedProperties.md) of all objects in Inxton.Vortex.Framework. They are used to represent the name of the variable or object in user friendly way. These attribute can be later used to label the variables consistently in the UI.

AttributeName for `TurnLightsOff` is `Lights off` . Text inside tags `<#` and `#>` is marked as LocalizedString. They are optional.

### AttributeUnits

AttributeUnit is default [added property](../../Inxton.vortex.compiler.console/Conceptual/AddedProperties.md) of all PrimitiveTwins in Inxton.Vortex.Framework. They are used to represent the unit of the variable.

~~~
{attribute addProperty Units "mm"}
_length : REAL;
~~~

~~~ C#
// Writes unit of the '_lenght' variable to the console.
System.Console.WriteLine(MainPlc.MAIN._length.AttributeUnits);
~~~

### AttributeMinimum

AttributeMinimum is default [added property](../../Inxton.vortex.compiler.console/Conceptual/AddedProperties.md) of all PrimitiveTwins in Inxton.Vortex.Framework. They are used to get or set the minimal value for the variable. By default this attribute contains the minimal value of given type.

~~~
{attribute addProperty Minimum 10.8f}
_length : REAL;
~~~

~~~ C#
// Writes min value of '_length' variable to the console.
System.Console.WriteLine(MainPlc.MAIN._length.AttributeMinimum);
~~~


### AttributeMaximum

AttributeMaximum is default [added property](../../Inxton.vortex.compiler.console/Conceptual/AddedProperties.md) of all PrimitiveTwins in Inxton.Vortex.Framework. They are used to get or set the maximal value for the variable. By default this attribute contains the maximal value of given type.

~~~
{attribute addProperty Maximum 1528.8f}
_length : REAL;
~~~

~~~ C#
// Writes max. value of '_length' variable to the console.
System.Console.WriteLine(MainPlc.MAIN._length.AttributeMaximum);
~~~

### AttributeMinimum and AttributeMaximum at run time

When the application tries to write to the variable it first validates that the value to be written corresponds to limit given by AttributeMinimum and AttributeMaximum. If the value to be written does not fall withing the range this value is not written to the PLC and writing operation is silently ignored.

### AttributeToolTip

AttributeToolTip allows you to describe the variable or an object. These can be then used to give short hints for the user in the application. This attribute can be localized.

~~~
{attribute addProperty ToolTip "Value indicates the length of product."}
_length : REAL;
~~~

~~~ C#
// Writes max. value of '_length' variable to the console.
System.Console.WriteLine(MainPlc.MAIN._length.AttributeToolTip);
~~~

### HumanReadable

HumanReadable property is concatenation of AttributeName in the hierarchy of the twin, where single AttributeName(s) are separated by '.'. Human readable can be used to represent the path to the object in user friendly manner.

HumanReadable from `TurnLightsOff` is `App.Settings.Lights off`



## [Dummy](../../api/Ix.Connector/Ix.Connector/DummyConnector.md)

Provides a possibility to work with PLC twins wihout target system.

## [WebAPI](../../api/Ix.Connector.S71500.WebAPI/Ix.Connector.S71500.WebAPI.md)

Provides connectivity to S7-15XX PLC systems using WebAPI interface.