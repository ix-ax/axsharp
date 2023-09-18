# TIA2AXTool

TIA2AXTool is simple CLI program, which is used for generation of AX# TwinObjects in form of Json from TIA datablocks. Output of CLI command is .json file which contains list of TIABrowseElements. This .json file can be deserialized into `TIARootObject` which than can be used for data exchange operations between TIA based plc and .NET application.



## Parameters 

- -o --output : output .json file with serialized datablocks structures (required)
- -i --ipaddress : ip address for connecting to PLC (required)
- -u --username : username for connection (default value: *Everybody*)
- -p --password : password for connection (default value: "")
- -d --datablocks : list of datablocks names, which should be scanned (if not provided, all datablocks will be serialized)


## Examples

tia2ax -i 10.10.10.180 -o D:\\testdata.json -d dbtest

Output is following .json file:

```json
{
  "TIABrowseElements": [
    {
      "Symbol": "TGlobalVariablesDB",
      "Datatype": 1,
      "IsNested": true,
      "Children": [
        {
          "Symbol": "myArrayComplexNested[0]",
          "Datatype": 81,
          "IsNested": true,
          "Children": [
            {
              "Symbol": "nesteditemBOOL",
              "Datatype": 2,
              "IsNested": false,
              "Children": []
            },
            //truncated...
```

This file can be deserialized and read in following way:

```C#
  // connect to your plc
  var connector = new WebApiConnector("10.10.10.180", "Everybody", "", true, string.Empty);
  // deserialize generated root object
  var rootObject = TIA2AXSharpSerializer.Deserialize("test.json");
  //creates adapter from deserialized root object
  var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector, rootObject);
  //retrieve all primitive variables
  var allVariables = adapter.RetrievePrimitives();
  // read variables  
  await connector.ReadBatchAsync(allVariables);

```

Note: Make sure, your application reference `AxSharp.TIA2AXSharp` project