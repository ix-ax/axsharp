// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

#undef TRACE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.BuilderHelpers;

/// <summary>
///     Helper methods for creating and manipulating arrays in Twin objects.
/// </summary>
public static class Arrays
{
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
    /// <param name="initializer">Intializes</param>
    // [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void InstantiateArray(Array array,
        ITwinObject parent,
        string readableTail,
        string symbolTail,
        Func<ITwinObject, string, string, object> initializer)
    {
        foreach (var element in GetIndices(array))
        {
            var indiceAsString = GetIndicesAsString(element);
            var a = initializer(parent,
                $"{readableTail}{indiceAsString}",
                $"{symbolTail}{indiceAsString}");
            array.SetValue(a, element);
        }
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
}