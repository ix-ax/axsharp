// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Threading.Tasks;
using AXSharp.Connector.ValueTypes.Online;
using AXSharp.Connector.ValueTypes.Shadows;
using AXSharp.Connector.ValueValidation;

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Class providing access to the WSTRING type online variable.
/// </summary>
public class OnlinerWString : OnlinerBase<string>, IOnlineWString, IShadowWString
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerWString" /> class.
    /// </summary>
    public OnlinerWString()
    {
        validator = new WStringValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerWString" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerWString(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new WStringValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override string InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : string.Empty;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override string InstanceMinValue => AttributeMinSet ? AttributeMinimum : string.Empty;

    /// <summary>
    ///     Gets tranlated and interpolated string of <see cref="Cyclic" /> value. Sets <see cref="Cyclic" /> value.
    /// </summary>
    public override string Cyclic
    {
        get => TranslatorBase.Translate(base.Cyclic).Interpolate(this);
        set => base.Cyclic = value;
    }

    /// <summary>
    ///     Get the value of this <see cref="OnlinerWString" />.
    /// </summary>
    /// <remarks>
    ///     This method accesses the item in a single request over communication layer;
    ///     accessing multiple items may result in performance degradation
    ///     over the communication interface with the target system.
    ///     This method is to be used only when you need the item to be read from the PLC
    ///     and the return the control of the program.
    ///     Consider using <see cref="ITwinObject" />s <see cref="TwinObjectExtensions.ReadAsync" />
    ///     or <see cref="TwinObjectExtensions.WriteAsync" /> methods for bulk access to entire structures.
    /// </remarks>
    /// <returns>Value of this <see cref="OnlinerWString" /></returns>
    public override async Task<string> GetAsync()
    {
        return TranslatorBase.Translate(await base.GetAsync()).Interpolate(this);
    }

    /// <summary>
    ///     Sets the value of this <see cref="OnlinerWString" />.
    /// </summary>
    /// <remarks>
    ///     This method accesses the item in a single request over communication layer;
    ///     accessing multiple items may result in performance degradation
    ///     over the communication interface with the target system.
    ///     This method is to be used only when you need the item to be read from the PLC
    ///     and the return the control of the program.
    ///     Consider using <see cref="ITwinObject" />s <see cref="TwinObjectExtensions.ReadAsync" />
    ///     or <see cref="TwinObjectExtensions.WriteAsync" /> methods for bulk access to entire structures.
    /// </remarks>
    /// <param name="value">Value to be set.</param>
    public override async Task<string> SetAsync(string value)
    {
        return await Task.Run(() =>
        {
            Cyclic = value;
            return value;
        });
    }
}