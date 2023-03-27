﻿// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector.ValueValidation;

/// <summary>
///     Contract for validation of a numerical value.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IValueBoundaries<out T>
{
    /// <summary>
    ///     Gets min. acceptable value.
    /// </summary>
    T InstanceMinValue { get; }

    /// <summary>
    ///     Gets max. acceptable valie.
    /// </summary>
    T InstanceMaxValue { get; }
}