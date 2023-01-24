# Ix.Connector assembly

## Ix.Connector namespace

| public type | description |
| --- | --- |
| abstract class [Connector](./Ix.Connector/Connector.md) | Abstract base class provides implementation contract for the PLC connector and basic common underlying logic. |
| class [ConnectorAdapter](./Ix.Connector/ConnectorAdapter.md) | Provides basic abstractions for twin connectors. |
| class [ConnectorAdapterBuilder](./Ix.Connector/ConnectorAdapterBuilder.md) | Provides access to extension method for creating connector adapters. |
| abstract class [ConnectorFactory](./Ix.Connector/ConnectorFactory.md) | Provides abstraction for creation of [`Connector`](./Ix.Connector/Connector.md) and value types (tags) for twin connector. |
| class [DummyConnector](./Ix.Connector/DummyConnector.md) | Provides a connector without real target system connections. |
| static class [DummyConnectorExtensions](./Ix.Connector/DummyConnectorExtensions.md) | Provides extensions for [`DummyConnector`](./Ix.Connector/DummyConnector.md) and [`DummyConnectorFactory`](./Ix.Connector/DummyConnectorFactory.md). |
| class [DummyConnectorFactory](./Ix.Connector/DummyConnectorFactory.md) | Dummy connector factory class. Serves as offline connector with no connection to a controller. |
| class [EnumeratorDiscriminatorAttribute](./Ix.Connector/EnumeratorDiscriminatorAttribute.md) | Attribute provides information about the member being treated as enum. |
| class [IgnoreReflectionAttribute](./Ix.Connector/IgnoreReflectionAttribute.md) | Indicates that the member should not be reflected within the ix framework. |
| interface [IPlain](./Ix.Connector/IPlain.md) | Basic abstraction for 'Plain' types. Implementation used in building process. Not to be declared by framework consumers. |
| interface [ITwinController](./Ix.Connector/ITwinController.md) | Represents twin object for PLC controller. |
| interface [ITwinElement](./Ix.Connector/ITwinElement.md) | Basic contract for any type that is product of building process. |
| interface [ITwinObject](./Ix.Connector/ITwinObject.md) | Basic contract for any complex object that is product of build process. |
| interface [ITwinOnlineObject](./Ix.Connector/ITwinOnlineObject.md) | Basic abstraction for 'IOnline' types. Implementation used in building process. Not to be declared by framework consumers. |
| interface [ITwinPrimitive](./Ix.Connector/ITwinPrimitive.md) | Provides basic contract for plc tag. |
| interface [ITwinPrimitiveInfo](./Ix.Connector/ITwinPrimitiveInfo.md) | Provides interface for information about value types symbols. |
| interface [ITwinShadowObject](./Ix.Connector/ITwinShadowObject.md) | Basic abstraction for 'IShadow' types. Implementation used in building process. Not to be declared by framework consumers. |
| static class [StringInterpolator](./Ix.Connector/StringInterpolator.md) | Provides extension methods for PLC's string interpolation. |
| static class [TwinObjectExtensions](./Ix.Connector/TwinObjectExtensions.md) | Extensions methods for [`ITwinObject`](./Ix.Connector/ITwinObject.md). |
| static class [TwinPrimitiveExtensions](./Ix.Connector/TwinPrimitiveExtensions.md) | Provides series of extension method for accessing primitive items. |

## Ix.Connector.Attributes namespace

| public type | description |
| --- | --- |
| enum [CompilerOmissionGroups](./Ix.Connector.Attributes/CompilerOmissionGroups.md) | Builder omission groups enumerator. Enumerates builder output groups suitable for the omission. |
| class [CompilerOmitsAttribute](./Ix.Connector.Attributes/CompilerOmitsAttribute.md) | Prevents ixc builder to create a member for specific group of output type (Shadow, Plain, Onliner). This attribute must be declared in the PLC code to be effective during build process. This example demonstrates how to prevent the ixc builder to compile specific property into specific output group. |
| class [ReadOnlyAttribute](./Ix.Connector.Attributes/ReadOnlyAttribute.md) | Attribute allows to prevent writing to the members of twin connector. |

## Ix.Connector.BuilderHelpers namespace

| public type | description |
| --- | --- |
| static class [Arrays](./Ix.Connector.BuilderHelpers/Arrays.md) | Helper methods for creating and manipulating arrays in Twin objects. |

## Ix.Connector.Identity namespace

| public type | description |
| --- | --- |
| interface [ITwinIdentity](./Ix.Connector.Identity/ITwinIdentity.md) | Defines identity object contract. |
| class [MemberByIdentityAttribute](./Ix.Connector.Identity/MemberByIdentityAttribute.md) | Provides information that the member is to be returned by its identity. |
| class [NullTwinIdentity](./Ix.Connector.Identity/NullTwinIdentity.md) | Represents null or non-existing [`ITwinIdentity`](./Ix.Connector.Identity/ITwinIdentity.md) object. |
| class [TwinIdentityProvider](./Ix.Connector.Identity/TwinIdentityProvider.md) | Provides access to the objects by their identities. |

## Ix.Connector.ValueTypes namespace

| public type | description |
| --- | --- |
| abstract class [OnlinerBase&lt;T&gt;](./Ix.Connector.ValueTypes/OnlinerBase-1.md) | Base generic class for onliner types. Instance of [`OnlinerBase`](./Ix.Connector.ValueTypes/OnlinerBase-1.md) is commonly referred to as 'tag' or 'PLC tag'. |
| abstract class [OnlinerBase](./Ix.Connector.ValueTypes/OnlinerBase.md) | Base non-generic class for onliner types |
| class [OnlinerBool](./Ix.Connector.ValueTypes/OnlinerBool.md) | Class providing access to the BOOL type online variable. |
| class [OnlinerByte](./Ix.Connector.ValueTypes/OnlinerByte.md) | Class providing access to the BYTE type online variable. |
| class [OnlinerDate](./Ix.Connector.ValueTypes/OnlinerDate.md) | Class providing access to the DATE type online variable. |
| class [OnlinerDateTime](./Ix.Connector.ValueTypes/OnlinerDateTime.md) | Class providing access to the DATE_AND_TIME (DT) type online variable. |
| class [OnlinerDInt](./Ix.Connector.ValueTypes/OnlinerDInt.md) | Class providing access to the DINT type online variable. |
| class [OnlinerDWord](./Ix.Connector.ValueTypes/OnlinerDWord.md) | Class providing access to the DWORD type online variable. |
| class [OnlinerInt](./Ix.Connector.ValueTypes/OnlinerInt.md) | Class providing access to the INT type online variable. |
| class [OnlinerLInt](./Ix.Connector.ValueTypes/OnlinerLInt.md) | Class providing access to the LINT type online variable. |
| class [OnlinerLReal](./Ix.Connector.ValueTypes/OnlinerLReal.md) | Class providing access to the LREAL type online variable. |
| class [OnlinerLTime](./Ix.Connector.ValueTypes/OnlinerLTime.md) | Class providing access to the LTIME type online variable. |
| class [OnlinerLWord](./Ix.Connector.ValueTypes/OnlinerLWord.md) | Class providing access to the LWORD type online variable. |
| class [OnlinerReal](./Ix.Connector.ValueTypes/OnlinerReal.md) | Class providing access to the REAL type online variable. |
| class [OnlinerSInt](./Ix.Connector.ValueTypes/OnlinerSInt.md) | Class providing access to the SINT type online variable. |
| class [OnlinerString](./Ix.Connector.ValueTypes/OnlinerString.md) | Class providing access to the STRING type online variable. |
| class [OnlinerTime](./Ix.Connector.ValueTypes/OnlinerTime.md) | Class providing access to the TIME type online variable. |
| class [OnlinerTimeOfDay](./Ix.Connector.ValueTypes/OnlinerTimeOfDay.md) | Class providing access to the TIME_OF_DAY type online variable. |
| class [OnlinerUDInt](./Ix.Connector.ValueTypes/OnlinerUDInt.md) | Class providing access to the UDINT type online variable. |
| class [OnlinerUInt](./Ix.Connector.ValueTypes/OnlinerUInt.md) | Class providing access to the UINT type online variable. |
| class [OnlinerULInt](./Ix.Connector.ValueTypes/OnlinerULInt.md) | Class providing access to the ULINT type online variable. |
| class [OnlinerUSInt](./Ix.Connector.ValueTypes/OnlinerUSInt.md) | Class providing access to the USINT type online variable. |
| class [OnlinerWord](./Ix.Connector.ValueTypes/OnlinerWord.md) | Class providing access to the WORD type online variable. |
| class [OnlinerWString](./Ix.Connector.ValueTypes/OnlinerWString.md) | Class providing access to the WSTRING type online variable. |
| enum [ReadWriteAccess](./Ix.Connector.ValueTypes/ReadWriteAccess.md) | Enumerates permitted access to a tag member. |
| class [ValueChangedEventArgs](./Ix.Connector.ValueTypes/ValueChangedEventArgs.md) | Provides class for encapsulation of arguments for [`ValueChangedEventHandlerDelegate`](./Ix.Connector.ValueTypes/ValueChangedEventHandlerDelegate.md). |
| delegate [ValueChangedEventHandlerDelegate](./Ix.Connector.ValueTypes/ValueChangedEventHandlerDelegate.md) | Provides delegate for notification of tags value change. |

## Ix.Connector.ValueTypes.Online namespace

| public type | description |
| --- | --- |
| interface [IOnline&lt;T&gt;](./Ix.Connector.ValueTypes.Online/IOnline-1.md) | Implementation contract for base types tags to access online values from the PLC. |
| interface [IOnlineBool](./Ix.Connector.ValueTypes.Online/IOnlineBool.md) | Defines contract to access online value of Boolean; BOOL type of the PLC. |
| interface [IOnlineByte](./Ix.Connector.ValueTypes.Online/IOnlineByte.md) | Defines contract to access online value of Byte; BUTE type of the PLC. |
| interface [IOnlineDate](./Ix.Connector.ValueTypes.Online/IOnlineDate.md) | Defines contract to access online value of DateTime; DATE type of the PLC. |
| interface [IOnlineDateTime](./Ix.Connector.ValueTypes.Online/IOnlineDateTime.md) | Defines contract to access online value of DateTime; DATE_AND_TIME (DT) type of the PLC. |
| interface [IOnlineDInt](./Ix.Connector.ValueTypes.Online/IOnlineDInt.md) | Defines contract to access online value of Int32; DINT type of the PLC. |
| interface [IOnlineDWord](./Ix.Connector.ValueTypes.Online/IOnlineDWord.md) | Defines contract to access online value of UInt32; DWORD type of the PLC. |
| interface [IOnlineInt](./Ix.Connector.ValueTypes.Online/IOnlineInt.md) | Defines contract to access online value of Int16; INT type of the PLC. |
| interface [IOnlineLInt](./Ix.Connector.ValueTypes.Online/IOnlineLInt.md) | Defines contract to access online value of Int64; LINT type of the PLC. |
| interface [IOnlineLReal](./Ix.Connector.ValueTypes.Online/IOnlineLReal.md) | Defines contract to access online value of Double; LREAL type of the PLC. |
| interface [IOnlineLTime](./Ix.Connector.ValueTypes.Online/IOnlineLTime.md) | Defines contract to access online value of TimeSpan; LTIME type of the PLC. |
| interface [IOnlineLWord](./Ix.Connector.ValueTypes.Online/IOnlineLWord.md) | Defines contract to access online value of UInt64; LWORD type of the PLC. |
| interface [IOnlineReal](./Ix.Connector.ValueTypes.Online/IOnlineReal.md) | Defines contract to access online value of Single; REAL type of the PLC. |
| interface [IOnlineSInt](./Ix.Connector.ValueTypes.Online/IOnlineSInt.md) | Defines contract to access online value of SByte; SINT type of the PLC. |
| interface [IOnlineString](./Ix.Connector.ValueTypes.Online/IOnlineString.md) | Defines contract to access online value of String; STRING type of the PLC. |
| interface [IOnlineTime](./Ix.Connector.ValueTypes.Online/IOnlineTime.md) | Defines contract to access online value of TimeSpan; TIME type of the PLC. |
| interface [IOnlineTimeOfDay](./Ix.Connector.ValueTypes.Online/IOnlineTimeOfDay.md) | Defines contract to access online value of TimeSpan; TIME_OF_DAY (TOD) type of the PLC. |
| interface [IOnlineUDInt](./Ix.Connector.ValueTypes.Online/IOnlineUDInt.md) | Defines contract to access online value of UInt32; UDINT type of the PLC. |
| interface [IOnlineUInt](./Ix.Connector.ValueTypes.Online/IOnlineUInt.md) | Defines contract to access online value of UInt16; UINT type of the PLC. |
| interface [IOnlineULInt](./Ix.Connector.ValueTypes.Online/IOnlineULInt.md) | Defines contract to access online value of UInt64; ULINT type of the PLC. |
| interface [IOnlineUSInt](./Ix.Connector.ValueTypes.Online/IOnlineUSInt.md) | Defines contract to access online value of Byte; USINT type of the PLC. |
| interface [IOnlineWord](./Ix.Connector.ValueTypes.Online/IOnlineWord.md) | Defines contract to access online value of UInt16; WORD type of the PLC. |
| interface [IOnlineWString](./Ix.Connector.ValueTypes.Online/IOnlineWString.md) | Defines contract to access online value of String; WSTRING type of the PLC. |

## Ix.Connector.ValueTypes.Shadows namespace

| public type | description |
| --- | --- |
| interface [IShadow&lt;T&gt;](./Ix.Connector.ValueTypes.Shadows/IShadow-1.md) | Implementation contract for base types tags to access shadow values. |
| interface [IShadowBool](./Ix.Connector.ValueTypes.Shadows/IShadowBool.md) | Defines contract to access shadow value of Boolean; BOOL type of the PLC. |
| interface [IShadowByte](./Ix.Connector.ValueTypes.Shadows/IShadowByte.md) | Defines contract to access shadow value of Byte; BYTE type of the PLC. |
| interface [IShadowDate](./Ix.Connector.ValueTypes.Shadows/IShadowDate.md) | Defines contract to access shadow value of DateTime; DATE type of the PLC. |
| interface [IShadowDateTime](./Ix.Connector.ValueTypes.Shadows/IShadowDateTime.md) | Defines contract to access shadow value of DateTime; DATE_AND_TIME (DT) type of the PLC. |
| interface [IShadowDInt](./Ix.Connector.ValueTypes.Shadows/IShadowDInt.md) | Defines contract to access shadow value of Int32; DINT type of the PLC. |
| interface [IShadowDWord](./Ix.Connector.ValueTypes.Shadows/IShadowDWord.md) | Defines contract to access shadow value of UInt32; DWORD type of the PLC. |
| interface [IShadowInt](./Ix.Connector.ValueTypes.Shadows/IShadowInt.md) | Defines contract to access shadow value of Int16; INT type of the PLC. |
| interface [IShadowLInt](./Ix.Connector.ValueTypes.Shadows/IShadowLInt.md) | Defines contract to access shadow value of Int64; LINT type of the PLC. |
| interface [IShadowLReal](./Ix.Connector.ValueTypes.Shadows/IShadowLReal.md) | Defines contract to access shadow value of Double; LREAL type of the PLC. |
| interface [IShadowLTime](./Ix.Connector.ValueTypes.Shadows/IShadowLTime.md) | Defines contract to access shadow value of TimeSpan; LTIME type of the PLC. |
| interface [IShadowLWord](./Ix.Connector.ValueTypes.Shadows/IShadowLWord.md) | Defines contract to access shadow value of UInt64; LWORD type of the PLC. |
| interface [IShadowReal](./Ix.Connector.ValueTypes.Shadows/IShadowReal.md) | Defines contract to access shadow value of Single; REAL type of the PLC. |
| interface [IShadowSInt](./Ix.Connector.ValueTypes.Shadows/IShadowSInt.md) | Defines contract to access shadow value of SByte; SINT type of the PLC. |
| interface [IShadowString](./Ix.Connector.ValueTypes.Shadows/IShadowString.md) | Defines contract to access shadow value of String; STRING type of the PLC. |
| interface [IShadowTime](./Ix.Connector.ValueTypes.Shadows/IShadowTime.md) | Defines contract to access shadow value of TimeSpan; TIME type of the PLC. |
| interface [IShadowTimeOfDay](./Ix.Connector.ValueTypes.Shadows/IShadowTimeOfDay.md) | Defines contract to access shadow value of TimeSpan; TIME_OF_DAY (TOD) type of the PLC. |
| interface [IShadowUDInt](./Ix.Connector.ValueTypes.Shadows/IShadowUDInt.md) | Defines contract to access shadow value of UInt32; UDINT type of the PLC. |
| interface [IShadowUInt](./Ix.Connector.ValueTypes.Shadows/IShadowUInt.md) | Defines contract to access shadow value of UInt16; UINT type of the PLC. |
| interface [IShadowULInt](./Ix.Connector.ValueTypes.Shadows/IShadowULInt.md) | Defines contract to access shadow value of UInt64; ULINT type of the PLC. |
| interface [IShadowUSInt](./Ix.Connector.ValueTypes.Shadows/IShadowUSInt.md) | Defines contract to access shadow value of Byte; USINT type of the PLC. |
| interface [IShadowWord](./Ix.Connector.ValueTypes.Shadows/IShadowWord.md) | Defines contract to access shadow value of UInt16; WORD type of the PLC. |
| interface [IShadowWString](./Ix.Connector.ValueTypes.Shadows/IShadowWString.md) | Defines contract to access shadow value of String; WSTRING type of the PLC. |

## Ix.Connector.ValueValidation namespace

| public type | description |
| --- | --- |
| class [BoolValueValidationRule](./Ix.Connector.ValueValidation/BoolValueValidationRule.md) | Provides validation rule for [`OnlinerBool`](./Ix.Connector.ValueTypes/OnlinerBool.md) type. |
| class [ByteValueValidationRule](./Ix.Connector.ValueValidation/ByteValueValidationRule.md) | Provides validation rule for [`OnlinerByte`](./Ix.Connector.ValueTypes/OnlinerByte.md) type. |
| class [DateTimeValueValidationRule](./Ix.Connector.ValueValidation/DateTimeValueValidationRule.md) | Provides validation rule for [`OnlinerDateTime`](./Ix.Connector.ValueTypes/OnlinerDateTime.md) type. |
| class [DateValueValidationRule](./Ix.Connector.ValueValidation/DateValueValidationRule.md) | Provides validation rule for [`OnlinerDate`](./Ix.Connector.ValueTypes/OnlinerDate.md) type. |
| class [DintValueValidationRule](./Ix.Connector.ValueValidation/DintValueValidationRule.md) | Provides validation rule for [`OnlinerDInt`](./Ix.Connector.ValueTypes/OnlinerDInt.md) type. |
| class [DWordValueValidationRule](./Ix.Connector.ValueValidation/DWordValueValidationRule.md) | Provides validation rule for [`OnlinerDWord`](./Ix.Connector.ValueTypes/OnlinerDWord.md) type. |
| class [IntValueValidationRule](./Ix.Connector.ValueValidation/IntValueValidationRule.md) | Provides validation rule for [`OnlinerInt`](./Ix.Connector.ValueTypes/OnlinerInt.md) type. |
| interface [IValueBoundaries&lt;T&gt;](./Ix.Connector.ValueValidation/IValueBoundaries-1.md) | Contract for validation of a numerical value. |
| class [LIntValueValidationRule](./Ix.Connector.ValueValidation/LIntValueValidationRule.md) | Provides validation rule for [`OnlinerLInt`](./Ix.Connector.ValueTypes/OnlinerLInt.md) type. |
| class [LRealValueValidationRule](./Ix.Connector.ValueValidation/LRealValueValidationRule.md) | Provides validation rule for [`OnlinerLReal`](./Ix.Connector.ValueTypes/OnlinerLReal.md) type. |
| class [LTimeValueValidationRule](./Ix.Connector.ValueValidation/LTimeValueValidationRule.md) | Provides validation rule for [`OnlinerLTime`](./Ix.Connector.ValueTypes/OnlinerLTime.md) type. |
| class [LWordValueValidationRule](./Ix.Connector.ValueValidation/LWordValueValidationRule.md) | Provides validation rule for [`OnlinerLWord`](./Ix.Connector.ValueTypes/OnlinerLWord.md) type. |
| abstract class [OnlinerValidationRule&lt;T&gt;](./Ix.Connector.ValueValidation/OnlinerValidationRule-1.md) | Generic class provides base for tags validation rules. |
| class [RealValueValidationRule](./Ix.Connector.ValueValidation/RealValueValidationRule.md) | Provides validation rule for [`OnlinerReal`](./Ix.Connector.ValueTypes/OnlinerReal.md) type. |
| class [SIntValueValidationRule](./Ix.Connector.ValueValidation/SIntValueValidationRule.md) | Provides validation rule for [`OnlinerSInt`](./Ix.Connector.ValueTypes/OnlinerSInt.md) type. |
| class [StringValueValidationRule](./Ix.Connector.ValueValidation/StringValueValidationRule.md) | Provides validation rule for [`OnlinerString`](./Ix.Connector.ValueTypes/OnlinerString.md) type. |
| class [TimeOfDayValueValidationRule](./Ix.Connector.ValueValidation/TimeOfDayValueValidationRule.md) | Provides validation rule for [`OnlinerTimeOfDay`](./Ix.Connector.ValueTypes/OnlinerTimeOfDay.md) type. |
| class [TimeValueValidationRule](./Ix.Connector.ValueValidation/TimeValueValidationRule.md) | Provides validation rule for [`OnlinerTime`](./Ix.Connector.ValueTypes/OnlinerTime.md) type. |
| class [UDIntValueValidationRule](./Ix.Connector.ValueValidation/UDIntValueValidationRule.md) | Provides validation rule for [`OnlinerUDInt`](./Ix.Connector.ValueTypes/OnlinerUDInt.md) type. |
| class [UIntValueValidationRule](./Ix.Connector.ValueValidation/UIntValueValidationRule.md) | Provides validation rule for [`OnlinerUInt`](./Ix.Connector.ValueTypes/OnlinerUInt.md) type. |
| class [ULIntValueValidationRule](./Ix.Connector.ValueValidation/ULIntValueValidationRule.md) | Provides validation rule for [`OnlinerULInt`](./Ix.Connector.ValueTypes/OnlinerULInt.md) type. |
| class [USintValueValidationRule](./Ix.Connector.ValueValidation/USintValueValidationRule.md) | Provides validation rule for [`OnlinerUSInt`](./Ix.Connector.ValueTypes/OnlinerUSInt.md) type. |
| class [ValidationResult](./Ix.Connector.ValueValidation/ValidationResult.md) | Provides return value of a validation process. |
| class [WordValueValidationRule](./Ix.Connector.ValueValidation/WordValueValidationRule.md) | Provides validation rule for [`OnlinerWord`](./Ix.Connector.ValueTypes/OnlinerWord.md) type. |
| class [WStringValueValidationRule](./Ix.Connector.ValueValidation/WStringValueValidationRule.md) | Provides validation rule for [`OnlinerWString`](./Ix.Connector.ValueTypes/OnlinerWString.md) type. |

## Ix.Localizations namespace

| public type | description |
| --- | --- |
| static class [IdentifierValidator](./Ix.Localizations/IdentifierValidator.md) | Helper methods for creating localizations. |
| class [LocalizableItem](./Ix.Localizations/LocalizableItem.md) | Provides data object for localizable items. |
| class [Localizables](./Ix.Localizations/Localizables.md) | Provides data object for localizables. |
| static class [LocalizationHelper](./Ix.Localizations/LocalizationHelper.md) | Provides series of helper methods for working with localizations. |

## Ix.Localizations.Abstractions namespace

| public type | description |
| --- | --- |
| abstract class [TranslatorBase](./Ix.Localizations.Abstractions/TranslatorBase.md) | Abstract class defines string translator. |

## Ix.Localizations.Resx namespace

| public type | description |
| --- | --- |
| class [Translator](./Ix.Localizations.Resx/Translator.md) | Provides translation mechanism for Resx files. |

<!-- DO NOT EDIT: generated by xmldocmd for Ix.Connector.dll -->
