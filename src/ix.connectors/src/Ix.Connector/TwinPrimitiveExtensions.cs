// Ix.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using Ix.Connector.ValueTypes;

namespace Ix.Connector;

/// <summary>
///     Provides series of extension method for accessing primitive items.
/// </summary>
public static class TwinPrimitiveExtensions
{
    /// <summary>
    ///     Sets the cyclic value of an onliner.
    /// </summary>
    /// <param name="primitive">Primitive item.</param>
    /// <param name="value">Value to be written to the cyclic variable.</param>
    /// <typeparam name="T">Type of value to be written.</typeparam>
    public static void SetCyclicValue<T>(this OnlinerBase primitive, T value)
    {
        if (primitive == null) throw new ArgumentNullException(nameof(primitive));
        ((dynamic)primitive).Cyclic = Cast<T>(((dynamic)primitive).Cyclic, value);
    }

    /// <summary>
    ///     Sets the shadow value of an onliner.
    /// </summary>
    /// <param name="primitive">Primitive item to which the shadow value will be written.</param>
    /// <param name="value">Value to be written.</param>
    /// <typeparam name="T">Type of value to be written.</typeparam>
    public static void SetShadowValue<T>(this OnlinerBase primitive, T value)
    {
        if (primitive == null) throw new ArgumentNullException(nameof(primitive));
        ((dynamic)primitive).Shadow = Cast<T>(((dynamic)primitive).Shadow, value);
    }

    private static T Cast<T>(this T taget, object obj)
    {
        return (T)obj;
    }


    /// <summary>
    ///     Get the cyclic value of a primitive item.
    /// </summary>
    /// <param name="primitive">Primitive item of which the value will be retrieved.</param>
    /// <typeparam name="T">Type of value to return.</typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Value of given primitive item.</returns>
    public static T GetCyclicValue<T>(this OnlinerBase primitive)
    {
        if (primitive == null) throw new ArgumentNullException("Value of cannot be null");
        return ((dynamic)primitive).Cyclic;
    }


    /// <summary>
    ///     Gets the last value retrieved by the twin.
    /// </summary>
    /// <param name="primitive">Primitive item of which the value will be retrieved.</param>
    /// <typeparam name="T">Type of value to return.</typeparam>
    /// <returns>Last value of primitive item.</returns>
    public static T GetLastValue<T>(this OnlinerBase primitive)
    {
        if (primitive == null) throw new ArgumentNullException(nameof(primitive));
        return ((dynamic)primitive).LastValue;
    }

    /// <summary>
    ///     Get current shadow value of the primitive item.
    /// </summary>
    /// <param name="primitive">Primitive item of which the value will be retrieved.</param>
    /// <typeparam name="T">Type of value to return.</typeparam>
    /// <returns>Shadow value of the primitive item.</returns>
    public static T GetShadowValue<T>(this OnlinerBase primitive)
    {
        if (primitive == null) throw new ArgumentNullException("Value cannot be null");
        return ((dynamic)primitive).Shadow;
    }
}