﻿// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace AXSharp.Presentation.Attributes
{
    using System;

    public class PresentationGroupAttribute : Attribute
    {
        protected PresentationGroupAttribute()
        {            
            
        }
      
        public PresentationGroupAttribute(string assembly, string fullTypeName) 
        {
            this.FullTypeName = fullTypeName;
            this.Assembly = assembly;
        }

        public PresentationGroupAttribute(string assembly, string fullTypeName, object parentHeader)
        {
            this.FullTypeName = fullTypeName;
            this.Assembly = assembly;
            this.ParentHeader = parentHeader;
        }

        public string Assembly { get; protected set; }

        public string FullTypeName { get; protected set; }

        public object ParentHeader { get; protected set; }        
    }
}
