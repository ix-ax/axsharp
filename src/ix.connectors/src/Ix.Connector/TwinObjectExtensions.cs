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
using Ix.Connector.ValueTypes;

namespace Ix.Connector;

/// <summary>
///     Extensions methods for <see cref="Ix.Connector.ITwinObject" />.
/// </summary>
public static class TwinObjectExtensions
{

    public static readonly string OnlineToPlainMethodName = "OnlineToPlainAsync";

    public static readonly string PlainToOnlineMethodName = "PlainToOnlineAsync";

    /// <summary>
    ///     Makes <see cref="Ix.Connector.ITwinObject" /> readonly for this application.
    /// </summary>
    /// <example>
    ///     <code>
    ///     // Renders whole structure value tag readonly
    ///     ReadonlyStructure.MakeReadOnly();
    ///     // Renders element [0,0] of the array readonly.
    ///     roArray[0, 0].MakeReadOnly();
    /// </code>
    /// </example>
    /// <param name="structure">Object to be rendered readonly</param>
    public static void MakeReadOnly(this ITwinObject structure)
    {
        ArgumentNullException.ThrowIfNull(structure);
        foreach (var item in structure.GetValueTags()) item.MakeReadOnly();

        foreach (var item in structure.GetChildren()) item.MakeReadOnly();
    }

    /// <summary>
    ///     Makes <see cref="Ix.Connector.ITwinObject" /> access only once during the lifetime
    ///     of the application.
    /// </summary>
    /// <example>
    ///     <code>
    ///     // Renders whole complex type access only once.
    ///     ReadonlyStructure.MakeReadOnce();
    ///     // Renders element [0,0] of the array readonly.
    ///     roArray[0, 0].MakeReadOnce();
    /// </code>
    /// </example>
    /// <param name="structure">Object to be rendered readonly</param>
    public static void MakeReadOnce(this ITwinObject structure)
    {
        ArgumentNullException.ThrowIfNull(structure);
        foreach (var item in structure.GetValueTags()) item.MakeReadOnce();

        foreach (var item in structure.GetChildren()) item.MakeReadOnce();
    }

    /// <summary>
    ///     Reads all value tags of instance <see cref="ITwinOnlineObject" />.
    /// </summary>
    /// <example>
    ///     <code>
    /// // Reads all value tags of the MAIN PRG. The value is stored in property 'Cyclic' and 'LastValue' of the respective value tag.
    /// Connector.MAIN.Read();
    /// </code>
    /// </example>
    /// <param name="structure"></param>
    public static async Task<IEnumerable<ITwinPrimitive>> ReadAsync(this ITwinObject structure)
    {
        ArgumentNullException.ThrowIfNull(structure);
        var primitives = RetrievePrimitives(structure);
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();
        await structure.GetConnector().ReadBatchAsync(twinPrimitives);
        return twinPrimitives;
    }

    /// <summary>
    ///     Writes all value tags of instance <see cref="ITwinOnlineObject" />
    /// </summary>
    /// <example>
    ///     <code>
    /// // Writes all modified values of the MAIN PRG.
    /// Connector.Online.MAIN.GrowingLayoutStructure.Write()
    /// </code>
    /// </example>
    /// <param name="structure">Structure to be written</param>
    /// <returns>Primitives that have been written.</returns>
    public static async Task<IEnumerable<ITwinPrimitive>> WriteAsync(this ITwinObject structure)
    {
        var primitives = RetrievePrimitives(structure);
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();
        await structure.GetConnector().WriteBatchAsync(twinPrimitives);
        return twinPrimitives;
    }


    /// <summary>
    ///     Retrieves all value tags of given object recursively.
    /// </summary>
    /// <param name="onlineObject">Object from which the value tags are to be retrieved.</param>
    /// <param name="valueTags">Pre-existing value tags.</param>
    /// <returns>Value tags of given object.</returns>
    /// <example>
    ///     This example demonstrates how to get all value tags of the MAIN PRG object.
    ///     <code>
    ///     var mainProgramTags = Connector.MAIN.RetrievePrimitives();
    /// </code>
    /// </example>
    public static IEnumerable<ITwinPrimitive> RetrievePrimitives(this ITwinObject onlineObject,
        List<ITwinPrimitive> valueTags = null)
    {
        ArgumentNullException.ThrowIfNull(onlineObject);

        valueTags ??= new List<ITwinPrimitive>();

        var children = onlineObject.GetChildren();

        if (children != null)
            foreach (var child in children)
                RetrievePrimitives(child, valueTags);

        var tags = onlineObject.GetValueTags();

        if (tags != null)
            foreach (var valueTag in tags)
                valueTags.Add(valueTag);

        return valueTags;
    }

    /// <summary>
    ///     Subscribes a delegate to be invoked when any <see cref="OnlinerBase{T}.Shadow" /> value on given object changes its
    ///     value.<see cref="Ix.Connector.ITwinObject" />
    /// </summary>
    /// <example>
    ///     <code>
    /// class ShadowValueChangeObserver
    /// {
    ///     public ShadowValueChangeObserver()
    ///     {
    ///         Connector.MAIN.SubscribeShadowValueChange(DetectShadowValueChange);                 
    ///     }
    ///     
    ///     private void DetectShadowValueChange(IValueTag valueTag, dynamic original, dynamic newValue)
    ///     {
    ///         Console.WriteLine($"Value '{valueTag.Symbol}' has changed form {original} to {newValue}.")
    ///     }
    ///      
    /// }
    /// </code>
    /// </example>
    /// <note>
    ///     Notice that the value change observer uses is not an event but simple delegate. Any previous delegate assignment
    ///     will be overwritten by the subscription of the new
    ///     delegate.
    /// </note>
    /// <param name="obj">Observed object.</param>
    /// <param name="valueChangeDelegate">Delegate to be invoked on shadow value change.</param>
    /// <returns>Primitives that have been subscribed for shadow value change.</returns>
    public static IEnumerable<ITwinPrimitive> SubscribeShadowValueChange(this ITwinObject obj,
        OnlinerBase.ValueChangeDelegate valueChangeDelegate)
    {
        ArgumentNullException.ThrowIfNull(valueChangeDelegate);
        var valueTags = obj.RetrievePrimitives();
        var subscribeShadowValueChange = valueTags as ITwinPrimitive[] ?? valueTags.ToArray();
        foreach (var tag in subscribeShadowValueChange) tag.ShadowValueChange = valueChangeDelegate;

        return subscribeShadowValueChange;
    }

    /// <summary>
    ///     Un-subscribes <see cref="OnlinerBase{T}.Shadow" /> value change delegate from the value tags of given object.
    /// </summary>
    /// <param name="obj">Object from which the shadow value change delegate has to be removed.</param>
    public static void UnSubscribeShadowValueChange(this ITwinObject obj)
    {
        var valueTags = obj.RetrievePrimitives();
        foreach (var tag in valueTags) tag.ShadowValueChange = null;
    }


    /// <summary>
    ///     Subscribes a delegate to be invoked when any <see cref="OnlinerBase{T}.Edit" /> value on given object changes its
    ///     value.<see cref="Ix.Connector.ITwinObject" />
    /// </summary>
    /// <example>
    ///     <code>
    /// class EditValueChangeObserver
    /// {
    ///     public EditValueChangeObserver()
    ///     {
    ///         Connector.MAIN.SubscribeEditValueChange(DetectEditValueChange);                 
    ///     }
    ///     
    ///     private void DetectEditValueChange(IValueTag valueTag, dynamic original, dynamic newValue)
    ///     {
    ///         Console.WriteLine($"Value '{valueTag.Symbol}' has changed form {original} to {newValue}.")
    ///     }
    ///      
    /// }
    /// </code>
    /// </example>
    /// <note>
    ///     Notice that the value change observer uses is not an event but simple delegate. Any previous delegate assignment
    ///     will be overwritten by the subscription of the new
    ///     delegate.
    /// </note>
    /// <param name="obj">Observed object.</param>
    /// <param name="valueChangeDelegate">Delegate to be invoked on Edit value change.</param>
    /// <returns></returns>
    public static IEnumerable<ITwinPrimitive> SubscribeEditValueChange(this ITwinObject obj,
        OnlinerBase.ValueChangeDelegate valueChangeDelegate)
    {
        ArgumentNullException.ThrowIfNull(valueChangeDelegate);
        var valueTags = obj.RetrievePrimitives();
        foreach (var tag in valueTags) tag.EditValueChange = valueChangeDelegate;

        return valueTags;
    }

    /// <summary>
    ///     Un-subscribes <see cref="OnlinerBase{T}.Edit" /> value change delegate from the value tags of given object.
    /// </summary>
    /// <param name="obj">Object from which the <see cref="OnlinerBase{T}.Edit" /> value change delegate has to be removed.</param>
    public static void UnSubscribeEditValueChange(this ITwinObject obj)
    {
        var valueTags = obj.RetrievePrimitives();
        foreach (var tag in valueTags) tag.EditValueChange = null;
    }

    /// <summary>
    /// Copies the data from Online primitive items (PLC) of an <see cref="ITwinObject"/> to shadow value holders.
    /// </summary>
    /// <param name="obj">Twin object to copy.</param>
    public static async Task OnlineToShadowAsync(this ITwinObject obj)
    {
        await obj.ReadAsync();
        obj.RetrievePrimitives().ToList().ForEach(p => p.FromOnlineToShadow());
    }

    /// <summary>
    /// Copies the data from Shadow value holder to online primitive items (PLC) of an <see cref="ITwinObject"/>.
    /// </summary>
    /// <param name="obj">Twin object to copy.</param>
    public static async Task ShadowToOnlineAsync(this ITwinObject obj)
    {
        obj.RetrievePrimitives().ToList().ForEach(p => p.FromShadowToOnline());
        await obj.WriteAsync();
    }
}