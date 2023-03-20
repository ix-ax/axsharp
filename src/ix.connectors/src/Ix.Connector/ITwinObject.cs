// Ix.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ix.Connector;

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
    /// <param name="twinObject">Child <see cref="Ix.Connector.ITwinObject" /></param>
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
    ///     Gets the instance of the <see cref="Connector" /> class to which this <see cref="Ix.Connector.ITwinObject" />
    ///     belongs.
    /// </summary>
    /// <returns>Connector</returns>
    Connector GetConnector();

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

}