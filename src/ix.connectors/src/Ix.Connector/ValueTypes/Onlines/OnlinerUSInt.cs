// Ix.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Connector.ValueTypes.Online;
using Ix.Connector.ValueTypes.Shadows;
using Ix.Connector.ValueValidation;

namespace Ix.Connector.ValueTypes;

/// <summary>
///     Class providing access to the USINT type online variable.
/// </summary>
public class OnlinerUSInt : OnlinerBase<byte>, IOnlineUSInt, IShadowUSInt
{
    /// <summary>
    ///     Gets the min value of <see cref="OnlinerUSInt" />.
    /// </summary>
    public static readonly byte MinValue = byte.MinValue;

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerUSInt" />.
    /// </summary>
    public static readonly byte MaxValue = byte.MaxValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerUSInt" /> class.
    /// </summary>
    public OnlinerUSInt()
    {
        validator = new USintValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerUSInt" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerUSInt(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new USintValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override byte InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override byte InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}