﻿// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;

public class RenderIgnoreAttribute : Attribute
{
    private readonly IEnumerable<string> Ignorables;

   
    public RenderIgnoreAttribute()
    {
    }

    public RenderIgnoreAttribute(params string[] args)
    {
        Ignorables = args;
    }

    public bool HasIgnore(string presentationType)
    {
        if(Ignorables == null || Ignorables.Count() == 0)
        {
            return true;
        }

        foreach (var item in presentationType.Split('-'))
        {
            if(Ignorables.Contains(item.Trim()))
            {
                return true;
            }
        }

        return false;
    }
}

