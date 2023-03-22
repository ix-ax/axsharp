﻿// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace AXSharp.Connector.ValueTypes.Shadows;

/// <summary>
///     Defines contract to access shadow value of <see cref="double" />; LREAL type of the PLC.
/// </summary>
public interface IShadowLReal : IShadow<double>
{
}