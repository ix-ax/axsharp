﻿// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Globalization;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.ValueValidation;

/// <summary>
///     Provides validation rule for <see cref="OnlinerTime" /> type.
/// </summary>
public class TimeValueValidationRule : OnlinerValidationRule<TimeSpan>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TimeValueValidationRule" /> class.
    /// </summary>
    /// <param name="onliner">
    ///     Onliner type.
    /// </param>
    public TimeValueValidationRule(OnlinerTime onliner) : base(onliner)
    {
    }

    /// <summary>
    ///     Validates value of <see cref="OnlinerTime" />
    /// </summary>
    /// <param name="value">Value to validate.</param>
    /// <param name="culture">Culture.</param>
    /// <returns>Validation result.</returns>
    public override ValidationResult Validate(TimeSpan value, CultureInfo culture)
    {
        if (value < Min || value > Max)
        {
            ValidationErrorTip = string.Format("Allowed range is: {0} - {1}.", Min, Max);
            return new ValidationResult(false, ValidationErrorTip);
        }

        return new ValidationResult(true, null);
    }
}