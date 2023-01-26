# Custom libraries
Custom libraries are a way to store own created views with different presentation types.

To create a custom components library, two projects must be created:
- **Connector project** - which will contain PLC compiled classes from Ix Builder.
- **Razor components project** - Razor class library, which will contain the implementation of corresponding views with dependency on Connector project.

It is important to note that the generated classes in the Connector project and created Razor views must be in **same namespace** to correctly locate views.

See the example below:

We have three projects:
- *ComponentsExamples* - custom components library 
- *IxBlazor.App* - Blazor Server application
- *PlcConnector* - class library containing generated files from IX Builder

Files in the *ComponentsExample* and the *PlcConnector* are in the same namespace. *IxBlazor.App* has a reference to the *ComponentsExample* project.

![alt text](assets/external.png "Renderignore and custom labels")

*ComponentsExamples* library must contain `RenderableBlazorAssemblyAttribute()`. Thanks to this attribute RenderableContentControl is able to load its assembly and find all created custom view. 

This attribute can be added to `AssemblyInfo.cs` class located in `Properties` folder. If there is no `Properties` folder, just create it with `AssemblyInfo.cs` class and paste there following code:

```C#
using Ix.Presentation.Blazor.Attributes;

[assembly: RenderableBlazorAssemblyAttribute()]
```



There are two ways for implementation of custom component:
- Custom component with **code-behind**
- Custom component with **ViewModel** support

## Component with code-behind
This is simple approach, where Blazor view inherits from `RenderableComplexComponentBase<T>` base class, where `T` is your custom component PLC type. However logic of your component must be specified in code-behind of view class. Important is, that both view and code-behind must be in same namespace as your `PlcConnector`. 

Look at the example below:

Blazor view `IxComponentServiceView.razor`:

```C#
@namespace Plc
@inherits RenderableComplexComponentBase<IxComponent>

<h3>IxComponentServiceView</h3>

<p>IxBool serviceView: @Component.ix_bool.Cyclic</p>
<p>IxInt serviceView: @Component.ix_int.Cyclic</p>
<p>IxString serviceView: @Component.ix_string.Cyclic</p>

```
`IxComponentServiceView.razor` view inherits from `RenderableComplexComponentBase<IxComponent>`. Thanks to this, framework will inject instance of *IxComponent* type into *Component* variable. After that, you can access values of *Component* variable and define logic of your custom component in code-behind. Code-behind must be partial class with the same name as view.

Code-behind `IxComponentServiceView.cs`:
```C#
namespace Plc
{
    public partial class IxComponentServiceView
    {
        protected override void OnInitialized()
        {
            UpdateValuesOnChange(Component);
        }
    }
}

```

If you want your UI to be updated everytime, when PLC values change, you must call `UpdateValuesOnChange(Component)` method in `OnInitialized()` method in code-behind.

## Component with ViewModel

With this approach it is possible to create component using MVVM design pattern.

First, ViewModel must be created, which will inherits from abstract class `RenderableViewModelBase`:

ViewModel `IxComponentBaseViewModel.cs`
```C#
namespace Plc
{
    public class IxComponentBaseViewModel : RenderableViewModelBase
    {
        public IxComponentBaseViewModel()
        {
        }
        public IxComponent Component { get;  set; }
        public override object Model { get => this.Component; set { this.Component = value as IxComponent; } }
    }
}
```
After that, Blazor view `IxComponentBaseView.razor` can be created:

```C#
@namespace Plc
@inherits RenderableViewModelComponentBase<IxComponentBaseViewModel>

<h3>IxComponentBaseView</h3>

<p>IxBool baseView: @ViewModel.Component.ix_bool.Cyclic</p>
<p>IxInt baseView: @ViewModel.Component.ix_int.Cyclic</p>
<p>IxString baseView: @ViewModel.Component.ix_string.Cyclic</p>

@code
{
    protected override void OnInitialized()
    {
        UpdateValuesOnChange(ViewModel.Component);
    }
}

```
The view must inherits from `RenderableViewModelComponentBase<T>` base class, where `T` is type of your ViewModel. Thanks to this, ViewModel instance is created and initialized with PLC type instance acquired within `RenderableContentControl`. After that, ViewModel can be accessed within Blazor view.

If you want your UI update everytime, when PLC values change, you can add `UpdatesValuesOnChange(ViewModel.Component)` directly to code section in view, or you can create partial class same as in previous example.

When you call RenderableContentControl component like this:

```
<RenderableContentControl Presentation="Base"
                          Context="@Entry.Plc.MAIN.instanceOfIxComponent">

</RenderableContentControl>
```

You will get generated UI, which you specified in your custom view:

![alt text](assets/baseview.png "Renderignore and custom labels")
