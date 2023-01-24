# ConnectorFactory class

Provides abstraction for creation of [`Connector`](./Connector.md) and value types (tags) for twin connector.

```csharp
public abstract class ConnectorFactory
```

## Public Members

| name | description |
| --- | --- |
| abstract [CreateBOOL](ConnectorFactory/CreateBOOL.md)(…) | Creates Boolean tag of a value type BOOL; |
| abstract [CreateBYTE](ConnectorFactory/CreateBYTE.md)(…) | Creates Byte tag of a value type BYTE; |
| abstract [CreateConnector](ConnectorFactory/CreateConnector.md)(…) | Creates new instance of connector. |
| abstract [CreateDATE](ConnectorFactory/CreateDATE.md)(…) | Creates DateTime tag of a value type DATE; |
| abstract [CreateDATE_AND_TIME](ConnectorFactory/CreateDATE_AND_TIME.md)(…) | Creates DateTime tag of a value type DT, DATE_TIME; |
| abstract [CreateDINT](ConnectorFactory/CreateDINT.md)(…) | Creates Int32 tag of a value type DINT; |
| abstract [CreateDWORD](ConnectorFactory/CreateDWORD.md)(…) | Creates UInt32 tag of a value type DWORD; |
| abstract [CreateINT](ConnectorFactory/CreateINT.md)(…) | Creates Int16 tag of a value type INT; |
| abstract [CreateLINT](ConnectorFactory/CreateLINT.md)(…) | Creates Int64 tag of a value type LINT; |
| abstract [CreateLREAL](ConnectorFactory/CreateLREAL.md)(…) | Creates Double tag of a value type LREAL; |
| abstract [CreateLTIME](ConnectorFactory/CreateLTIME.md)(…) | Creates TimeSpan tag of a value type LTIME; |
| abstract [CreateLWORD](ConnectorFactory/CreateLWORD.md)(…) | Creates UInt64 tag of a value type DWORD; |
| abstract [CreateREAL](ConnectorFactory/CreateREAL.md)(…) | Creates Single tag of a value type REAL; |
| abstract [CreateSINT](ConnectorFactory/CreateSINT.md)(…) | Creates SByte tag of a value type SINT; |
| abstract [CreateSTRING](ConnectorFactory/CreateSTRING.md)(…) | Creates String tag of a value type STRING; |
| abstract [CreateTIME](ConnectorFactory/CreateTIME.md)(…) | Creates TimeSpan tag of a value type TIME; |
| abstract [CreateTIME_OF_DAY](ConnectorFactory/CreateTIME_OF_DAY.md)(…) | Creates TimeSpan tag of a value type TOD (Time of day); |
| abstract [CreateUDINT](ConnectorFactory/CreateUDINT.md)(…) | Creates UInt32 tag of a value type UDINT; |
| abstract [CreateUINT](ConnectorFactory/CreateUINT.md)(…) | Creates UInt16 tag of a value type UINT; |
| abstract [CreateULINT](ConnectorFactory/CreateULINT.md)(…) | Creates UInt64 tag of a value type ULINT; |
| abstract [CreateUSINT](ConnectorFactory/CreateUSINT.md)(…) | Creates Byte tag of a value type USINT; |
| abstract [CreateWORD](ConnectorFactory/CreateWORD.md)(…) | Creates UInt16 tag of a value type WORD; |
| abstract [CreateWSTRING](ConnectorFactory/CreateWSTRING.md)(…) | Creates String tag of a value type WSTRING; |
| static [CreateBOOL](ConnectorFactory/CreateBOOL.md)() | Creates Boolean empty tag of a value type BOOL; |
| static [CreateBYTE](ConnectorFactory/CreateBYTE.md)() | Creates Byte empty tag of a value type BYTE; |
| static [CreateDATE](ConnectorFactory/CreateDATE.md)() | Creates DateTime empty tag of a value type DATE; |
| static [CreateDATE_TIME](ConnectorFactory/CreateDATE_TIME.md)() | Creates DateTime empty tag of a value type DATE_AND_TIME (DT); |
| static [CreateDINT](ConnectorFactory/CreateDINT.md)() | Creates Int32 empty tag of a value type DINT; |
| static [CreateDWORD](ConnectorFactory/CreateDWORD.md)() | Creates UInt32 empty tag of a value type DWORD; |
| static [CreateINT](ConnectorFactory/CreateINT.md)() | Creates Int16 empty tag of a value type INT; |
| static [CreateLINT](ConnectorFactory/CreateLINT.md)() | Creates Int64 empty tag of a value type LINT; |
| static [CreateLREAL](ConnectorFactory/CreateLREAL.md)() | Creates Double empty tag of a value type LREAL; |
| static [CreateLTIME](ConnectorFactory/CreateLTIME.md)() | Creates TimeSpan empty tag of a value type LTIME; |
| static [CreateLWORD](ConnectorFactory/CreateLWORD.md)() | Creates UInt64 empty tag of a value type LWORD; |
| static [CreateREAL](ConnectorFactory/CreateREAL.md)() | Creates Single empty tag of a value type REAL; |
| static [CreateSINT](ConnectorFactory/CreateSINT.md)() | Creates SByte empty tag of a value type SINT; |
| static [CreateSTRING](ConnectorFactory/CreateSTRING.md)() | Creates String empty tag of a value type STRING; |
| static [CreateTIME](ConnectorFactory/CreateTIME.md)() | Creates TimeSpan empty tag of a value type TIME; |
| static [CreateTIME_OF_DAY](ConnectorFactory/CreateTIME_OF_DAY.md)() | Creates TimeSpan empty tag of a value type TIME_OF_DAY; |
| static [CreateUDINT](ConnectorFactory/CreateUDINT.md)() | Creates UInt32 empty tag of a value type UDINT; |
| static [CreateUINT](ConnectorFactory/CreateUINT.md)() | Creates UInt16 empty tag of a value type UINT; |
| static [CreateULINT](ConnectorFactory/CreateULINT.md)() | Creates UInt64 empty tag of a value type ULINT; |
| static [CreateUSINT](ConnectorFactory/CreateUSINT.md)() | Creates Byte empty tag of a value type USINT; |
| static [CreateWORD](ConnectorFactory/CreateWORD.md)() | Creates UInt16 empty tag of a value type WORD; |
| static [CreateWSTRING](ConnectorFactory/CreateWSTRING.md)() | Creates String empty tag of a value type WSTRING; |

## Protected Members

| name | description |
| --- | --- |
| [ConnectorFactory](ConnectorFactory/ConnectorFactory.md)() | The default constructor. |

## See Also

* namespace [Ix.Connector](../Ix.Connector.md)

<!-- DO NOT EDIT: generated by xmldocmd for Ix.Connector.dll -->
