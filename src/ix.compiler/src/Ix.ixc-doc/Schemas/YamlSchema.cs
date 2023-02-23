﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YamlDotNet.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using YamlDotNet.Serialization.NamingConventions;

namespace Ix.ixc_doc.Schemas
{
    // <auto-generated />
    //
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using CodeBeautify;
    //
    //    var welcome8 = Welcome8.FromJson(jsonString);



    public partial class YamlSchema
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("references")]
        public Reference[] References { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("commentId")]
        public string CommentId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Children { get; set; }

        [JsonProperty("langs")]
        public string[] Langs { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameWithType")]
        public string NameWithType { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("assemblies")]
        public string[] Assemblies { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("remakrs")]
        public string Remarks { get; set; }

        [JsonProperty("example")]
        public object[] Example { get; set; }

        [JsonProperty("syntax")]
        public Syntax Syntax { get; set; }

        [JsonProperty("inheritance", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Inheritance { get; set; }

        [JsonProperty("inheritedMembers", NullValueHandling = NullValueHandling.Ignore)]
        public string[] InheritedMembers { get; set; }

        [JsonProperty("overload", NullValueHandling = NullValueHandling.Ignore)]
        public string Overload { get; set; }
    }

    public partial class Source
    {
        [JsonProperty("remote")]
        public Remote Remote { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("startLine")]
        public long StartLine { get; set; }
    }

    public partial class Remote
    {
        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public partial class Syntax
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("content.vb")]
        public string ContentVb { get; set; }

        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public Parameter[] Parameters { get; set; }

        [JsonProperty("return", NullValueHandling = NullValueHandling.Ignore)]
        public Return Return { get; set; }
    }

    public partial class Parameter
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class Return
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public partial class Reference
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("commentId")]
        public string CommentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameWithType")]
        public string NameWithType { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("parent", NullValueHandling = NullValueHandling.Ignore)]
        public string Parent { get; set; }

        [JsonProperty("isExternal", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExternal { get; set; }
    }

    public partial class Spec
    {
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isExternal", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExternal { get; set; }
    }




}
