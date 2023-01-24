// Ix.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace Ix.Connector.ValueTypes;

/// <summary>
///     Provides delegate for notification of tags value change.
/// </summary>
/// <param name="sender">Value tag where the change occurred.</param>
/// <param name="args">Value change arguments.</param>
public delegate void ValueChangedEventHandlerDelegate(ITwinPrimitive sender, ValueChangedEventArgs args);