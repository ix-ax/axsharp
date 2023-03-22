﻿// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using AXSharp.Connector.ValueTypes.Online;
using AXSharp.Connector.ValueTypes.Shadows;
using AXSharp.Connector.ValueValidation;

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Class providing access to the DATE type online variable.
/// </summary>
public class OnlinerDate : OnlinerBase<DateOnly>, IOnlineDate, IShadowDate
{
    private static readonly long MinNs = new DateTime(1970, 1, 1).Ticks;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerDate" /> class.
    /// </summary>
    public OnlinerDate()
    {
        validator = new DateValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerDate" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerDate(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new DateValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerDate" />.
    /// </summary>
    public static DateOnly MaxValue { get; } = new(2262, 4, 11);

    /// <summary>
    ///     Gets the min value of <see cref="OnlinerDate" />.
    /// </summary>
    public static DateOnly MinValue { get; } = new(1970, 01, 1);

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override DateOnly InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override DateOnly InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}