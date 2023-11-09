﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace AXSharp.TIA2AX.Transformer
{
    public class Options
    {
        [Option('d', "db-files", Required = false, HelpText = "list of exported db blocks from TIA portal which should be used")]
        public IEnumerable<string> DataBlocks { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output folder for pseudo AX project")]
        public string? Output{ get; set; }
    }
}
