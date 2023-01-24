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
///     Class providing access to the LINT type online variable.
/// </summary>
public class OnlinerLInt : OnlinerBase<long>, IOnlineLInt, IShadowLInt
{
    /// <summary>
    ///     Gets the min value of <see cref="OnlinerLInt" />.
    /// </summary>
    public static readonly long MinValue = long.MinValue;

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerLInt" />.
    /// </summary>
    public static readonly long MaxValue = long.MaxValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerLInt" /> class.
    /// </summary>
    public OnlinerLInt()
    {
        validator = new LIntValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerLInt" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerLInt(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new LIntValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override long InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override long InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}