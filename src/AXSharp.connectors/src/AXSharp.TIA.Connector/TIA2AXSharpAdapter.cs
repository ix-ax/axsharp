// AXSharp.TIA2AXSharp
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/dev/notices.md

using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using Siemens.Simatic.S7.Webserver.API.Enums;
using Siemens.Simatic.S7.Webserver.API.Models;
using Siemens.Simatic.S7.Webserver.API.Services;
using Siemens.Simatic.S7.Webserver.API.Services.RequestHandling;

namespace AXSharp.TIA.Connector;

public class TIA2AXSharpAdapter
{
    private readonly ApiStandardServiceFactory serviceFactory = new();

    private TIA2AXSharpAdapter()
    {
    }

    public static async Task<IList<ITwinObject>> CreateAdapter(WebApiConnector connector)
    {
        IList<ITwinObject> twinDataBlocks = new List<ITwinObject>();
        var adapter = new TIA2AXSharpAdapter();
        var requestHandler =
            await adapter.serviceFactory.GetApiHttpClientRequestHandlerAsync(connector.IPAddress, "Everybody", "");

        List<ApiPlcProgramData> programBlocks =
            (await requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children)).Result;


        // TODO consider multiple program data blocks
        //var myPlcProgramDataBlock = programBlocks.First(el => el.Datatype == ApiPlcProgramDataType.DataBlock);
        //var db = new TIATwinObject(connector, $"\"{myPlcProgramDataBlock.Name}\"", $"\"{myPlcProgramDataBlock.Name}\"");

        //await BrowseParent(myPlcProgramDataBlock, requestHandler, db, 1);
        //twinDataBlocks.Add(db);


        var dataBlockNodes = programBlocks.Where(el => el.Datatype == ApiPlcProgramDataType.DataBlock);

        foreach (var dataBlockNode in dataBlockNodes)
        {
            var db = new TIATwinObject(connector, $"\"{dataBlockNode.Name}\"", $"\"{dataBlockNode.Name}\"");
            await BrowseParent(dataBlockNode, requestHandler, db, 1);
            twinDataBlocks.Add(db);
        }

        return twinDataBlocks;
    }

    private static async Task BrowseParent(ApiPlcProgramData parentNode, IApiRequestHandler requestHandler,
        TIATwinObject parentTwinObject, int dept)
    {
        dept++;

        if (dept > 9)
            return;



        var children = requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children, parentNode).Result.Result;
        parentNode.Children = children;


        if (children == null)
        {
            Console.WriteLine("null");
        }
        await CreateTwin(parentNode, requestHandler, parentTwinObject, dept);

      
        //if (parentNode.ArrayElements.Count > 0)
        //{
        //    foreach (var arrayElement in parentNode.ArrayElements)
        //    {
        //    //browsing children of array
        //        var children = requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children, arrayElement).Result
        //            .Result;

        //        arrayElement.Children = children;

        //        await CreateTwin(arrayElement, requestHandler, parentTwinObject, dept);
        //    }
        //}
        //else
        //{

        //}



        dept--;
    }

    private static async Task CreateTwin(ApiPlcProgramData parentNode, IApiRequestHandler requestHandler,
        TIATwinObject parentTwinObject, int dept)
    {
        foreach (var child in parentNode.Children)
        {

            child.Parents.AddRange(parentNode.Parents);
            child.Parents.Add(parentNode);


            if(child.ArrayElements.Count > 0)
            {

                foreach (var arrayElement in child.ArrayElements)
                {
                    if (arrayElement.Has_children == true)
                    {
                        //is this neccessary? will plcread read arrays of complex objects?
                        var nested = new TIATwinObject(parentTwinObject, arrayElement.Name, arrayElement.Name);
                        parentTwinObject.AddChild(nested);
                        parentTwinObject.AddKid(nested);
                        await BrowseParent(arrayElement, requestHandler, nested, dept);
                    }
                    else
                    {
                        //this method automaticcaly adds valuetag and kid
                        CreatePrimitive(arrayElement, parentTwinObject);
                        //parentTwinObject.AddValueTag(primitive);
                        // parentTwinObject.AddKid(primitive);
                    }
                }
                

            }
            else if (child.Has_children == true)
            {

                if (string.IsNullOrEmpty(child.Name))
                {
                    child.Name = "inheritance";
                    var nested = new TIATwinObject(parentTwinObject, child.Name, child.Name);
                    parentTwinObject.AddChild(nested);
                    parentTwinObject.AddKid(nested);
                    await BrowseParent(child, requestHandler, nested, dept);

                    var x = 10;
                }
                else
                {
                    var nested = new TIATwinObject(parentTwinObject, child.Name, child.Name);
                    parentTwinObject.AddChild(nested);
                    parentTwinObject.AddKid(nested);
                    await BrowseParent(child, requestHandler, nested, dept);
                }
            }
            else
            {
                //this method automaticcaly adds valuetag and kid
               CreatePrimitive(child, parentTwinObject);
                //parentTwinObject.AddValueTag(primitive);
                // parentTwinObject.AddKid(primitive);
            }
        }
    }

    private static ITwinPrimitive CreatePrimitive(ApiPlcProgramData node, ITwinObject parent)
    {
        var symbolTail = node.Name;
        var nameTail = symbolTail;

        switch (node.Datatype)
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