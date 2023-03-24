// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Globalization;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.ValueValidation;

/// <summary>
///     Provides validation rule for <see cref="OnlinerLReal" /> type.
/// </summary>
public class LRealValueValidationRule : OnlinerValidationRule<double>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LRealValueValidationRule" /> class.
    /// </summary>
    /// <param name="onliner">
    ///     Onliner type.
    /// </param>
    public LRealValueValidationRule(OnlinerLReal onliner) : base(onliner)
    {
    }

    /// <summary>
    ///     Validates value of <see cref="OnlinerLReal" />
    /// </summary>
    /// <param name="value">Value to validate.</param>
    /// <param name="culture">Culture.</param>
    /// <returns>Validation result.</returns>
    public override ValidationResult Validate(double value, CultureInfo culture)
    {
        if (value < Min || value > Max)
        {
            ValidationErrorTip = string.Format("Allowed range is: {0} - {1}.", Min, Max);
            return new ValidationResult(false, ValidationErrorTip);
        }

        return new ValidationResult(true, null);
    }
}