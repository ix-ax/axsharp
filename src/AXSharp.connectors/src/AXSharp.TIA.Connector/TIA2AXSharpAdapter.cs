// AXSharp.TIA2AXSharp
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/dev/notices.md

using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using AXSharp.TIA2AXSharp;
using Newtonsoft.Json;
using Siemens.Simatic.S7.Webserver.API.Enums;
using Siemens.Simatic.S7.Webserver.API.Models;
using Siemens.Simatic.S7.Webserver.API.Services;
using Siemens.Simatic.S7.Webserver.API.Services.RequestHandling;
using System.Runtime.InteropServices;

namespace AXSharp.TIA.Connector;

public class TIA2AXSharpAdapter
{
    private readonly ApiStandardServiceFactory serviceFactory = new();

    private TIA2AXSharpAdapter()
    {
    }

    public static async Task<TIARootObject> CreateTIARootObject(WebApiConnector connector, string[] dataBlockNames)
    {
        var browseElements = new List<TIABrowseElement>();
        var dataBlockNodes = new List<ApiPlcProgramData>();
        var adapter = new TIA2AXSharpAdapter();
        var requestHandler =
               await adapter.serviceFactory.GetApiHttpClientRequestHandlerAsync(connector.IPAddress, "Everybody", "");


        List<ApiPlcProgramData> programBlocks =
        (await requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children)).Result;



        foreach (var dataBlockName in dataBlockNames)
        {
            var dataBlockNode = programBlocks.FirstOrDefault(p => p.Name == dataBlockName);
            if(dataBlockNode != null)
            {
                dataBlockNodes.Add(dataBlockNode);
            }
        }


        foreach (var dataBlockNode in dataBlockNodes)
        {
            var db = new TIATwinObject(connector, $"\"{dataBlockNode.Name}\"", $"\"{dataBlockNode.Name}\"");
            var dbe = new TIABrowseElement(dataBlockNode.Name, dataBlockNode.Datatype, true);
            await BrowseParent(dataBlockNode, requestHandler, db, dbe, 1);
            browseElements.Add(dbe);
        }

        return new TIARootObject { TIATwinObjects = browseElements };
    }

    public static async Task<TIARootObject> CreateTIARootObject(WebApiConnector connector)
    {
        var browseElements = new List<TIABrowseElement>();
        var adapter = new TIA2AXSharpAdapter();
        var requestHandler =
               await adapter.serviceFactory.GetApiHttpClientRequestHandlerAsync(connector.IPAddress, "Everybody", "");


        List<ApiPlcProgramData> programBlocks =
        (await requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children)).Result;

        var dataBlockNodes = programBlocks.Where(el => el.Datatype == ApiPlcProgramDataType.DataBlock);

        foreach (var dataBlockNode in dataBlockNodes)
        {
            var db = new TIATwinObject(connector, $"\"{dataBlockNode.Name}\"", $"\"{dataBlockNode.Name}\"");
            var dbe = new TIABrowseElement(dataBlockNode.Name, dataBlockNode.Datatype, true);
            await BrowseParent(dataBlockNode, requestHandler, db, dbe, 1);
            browseElements.Add(dbe);
        }

        return new TIARootObject { TIATwinObjects = browseElements };
    }

    public static async Task<IEnumerable<ITwinObject>> CreateAdapter(WebApiConnector connector, string[] dataBlockNames, TIARootObject rootObject )
    {
        var twins = new List<ITwinObject>();
        
        foreach (var datablockName in dataBlockNames)
        {
            var dataBlockNode = rootObject.TIATwinObjects.FirstOrDefault(p => p.Symbol == datablockName);

            if (dataBlockNode != null)
            {
                var db = new TIATwinObject(connector, $"\"{dataBlockNode.Symbol}\"", $"\"{dataBlockNode.Symbol}\"");
                await CreateTwinFromSerialized(dataBlockNode, db, 1);
                twins.Add(db);
            }
        }
        return twins;
    }

    public static async Task<IEnumerable<ITwinObject>> CreateAdapter(WebApiConnector connector, TIARootObject rootObject)
    {
        var twins = new List<ITwinObject>();

        foreach (var dataBlockNode in rootObject.TIATwinObjects)
        {
            var db = new TIATwinObject(connector, $"\"{dataBlockNode.Symbol}\"", $"\"{dataBlockNode.Symbol}\"");
            await CreateTwinFromSerialized(dataBlockNode, db, 1);
            twins.Add(db);
        }
        return twins;
    }



    public static async Task<IEnumerable<ITwinObject>> CreateAdapter(WebApiConnector connector, bool serializeDefault)
    {
     
        var twins = new List<ITwinObject>();
        var browseElements = new List<TIABrowseElement>();
        var adapter = new TIA2AXSharpAdapter();
        var rootObject = new TIARootObject();

        TIARootObject? deserialize = TryToDeSerializeDefaultRootObject();

        if (deserialize != null)
        {
            foreach (var dataBlockNode in deserialize.TIATwinObjects)
            {
                var db = new TIATwinObject(connector, $"\"{dataBlockNode.Symbol}\"", $"\"{dataBlockNode.Symbol}\"");
                await CreateTwinFromSerialized(dataBlockNode, db, 1);
                twins.Add(db);
            }
        }
        else
        {

            var requestHandler =
                await adapter.serviceFactory.GetApiHttpClientRequestHandlerAsync(connector.IPAddress, "Everybody", "");


            List<ApiPlcProgramData> programBlocks =
            (await requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children)).Result;

            var dataBlockNodes = programBlocks.Where(el => el.Datatype == ApiPlcProgramDataType.DataBlock);

            foreach (var dataBlockNode in dataBlockNodes)
            {
                var db = new TIATwinObject(connector, $"\"{dataBlockNode.Name}\"", $"\"{dataBlockNode.Name}\"");
                var dbe = new TIABrowseElement(dataBlockNode.Name, dataBlockNode.Datatype, true);
                await BrowseParent(dataBlockNode, requestHandler, db, dbe, 1);
                twins.Add(db);
                browseElements.Add(dbe);
            }

            if(serializeDefault)
            {
                var rootObj = new TIARootObject();
                rootObj.TIATwinObjects = browseElements;
                SerializeDefault(rootObj);
            }
        }
        return twins;
    }

    private static TIARootObject? TryToDeSerializeDefaultRootObject()
    {

        if (!File.Exists("defaultRootObj.json")) return null;

        using (StreamReader file = File.OpenText("defaultRootObj.json"))
        {
            JsonSerializer serializer = new JsonSerializer ();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            return (TIARootObject?)serializer.Deserialize(file, typeof(TIARootObject));
        }
    }


    private static void SerializeDefault(TIARootObject adapter)
    {
        JsonSerializer serializer = new JsonSerializer();
        serializer.NullValueHandling = NullValueHandling.Ignore;
        serializer.Formatting = Formatting.Indented;

        using (StreamWriter sw = new StreamWriter("defaultRootObj.json"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, adapter);
        }
    }

    private static async Task CreateTwinFromSerialized(TIABrowseElement parentNode,
        ITwinObject parentTwinObject, int dept)
    {

        foreach (var child in parentNode.Children)
        {
            if (child.IsNested)
            {
                var nested = new TIATwinObject(parentTwinObject, child.Symbol, child.Symbol);
                parentTwinObject.AddChild(nested);
                parentTwinObject.AddKid(nested);
                await CreateTwinFromSerialized(child, nested, dept);
            }
            else
            {
                CreatePrimitiveFromSerialized(child, parentTwinObject);
            }
        }

    }
   
    private static async Task BrowseParent(ApiPlcProgramData parentNode, IApiRequestHandler requestHandler,
        TIATwinObject parentTwinObject, TIABrowseElement parentBrowseObject, int dept)
    {
        dept++;

        if (dept > 9)
            return;

        var children = requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children, parentNode).Result.Result;
        parentNode.Children = children;

        await CreateTwin(parentNode, requestHandler, parentTwinObject, parentBrowseObject, dept);

    }

    private static async Task CreateTwin(ApiPlcProgramData parentNode, IApiRequestHandler requestHandler,
        TIATwinObject parentTwinObject, TIABrowseElement parentBrowseObject, int dept)
    {

        foreach (var child in parentNode.Children)
        {
            child.Parents.AddRange(parentNode.Parents);
            child.Parents.Add(parentNode);

            if(child.ArrayElements.Any())
            {
                foreach (var arrayElement in child.ArrayElements)
                {
                    if (arrayElement.Has_children == true)
                    {
                        //is this neccessary? will plcread read arrays of complex objects?
                        var nested = new TIATwinObject(parentTwinObject, arrayElement.Name, arrayElement.Name);
                        parentTwinObject.AddChild(nested);
                        parentTwinObject.AddKid(nested);

                        var browse = new TIABrowseElement(arrayElement.Name, arrayElement.Datatype, true);
                        parentBrowseObject.Children.Add(browse);

                        await BrowseParent(arrayElement, requestHandler, nested, browse, dept);
                    }
                    else
                    {
                        //this method automaticcaly adds valuetag and kid
                        CreatePrimitive(arrayElement, parentTwinObject);

                        var primitive = new TIABrowseElement(arrayElement.Name, arrayElement.Datatype, false);
                        parentBrowseObject.Children.Add(primitive);
                    }
                }
                
            }
            else if (child.Has_children == true)
            {

                if (string.IsNullOrEmpty(child.Name))
                {
                    throw new Exception($"Inheritance error, not supported. Parent is {parentNode.Name}");
                }
                else
                {
                    var nested = new TIATwinObject(parentTwinObject, child.Name, child.Name);
                    parentTwinObject.AddChild(nested);
                    parentTwinObject.AddKid(nested);

                    var browse = new TIABrowseElement(child.Name, child.Datatype, true);
                    parentBrowseObject.Children.Add(browse);

                    await BrowseParent(child, requestHandler, nested, browse, dept);
                }
            }
            else
            {
               //this method automaticcaly adds value tags and kids
               CreatePrimitive(child, parentTwinObject);

                var primitive = new TIABrowseElement(child.Name, child.Datatype, false);
                parentBrowseObject.Children.Add(primitive);
            }
        }

    }

    private static ITwinPrimitive CreatePrimitive(ApiPlcProgramData node, ITwinObject parent)
    {
        string symbolTail = node.Name;
        string nameTail = symbolTail;
        return GetDatatype(node.Datatype, parent, symbolTail, nameTail);
    }

    private static ITwinPrimitive CreatePrimitiveFromSerialized(TIABrowseElement node, ITwinObject parent)
    {
        string symbolTail = node.Symbol;
        string nameTail = symbolTail;
        return GetDatatype(node.Datatype, parent, symbolTail, nameTail);
    }

    private static ITwinPrimitive GetDatatype(ApiPlcProgramDataType datatype, ITwinObject parent, string symbolTail, string nameTail)
    {
        switch (datatype)
        {
            case ApiPlcProgramDataType.Bool:
                return new WebApiBool(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Byte:
                return new WebApiByte(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Usint:
                return new WebApiUSInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Sint:
                return new WebApiSInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Char:
                return new WebApiChar(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Wchar:
                return new WebApiWChar(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Int:
                return new WebApiInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Date:
                return new WebApiDate(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Uint:
                return new WebApiUInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Word:
                return new WebApiWord(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Date_and_time:
                return new WebApiDateTime(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Dint:
                return new WebApiDInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Time:
                return new WebApiTime(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Dword:
                return new WebApiDWord(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Time_of_day:
                return new WebApiTimeOfDay(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Udint:
                return new WebApiUdInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Real:
                return new WebApiReal(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Lreal:
                return new WebApiLReal(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Ldt:
                return new WebApiLDateTime(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Lint:
                return new WebApiLInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Ltime:
                return new WebApiLTime(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Ulint:
                return new WebApiULInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Lword:
                return new WebApiLWord(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Ltime_of_day:
                return new WebApiLTimeOfDay(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.String:
                return new WebApiString(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Wstring:
                return new WebApiWString(parent, symbolTail, nameTail);
            default:
                return null;
        }
    }



}