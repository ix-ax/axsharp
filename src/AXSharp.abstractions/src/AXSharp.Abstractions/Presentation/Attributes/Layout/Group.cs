﻿// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Linq;
using AXSharp.Abstractions.Presentation;
using AXSharp.Presentation.Attributes;

public class GroupAttribute : PresentationGroupAttribute
{    
    /// <summary>Initializes a new instance of the <see cref="ContainerAttribute" /> class.</summary>
    public GroupAttribute(GroupLayout layoutType) : base()
    {
        var containterInfo = PresentationProvider.Get.GroupLayoutProvider.GetControl(layoutType);
        this.Assembly = containterInfo.assembly;
        this.FullTypeName = containterInfo.fullTypeName;
    }

    public GroupAttribute(GroupLayout layoutType, object parentHeader) : base()
    {
        var containterInfo = PresentationProvider.Get.GroupLayoutProvider.GetControl(layoutType);
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