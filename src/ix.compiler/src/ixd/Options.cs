using Ix.Compiler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Ix.ixc_doc
{
    internal class Options : ICompilerOptions
    {
        [Option('x', "source-project-folder", Required = true, HelpText = "Simatic-ax project folder")]
        public string? AxSourceProjectFolder { get; set; }

        [Option('o', "output-project-folder", Required = true,
            HelpText = "Output project folder where compiler emits result.")]
        public string? OutputProjectFolder { get; set; }

    }
}
