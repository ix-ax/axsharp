# OnlinerBase&lt;T&gt; class

Base generic class for onliner types. Instance of [`OnlinerBase`](./OnlinerBase-1.md) is commonly referred to as 'tag' or 'PLC tag'.

```csharp
public abstract class OnlinerBase<T> : OnlinerBase, INotifyPropertyChanged, IOnline<T>, IShadow<T>, 
    IValueBoundaries<T>
```

## Public Members

| name | description |
| --- | --- |
| [AttributeMaximum](OnlinerBase-1/AttributeMaximum.md) { get; set; } | Gets or sets max. value allowed for this tag. If the value is not set the max. value will be the maximum allowed for this type. |
| [AttributeMinimum](OnlinerBase-1/AttributeMinimum.md) { get; set; } | Gets or sets min. value allowed for this tag. If the value is not set the min. value will be the maximum allowed for this type. |
| [AttributeToolTip](OnlinerBase-1/AttributeToolTip.md) { get; set; } | Gets or sets tool tip message for this tag. |
| [AttributeUnits](OnlinerBase-1/AttributeUnits.md) { get; set; } | Gets or sets the attribute units. |
| virtual [Cyclic](OnlinerBase-1/Cyclic.md) { get; set; } | Gets the value that was read in the last cycle or sets the value to be written in the next cycle. |
| [Edit](OnlinerBase-1/Edit.md) { get; set; } | Gets the cyclically read value. Setter set the value that will be written in the next cycle. The value is validated prior to performing write operation. The value must fall between [`InstanceMinValue`](./OnlinerBase-1/InstanceMinValue.md) and [`InstanceMaxValue`](./OnlinerBase-1/InstanceMaxValue.md), any value outside permissible range will not be written and original value will remain unaltered. Editing of the value invokes EditValue which allows for value change logging via [`EditValueChange`](./OnlinerBase-1/EditValueChange.md) delegate. |
| override [EditValueChange](OnlinerBase-1/EditValueChange.md) { get; set; } | Gets or sets delegate that executes when [`Edit`](./OnlinerBase-1/Edit.md) property is changed. |
| abstract [InstanceMaxValue](OnlinerBase-1/InstanceMaxValue.md) { get; } | Gets the maximum value allowed for this tag. The default max. value is maximum of this type if [`AttributeMaximum`](./OnlinerBase-1/AttributeMaximum.md) is not set. If the [`AttributeMaximum`](./OnlinerBase-1/AttributeMaximum.md) property is set then [`InstanceMaxValue`](./OnlinerBase-1/InstanceMaxValue.md) will return its value. |
| abstract [InstanceMinValue](OnlinerBase-1/InstanceMinValue.md) { get; } | Gets the minimal value allowed for this tag. The default min. value is minimum of this type if [`AttributeMinimum`](./OnlinerBase-1/AttributeMinimum.md) is not set. If the [`AttributeMinimum`](./OnlinerBase-1/AttributeMinimum.md) property is set then [`InstanceMinValue`](./OnlinerBase-1/InstanceMinValue.md) will return its value. |
| virtual [LastValue](OnlinerBase-1/LastValue.md) { get; } |  |
| virtual [Raw](OnlinerBase-1/Raw.md) { get; } | Gets the raw value read from the controller. The value is not affected by [`TranslatorBase`](../Ix.Localizations.Abstractions/TranslatorBase.md) or [`StringInterpolator`](../Ix.Connector/StringInterpolator.md). |
| [ReadFromPlcIsRequested](OnlinerBase-1/ReadFromPlcIsRequested.md) { get; protected set; } | Gets or sets a value indicating whether read from plc is requested. |
| [Shadow](OnlinerBase-1/Shadow.md) { get; set; } | Gets or sets shadow placeholder for the value of this instance. [`Shadow`](./OnlinerBase-1/Shadow.md) is off line placeholder that does not affect the PLC controller's value of this tag. Shadow values can be written to PLC controller via 'FlushShadowToOnline' method of a respective structure. Online values can be written to its Shadow representation via 'FlushOnlineToShadow' method of the respective structure. The value must fall between [`InstanceMinValue`](./OnlinerBase-1/InstanceMinValue.md) and [`InstanceMaxValue`](./OnlinerBase-1/InstanceMaxValue.md), any value outside permissible range will not be set and original value will remain unaltered. Change of the shadow value can be observed via [`ShadowValueChange`](./OnlinerBase-1/ShadowValueChange.md) delegate. |
| override [ShadowValueChange](OnlinerBase-1/ShadowValueChange.md) { get; set; } | Gets or sets delegate that executed when [`Shadow`](./OnlinerBase-1/Shadow.md) property is changed. |
| [Validator](OnlinerBase-1/Validator.md) { get; } | Gets the validation rule for this type. Validation occurs only when property [`Edit`](./OnlinerBase-1/Edit.md) is changed. |
| [VariableInfo](OnlinerBase-1/VariableInfo.md) { get; protected set; } | Gets information about this tag's online variable info. |
| event [ShadowValueChangeEvent](OnlinerBase-1/ShadowValueChangeEvent.md) | [`Shadow`](./OnlinerBase-1/Shadow.md) value changed event. |
| virtual [GetAsync](OnlinerBase-1/GetAsync.md)() | Gets the value of this variable from the controller asynchronously. |
| [GetDeclaringAssembly](OnlinerBase-1/GetDeclaringAssembly.md)() | Gets the assembly that declares the instance of this tag. |
| virtual [GetLastAvailableValue](OnlinerBase-1/GetLastAvailableValue.md)() | Gets tha most recent value either [`LastValue`](./OnlinerBase-1/LastValue.md) or [`Cyclic`](./OnlinerBase-1/Cyclic.md) if that is more recent. |
| [HasWriteAccess](OnlinerBase-1/HasWriteAccess.md)() | Get true when this tag has write access. |
| virtual [SetAsync](OnlinerBase-1/SetAsync.md)(…) | Sets the value of this variable to the controller asynchronously. |

## Protected Members

| name | description |
| --- | --- |
| [OnlinerBase](OnlinerBase-1/OnlinerBase.md)() | Allows for parameter-less constructor in derived class. |
| [OnlinerBase](OnlinerBase-1/OnlinerBase.md)(…) | Creates an instance of [`OnlinerBase`](./OnlinerBase-1.md). |
| [CyclicToWrite](OnlinerBase-1/CyclicToWrite.md) { get; set; } | Gets the value that will be written in the next cycle. |
| [validator](OnlinerBase-1/validator.md) | Holder field for validation rule. |
| virtual [ReadFromItem](OnlinerBase-1/ReadFromItem.md)() | NVI should be implemented in derived class. When implemented in derived class provide reading of the [`Cyclic`](./OnlinerBase-1/Cyclic.md)value for this tag from a stream. |
| [UpdateRead](OnlinerBase-1/UpdateRead.md)(…) | Updates cyclically read value and performs notifications. |
| virtual [WriteItem](OnlinerBase-1/WriteItem.md)() | NVI should be implemented in derived class. When implemented in derived class provide writing of the [`Cyclic`](./OnlinerBase-1/Cyclic.md)value for this tag from a stream. |
| static [InitializeDefaults](OnlinerBase-1/InitializeDefaults.md)() | Initializes default value of this generic [`OnlinerBase`](./OnlinerBase-1.md). |

## See Also

* class [OnlinerBase](./OnlinerBase.md)
* interface [IOnline&lt;T&gt;](../Ix.Connector.ValueTypes.Online/IOnline-1.md)
* interface [IShadow&lt;T&gt;](../Ix.Connector.ValueTypes.Shadows/IShadow-1.md)
* interface [IValueBoundaries&lt;T&gt;](../Ix.Connector.ValueValidation/IValueBoundaries-1.md)
* namespace [Ix.Connector.ValueTypes](../Ix.Connector.md)

<!-- DO NOT EDIT: generated by xmldocmd for Ix.Connector.dll -->
