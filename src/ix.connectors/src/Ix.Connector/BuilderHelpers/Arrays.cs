// Ix.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

#undef TRACE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Ix.Connector.ValueTypes;

namespace Ix.Connector.BuilderHelpers;

/// <summary>
///     Helper methods for creating and manipulating arrays in Twin objects.
/// </summary>
public static class Arrays
{
    private static T CreateJaggedArray<T>(params int[] lengths)
    {
        return (T)InitializeJaggedArray(typeof(T).GetElementType(), 0, lengths);
    }

    private static object InitializeJaggedArray(Type type, int index, int[] lengths)
    {
        var array = Array.CreateInstance(type, lengths[index]);
        var elementType = type.GetElementType();

        if (elementType != null)
            for (var i = 0; i < lengths[index]; i++)
                array.SetValue(
                    InitializeJaggedArray(elementType, index + 1, lengths), i);

        return array;
    }

    /// <summary>
    ///     Instantiates an array.
    /// </summary>
    /// <param name="array">Array</param>
    /// <param name="parent">Parent object.</param>
    /// <param name="readableTail">Human readable tail.</param>
    /// <param name="symbolTail">Symbol tail.</param>
    /// <param name="intializer">Intializes</param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void InstantiateArray(Array array,
        ITwinObject parent,
        string readableTail,
        string symbolTail,
        Func<ITwinObject, string, string, object> intializer)
    {
        foreach (var element in GetIndices(array))
        {
            var indiceAsString = GetIndicesAsString(element);
            var a = intializer(parent,
                $"{readableTail}{indiceAsString}",
                $"{symbolTail}{indiceAsString}");
            array.SetValue(a, element);
        }
    }

    /// <summary>
    ///     Instantiates an array.
    /// </summary>
    /// <param name="array">Array</param>
    /// <param name="intializer">Intializer</param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void InstantiateArray(Array array,
        Func<object> intializer)
    {
        foreach (var element in GetIndices(array)) array.SetValue(intializer(), element);
    }

    /// <summary>
    ///     Copies shadows of an array of primitive type into onliners. [Internal use only]
    /// </summary>
    /// <typeparam name="T">Array type</typeparam>
    /// <param name="arr">Array to have the values copied.</param>
    //[Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CopyShadowToCyclicPrimitive<T>(Array arr) where T : OnlinerBase
    {
        foreach (T element in arr) element.SetCyclicValue(element.GetShadowValue<T>());
    }

    /// <summary>
    ///     Copies shadows of an array of a complex type into onliners. [Internal use only]
    /// </summary>
    /// <param name="arr">Array to have the values copied.</param>
    //[Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CopyShadowToCyclicComplex(Array arr)
    {
        var methodInfo = arr.GetMethodForNonPublicTypes("LazyShadowToOnline");

        if (methodInfo == null)
            foreach (var element in arr)
                ((dynamic)element).LazyShadowToOnline();
        else
            foreach (var element in arr)
                methodInfo.Invoke(element, new object[] { });
    }


    /// <summary>
    ///     Copies onliners of an array of a primitive type into shadows. [Internal use only]
    /// </summary>
    /// <typeparam name="T">Array type</typeparam>
    /// <param name="arr">Array to have the values copied.</param>
    [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CopyCyclicToShadowPrimitive<T>(Array arr) where T : OnlinerBase
    {
        foreach (var index in GetIndices(arr))
        {
            var element = arr.GetValue(index) as T;
            element.SetShadowValue(element.GetCyclicValue<T>());
        }
    }

    /// <summary>
    ///     Copies onliners of an array of a complex type into shadows. [Internal use only]
    /// </summary>
    /// <typeparam name="T">Array type</typeparam>
    /// <param name="arr">Array to have the values copied.</param>
    [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CopyCyclicToShadowComplex<T>(Array arr) where T : ITwinObject
    {
        var methodInfo = arr.GetMethodForNonPublicTypes("LazyOnlineToShadow");

        if (methodInfo == null)
            foreach (var element in arr)
                ((dynamic)element).LazyOnlineToShadow();
        else
            foreach (var element in arr)
                methodInfo.Invoke(element, new object[] { });
    }

    /// <summary>
    ///     Gets index of an array as string.[Internal use only]
    /// </summary>
    /// <param name="index">Array index.</param>
    /// <returns>String representation of index</returns>
    //[Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string GetIndicesAsString(int[] index)
    {
        var ind = string.Empty;

        foreach (var a in index)
        {
            var b = a == -1 ? 0 : a;
            ind = string.IsNullOrEmpty(ind) ? $"[{b}" : $"{ind},{b}";
        }

        ind = $"{ind}]";

        return ind;
    }

    /// <summary>
    ///     Get indices of an array as an array of string.[Internal use only]
    /// </summary>
    /// <param name="array">Array index.</param>
    /// <returns>Array of string representation of index.</returns>
    //[Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string[] GetIndicesAsString(Array array)
    {
        var indices = new List<string>();
        foreach (var index in GetIndices(array))
        {
            var ind = string.Empty;

            foreach (var a in index)
            {
                var b = a == -1 ? 0 : a;
                ind = string.IsNullOrEmpty(ind) ? $"[{b}" : $"{ind},{b}";
            }

            ind = $"{ind}]";

            indices.Add(ind);
        }

        return indices.ToArray();
    }

    /// <summary>
    ///     Creates list of indicies of give array.
    /// </summary>
    /// <param name="array">Array from which indiecies are being created.</param>
    /// <returns></returns>
    //[Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static int[][] GetIndices(Array array)
    {
        var arrays = EmptyDimensions(array);

        // Add first dimension
        var arrayIndex = 0;
        var rank = 0;
        for (var dimension = 0; dimension <= array.GetUpperBound(rank); dimension++)
        {
            arrays[dimension][0] = dimension;
            arrayIndex = dimension;
        }

        // Add from second dimension on
        for (rank = 1; rank < array.Rank; rank++)
        {
            var filled = arrays.TakeWhile(p => p[0] != -1).ToArray();

            for (var dimension = 0; dimension <= array.GetUpperBound(rank); dimension++)
            {
                // do not add 0 dimension that has been already added in the previous iteration.
                if (dimension == 0)
                    continue;

                // Copy previous array items
                foreach (var item in filled)
                {
                    arrayIndex++;
                    item.CopyTo(arrays[arrayIndex], 0);
                    arrays[arrayIndex][rank] = dimension;
                }
            }
        }

        for (var ari = 0; ari < arrays.Length; ari++)
        for (var rnk = 0; rnk < arrays[ari].Length; rnk++)
            arrays[ari][rnk] = arrays[ari][rnk] == -1 ? 0 : arrays[ari][rnk];

        return arrays;
    }

    private static int[][] EmptyDimensions(Array array)
    {
        var emptyDimensions = new List<List<int>>();

        var numberOfElements = 1;
        for (var rank = 0; rank < array.Rank; rank++)
            numberOfElements = numberOfElements * (array.GetUpperBound(rank) + 1);

        for (var i = 0; i < numberOfElements; i++)
        {
            var n = new List<int>();
            for (var rank = 0; rank < array.Rank; rank++) n.Add(-1);

            emptyDimensions.Add(n);
        }

        var arrays = emptyDimensions.Select(a => a.ToArray()).ToArray();
        return arrays;
    }

    /// <summary>
    ///     Copies plainers of an array into onliners. [Internal use only]
    /// </summary>
    /// <param name="plainSource">Source array.</param>
    /// <param name="onlineTarget">Destination array.</param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CopyPlainToOnline<P, O>(Array plainSource, Array onlineTarget)
    {
        var sourceEnumerable = plainSource.Cast<P>();
        var targetEnumerable = onlineTarget.Cast<O>();

        var enumerable = targetEnumerable as O[] ?? targetEnumerable.ToArray();
        var ps = sourceEnumerable as P[] ?? sourceEnumerable.ToArray();
        switch (enumerable.ElementAtOrDefault(0))
        {
            case ITwinPrimitive t:
                for (var i = 0; i < ps.Length; i++)
                    ((dynamic)enumerable.ElementAt(i)).Cyclic = ps.ElementAt(i);
                break;
            case ITwinObject t:

                var methodInfo = plainSource.GetMethodForNonPublicTypes("CopyPlainToCyclic");

                for (var i = 0; i < ps.Length; i++)
                    if (methodInfo == null)
                        ((dynamic)ps.ElementAt(i)).CopyPlainToCyclic(enumerable.ElementAt(i));
                    else
                        methodInfo.Invoke(ps.ElementAt(i),
                            new object[] { enumerable.ElementAt(i) });
                break;
        }
    }

    private static MethodInfo GetMethodForNonPublicTypes(this Array array, string methodName)
    {
        var firstElement = array.Cast<object>().FirstOrDefault();

        if (firstElement != null && !firstElement.GetType().IsPublic)
            return firstElement.GetType().GetMethods().FirstOrDefault(p => p.Name == methodName);

        return null;
    }

    /// <summary>
    ///     Copies plainers of an array into shadows. [Internal use only]
    /// </summary>
    /// <param name="plainSource">Source array.</param>
    /// <param name="onlineTarget">Destination array.</param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CopyPlainToShadow<P, O>(Array plainSource, Array onlineTarget)
    {
        var sourceEnumerable = plainSource.Cast<P>();
        var targetEnumerable = onlineTarget.Cast<O>();

        var enumerable = targetEnumerable as O[] ?? targetEnumerable.ToArray();
        var ps = sourceEnumerable as P[] ?? sourceEnumerable.ToArray();
        switch (enumerable.ElementAtOrDefault(0))
        {
            case ITwinPrimitive t:
                for (var i = 0; i < ps.Length; i++)
                    ((dynamic)enumerable.ElementAt(i)).Shadow = ps.ElementAt(i);
                break;
            case ITwinObject t:

                var methodInfo = plainSource.GetMethodForNonPublicTypes("CopyPlainToShadow");

                for (var i = 0; i < ps.Length; i++)
                    if (methodInfo == null)
                        ((dynamic)ps.ElementAt(i)).CopyPlainToShadow(enumerable.ElementAt(i));
                    else
                        methodInfo.Invoke(ps.ElementAt(i),
                            new object[] { enumerable.ElementAt(i) });
                break;
        }
    }

    /// <summary>
    ///     Copies onliners of an array into plainers. [Internal use only]
    /// </summary>
    /// <param name="onlineSource">Source array.</param>
    /// <param name="plainTarget">Destination array.</param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CopyOnlineToPlain<O, P>(Array onlineSource, Array plainTarget)
    {
        var sourceEnumerable = onlineSource.Cast<O>();
        var targetEnumerable = plainTarget.Cast<P>();

        var enumerable = sourceEnumerable as O[] ?? sourceEnumerable.ToArray();
        switch (enumerable.ElementAtOrDefault(0))
        {
            case ITwinPrimitive t:

                foreach (var index in GetIndices(onlineSource))
                    plainTarget.SetValue(((dynamic)onlineSource.GetValue(index)).LastValue, index);

                break;
            case ITwinObject t:

                var methodInfo = plainTarget.GetMethodForNonPublicTypes("CopyCyclicToPlain");

                for (var i = 0; i < enumerable.Count(); i++)
                {
                    var targetEnumerable1 = targetEnumerable as P[] ?? targetEnumerable.ToArray();
                    var ps = targetEnumerable as P[] ?? targetEnumerable1.ToArray();
                    if (methodInfo == null)
                        ((dynamic)ps.ElementAt(i)).CopyCyclicToPlain(enumerable.ElementAt(i));
                    else
                        methodInfo.Invoke(ps.ElementAt(i),
                            new object[] { enumerable.ElementAt(i) });
                }

                break;
        }
    }

    /// <summary>
    ///     Copies shadows of an array into plainers. [Internal use only]
    /// </summary>
    /// <param name="onlineSource">Source array.</param>
    /// <param name="plainTarget">Destination array.</param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CopyShadowToPlain<O, P>(Array onlineSource, Array plainTarget)
    {
        var sourceEnumerable = onlineSource.Cast<O>();
        var targetEnumerable = plainTarget.Cast<P>().ToArray();

        var enumerable = sourceEnumerable as O[] ?? sourceEnumerable.ToArray();
        switch (enumerable.ElementAtOrDefault(0))
        {
            case ITwinPrimitive t:

                foreach (var indice in GetIndices(onlineSource))
                    plainTarget.SetValue(((dynamic)onlineSource.GetValue(indice)).Shadow, indice);
                break;
            case ITwinObject t:

                var methodInfo = plainTarget.GetMethodForNonPublicTypes("CopyShadowToPlain");

                for (var i = 0; i < enumerable.Count(); i++)
                    if (methodInfo == null)
                        ((dynamic)targetEnumerable.ElementAt(i)).CopyShadowToPlain(enumerable.ElementAt(i));
                    else
                        methodInfo.Invoke(targetEnumerable.ElementAt(i),
                            new object[] { enumerable.ElementAt(i) });
                break;
        }
    }

    /// <summary>
    ///     Creates an array of plainer type. [Internal use only]
    /// </summary>
    /// <typeparam name="P"></typeparam>
    /// <param name="array"></param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void CreatePlainerType<P>(Array array) where P : new()
    {
        foreach (var indice in GetIndices(array)) array.SetValue(new P(), indice);
    }


    /// <summary>
    ///     Instantites an array of plainer type. [Internal use only]
    /// </summary>
    /// <typeparam name="P"></typeparam>
    /// <param name="array"></param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void InstantiatePlainerType<P>(Array array) where P : new()
    {
        foreach (var indice in GetIndices(array)) array.SetValue(new P(), indice);
    }
}