# OnlinerBaseType&lt;T&gt; class

Base generic class for onliner types. Instance of [`OnlinerBaseType`](./OnlinerBaseType-1.md) is commonly referred to as 'tag' or 'PLC tag'.

```csharp
public abstract class OnlinerBaseType<T> : OnlinerBaseType, INotifyPropertyChanged, IOnline<T>, 
    IShadow<T>, ITwinPrimitive, IValueBoundaries<T>
```

## Public Members

| name | description |
| --- | --- |
| [AttributeMaximum](OnlinerBaseType-1/AttributeMaximum.md) { get; set; } | Gets or sets max. value allowed for this tag. If the value is not set the max. value will be the maximum allowed for this type. |
| [AttributeMinimum](OnlinerBaseType-1/AttributeMinimum.md) { get; set; } | Gets or sets min. value allowed for this tag. If the value is not set the min. value will be the maximum allowed for this type. |
| [AttributeName](OnlinerBaseType-1/AttributeName.md) { get; set; } | Gets or sets the attribute name. |
| [AttributeToolTip](OnlinerBaseType-1/AttributeToolTip.md) { get; set; } | Gets or sets tool tip message for this tag. |
| [AttributeUnits](OnlinerBaseType-1/AttributeUnits.md) { get; set; } | Gets or sets the attribute units. |
| virtual [Cyclic](OnlinerBaseType-1/Cyclic.md) { get; set; } | Gets the value that was read in the last cycle or sets the value to be written in the next cycle. |
| [CyclicReading](OnlinerBaseType-1/CyclicReading.md) { get; set; } | Gets or sets a value indicating whether cyclic reading of this tag is active. |
| [Edit](OnlinerBaseType-1/Edit.md) { get; set; } | Gets the cyclicly read value. Setter set the value that will be written in the next cycle. The value is validated prior to performing write operation. The value must fall between [`InstanceMinValue`](./OnlinerBaseType-1/InstanceMinValue.md) and [`InstanceMaxValue`](./OnlinerBaseType-1/InstanceMaxValue.md), any value outside permissible range will not be written and original value will remain unaltered. Editing of the value invokes EditValue which allows for value change logging via [`EditValueChange`](./OnlinerBaseType-1/EditValueChange.md) delegate. |
| override [EditValueChange](OnlinerBaseType-1/EditValueChange.md) { get; set; } | Gets or sets delegate that executes when [`Edit`](./OnlinerBaseType-1/Edit.md) property is changed. |
| [HasBeenWritten](OnlinerBaseType-1/HasBeenWritten.md) { get; set; } | Gets or sets a value indicating whether has been written in the last cycle. |
| [HumanReadable](OnlinerBaseType-1/HumanReadable.md) { get; protected set; } | Provides a string combined from [`AttributeName`](./OnlinerBaseType-1/AttributeName.md) of ancestors ([`GetParent`](./OnlinerBaseType-1/GetParent.md)) of this instance and the tail of this instance. |
| abstract [InstanceMaxValue](OnlinerBaseType-1/InstanceMaxValue.md) { get; } | Gets the maximum value allowed for this tag. The default max. value is maximum of this type if [`AttributeMaximum`](./OnlinerBaseType-1/AttributeMaximum.md) is not set. If the [`AttributeMaximum`](./OnlinerBaseType-1/AttributeMaximum.md) property is set then [`InstanceMaxValue`](./OnlinerBaseType-1/InstanceMaxValue.md) will return its value. |
| abstract [InstanceMinValue](OnlinerBaseType-1/InstanceMinValue.md) { get; } | Gets the minimal value allowed for this tag. The default min. value is minimum of this type if [`AttributeMinimum`](./OnlinerBaseType-1/AttributeMinimum.md) is not set. If the [`AttributeMinimum`](./OnlinerBaseType-1/AttributeMinimum.md) property is set then [`InstanceMinValue`](./OnlinerBaseType-1/InstanceMinValue.md) will return its value. |
| [IsToBeRead](OnlinerBaseType-1/IsToBeRead.md) { get; } | Gets a value indicating whether is to be read in the next cycle. |
| [IsToBeWritten](OnlinerBaseType-1/IsToBeWritten.md) { get; } | Gets a value indicating whether is to be written. |
| [LastValue](OnlinerBaseType-1/LastValue.md) { get; } | Gets the last value retrieved from cyclical or batched reading. Without requesting cyclical read operation on this onliner. |
| virtual [Raw](OnlinerBaseType-1/Raw.md) { get; } | Gets the raw value read from the controller. The value is not affected by [`Translator`](./OnlinerBaseType-1/Translator.md) or [`StringInterpolator`](../Ix.Connector/StringInterpolator.md). |
| [ReadFromPlcIsRequested](OnlinerBaseType-1/ReadFromPlcIsRequested.md) { get; protected set; } | Gets or sets a value indicating whether read from plc is requested. |
| [Shadow](OnlinerBaseType-1/Shadow.md) { get; set; } | Gets or sets shadow placeholder for the value of this instance. [`Shadow`](./OnlinerBaseType-1/Shadow.md) is off line placeholder that does not affect the PLC controller's value of this tag. Shadow values can be written to PLC controller via 'FlushShadowToOnline' method of a respective structure. Online values can be written to its Shadow representation via 'FlushOnlineToShadow' method of the respective structure. The value must fall between [`InstanceMinValue`](./OnlinerBaseType-1/InstanceMinValue.md) and [`InstanceMaxValue`](./OnlinerBaseType-1/InstanceMaxValue.md), any value outside permissible range will not be set and original value will remain unaltered. Change of the shadow value can be observed via [`ShadowValueChange`](./OnlinerBaseType-1/ShadowValueChange.md) delegate. |
| override [ShadowValueChange](OnlinerBaseType-1/ShadowValueChange.md) { get; set; } | Gets or sets delegate that executed when [`Shadow`](./OnlinerBaseType-1/Shadow.md) property is changed. |
| [Symbol](OnlinerBaseType-1/Symbol.md) { get; } | Gets the symbol of this on line variable. |
| virtual [Synchron](OnlinerBaseType-1/Synchron.md) { get; set; } | Gets or sets the online value synchronously. |
| [Translator](OnlinerBaseType-1/Translator.md) { get; } | Provides translator for the localization for this tag. |
| [Validator](OnlinerBaseType-1/Validator.md) { get; } | Gets the validation rule for this type. Validation occurs only when property [`Edit`](./OnlinerBaseType-1/Edit.md) is changed. |
| [VariableInfo](OnlinerBaseType-1/VariableInfo.md) { get; protected set; } | Gets information about this tag's online variable info. |
| [WriteToPlcIsRequested](OnlinerBaseType-1/WriteToPlcIsRequested.md) { get; set; } | Gets or sets a value indicating whether this variable should be written to the controller in the next cycle. |
| event [PropertyChanged](OnlinerBaseType-1/PropertyChanged.md) | Implementation of INotifyPropertyChanged interface. |
| event [ShadowValueChangeEvent](OnlinerBaseType-1/ShadowValueChangeEvent.md) | [`Shadow`](./OnlinerBaseType-1/Shadow.md) value changed event. |
| event [ValueChangeEvent](OnlinerBaseType-1/ValueChangeEvent.md) | Online [`Cyclic`](./OnlinerBaseType-1/Cyclic.md) property changed event. |
| [GetDeclaringAssembly](OnlinerBaseType-1/GetDeclaringAssembly.md)() | Gets the assembly that declares the instance of this tag. |
| virtual [GetLastAvailableValue](OnlinerBaseType-1/GetLastAvailableValue.md)() | Gets tha most recent value either [`LastValue`](./OnlinerBaseType-1/LastValue.md) or [`Cyclic`](./OnlinerBaseType-1/Cyclic.md) if that is more recent. |
| [GetParent](OnlinerBaseType-1/GetParent.md)() | Gets the parent object of this instance. Parent object is the object that created this instance. |
| [GetSymbolTail](OnlinerBaseType-1/GetSymbolTail.md)() | Gets tail of the this tag symbol. |
| [GetValueChangeEventSubscribers](OnlinerBaseType-1/GetValueChangeEventSubscribers.md)() | Gets delegates associated with ValueChangeEvent |
| [HasWriteAccess](OnlinerBaseType-1/HasWriteAccess.md)() | Get true when this tag has write access. |
| [Subscribe](OnlinerBaseType-1/Subscribe.md)(…) | Subscribes this tag for cyclical reading and invokes [`ValueChangedEventHandlerDelegate`](./ValueChangedEventHandlerDelegate.md) when the value changes. |
| [UnSubscribe](OnlinerBaseType-1/UnSubscribe.md)(…) | Un-subscribes this tag from handling the value change. The cyclical reading of this tag will not be cancelled. |

## Protected Members

| name | description |
| --- | --- |
| [OnlinerBaseType](OnlinerBaseType-1/OnlinerBaseType.md)() | Allows for parameterless constructor in derived class. |
| [OnlinerBaseType](OnlinerBaseType-1/OnlinerBaseType.md)(…) | Creates an instance of [`OnlinerBaseType`](./OnlinerBaseType-1.md). |
| [cyclicToWrite](OnlinerBaseType-1/cyclicToWrite.md) { get; set; } | Gets the value that will be written in the next cycle. |
| [Parent](OnlinerBaseType-1/Parent.md) { get; } | Gets the parent object of this instance. Parent object is the object that created this instance. |
| [SymbolTail](OnlinerBaseType-1/SymbolTail.md) { get; set; } | Gets or sets the tail of the symbol of this tag. |
| [validator](OnlinerBaseType-1/validator.md) | Holder field for validation rule. |
| [NotifyPropertyChanged](OnlinerBaseType-1/NotifyPropertyChanged.md)(…) | Implementation of INotifyPropertyChanged. |
| [OnValueChangeEvent](OnlinerBaseType-1/OnValueChangeEvent.md)(…) | Notifies the change of [`Cyclic`](./OnlinerBaseType-1/Cyclic.md)value. |
| virtual [ReadFromItem](OnlinerBaseType-1/ReadFromItem.md)() | NVI should be implemented in derived class. When implemented in derived class provide reading of the [`Cyclic`](./OnlinerBaseType-1/Cyclic.md)value for this tag from a stream. |
| [UpdateRead](OnlinerBaseType-1/UpdateRead.md)(…) | Updates cyclically read value and performs notifications. |
| virtual [WriteItem](OnlinerBaseType-1/WriteItem.md)() | NVI should be implemented in derived class. When implemented in derived class provide writing of the [`Cyclic`](./OnlinerBaseType-1/Cyclic.md)value for this tag from a stream. |
| static [InitializeDefaults](OnlinerBaseType-1/InitializeDefaults.md)() | Initializes default value of this generic [`OnlinerBaseType`](./OnlinerBaseType-1.md). |

## See Also

* class [OnlinerBaseType](./OnlinerBaseType.md)
* interface [IOnline&lt;T&gt;](../Ix.Connector.ValueTypes.Online/IOnline-1.md)
* interface [IShadow&lt;T&gt;](../Ix.Connector.ValueTypes.Shadows/IShadow-1.md)
* interface [ITwinPrimitive](../Ix.Connector/ITwinPrimitive.md)
* interface [IValueBoundaries&lt;T&gt;](../Ix.Connector.ValueValidation/IValueBoundaries-1.md)
* namespace [Ix.Connector.ValueTypes](../Ix.Connector.md)

<!-- DO NOT EDIT: generated by xmldocmd for Ix.Connector.dll -->
