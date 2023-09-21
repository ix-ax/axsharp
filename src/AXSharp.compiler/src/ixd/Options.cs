﻿using AXSharp.Compiler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace AXSharp.ixc_doc
{
    internal class Options : ICompilerOptions
    {
        [Option('x', "source-project-folder", Required = true, HelpText = "Simatic-ax project folder")]
        public IEnumerable<string> AxSourceProjectFolder { get; set; }

        [Option('o', "output-project-folder", Required = true,
            HelpText = "Output project folder where compiler emits result.")]
        public string? OutputProjectFolder { get; set; }

        [Option('b', "use-base-symbol", Required = false, Default = false,
            HelpText = "Will use base symbol in inherited types")]
        public bool UseBase { get; set; }
    }
}
