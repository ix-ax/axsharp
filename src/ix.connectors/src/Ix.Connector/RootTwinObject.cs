// Ix.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using Ix.Connector.ValueTypes;

namespace Ix.Connector;

/// <summary>
///     Object to represent the root TwinObject. Instance of this class is typically used as root for the connector object.
/// </summary>
/// <note>This object is used in building process and should not be directly instantiated by framework consumers.</note>
[Obsolete("This class should not be directly instantiated by framework consumers.")]
public class RootTwinObject : ITwinObject
{
    /// <summary>
    ///     Gets list of children fot this root object.
    /// </summary>
    private readonly List<ITwinObject> Children = new();

    /// <summary>
    ///     Gets list of children fot this root object.
    /// </summary>
    private readonly List<ITwinElement> Kids = new();

    internal string SymbolTail { get; } = string.Empty;

    private List<ITwinPrimitive> ValueTags { get; } = new();

    /// <summary>
    ///     Get sn empty identity for this root object.
    /// </summary>
    public OnlinerULInt Identity => new();

    /// <summary>
    ///     Get symbol tail of this object.
    /// </summary>
    /// <returns></returns>
    public string GetSymbolTail()
    {
        return SymbolTail;
    }

    /// <inheritdoc />
    public void Poll()
    {
        // We do not allow polling for root objects.
    }

    /// <summary>
    ///     Gets empty name for this root object.
    /// </summary>
    public string AttributeName => string.Empty;

    /// <summary>
    ///     Gets empty symbol for this root object.
    /// </summary>
    public string Symbol => string.Empty;

    /// <summary>
    ///     Gets empty human readable of this root object.
    /// </summary>
    public string HumanReadable => string.Empty;

    /// <summary>
    ///     Get list for this root object.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ITwinObject> GetChildren()
    {
        return Children;
    }

    /// <summary>
    ///     Gets this instance as parent object.
    /// </summary>
    /// <returns></returns>
    public ITwinObject GetParent()
    {
        return this;
    }

    /// <summary>
    ///     Adds child object to this root object.
    /// </summary>
    /// <param name="twinObject"></param>
    public void AddChild(ITwinObject twinObject)
    {
        ArgumentNullException.ThrowIfNull(twinObject);
        Children.Add(twinObject);
    }

    /// <summary>
    ///     Gets value tags of this root object.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ITwinPrimitive> GetValueTags()
    {
        return ValueTags;
    }

    /// <summary>
    ///     Adds value tag to this root object.
    /// </summary>
    /// <param name="twinPrimitive"></param>
    public void AddValueTag(ITwinPrimitive twinPrimitive)
    {
        ArgumentNullException.ThrowIfNull(twinPrimitive);
        ValueTags.Add(twinPrimitive);
    }

    /// <summary>
    ///     Get the instance of connector of this root object.
    /// </summary>
    /// <returns></returns>
    public Connector GetConnector()
    {
        return this as Connector;
    }

    public object OnlineToPlain()
    {
        throw new NotImplementedException();
    }

    public void PlainToOnline(object plain)
    {
        throw new NotImplementedException();
    }

    public object ShadowToPlain()
    {
        throw new NotImplementedException();
    }

    public void PlainToShadow(object plain)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    ///     Add member to the list of kids
    /// </summary>
    /// <param name="kid">Kid</param>
    public void AddKid(ITwinElement kid)
    {
        ArgumentNullException.ThrowIfNull(kid);
        Kids.Add(kid);
    }

    /// <summary>
    ///     Gets all kids of this object.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ITwinElement> GetKids()
    {
        return Kids;
    }
}