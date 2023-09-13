﻿// AXSharp.TIA2AXSharp
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/dev/notices.md

using AXSharp.Connector;
using AXSharp.Connector.Localizations;
using Newtonsoft.Json;

namespace AXSharp.TIA.Connector;

public class TIATwinObject : ITwinObject
{
    public readonly IList<ITwinObject> _children = new List<ITwinObject>();
    public readonly IList<ITwinElement> _kids = new List<ITwinElement>();

    public readonly ITwinObject _parent;

    public readonly IList<ITwinPrimitive> _primitives = new List<ITwinPrimitive>();


    public TIATwinObject(ITwinObject parent, string readableTail, string symbolTail)
    {
        _parent = parent;
        Symbol = Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
    }

    public string Symbol { get; }
    public string AttributeName { get; }
    public string HumanReadable { get; }

    public ITwinObject GetParent()
    {
        return _parent;
    }

    public string GetSymbolTail()
    {
        throw new NotImplementedException();
    }

    public void Poll()
    {
        throw new NotImplementedException();
    }

    public Translator Interpreter { get; }

    public IEnumerable<ITwinObject> GetChildren()
    {
        return _children;
    }

    public IEnumerable<ITwinElement> GetKids()
    {
        return _kids;
    }

    public IEnumerable<ITwinPrimitive> GetValueTags()
    {
        return _primitives;
    }

    public void AddChild(ITwinObject twinObject)
    {
        _children.Add(twinObject);
    }

    public void AddValueTag(ITwinPrimitive twinPrimitive)
    {
        _primitives.Add(twinPrimitive);
    }

    public void AddKid(ITwinElement kid)
    {
        _kids.Add(kid);
    }

    public AXSharp.Connector.Connector GetConnector()
    {
        return _parent.GetConnector();
    }

    public Task<T> OnlineToPlain<T>()
    {
        throw new NotImplementedException();
    }

    public Task PlainToOnline<T>(T plain)
    {
        throw new NotImplementedException();
    }

    public Task<T> ShadowToPlain<T>()
    {
        throw new NotImplementedException();
    }

    public Task PlainToShadow<T>(T plain)
    {
        throw new NotImplementedException();
    }
}