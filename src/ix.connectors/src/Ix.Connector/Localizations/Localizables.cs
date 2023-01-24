// Ix.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace Ix.Localizations;

/// <summary>
///     Provides data object for localizables.
/// </summary>
public class Localizables
{
    /// <summary>
    ///     Gets or sets the location of this localizable item.
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    ///     Gets or sets the key of this localizable item.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    ///     Gets or sets whether the localizable item is used.
    /// </summary>
    public bool Used { get; set; }
}