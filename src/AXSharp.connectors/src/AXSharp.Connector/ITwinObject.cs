﻿// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AXSharp.Connector;

/// <summary>
///     Basic contract for any complex object that is product of build process.
/// </summary>
public interface ITwinObject : ITwinElement
{
    /// <summary>
    ///     Gets all complex object that are created by this instance.
    /// </summary>
    /// <returns></returns>
    IEnumerable<ITwinObject> GetChildren();

    /// <summary>
    ///     Gets kids of this instance.
    /// </summary>
    /// <returns></returns>
    IEnumerable<ITwinElement> GetKids();

    /// <summary>
    ///     Get all base type objects (tags) that are created by this instance.
    /// </summary>
    /// <returns></returns>
    IEnumerable<ITwinPrimitive> GetValueTags();

    /// <summary>
    ///     Adds child object to the list of children of this object. <see cref="GetChildren" />
    ///     <note type="note">
    ///         This method is used by the objects built in building process. Not to be used by framework
    ///         consumers.
    ///     </note>
    /// </summary>
    /// <param name="twinObject">Child <see cref="AXSharp.Connector.ITwinObject" /></param>
    void AddChild(ITwinObject twinObject);

    /// <summary>
    ///     Adds child value tag to the list of value tags of this object. <see cref="GetValueTags" />
    ///     <note type="note">
    ///         This method is used by the objects built in building process. Not to be used by framework
    ///         consumers.
    ///     </note>
    /// </summary>
    /// <param name="twinPrimitive">Child <see cref="ITwinPrimitive" /></param>
    void AddValueTag(ITwinPrimitive twinPrimitive);

    /// <summary>
    ///     Adds kid object to the list of kids of this object.
    ///     <note type="note">
    ///         This method is used by the objects built in building process. Not to be used by framework
    ///         consumers.
    ///     </note>
    /// </summary>
    /// <param name="kid">Child <see cref="ITwinElement" /></param>
    void AddKid(ITwinElement kid);

    /// <summary>
    ///     Gets the instance of the <see cref="Connector" /> class to which this <see cref="AXSharp.Connector.ITwinObject" />
    ///     belongs.
    /// </summary>
    /// <returns>Connector</returns>
    Connector GetConnector();

    /// <summary>
    /// Reads online data and retrieved POCO object populated with actual online data.
    /// </summary>
    /// <returns>POCO with online data of this object</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<T> OnlineToPlain<T>()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Writes data from POCO object to online data (PLC)
    /// </summary>
    /// <param name="plain">POCO object to be written to the controller.</param>
    /// <exception cref="NotImplementedException"></exception>
    public Task PlainToOnline<T>(T plain)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Read data from shadows of this object to a new instance of a POCO object.
    /// </summary>
    /// <returns>POCO object populated by data from the shadows of this object.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<T> ShadowToPlain<T>()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Writes data from POCO object to shadow data of this object.
    /// </summary>
    /// <param name="plain">POCO object to be written to the shadows of this object.</param>
    /// <exception cref="NotImplementedException"></exception>
    public Task<T> PlainToShadow<T>(T plain)
    {
        throw new NotImplementedException();
    }
}