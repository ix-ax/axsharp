// Ix.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Linq;
using Ix.Abstractions.Presentation;
using Ix.Presentation.Attributes;

public class GroupAttribute : PresentationGroupAttribute
{    
    /// <summary>Initializes a new instance of the <see cref="ContainerAttribute" /> class.</summary>
    public GroupAttribute(Layout layoutType) : base()
    {
        var containterInfo = PresentationProvider.Get.LayoutProvider.GetControl(layoutType);
        this.Assembly = containterInfo.assembly;
        this.FullTypeName = containterInfo.fullTypeName;
    }

    public GroupAttribute(Layout layoutType, object parentHeader) : base()
    {
        var containterInfo = PresentationProvider.Get.LayoutProvider.GetControl(layoutType);
        this.Assembly = containterInfo.assembly;
        this.FullTypeName = containterInfo.fullTypeName;
        this.ParentHeader = parentHeader;
    }

    public GroupAttribute(string assembly, string fullTypeName) : base(assembly, fullTypeName)
    {

    }

    public GroupAttribute(string assembly, string fullTypeName, object parentHeader) : base(assembly, fullTypeName, parentHeader)
    {

    }
}