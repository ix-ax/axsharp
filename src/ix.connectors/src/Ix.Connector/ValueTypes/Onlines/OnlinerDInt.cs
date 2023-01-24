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
///     Class providing access to the DINT type online variable.
/// </summary>
public class OnlinerDInt : OnlinerBase<int>, IOnlineDInt, IShadowDInt
{
    /// <summary>
    ///     Gets the min value of <see cref="OnlinerDInt" />.
    /// </summary>
    public static readonly int MinValue = int.MinValue;

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerDInt" />.
    /// </summary>
    public static readonly int MaxValue = int.MaxValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerDInt" /> class.
    /// </summary>
    public OnlinerDInt()
    {
        validator = new DintValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerDInt" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerDInt(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new DintValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override int InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override int InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}